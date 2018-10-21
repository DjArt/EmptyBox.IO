using EmptyBox.IO.Test;
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
using System.Reflection;
using EmptyBox.ScriptRuntime.Extensions;
using Windows.UI.Core;

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
            List <Type> tests = Assembly.Load(new AssemblyName("EmptyBox.IO.Test")).ExportedTypes.Where(x => x.GetTypeInfo().IsClass & x.GetTypeInfo().ImplementedInterfaces.Contains(typeof(ITest))).ToList();
            foreach(Type test in tests)
            {
                l_Tests.Items.Add(test.GenerateEmptyObject());
            }
            l_Tests.Items.Add("Audio");
        }

        private void l_Tests_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is ITest _test)
            {
                Frame.Navigate(typeof(Test), _test);
            }
            else if (e.ClickedItem is string _namedTest)
            {
                switch (_namedTest)
                {
                    case "Audio":
                        Frame.Navigate(typeof(AudioTest));
                        break;
                }
            }
        }
    }
}
