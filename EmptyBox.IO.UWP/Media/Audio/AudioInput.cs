using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Media.Audio;
using Windows.Devices;
using Windows.Devices.Enumeration;

namespace EmptyBox.IO.Media.Audio
{
    public sealed class AudioInput : IAudioInput
    {

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
