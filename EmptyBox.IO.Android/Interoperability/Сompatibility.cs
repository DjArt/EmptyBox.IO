using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmptyBox.IO.Interoperability
{
    public static class Сompatibility
    {
        [StandardRealization]
        public static bool IsCompatible()
        {
            try
            {
                if (Build.Device != null)
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