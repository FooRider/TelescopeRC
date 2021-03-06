using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using FooRider.TelescopeRC.App.Services;

namespace FooRider.TelescopeRC.App.Droid
{
  [Activity(Label = "FooRider.TelescopeRC.App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      Xamarin.Essentials.Platform.Init(this, savedInstanceState);
      global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

      RegisterServices();
      LoadApplication(new App());
    }
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
      Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

      base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    private void RegisterServices()
    {
      //DependencyService.RegisterSingleton<IBluetoothAdapter>(new BluetoothAdapter());
      DependencyService.RegisterSingleton<IBluetoothCommunicator>(new BluetoothCommunicator());
      //DependencyService.RegisterSingleton<IBluetoothCom>(new BluetoothCom(this.ApplicationContext));
    }
  }
}