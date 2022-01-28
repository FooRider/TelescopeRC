using Android.Bluetooth;
using FooRider.TelescopeRC.App.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FooRider.TelescopeRC.App.Services
{
  internal class BluetoothCommunicator : IBluetoothCommunicator, IDisposable
  {
    private readonly BluetoothManager manager;
    private readonly BluetoothAdapter adapter;

    private BluetoothDeviceModel deviceModel;

    private BluetoothDevice device;
    private BluetoothSocket socket;

    public BluetoothCommunicator()
    {
      manager = (BluetoothManager)Android.App.Application.Context.GetSystemService("bluetooth");
      adapter = manager.Adapter;
    }

    public Task<IEnumerable<BluetoothDeviceModel>> ListDevices()
    {
      var result = adapter.BondedDevices.Select(d => new BluetoothDeviceModel(d.Address, d.Name)).ToList();
      return Task.FromResult(result.AsEnumerable());
    }

    public async Task SetBluetoothDevice(BluetoothDeviceModel deviceModel)
    {
      try
      {
        this.deviceModel = deviceModel;

        CloseConnection();

        device = adapter.GetRemoteDevice(deviceModel.Address);
        socket = await CreateSocket(device);
        await socket.ConnectAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("SendTxt(): Exception caught: " + ex.Message);
        Debug.WriteLine(ex.StackTrace);

        CloseConnection();
      }
    }

    private async Task<BluetoothSocket> CreateSocket(BluetoothDevice bluetoothDevice)
    {
      var bdclass = Java.Lang.Class.FromType(typeof(BluetoothDevice));

      var methods = bdclass.GetMethods().Where(m => m.Name == "createInsecureRfcommSocket").ToArray();
      var method = methods.First();

      var res = method.Invoke(bluetoothDevice, 1);

      var sock = res as BluetoothSocket;

      return sock;
    }

    public async Task SendTxt(string text)
    {
      try
      {
        var data = Encoding.UTF8.GetBytes(text);

        await socket?.OutputStream.WriteAsync(data, 0, data.Length);
      }
      catch (Exception ex)
      {
        Debug.WriteLine("SendTxt(): Exception caught: " + ex.Message);
        Debug.WriteLine(ex.StackTrace);
      }
    }

    private void CloseConnection()
    {
      using (socket?.InputStream) { }
      using (socket?.OutputStream) { }
      using (socket) { }
      using (device) { }

      socket = null;
      device = null;
    }

    public void Dispose()
    {
      CloseConnection();
    }
  }
}
