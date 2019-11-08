using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace EmptyBox.IO.Interoperability
{
    public static class Сompatibility
    {
        public static bool IsCompatible()
        {
            try
            {
                if (UIDevice.CurrentDevice.Name != null)
                {
                    
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}