using EmptyBox.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.GPIO.PWM
{
    public interface IPWMPin : IDevice, ITreeNode<IPWMPin>
    {
        double DutyCycle { get; set; }
        uint PinNumber { get; }
        bool InvertedPolarity { get; set; }
        new IPWMController Parent { get; }

        void Start();
        void Stop();
    }
}
