using FooRider.TelescopeRC.App.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FooRider.TelescopeRC.App.Services
{
  internal class BluetoothCommunicator : IBluetoothCommunicator, IDisposable
  {
    private readonly IBluetoothAdapter bluetoothAdapter;

    private BluetoothDeviceModel bluetoothDevice;
    //private IBluetoothManagedConnection connection;
    private IBluetoothConnection basicConnection;

    public BluetoothCommunicator()
    {
      bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();
      if (!bluetoothAdapter.Enabled)
        bluetoothAdapter.Enable();
    }

    public Task<IEnumerable<BluetoothDeviceModel>> ListDevices()
    {
      return Task.FromResult(bluetoothAdapter.BondedDevices);
    }

    public async Task SetBluetoothDevice(BluetoothDeviceModel bluetoothDevice)
    {
      try
      {
        this.bluetoothDevice = bluetoothDevice;

        CloseConnection();

        //connection = bluetoothAdapter.CreateManagedConnection(bluetoothDevice);
        //connection.OnTransmitted += Connection_OnTransmitted;
        //connection.OnStateChanged += Connection_OnStateChanged;
        //connection.OnRecived += Connection_OnRecived;
        //connection.OnError += Connection_OnError;
        //connection.Connect();

        basicConnection = bluetoothAdapter.CreateConnection(bluetoothDevice);
        await basicConnection.ConnectAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("SendTxt(): Exception caught: " + ex.Message);
        Debug.WriteLine(ex.StackTrace);
      }
    }

    public async Task SendTxt(string text)
    {
      try
      {
        var data = Encoding.UTF8.GetBytes(text);

        //if (connection != null)
        //{
        //  connection.Transmit(data, 0, data.Length);
        //}

        if (basicConnection != null)
        {
          await basicConnection.TransmitAsync(data, 0, data.Length);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("SendTxt(): Exception caught: " + ex.Message);
        Debug.WriteLine(ex.StackTrace);
      }
    }

    //private void Connection_OnError(object sender, System.Threading.ThreadExceptionEventArgs threadExceptionEventArgs)
    //{
    //  Debug.WriteLine("Connection_OnError(): " + threadExceptionEventArgs?.Exception?.Message);
    //  Debug.WriteLine(threadExceptionEventArgs?.Exception?.StackTrace);
    //}

    //private void Connection_OnRecived(object sender, RecivedEventArgs recivedEventArgs)
    //{
    //  Debug.WriteLine($"Connection_OnReceived(): ...");
    //}

    //private void Connection_OnStateChanged(object sender, StateChangedEventArgs stateChangedEventArgs)
    //{
    //  Debug.WriteLine($"Connection_OnStateChanged(): {stateChangedEventArgs.ConnectionState}");
    //}

    //private void Connection_OnTransmitted(object sender, TransmittedEventArgs transmittedEventArgs)
    //{
    //  Debug.WriteLine($"Connection_OnTransmitted(): ...");
    //}

    private void CloseConnection()
    {
      //if (connection != null)
      //{
      //  using (connection)
      //  {
      //    connection.OnTransmitted -= Connection_OnTransmitted;
      //    connection.OnStateChanged -= Connection_OnStateChanged;
      //    connection.OnRecived -= Connection_OnRecived;
      //    connection.OnError -= Connection_OnError;
      //    connection = null;
      //  }
      //}

      if (basicConnection != null)
      {
        using (basicConnection) { }
        basicConnection = null;
      }
    }

    public void Dispose()
    {
      CloseConnection();
    }
  }
}
