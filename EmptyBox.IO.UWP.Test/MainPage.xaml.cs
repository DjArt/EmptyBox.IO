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
using EmptyBox.IO.Devices.GPIO;
using EmptyBox.IO.Devices.GPIO.PWM;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using EmptyBox.IO.Devices;
using EmptyBox.IO.Test.Devices.Bluetooth;
using EmptyBox.IO.Devices.Enumeration;
using EmptyBox.IO.Devices.Bluetooth;
using Windows.UI.Core;
using EmptyBox.IO.Network.Bluetooth;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace EmptyBox.IO.UWP.Test
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            tb.Text = "";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            IDeviceEnumerator enumerator = DeviceEnumeratorProvider.Get();
            IBluetoothAdapter adapter = await enumerator.GetDefault<IBluetoothAdapter>();
            adapter.DeviceFound += Adapter_DeviceFound;
            adapter.DeviceLost += Adapter_DeviceLost;
            await adapter.StartWatcher();
        }

        private async void Adapter_DeviceLost(IDeviceProvider<IBluetoothDevice> provider, IBluetoothDevice device)
        {
        }

        private async void Adapter_DeviceFound(IDeviceProvider<IBluetoothDevice> provider, IBluetoothDevice device)
        {

        }
    }
}
