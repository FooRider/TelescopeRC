using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.TelescopeRC.App.Models
{
  public class BluetoothDeviceModel
  {
    public string Address { get; }
    public string Name { get; }

    public BluetoothDeviceModel(string Address, string Name)
    {
      this.Address = Address;
      this.Name = Name;
    }
  }
}
