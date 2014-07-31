using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Mandelbrot_WF
{
    public partial class Form1 : Form
    {
        //This Code 

        #region Data Members
        private bool saveImages = false;
        private int shardFactor = 16;
        private int numFrames = 3000;//3500;
        private const int endMaxIterations = 5355;//5903;
        private const int startMaxIterations = 5355;
        private static int[] currentMaxIterations = null;
        private int imageWidth = 720;//15720;//1920;//3840;//19200;//19200;
        private int imageHeight = 720;//4320;//951;//2160;//19200;//10800;
        private System.ComponentModel.BackgroundWorker bw;
        private string saveLocation = @"C:\dev\Mandelbrot_Video";
        private double aspectRatio;
        private double frameZoomPercent = 0.01D;

        #region Colors
        private const int numColorCombinations = 255 * 255 * 255;
        private const int rangePerColor = numColorCombinations / 8;
        private const int modit = 1437;
        #endregion //Colors

        #region doubles
        private static double XCenterDouble;// = XCenterACNE.ToDouble();//-0.738776459458874D;//-1.7685736563152709932817429153295447129341200534055498823375111352827765533646353820119779335363321986478087958745766432300344486098206084588445291690832853792608335811319613234806674959498380432536269122404488847453646628324959064543D;//-0.738773765D;
        private static double YCenterDouble;// = YCenterACNE.ToDouble();//0.166283372946165D;//-0.0009642968513582800001762427203738194482747761226565635652857831533070475543666558930286153827950716700828887932578932976924523447497708248894734256480183898683164582055541842171815899305250842692638349057118793296768325124255746563D;//0.16628672D;
        private static double endXRadiusDouble;// = endXRadiusACNE.ToDouble();//0.0000000000000051D;//0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001920D;//0.00001471670625D;
        private static double endYRadiusDouble;// = endYRadiusACNE.ToDouble();//0.000000000000005525095D;//0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001080D;//0.000008278147265625D;

        private static double endMinPlotXDouble;// = endMinPlotXACNE.ToDouble();//XCenterDouble - endXRadiusDouble;
        private static double endMaxPlotXDouble;// = endMaxPlotXACNE.ToDouble();//XCenterDouble + endXRadiusDouble;
        private static double endMinPlotYDouble;// = endMinPlotYACNE.ToDouble();//YCenterDouble - endYRadiusDouble;
        private static double endMaxPlotYDouble;// = endMaxPlotYACNE.ToDouble();//YCenterDouble + endYRadiusDouble;
        private static double endBlockSizeXDouble;// = endBlockSizeXACNE.ToDouble();//Math.Abs((endMaxPlotXDouble - endMinPlotXDouble) / imageWidth);
        private static double endBlockSizeYDouble;// = endBlockSizeYACNE.ToDouble();//Math.Abs((endMaxPlotYDouble - endMinPlotYDouble) / imageHeight);

        private static double startMinPlotXDouble;// = startMinPlotXACNE.ToDouble();//-32D / 9D;
        private static double startMaxPlotXDouble;// = startMaxPlotXACNE.ToDouble();//32D / 9D;
        private static double startMinPlotYDouble;// = startMinPlotYACNE.ToDouble();//-2D;
        private static double startMaxPlotYDouble;// = startMaxPlotYACNE.ToDouble();//2D;
        private static double startBlockSizeXDouble;// = startBlockSizeXACNE.ToDouble();//Math.Abs((startMaxPlotXDouble - startMinPlotXDouble) / imageWidth);
        private static double startBlockSizeYDouble;// = startBlockSizeYACNE.ToDouble();//Math.Abs((startMaxPlotYDouble - startMinPlotYDouble) / imageHeight);
        private static double startXRadiusDouble;// = startXRadiusACNE.ToDouble();//Math.Abs(startMinPlotXDouble - XCenterDouble);
        private static double startYRadiusDouble;// = startYRadiusACNE.ToDouble();//Math.Abs(startMinPlotYDouble - YCenterDouble);

        private static double[] currentMinPlotXDouble;// = currentMinPlotXACNE.ToDouble();//startMinPlotXDouble;
        private static double[] currentMaxPlotXDouble;// = currentMaxPlotXACNE.ToDouble();//startMaxPlotXDouble;
        private static double[] currentMinPlotYDouble;// = currentMinPlotYACNE.ToDouble();//startMinPlotYDouble;
        private static double[] currentMaxPlotYDouble;// = currentMaxPlotYACNE.ToDouble();//startMaxPlotYDouble;
        private static double[] currentBlockSizeXDouble;// = currentBlockSizeXACNE.ToDouble();//startBlockSizeXDouble;
        private static double[] currentBlockSizeYDouble;// = currentBlockSizeYACNE.ToDouble();//startBlockSizeYDouble;
        private static double[] currentXRadiusDouble;
        private static double[] currentYRadiusDouble;
        #endregion //doubles

        int[,] iterations;
        int[] iterationsAMP;
        double ratioXDouble = 0.99D;
        double ratioYDouble = 0.99D;
        int AMPCooldownCount = 0;
        bool AMPWasUsed = true;

        private byte[] _imageBytes;
        private Bitmap _bitmap;
        #endregion //Data Members        

        #region AMP
        /// <summary>
        /// Function defined in HelloWorldLib.dll to find iterations for pixels in a Madelbrot frame using C++ AMP
        /// </summary>
        [DllImport("HelloWorldLib", CallingConvention = CallingConvention.StdCall)]
        extern unsafe static void iterate_double_gpu(int* iterations, double ratioXDouble, double ratioYDouble, int currentMaxIterations, int width, int height, double currentMinPlotXDouble, double currentMaxPlotXDouble, double currentMinPlotYDouble, double currentMaxPlotYDouble, double currentBlockSizeXDouble, double currentBlockSizeYDouble);

        /// <summary>
        /// Function defined in HelloWorldLib.dll to find iterations for pixels in a Madelbrot frame using C++ AMP
        /// </summary>
        [DllImport("HelloWorldLib", CallingConvention = CallingConvention.StdCall)]
        extern unsafe static void iterate_partial_double_gpu(int* iterations, int currentMaxIterations, int width, int height, int offsetHeight, double currentMinPlotXDouble, double currentMaxPlotXDouble, double currentMinPlotYDouble, double currentMaxPlotYDouble, double currentBlockSizeXDouble, double currentBlockSizeYDouble);
        #endregion //AMP

        public Form1()
        {
            InitializeComponent();
            frameZoomPercent = 0.01D;
            textBox_ZoomPercent.Text = frameZoomPercent.ToString();
            XCenterDouble = -0.738776459458874D;
            YCenterDouble = 0.166283372946165D;
            textBox_XCenter.Text = XCenterDouble.ToString();
            textBox_YCenter.Text = YCenterDouble.ToString();
            aspectRatio = System.Convert.ToDouble(imageWidth) / System.Convert.ToDouble(imageHeight);
            textBox_SaveLocation.Text = saveLocation;
            textBox_Res_x.Text = imageWidth.ToString();
            textBox_Res_y.Text = imageHeight.ToString();
            checkBox_SaveImages.Checked = false;
            checkBox_SaveImages.Text = "Save Images";
            saveImages = false;
            button1.Text = "Start";
            toolStripProgressBar1.ProgressBar.Value = 0;
            toolStripProgressBar1.ProgressBar.Visible = false;
            _imageBytes = new byte[imageHeight * imageWidth];
            _bitmap = new Bitmap(imageWidth, imageHeight, PixelFormat.Format24bppRgb);
            SetInitImage();
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(RenderMandelbrotZoom);
            bw.ProgressChanged += new ProgressChangedEventHandler(UpdateImage);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FinishedCancellation);

            pictureBox1.ClientSizeChanged += new EventHandler(ImageResized);
        }

        #region Click Events
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            saveImages = checkBox_SaveImages.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
            {
                toolStripProgressBar1.ProgressBar.Visible = true;
                toolStripProgressBar1.Maximum = numFrames;
                toolStripProgressBar1.ProgressBar.Value = 0;
                textBox_XCenter.Enabled = false;
                textBox_YCenter.Enabled = false;
                textBox_ZoomPercent.Enabled = false;
                textBox_SaveLocation.Enabled = false;
                textBox_Res_x.Enabled = false;
                textBox_Res_y.Enabled = false;
                bw.RunWorkerAsync();
                button1.Text = "Working";
            }
            else
            {
                if (!bw.CancellationPending) bw.CancelAsync();
                button1.Text = "Cancelling...";
            }
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }
        #endregion //Click Events

        #region Events
        private void textBox_XCenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XCenterDouble = System.Convert.ToDouble(textBox_XCenter.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox_YCenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                YCenterDouble = System.Convert.ToDouble(textBox_YCenter.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox_ZoomPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Double.TryParse(textBox_ZoomPercent.Text, out frameZoomPercent))
                {
                    if (!((frameZoomPercent >= 1D) || (frameZoomPercent <= 0D)))
                    {
                        ratioXDouble = 1D - frameZoomPercent;
                        ratioYDouble = 1D - frameZoomPercent;
                        numFrames = System.Convert.ToInt32(Math.Log(8.046069742102711340495974910424e-14D, (1D - frameZoomPercent)));
                    }
                    else throw new Exception("Number not in range.");
                }                
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox_SaveLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                saveLocation = textBox_SaveLocation.Text;
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox_Res_x_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Int32.TryParse(textBox_Res_x.Text, out imageWidth))
                {
                    if (imageWidth > 0)
                    {
                        imageWidth = System.Convert.ToInt32(textBox_Res_x.Text);
                        aspectRatio = System.Convert.ToDouble(imageWidth) / System.Convert.ToDouble(imageHeight);
                        _bitmap = new Bitmap(imageWidth, imageHeight, PixelFormat.Format24bppRgb);
                        pictureBox1.Invalidate();
                        pictureBox1.Image = _bitmap;
                        pictureBox1.Refresh();
                    }                    
                }                
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox_Res_y_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Int32.TryParse(textBox_Res_y.Text, out imageHeight))
                {
                    if (imageHeight > 0)
                    {
                        imageHeight = System.Convert.ToInt32(textBox_Res_y.Text);
                        aspectRatio = System.Convert.ToDouble(imageWidth) / System.Convert.ToDouble(imageHeight);
                        _bitmap = new Bitmap(imageWidth, imageHeight, PixelFormat.Format24bppRgb);
                        pictureBox1.Invalidate();
                        pictureBox1.Image = _bitmap;
                        pictureBox1.Refresh();
                    }                    
                }                
            }
            catch (Exception ex)
            {

            }
        }

        private void FinishedCancellation(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Text = "Start";
            toolStripProgressBar1.ProgressBar.Value = 0;
            toolStripProgressBar1.ProgressBar.Visible = false;
            toolStripProgressBar1.ProgressBar.Refresh();
            textBox_XCenter.Enabled = true;
            textBox_YCenter.Enabled = true;
            textBox_ZoomPercent.Enabled = true;
            textBox_SaveLocation.Enabled = true;
            textBox_Res_x.Enabled = true;
            textBox_Res_y.Enabled = true;
        }

        private void UpdateImage(object sender, ProgressChangedEventArgs e)
        {
            pictureBox1.Refresh();
            toolStripProgressBar1.ProgressBar.Value = e.ProgressPercentage;
            toolStripProgressBar1.ProgressBar.Refresh();
        }

        private void ImageResized(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            toolStripProgressBar1.Width = pictureBox1.Width - 20;
            int i = 0;
        }
        #endregion //Events

        #region Work
        private unsafe void RenderMandelbrotZoom(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Lowest;
            currentMaxIterations = new int[numFrames];            
            InitVars();
            if (saveImages && (!CheckFolder())) return;

            for (int i = 0; i < numFrames; i++)
            {
                try
                {
                    if (bw.CancellationPending) { return; }
                    #region Calculate Iterations
                    //Console.WriteLine("Calculating pixel colors for frame " + i.ToString() + ".");   
                    try
                    {
                        if ((!AMPWasUsed) && (AMPCooldownCount < 25)) throw new Exception();
                        AMPCooldownCount = 0;
                        for (int p = 0; p < shardFactor; p++)
                        {
                            if (bw.CancellationPending) { return; }
                            int partialHeight = imageHeight / shardFactor;
                            int offsetHeight = p * partialHeight;
                            if (p == shardFactor - 1) { partialHeight += (imageHeight % shardFactor); }
                            int[] partialIterations = new int[partialHeight * imageWidth];
                            fixed (int* iterationsPtr = &(partialIterations[0]))
                            {
                                iterate_partial_double_gpu(iterationsPtr, currentMaxIterations[i], imageWidth, partialHeight, offsetHeight, currentMinPlotXDouble[i], currentMaxPlotXDouble[i], currentMinPlotYDouble[i], currentMaxPlotYDouble[i], currentBlockSizeXDouble[i], currentBlockSizeYDouble[i]);
                                partialIterations.CopyTo(iterationsAMP, (offsetHeight * imageWidth));
                            }
                        }
                        //fixed (int* iterationsPtr = &(iterationsAMP[0]))
                        //{
                        //    iterate_double_gpu(iterationsPtr, ratioXDouble, ratioYDouble, currentMaxIterations[i], imageWidth, imageHeight, currentMinPlotXDouble[i], currentMaxPlotXDouble[i], currentMinPlotYDouble[i], currentMaxPlotYDouble[i], currentBlockSizeXDouble[i], currentBlockSizeYDouble[i]);
                        //}
                        AMPWasUsed = true;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("AMP took too long #" + AMPCooldownCount);
                        AMPWasUsed = false;
                        AMPCooldownCount++;
                        Parallel.For(0, imageWidth, (w, loopstate) =>
                        {
                            if (bw.CancellationPending) { loopstate.Stop(); }
                            double dx = 0D, dy = 0D;
                            for (int h = 0; h < imageHeight; h++)
                            {
                                dx = (w * currentBlockSizeXDouble[i]) + currentMinPlotXDouble[i];
                                dy = currentMaxPlotYDouble[i] - (h * currentBlockSizeYDouble[i]);
                                iterations[w, h] = GetIterationsDouble(dx, dy, currentMaxIterations[i]);
                            }
                        });
                    }
                    #endregion //Calculate Iterations

                    #region Color and save
                    if (bw.CancellationPending) { return; }
                    unsafe
                    {
                        BitmapData bitmapData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, _bitmap.PixelFormat);
                        try
                        {
                            int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(_bitmap.PixelFormat) / 8;
                            int HeightInPixels = bitmapData.Height;
                            int WidthInBytes = bitmapData.Width * BytesPerPixel;
                            byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                            Parallel.For(0, HeightInPixels, h =>
                            {
                                byte* CurrentLine = PtrFirstPixel + (h * bitmapData.Stride);
                                int x = 0;
                                for (int w = 0; w < WidthInBytes; w = w + BytesPerPixel)
                                {
                                    byte red = 0, green = 0, blue = 0;
                                    if (AMPWasUsed) { GetColorFromIterations(iterationsAMP[(h * imageWidth) + x], currentMaxIterations[i], out red, out green, out blue); }
                                    else { GetColorFromIterations(iterations[x, h], currentMaxIterations[i], out red, out green, out blue); }
                                    CurrentLine[w] = blue;
                                    CurrentLine[w + 1] = green;
                                    CurrentLine[w + 2] = red;
                                    ++x;
                                }
                            });
                            _bitmap.UnlockBits(bitmapData);
                        }
                        catch (Exception ex)
                        {
                            _bitmap.UnlockBits(bitmapData);
                        }
                    }

                    try
                    {
                        //TODO fix scenario where textbox ends in slash
                        if (saveImages) _bitmap.Save(saveLocation + @"\AMP_Mandelbrot_" + i.ToString("D8") + ".bmp");
                        ((BackgroundWorker)sender).ReportProgress(i);
                    }
                    catch (Exception ex)
                    {
                        int g = 0;
                        g += 1;
                    }
                    #endregion //Color and save
                }
                catch (Exception exc)
                {

                }                
            }
        }

        private unsafe void SetInitImage()
        {
            unsafe
            {
                BitmapData bitmapData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.WriteOnly, _bitmap.PixelFormat);
                try
                {
                    int BytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(_bitmap.PixelFormat) / 8;
                    int HeightInPixels = bitmapData.Height;
                    int WidthInBytes = bitmapData.Width * BytesPerPixel;
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                    Parallel.For(0, HeightInPixels, h =>
                    {
                        byte* CurrentLine = PtrFirstPixel + (h * bitmapData.Stride);
                        int x = 0;
                        for (int w = 0; w < WidthInBytes; w = w + BytesPerPixel)
                        {
                            byte red = 0, green = 0, blue = 0;
                            CurrentLine[w] = blue;
                            CurrentLine[w + 1] = green;
                            CurrentLine[w + 2] = red;
                            ++x;
                        }
                    });
                    _bitmap.UnlockBits(bitmapData);
                }
                catch (Exception ex)
                {
                    _bitmap.UnlockBits(bitmapData);
                }
            }
            pictureBox1.Image = _bitmap;
        }

        private bool CheckFolder()
        {
            try
            {
                if (!Directory.Exists(saveLocation))
                {
                    Directory.CreateDirectory(saveLocation);
                    return Directory.Exists(saveLocation);
                }
                else return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }
        #endregion //Work

        #region Helpers
        private void InitVars()
        {
            startMinPlotYDouble = -2D;
            startMaxPlotYDouble = 2D;
            startMinPlotXDouble = aspectRatio * startMinPlotYDouble;//-7.1111111111111111111111111111111D;
            startMaxPlotXDouble = aspectRatio * startMaxPlotYDouble;//7.1111111111111111111111111111111D;            
            startBlockSizeXDouble = Math.Abs((startMaxPlotXDouble - startMinPlotXDouble) / imageWidth);
            startBlockSizeYDouble = Math.Abs((startMaxPlotYDouble - startMinPlotYDouble) / imageHeight);
            startYRadiusDouble = 2D;//Math.Abs(startMinPlotYDouble - YCenterDouble);
            startXRadiusDouble = aspectRatio * startYRadiusDouble;//7.1111111111111111111111111111111D;// Math.Abs(startMinPlotXDouble - XCenterDouble);
            
            currentMaxIterations[0] = startMaxIterations;
            XCenterDouble = System.Convert.ToDouble(textBox_XCenter.Text);//-0.738776459458874D;//-0.738776459458874D;//-1.7685736563152709932817429153295447129341200534055498823375111352827765533646353820119779335363321986478087958745766432300344486098206084588445291690832853792608335811319613234806674959498380432536269122404488847453646628324959064543D;//-0.738773765D;
            YCenterDouble = System.Convert.ToDouble(textBox_YCenter.Text); //0.166283372946165D;//0.166283372946165D;//-0.0009642968513582800001762427203738194482747761226565635652857831533070475543666558930286153827950716700828887932578932976924523447497708248894734256480183898683164582055541842171815899305250842692638349057118793296768325124255746563D;//0.16628672D;
            endXRadiusDouble = Math.Pow(1D - frameZoomPercent, numFrames) * startXRadiusDouble;//0.00000000000000002158240234375D;//0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001920D;//0.00001471670625D;
            endYRadiusDouble = Math.Pow(1D - frameZoomPercent, numFrames) * startYRadiusDouble;//6.0700506591796875e-18D;// 0.000000000000000019921875D;//0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001080D;//0.000008278147265625D;

            endMinPlotXDouble = XCenterDouble - endXRadiusDouble;
            endMaxPlotXDouble = XCenterDouble + endXRadiusDouble;
            endMinPlotYDouble = YCenterDouble - endYRadiusDouble;
            endMaxPlotYDouble = YCenterDouble + endYRadiusDouble;
            endBlockSizeXDouble = Math.Abs((endMaxPlotXDouble - endMinPlotXDouble) / imageWidth);
            endBlockSizeYDouble = Math.Abs((endMaxPlotYDouble - endMinPlotYDouble) / imageHeight);

            currentMinPlotXDouble = new double[numFrames];
            currentMaxPlotXDouble = new double[numFrames];
            currentMinPlotYDouble = new double[numFrames];
            currentMaxPlotYDouble = new double[numFrames];
            currentBlockSizeXDouble = new double[numFrames];
            currentBlockSizeYDouble = new double[numFrames];
            currentXRadiusDouble = new double[numFrames];
            currentYRadiusDouble = new double[numFrames];
            currentXRadiusDouble[0] = startXRadiusDouble;
            currentYRadiusDouble[0] = startYRadiusDouble;
            currentMinPlotXDouble[0] = startMinPlotXDouble;
            currentMaxPlotXDouble[0] = startMaxPlotXDouble;
            currentMinPlotYDouble[0] = startMinPlotYDouble;
            currentMaxPlotYDouble[0] = startMaxPlotYDouble;
            currentBlockSizeXDouble[0] = startBlockSizeXDouble;
            currentBlockSizeYDouble[0] = startBlockSizeYDouble;

            iterations = new int[imageWidth, imageHeight];
            iterationsAMP = new int[imageWidth * imageHeight];

            #region initialize frame arrays
            for (int i = 1; i < numFrames; i++)
            {
                currentXRadiusDouble[i] = currentXRadiusDouble[i - 1] * ratioXDouble;
                currentYRadiusDouble[i] = currentYRadiusDouble[i - 1] * ratioYDouble;

                currentMinPlotXDouble[i] = XCenterDouble - currentXRadiusDouble[i];//XCenterDouble - (startXRadiusDouble * Math.Pow(ratioXDouble, i));
                currentMaxPlotXDouble[i] = XCenterDouble + currentXRadiusDouble[i];//XCenterDouble + (startXRadiusDouble * Math.Pow(ratioXDouble, i));
                currentMinPlotYDouble[i] = YCenterDouble - currentYRadiusDouble[i];//YCenterDouble - (startYRadiusDouble * Math.Pow(ratioYDouble, i));
                currentMaxPlotYDouble[i] = YCenterDouble + currentYRadiusDouble[i];//YCenterDouble + (startYRadiusDouble * Math.Pow(ratioYDouble, i));

                currentBlockSizeXDouble[i] = Math.Abs((currentMaxPlotXDouble[i] - currentMinPlotXDouble[i]) / imageWidth);
                currentBlockSizeYDouble[i] = Math.Abs((currentMaxPlotYDouble[i] - currentMinPlotYDouble[i]) / imageHeight);
                if ((endMaxIterations - startMaxIterations) > numFrames)
                {
                    currentMaxIterations[i] += ((endMaxIterations - startMaxIterations) / numFrames) + ((((endMaxIterations - startMaxIterations) % numFrames) * (i - 1)) / numFrames);
                }
                else if ((endMaxIterations - startMaxIterations) == numFrames)
                {
                    currentMaxIterations[i] = currentMaxIterations[i - 1] + 1;
                }
                else //if ((endMaxIterations - startMaxIterations) < numFrames)
                {
                    currentMaxIterations[i] = startMaxIterations + (((endMaxIterations - startMaxIterations) * (i - 1)) / numFrames);// double check the math for this branch
                }
            }
            #endregion //initialize frame arrays
        }

        static int GetIterationsDouble(double x, double y, int maxIterations)
        {
            double Zx = 0d;
            double Zy = 0d;
            double ZSquaredx = 0d;
            double ZSquaredy = 0d;
            double Magnitudex = 0d;
            double Magnitudey = 0d;
            double Magnitude = 0d;

            int iteration = 0;
            while ((iteration < maxIterations) && (Magnitude < 4d))
            {
                ZSquaredx = (Zx * Zx) - (Zy * Zy);
                ZSquaredy = 2d * Zx * Zy;
                Magnitudex = ZSquaredx + x;
                Magnitudey = ZSquaredy + y;
                Magnitude = (Magnitudex * Magnitudex) + (Magnitudey * Magnitudey);
                Zx = Magnitudex;
                Zy = Magnitudey;
                iteration++;
            }

            return iteration;
        }

        static void GetColorFromIterations(int iterations, int maxIterations, out byte red, out byte green, out byte blue)
        {
            if (iterations >= maxIterations)
            {
                red = 0; green = 0; blue = 0;
                return;
            }
            else
            {
                double IRatio = System.Convert.ToDouble(iterations % modit) / ((double)modit);

                if ((IRatio >= 0D) && (IRatio < 0.125))
                {
                    red = (byte)((IRatio / 0.125) * (512D) + 0.5);
                    green = 0;
                    blue = 0;
                }

                else if ((IRatio >= 0.125) && (IRatio < 0.250))
                {
                    red = 255;
                    green = (byte)(((IRatio - 0.125) / 0.125) * (512D) + 0.5);
                    blue = 0;
                }

                else if ((IRatio >= 0.250) && (IRatio < 0.375))
                {
                    red = (byte)((1D - ((IRatio - 0.250) / 0.125)) * (512D) + 0.5);
                    green = 255;
                    blue = 0;
                }

                else if ((IRatio >= 0.375) && (IRatio < 0.500))
                {
                    red = 0;
                    green = 255;
                    blue = (byte)(((IRatio - 0.375) / 0.125) * (512D) + 0.5);
                }

                else if ((IRatio >= 0.500) && (IRatio < 0.625))
                {
                    red = 0;
                    green = (byte)((1D - ((IRatio - 0.500) / 0.125)) * (512D) + 0.5);
                    blue = 255;
                }

                else if ((IRatio >= 0.625) && (IRatio < 0.750))
                {
                    red = (byte)(((IRatio - 0.625) / 0.125) * (512D) + 0.5);
                    green = 0;
                    blue = 255;
                }

                else if ((IRatio >= 0.750) && (IRatio < 0.875))
                {
                    red = 255;
                    green = (byte)(((IRatio - 0.750) / 0.125) * (512D) + 0.5);
                    blue = 255;
                }

                else// if ((IRatio >= 0.875) && (IRatio <= 1.000))
                {
                    red = (byte)((1D - ((IRatio - 0.875) / 0.125)) * (512D) + 0.5);
                    green = (byte)((1D - ((IRatio - 0.875) / 0.125)) * (512D) + 0.5);
                    blue = (byte)((1D - ((IRatio - 0.875) / 0.125)) * (512D) + 0.5);
                }
            }
        }
        #endregion //Helpers
    }
}
