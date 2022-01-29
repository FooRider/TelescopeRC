using FooRider.TelescopeRC.App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class ConnectionVM : BaseViewModel
  {
    private MainVM mainViewModel;
    public MainVM MainViewModel
    {
      get => mainViewModel;
      set => SetProperty(ref mainViewModel, value);
    }

    private ObservableCollection<BluetoothDeviceVM> btDevices = new ObservableCollection<BluetoothDeviceVM>();
    public ObservableCollection<BluetoothDeviceVM> BtDevices
    {
      get => btDevices;
      set => SetProperty(ref btDevices, value);
    }


    private ICommand listDevicesCmd;
    public ICommand ListDevicesCmd => listDevicesCmd ?? (listDevicesCmd = new Command(async () => await ListDevices()));


    private ICommand connectDeviceCmd;
    public ICommand ConnectDeviceCmd => connectDeviceCmd ?? (connectDeviceCmd = new Command<BluetoothDeviceVM>(async (vm) => await ConnectDevice(vm)));

    private ICommand testSendCmd;
    public ICommand TestSendCmd => testSendCmd ?? (testSendCmd = new Command(async () => await TestSend()));

    private async Task ConnectDevice(BluetoothDeviceVM btvm)
    {
      await MainViewModel.BluetoothCommunicator.SetBluetoothDeviceAsync(btvm.BluetoothDevice);
    }

    private async Task ListDevices()
    {
      btDevices.Clear();

      var devices = await MainViewModel.BluetoothCommunicator.ListDevicesAsync();

      foreach (var bd in devices)
      {
        var vm = new BluetoothDeviceVM()
        {
          Address = bd.Address,
          Name = bd.Name,
          BluetoothDevice = bd
        };
        btDevices.Add(vm);
      }
    }

    private async Task TestSend()
    {
      await MainViewModel.BluetoothCommunicator.SendCommandAsync("Hello world!");
    }
  }
}
