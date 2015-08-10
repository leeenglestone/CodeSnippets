using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2015.FacialRecognition.Library
{
    public class FacialRecognition
    {
        public static void Run(string imagePath)
        {
            //Mat image = new Mat("lena.jpg", LoadImageType.Color); //Read the files as an 8-bit Bgr image  
            Mat image = new Mat(imagePath, LoadImageType.Color); //Read the files as an 8-bit Bgr image  

            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();

            //The cuda cascade classifier doesn't seem to be able to load "haarcascade_frontalface_default.xml" file in this release
            //disabling CUDA module for now
            bool tryUseCuda = false;
            bool tryUseOpenCL = true;

            DetectFace.Detect(
              image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",
              faces, eyes,
              tryUseCuda,
              tryUseOpenCL,
              out detectionTime);

            foreach (Rectangle face in faces)
                CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);

            //foreach (Rectangle eye in eyes)
            //   CvInvoke.Rectangle(image, eye, new Bgr(Color.Blue).MCvScalar, 2);

            //display the image 
            ImageViewer.Show(image, String.Format(
               "Completed face and eye detection using {0} in {1} milliseconds",
               (tryUseCuda && CudaInvoke.HasCuda) ? "GPU"
               : (tryUseOpenCL && CvInvoke.HaveOpenCLCompatibleGpuDevice) ? "OpenCL"
               : "CPU",
               detectionTime));
        }
    }
}
