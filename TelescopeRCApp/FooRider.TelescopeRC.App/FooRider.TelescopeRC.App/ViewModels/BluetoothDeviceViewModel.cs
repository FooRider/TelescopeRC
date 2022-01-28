using Plugin.BluetoothClassic.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class BluetoothDeviceViewModel : BaseViewModel
  {
    private BluetoothDeviceModel bluetoothDevice;
    public BluetoothDeviceModel BluetoothDevice
    {
      get => bluetoothDevice;
      set => SetProperty(ref bluetoothDevice, value);
    }

    private string address;
    public string Address
    {
      get => address;
      set => SetProperty(ref address, value);
    }

    private string name;
    public string Name
    {
      get => name;
      set => SetProperty(ref name, value);
    }
  }
}
