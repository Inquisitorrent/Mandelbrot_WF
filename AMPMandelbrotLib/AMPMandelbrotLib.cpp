#include "stdafx.h"
#include <amp.h>

using namespace concurrency;

extern "C" __declspec ( dllexport ) void _stdcall iterate_double_gpu(int* iterations, double ratioXDouble, double ratioYDouble, int currentMaxIterations, int width, int height, double currentMinPlotXDouble, double currentMaxPlotXDouble, double currentMinPlotYDouble, double currentMaxPlotYDouble, double currentBlockSizeXDouble, double currentBlockSizeYDouble)
{
	// Create a view over the data on the CPU
    array_view<int,2> iterationsAV(width, height, &iterations[0]);

    // Run code on the GPU
    parallel_for_each(iterationsAV.extent, [=] (index<2> idx) restrict(amp)
    {
		double dx = (idx[0] * currentBlockSizeXDouble) + currentMinPlotXDouble;
		double dy = currentMaxPlotYDouble - (idx[1] * currentBlockSizeYDouble);

		double Zx = 0.0;
		double Zy = 0.0;
		double ZSquaredx = 0.0;
		double ZSquaredy = 0.0;
		double Magnitudex = 0.0;
		double Magnitudey = 0.0;
		double Magnitude = 0.0;

		int iteration = 0;
		while ((iteration < currentMaxIterations) && (Magnitude < 4.0))
		{
			ZSquaredx = (Zx * Zx) - (Zy * Zy);
			ZSquaredy = 2.0 * Zx * Zy;
			Magnitudex = ZSquaredx + dx;
			Magnitudey = ZSquaredy + dy;
			Magnitude = (Magnitudex * Magnitudex) + (Magnitudey * Magnitudey);
			Zx = Magnitudex;
			Zy = Magnitudey;
			++iteration;
		}

		iterationsAV[idx] = iteration;
    });

	// Copy data from GPU to CPU
    iterationsAV.synchronize();	
}