using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FooRider.TelescopeRC.App.Services;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace FooRider.TelescopeRC.App.Droid.Services
{ 
  [Deprecated]
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

      //return manager.Adapter.State.ToString();

      

      var adapter = manager.Adapter;

      var sb = new System.Text.StringBuilder();

      if (!adapter.StartDiscovery())
        return "Could not start discovery";

      await Task.Delay(3000);

      if (adapter.BondedDevices != null)
        foreach (var bd in adapter.BondedDevices)
          sb.Append($"{bd.Name}; ");

      return sb.ToString();

      return "OK";
    }
  }
}