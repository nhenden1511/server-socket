using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading;

namespace server_socket
{
    [Activity(Label = "server_socket", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var _btn1 = FindViewById<Button>(Resource.Id.btn1);
            var _btn2 = FindViewById<Button>(Resource.Id.btn2);
            var _btn3 = FindViewById<Button>(Resource.Id.btn3);
            var _btn4 = FindViewById<Button>(Resource.Id.btn4);
            var _txt2 = FindViewById<TextView>(Resource.Id.txt2);


            _btn1.Click += delegate
            {
                AsynchronousSocketListener.StartListening();

            };

            var signal = new ManualResetEvent(false);
            // or signal = new AutoResetEvent(false);

            
            _btn2.Click += delegate
            {
                signal.Reset();
                var newThreead = new Thread(() =>
                {
                    RunOnUiThread(() =>
                    {
                        _txt2.Text = "Wait....";
                    });

                    signal.WaitOne();

                    RunOnUiThread(() =>
                    {
                        _txt2.Text = "Done...";
                    });
                });
                newThreead.Start();
                
            };

            _btn3.Click += delegate
            {
                signal.Set();
            };


        }
    }
}

