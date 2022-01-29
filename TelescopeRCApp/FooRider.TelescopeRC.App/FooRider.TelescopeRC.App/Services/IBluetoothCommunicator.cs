using FooRider.TelescopeRC.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FooRider.TelescopeRC.App.Services
{
  public interface IBluetoothCommunicator
  {
    Task SetBluetoothDeviceAsync(BluetoothDeviceModel bluetoothDevice);
    Task SendCommandAsync(string command);
    Task<IEnumerable<BluetoothDeviceModel>> ListDevicesAsync();

    Task<bool> GetIsConnectedAsync();
  }
}
