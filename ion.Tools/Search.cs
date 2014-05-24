//using ion.Structs;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Threading;

//namespace ion.Tools
//{
//    //ATENTIE PIXEL FORMAT
//    // B   G   R   A ; B   G    R    A
//    //[0, 10, 20, 255; 10, 25, 200, 255]
//    //B = 0
//    //G = 1
//    //R = 2
//    //A = 3
//    public class Search
//    {
//        public static bool PatternsInHandle(IntPtr Handle, PatternList patterns, int waitDelay = 0)
//        {
//            System.Drawing.Bitmap screenshot = Screenshot.Handle(Handle);
//            return PatternsInBitmap(screenshot, patterns, waitDelay);
//        }

//        public static unsafe bool PatternsInBitmap(System.Drawing.Bitmap bitmap, PatternList patterns, int waitDelay = 0)
//        {
//            if (waitDelay != 0)
//                Thread.Sleep(waitDelay);

//            int width = bitmap.Width;
//            int height = bitmap.Height;

//            System.Drawing.Imaging.BitmapData bData = bitmap.LockBits(
//                new System.Drawing.Rectangle(0, 0, width, height),
//                System.Drawing.Imaging.ImageLockMode.ReadOnly,
//                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

//            byte* scan0 = (byte*)bData.Scan0.ToPointer();
//            int maxScan = bData.Stride * height;

//            patterns.ResetInstances();

//            foreach (Pattern pat in patterns)
//            {
//                int overflow = bData.Stride * pat.Height + pat.Stride; // corect
//                int schimbaRow = bData.Stride - pat.Stride;

//                int index = 0;
//                //MAIN screenshot loop
//                for (int y = 0; y < height; y++)
//                {
//                    for (int x = 0; x < width; x++, index += 4)
//                    {
//                        //Cauta primul pixel not negru din pattern in screenshot
//                        if (pat.CheckFirstPixel(scan0[index + 0], scan0[index + 1], scan0[index + 2]))
//                        {
//                            //check buffer overflow
//                            if ((index + overflow) > maxScan)
//                                goto END;

//                            //deruleaza buffer inapoi
//                            int iTmp = index - pat.FirstPixelIndex;
//                            int iPat = 0;
//                            List<MyPoint> tmpList = new List<MyPoint>();

//                            //PATTERN loop
//                            for (int yy = 0; yy < pat.Height; yy++, iTmp += schimbaRow)
//                            {
//                                for (int xx = 0; xx < pat.Width; xx++, iTmp += 4, iPat += 4)
//                                {
//                                    //Check Pixel Negru
//                                    if (pat.BlackPixel(iPat))
//                                        continue;

//                                    //check Threshold
//                                    if (pat.CheckThreshold(scan0[iTmp + 0], scan0[iTmp + 1], scan0[iTmp + 2], iPat))
//                                    {
//                                        tmpList.Add(new MyPoint(xx + x, yy + y));
//                                        continue;
//                                    }
//                                    else goto OUT;
//                                }//PATTERN Loop x
//                            }//PATTERN Loop y

//                            pat.AddInstance(tmpList);
//                        }//end if

//                    OUT: ;
//                    }//MAIN Loop x
//                }//MAIN Loop y

//                END: ;
//            }//end foreach

//            bitmap.UnlockBits(bData);

//            if (patterns.HasDetection)
//                return true;
//            return false;
//        }

//        public static unsafe bool PatternInBitmap(System.Drawing.Bitmap bitmap, Pattern pat, int waitDelay = 0)
//        {
//            Stopwatch sw = new Stopwatch();
//            sw.Start();

//            if (waitDelay != 0)
//                Thread.Sleep(waitDelay);

//            int width = bitmap.Width;
//            int height = bitmap.Height;

//            System.Drawing.Imaging.BitmapData bData = bitmap.LockBits(
//                new System.Drawing.Rectangle(0, 0, width, height),
//                System.Drawing.Imaging.ImageLockMode.ReadOnly,
//                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

//            byte* scan0 = (byte*)bData.Scan0.ToPointer();
//            int maxScan = bData.Stride * height;
//            int overflow = bData.Stride * pat.Height + pat.Stride; // corect
//            int schimbaRow = bData.Stride - pat.Stride;

//            pat.ResetInstances();
//            int index = 0;
//            //MAIN screenshot loop
//            for (int y = 0; y < height; y++)
//            {
//                for (int x = 0; x < width; x++, index += 4)
//                {
//                    //Cauta primul pixel not negru din pattern in screenshot
//                    if (pat.CheckFirstPixel(scan0[index + 0], scan0[index + 1], scan0[index + 2]))
//                    {
//                        //check buffer overflow
//                        if ((index + overflow) > maxScan)
//                            goto END;

//                        //deruleaza buffer inapoi
//                        int iTmp = index - pat.FirstPixelIndex;
//                        int iPat = 0;
//                        List<MyPoint> tmpList = new List<MyPoint>();

//                        //PATTERN loop
//                        for (int yy = 0; yy < pat.Height; yy++, iTmp += schimbaRow)
//                        {
//                            for (int xx = 0; xx < pat.Width; xx++, iTmp += 4, iPat += 4)
//                            {
//                                //Check Pixel Negru
//                                if (pat.BlackPixel(iPat))
//                                    continue;

//                                //check Threshold
//                                if (pat.CheckThreshold(scan0[iTmp + 0], scan0[iTmp + 1], scan0[iTmp + 2], iPat))
//                                {
//                                    tmpList.Add(new MyPoint(xx + x, yy + y));
//                                    continue;
//                                }
//                                else goto OUT;
//                            }//PATTERN Loop x
//                        }//PATTERN Loop y

//                        pat.AddInstance(tmpList);
//                    }//end if

//                OUT: ;
//                }//MAIN Loop x
//            }//MAIN Loop y

//                END: ;

//            bitmap.UnlockBits(bData);

//            sw.Stop();
//            Console.Write(sw.ElapsedMilliseconds + " ms ==>> ");

//            if (pat.HasDetection)
//            {
//                Console.Write(true + "\n");
//                return true;
//            }
//            Console.Write(false + "\n");
//            return false;
//        }
//    }
//}