using Android.App;
using Android.Widget;
using Android.OS;
using static Android.Views.ViewGroup;
using EmptyBox.IO.Test.Devices.Bluetooth;
using System;
using EmptyBox.IO.Test;

namespace EmptyBox.IO.Android.Test
{
    [Activity(Label = "EmptyBox.IO.Android.Test", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            EditText mButton = (EditText)FindViewById(Resource.Id.editText1);
            try
            {
                ITest test = new GetPairedBluetoothDevices();
                mButton.Text = await test.Run();
            }
            catch (Exception ex)
            {
                mButton.Text = ex.ToString();
            }
        }
    }
}

