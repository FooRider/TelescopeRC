using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FooRider.TelescopeRC.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


//[assembly: Dependency(typeof(FooRider.TelescopeRC.App.Droid.Services.BluetoothCom))]
namespace FooRider.TelescopeRC.App.Droid.Services
{ 
  class BluetoothCom : IBluetoothCom
  {
    private readonly BluetoothManager manager;

    public BluetoothCom(Context context)
    {
      var x = context.GetSystemService(Context.BluetoothService);
      this.manager = x.JavaCast<BluetoothManager>();
      //this.adapter = DependencyService.Resolve<BluetoothAdapter>();
      //this.adapter = BluetoothAdapter.DefaultAdapter;
    }

    public async Task<string> DoSomething(string arg)
    {
      if (manager == null)
        return "NULL MANAGER";

      if (manager.Adapter == null)
        return "NULL ADAPTER";

      return manager.Adapter.State.ToString();

      return "OK";
    }
  }
}