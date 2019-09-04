using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Android.Util;

namespace XamarinFirebaseExample.Droid
{
    [Activity(Label = "XamarinFirebaseExample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Required for push notifications so check if Play services are available on the device or navigate to store to install them
            if (!IsPlayServicesAvailable())
            {
                GoogleApiAvailability.Instance.MakeGooglePlayServicesAvailable(this);
            }

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    Log.Info("IsPlayServicesAvailable", "Sorry, this device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {
                Log.Info("IsPlayServicesAvailable", "Google Play Services is available.");
                return true;
            }
        }
    }
}