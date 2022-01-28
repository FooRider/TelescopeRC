using FooRider.TelescopeRC.App.Services;
using Plugin.BluetoothClassic.Abstractions;
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

    private async Task ConnectDevice(BluetoothDeviceViewModel btvm)
    {

    }

    private async Task ListDevices()
    {
      var ba = DependencyService.Resolve<IBluetoothAdapter>();

      if (!ba.Enabled)
        ba.Enable();

      btDevices.Clear();

      foreach (var bd in ba.BondedDevices)
      {
        var vm = new BluetoothDeviceViewModel()
        {
          Address = bd.Address,
          Name = bd.Name,
        };
        btDevices.Add(vm);
      }
    }

    //private string test1 = "a";
    //public string Test1
    //{
    //  get => test1;
    //  set
    //  {
    //    if (test1 != value)
    //    {
    //      test1 = value;
    //      OnPropertyChanged();
    //    }
    //  }
    //}

    //private ICommand testCmd;
    //public ICommand TestCmd => testCmd ?? (testCmd = new Command(async () => await TestList()));

    //private async Task Test()
    //{
    //  var bc = DependencyService.Resolve<IBluetoothCom>();

    //  Test1 = await bc.DoSomething("ahoj");

    //  //Test1 = DateTime.Now.ToString();
    //  //await Task.Delay(1000);
    //  //Test1 = DateTime.Now.ToString();
    //  //await Task.Delay(1000);
    //  //Test1 = DateTime.Now.ToString();
    //}

    //private async Task TestList()
    //{
    //  var sb = new StringBuilder();


    //  var bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();

    //  if (!bluetoothAdapter.Enabled)
    //    bluetoothAdapter.Enable();

    //  var deviceVMs = bluetoothAdapter.BondedDevices;
    //  foreach (var d in deviceVMs)
    //    sb.Append($"{d.Address} {d.Name}; " + Environment.NewLine);

    //  Test1 = sb.ToString();
    //}
  }
}
