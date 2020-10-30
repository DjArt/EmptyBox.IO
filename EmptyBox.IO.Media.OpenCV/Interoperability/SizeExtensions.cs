using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.OpenCV.Interoperability
{
    public static class SizeExtensions
    {
        public static OpenCvSharp.Size ToOcvSize(this Foundation.Size size)
        {
            return new OpenCvSharp.Size(size.Width, size.Height);
        }
    }
}
