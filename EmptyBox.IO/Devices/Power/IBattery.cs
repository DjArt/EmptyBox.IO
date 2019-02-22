using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Power
{
    public interface IBattery : IDevice
    {
        double? Level { get; }
        double? WearLevel { get; }
        int? ChargeRate { get; }
        uint? ChargeVoltage { get; }
        int? ChargeCurrent { get; }
        uint? DesignedCapacity { get; }
        uint? FullyChargedCapacity { get; }
        uint? RemainingCapacity { get; }
        BatteryTechnology? Technology { get; }
    }
}