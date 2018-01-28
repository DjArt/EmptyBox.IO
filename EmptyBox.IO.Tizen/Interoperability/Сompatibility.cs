using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tizen.System;

namespace EmptyBox.IO.Interoperability
{
    public static class Сompatibility
    {
        [StandardRealization]
        public static bool IsCompatible()
        {
            try
            {
                if (SystemSettings.DeviceName != null)
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