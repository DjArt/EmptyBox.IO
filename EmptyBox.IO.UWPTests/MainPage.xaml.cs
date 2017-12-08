using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Devices.Radio;
using System.Threading.Tasks;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;
using Windows.UI.Core;

namespace EmptyBox.IO.UWPTests
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            TY();
        }

        async void TY()
        {
            BluetoothAdapter bt = await BluetoothAdapter.GetDefaultBluetoothAdapter();
            BluetoothConnectionSocketHandler listener = new BluetoothConnectionSocketHandler(new BluetoothAccessPoint(new MACAddress(), new BluetoothPort(0x4444)));
            await listener.Start();
            var x = await bt.FindDevices();
            foreach (var j0 in x)
            {
                var i = await j0.GetServices();
                foreach (var j1 in i)
                {
                    Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => list.Items.Add(j1));
                }
            }
        }

        private void Bt_ServiceAdded(IBluetoothAdapter connection, BluetoothAccessPoint device)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => list.Items.Add(device));
        }
    }
}
