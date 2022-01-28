using FooRider.TelescopeRC.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FooRider.TelescopeRC.App.Services
{
  public interface IBluetoothCommunicator
  {
    Task SetBluetoothDevice(BluetoothDeviceModel bluetoothDevice);
    Task SendTxt(string text);
    Task<IEnumerable<BluetoothDeviceModel>> ListDevices();
  }
}
