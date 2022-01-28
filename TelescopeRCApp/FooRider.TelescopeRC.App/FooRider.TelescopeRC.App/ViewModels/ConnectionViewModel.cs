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
  public class ConnectionViewModel : BaseViewModel
  {
    private readonly IBluetoothCommunicator bluetoothCommunicator;

    private MainViewModel mainViewModel;
    public MainViewModel MainViewModel
    {
      get => mainViewModel;
      set => SetProperty(ref mainViewModel, value);
    }

    private ObservableCollection<BluetoothDeviceViewModel> btDevices = new ObservableCollection<BluetoothDeviceViewModel>();
    public ObservableCollection<BluetoothDeviceViewModel> BtDevices
    {
      get => btDevices;
      set => SetProperty(ref btDevices, value);
    }


    private ICommand listDevicesCmd;
    public ICommand ListDevicesCmd => listDevicesCmd ?? (listDevicesCmd = new Command(async () => await ListDevices()));


    private ICommand connectDeviceCmd;
    public ICommand ConnectDeviceCmd => connectDeviceCmd ?? (connectDeviceCmd = new Command<BluetoothDeviceViewModel>(async (vm) => await ConnectDevice(vm)));

    private ICommand testSendCmd;
    public ICommand TestSendCmd => testSendCmd ?? (testSendCmd = new Command(async () => await TestSend()));

    public ConnectionViewModel()
    {
      bluetoothCommunicator = DependencyService.Resolve<IBluetoothCommunicator>();
    }

    private async Task ConnectDevice(BluetoothDeviceViewModel btvm)
    {
      await bluetoothCommunicator.SetBluetoothDevice(btvm.BluetoothDevice);
    }

    private async Task ListDevices()
    {
      btDevices.Clear();

      var devices = await bluetoothCommunicator.ListDevices();

      foreach (var bd in devices)
      {
        var vm = new BluetoothDeviceViewModel()
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
      await bluetoothCommunicator.SendTxt("Hello world!");
    }
  }
}
