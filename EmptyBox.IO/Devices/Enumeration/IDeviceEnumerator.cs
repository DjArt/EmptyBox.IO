﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Enumeration
{
    public interface IDeviceEnumerator
    {
        Task<IDevice> GetRoot();
        Task<TDevice> GetDefault<TDevice>()
            where TDevice : class, IDevice;
        Task<IEnumerable<TDevice>> FindAll<TDevice>()
            where TDevice : class, IDevice;
    }
}