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
            await bt.StartListener(new BluetoothPort(0x4444), new byte[0]);
            IEnumerable<BluetoothAccessPoint> services = await bt.FindServices(new BluetoothPort(0x4444));
            services.All(x => { list.Items.Add(x); return true; });
        }
    }
}
