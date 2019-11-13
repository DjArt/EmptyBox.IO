using EmptyBox.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO.PWM
{
    public interface IPWMController : IDevice, IObservableTreeNode<IPWMPin>
    {
        double MaxFrequency { get; }
        double MinFrequency { get; }
        double Frequency { get; set; }
        uint PinCount { get; }

        Task<IPWMPin> OpenPin(uint number);
    }
}
