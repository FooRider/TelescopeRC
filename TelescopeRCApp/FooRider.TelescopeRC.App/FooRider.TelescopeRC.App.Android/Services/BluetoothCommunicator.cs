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
    private readonly Java.Util.UUID sppRecordUuid = Java.Util.UUID.FromString(BluetoothConstants.SppRecordUUID);

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

    public Task<IEnumerable<BluetoothDeviceModel>> ListDevicesAsync()
    {
      var result = new List<BluetoothDeviceModel>();
      foreach (var bd in adapter.BondedDevices)
      {
        if (!bd.GetUuids().Any(pu => pu.Uuid.CompareTo(sppRecordUuid) == 0))
          continue;

        var vm = new BluetoothDeviceModel(bd.Address, bd.Name);
        result.Add(vm);
      }

      return Task.FromResult(result.AsEnumerable());
    }

    public async Task SetBluetoothDeviceAsync(BluetoothDeviceModel deviceModel)
    {
      try
      {
        this.deviceModel = deviceModel;

        CloseConnection();

        device = adapter.GetRemoteDevice(deviceModel.Address);
        socket = await CreateSocketAsync(device);
        await socket.ConnectAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("SendTxt(): Exception caught: " + ex.Message);
        Debug.WriteLine(ex.StackTrace);

        CloseConnection();
      }
    }

    private async Task<BluetoothSocket> CreateSocketAsync(BluetoothDevice bluetoothDevice)
    {
      var bdclass = Java.Lang.Class.FromType(typeof(BluetoothDevice));

      var methods = bdclass.GetMethods().Where(m => m.Name == "createInsecureRfcommSocket").ToArray();
      var method = methods.First();

      var res = method.Invoke(bluetoothDevice, 1);

      var sock = res as BluetoothSocket;

      return sock;
    }

    public async Task SendCommandAsync(string command)
    {
      try
      {
        var data = Encoding.UTF8.GetBytes(command);

        if (socket != null)
          await socket.OutputStream.WriteAsync(data, 0, data.Length);
      }
      catch (Exception ex)
      {
        Debug.WriteLine("SendTxt(): Exception caught: " + ex.Message);
        Debug.WriteLine(ex.StackTrace);
      }
    }

    public async Task<bool> GetIsConnectedAsync()
    {
      return socket?.IsConnected ?? false;
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
