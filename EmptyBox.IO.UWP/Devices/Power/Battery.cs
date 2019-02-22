using EmptyBox.IO.Interoperability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Power;

namespace EmptyBox.IO.Devices.Power
{
    public sealed class Battery : IBattery
    {
        private BatteryReport _Report;

        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public double? Level => RemainingCapacity / FullyChargedCapacity;
        public double? WearLevel => FullyChargedCapacity / DesignedCapacity;
        public int? ChargeRate => _Report.ChargeRateInMilliwatts;
        public uint? ChargeVoltage => null;
        public int? ChargeCurrent => null;
        public uint? DesignedCapacity => _Report.DesignCapacityInMilliwattHours.ToUInt();
        public uint? FullyChargedCapacity => _Report.FullChargeCapacityInMilliwattHours.ToUInt();
        public uint? RemainingCapacity => _Report.RemainingCapacityInMilliwattHours.ToUInt();
        public BatteryTechnology? Technology => null;
        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();
        public string Name { get; }
        public IDevice Parent => throw new NotImplementedException();
        public Windows.Devices.Power.Battery InternalDevice { get; }
        public DeviceInformation DeviceInformation { get; }

        internal Battery(Windows.Devices.Power.Battery battery)
        {
            InternalDevice = battery;
            _Report = InternalDevice.GetReport();
            InternalDevice.ReportUpdated += InternalDevice_ReportUpdated;
            DeviceInformation = DeviceInformation.CreateFromIdAsync(InternalDevice.DeviceId).GetResults();
            Name = DeviceInformation.Name;
        }

        private void InternalDevice_ReportUpdated(Windows.Devices.Power.Battery sender, object args)
        {
            _Report = InternalDevice.GetReport();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
