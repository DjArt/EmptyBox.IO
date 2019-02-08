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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace EmptyBox.IO.UWP.Test
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IGPIOController ctrl;
        IPWMController ctrl2;
        IPWMPin pin;
        Task T;

        public MainPage()
        {
            this.InitializeComponent();
            Test();
        }

        async Task Test()
        {
            ctrl = (await GPIOController.GetDefault()).Result;
            ctrl2 = new SoftwarePWMController(ctrl);
            ctrl2.Frequency = 1;
            pin = (await ctrl2.OpenPin(26)).Result;
            pin.DutyCycle = 0.5;
            pin.Start();
            T = Task.Run(() =>
            {
                Task.Delay(2000).Wait();
                //double j = -0.000001;
                while (ctrl2.Frequency < 1000)
                {
                    //if (j + pin.DutyCycle > 1 || j + pin.DutyCycle < 0)
                    //{
                    //    j *= -1;
                    //}
                    //pin.DutyCycle += j;
                    //Task.Delay(10).Wait();
                    ctrl2.Frequency++;
                    Task.Delay(10).Wait();
                }
            });
        }
    }
}
