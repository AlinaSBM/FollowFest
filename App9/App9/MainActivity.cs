using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using System.Collections;

namespace App9
{
    [Activity(Label = "App9", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        Button onBtn, offBtn, visibleBtn, listBtn, change_Bluetooth_Name, display_Name;
        BluetoothAdapter blue;      //Bleutooth adapter class variable

        ListView list_Of_Devices;       //list view for paired devices
        EditText bluetoothName; 		//user bluetooth name edit text


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            initialize();
            blue = BluetoothAdapter.DefaultAdapter;	//getting bluetooth defapult adapter
        }

        public void initialize()
        {
            onBtn = (Button)FindViewById(Resource.Id.onB);
            onBtn.Click += OnBtn_Click;
            offBtn = (Button)FindViewById(Resource.Id.offB);
            offBtn.Click += OnBtn_Click;
            //  visibleBtn = (Button)FindViewById(Resource.Id.visibleD);
            listBtn = (Button)FindViewById(Resource.Id.pairD);
            listBtn.Click += OnBtn_Click;
            bluetoothName = (EditText)FindViewById(Resource.Id.name);
            //change_bluetooth_Name = (Botton)FindViewById(Resource.Id.nameBtn);
            display_Name = (Button)FindViewById(Resource.Id.showName);
            list_Of_Devices = (ListView)FindViewById(Resource.Id.list_devices);

        }

        private void OnBtn_Click(object sender, EventArgs e)
        {
            OnClick((View)sender);
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.onB:
                    if (!blue.Enable())
                    {
                        Intent o = new Intent(BluetoothAdapter.ActionRequestEnable);
                        StartActivityForResult(o, 0);

                    }
                    Toast.MakeText(this, "Enable", ToastLength.Short).Show();
                    break;
                case Resource.Id.offB:
                    if (blue.Enable())
                    {
                        blue.Disable();
                        Toast.MakeText(this, "Bluetooth Disable", ToastLength.Short).Show();
                    }
                    break;
                // case Resource.Id.visibleD:
                //    Intent visible = new Intent(BluetoothAdapter.ActionsRequestDiscoverable)'

                //     StartActivityForResult(visible, 0);
                //break;
                case Resource.Id.pairD:

                    ArrayList list = new ArrayList();
                    foreach (BluetoothDevice bt in blue.BondedDevices)
                    {
                        list.Add(bt.Name);
                    }

                    ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, list);
                    list_Of_Devices.SetAdapter(adapter);
                    break;
                //case Resource.Id.nameBtn:
                //    if (!bluetoothName.Text.ToString().Equals(""))
                //    {
                //        String n = bluetoothName.Text.Tostring();
                //        blue.SetName(n);
                //    }
                //    else
                //    {
                //        Toast.MakeText(this, "Please enter name", ToastLength.Short).Show();
                //    }
                //    break;
                case Resource.Id.showName:
                    Toast.MakeText(this, blue.Name.ToString(), ToastLength.Short).Show();
                    break;
            }

        }

    }
}
