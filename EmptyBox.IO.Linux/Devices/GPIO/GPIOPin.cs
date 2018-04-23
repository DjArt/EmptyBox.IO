using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public class GPIOPin : IGPIOPin
    {
        public string Name => "GPIO" + Number;
        public uint Number { get; private set; }

        internal GPIOPin(uint number)
        {
            Number = number;
        }
    }
}
