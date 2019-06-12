using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace EmptyBox.IO.Interoperability
{
    public static class Сompatibility
    {
        public static bool IsCompatible()
        {
            try
            {
                if (new EasClientDeviceInformation().FriendlyName != null)
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