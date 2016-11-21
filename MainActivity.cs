using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Hardware;
using static Android.Hardware.Camera;
using Java.Lang;
using Android.Util;
using Android.Widget;
using Android.Media;
using Java.IO;
using Android.Graphics;

namespace AndroidCameraApps
{
    [Activity(Label = "AndroidCameraApps", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {

        private static Android.Hardware.Camera camera=null;
        private Parameters mParams;
        private bool isFlashLight;
        private ImageView btnFlash;
        private MediaPlayer player;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Flash Light";

            btnFlash = FindViewById<ImageView>(Resource.Id.imageView);
            player = MediaPlayer.Create(this, Resource.Raw.sound_click);

            btnFlash.Click += delegate {
                FlashLight();
            };



            bool hasFlash = ApplicationContext.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureCameraFlash);
            if (!hasFlash)
            {
                Android.App.AlertDialog alert = new Android.App.AlertDialog.Builder(this).Create();
                alert.SetTitle("Error");
                alert.SetMessage("Sorry,your device doesn't support flash light");
                alert.SetButton("OK", (s, e) => { return; });
                alert.Show();


            }

            getCamera();
        }

        private void getCamera()
        {
            if (camera == null)
            {
                try
                {
                    camera = Android.Hardware.Camera.Open();
                    mParams = camera.GetParameters();
                }
                catch(RuntimeException ex)
                {
                    Log.Info("ERROR", ex.Message);
                }
            }
        }

        private void FlashLight()
        {
            if (camera == null || mParams == null)
                return;

           
            if (!isFlashLight)
            {
                player.Start();
                //mParams = camera.GetParameters();
                //mParams.FlashMode = Parameters.FlashModeTorch;
                //camera.SetParameters(mParams);
                //camera.StartPreview();
                //isFlashLight = true;
                //btnFlash.SetImageResource(Resource.Drawable.power_on);
                camera.Release();
                camera = null;
                camera = Android.Hardware.Camera.Open();
                Android.Hardware.Camera.Parameters mParams = camera.GetParameters();
                mParams.FlashMode = (Android.Hardware.Camera.Parameters.FlashModeTorch);
                camera.SetParameters(mParams);
                var mPreviewTexture = new SurfaceTexture(0);
               
                    camera.SetPreviewTexture(mPreviewTexture);
                
                camera.StartPreview();
                btnFlash.SetImageResource(Resource.Drawable.power_on);
                isFlashLight = true;

            }
            else
            {
                //camera.Release();
                //camera.StopPreview();
                //camera = null;
                //player.Start();
                //mParams = camera.GetParameters();
                //mParams.FlashMode = Parameters.FlashModeOff;
                //camera.SetParameters(mParams);
                //camera.StartPreview();
                //isFlashLight = false;
                //btnFlash.SetImageResource(Resource.Drawable.power_off);
                camera.Release();
                camera = null;
                camera = Android.Hardware.Camera.Open();
                Android.Hardware.Camera.Parameters mParams = camera.GetParameters();
                mParams.FlashMode = (Android.Hardware.Camera.Parameters.FlashModeOff);
                camera.SetParameters(mParams);
                var mPreviewTexture = new SurfaceTexture(0);
               
                    camera.SetPreviewTexture(mPreviewTexture);
                
                camera.StartPreview();
                btnFlash.SetImageResource(Resource.Drawable.power_off);
                isFlashLight = false;
            }
        }
    }
}