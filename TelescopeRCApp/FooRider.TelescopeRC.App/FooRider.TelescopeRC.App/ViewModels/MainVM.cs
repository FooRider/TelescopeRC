using FooRider.TelescopeRC.App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class MainVM : BaseViewModel
  {
    private IBluetoothCommunicator bluetoothCommunicator;
    public IBluetoothCommunicator BluetoothCommunicator => bluetoothCommunicator;

    private ConnectionVM connectionViewModel;
    public ConnectionVM ConnectionViewModel
    {
      get => connectionViewModel;
      set => SetProperty(ref connectionViewModel, value);
    }

    private FocusingVM focusingViewModel;
    public FocusingVM FocusingViewModel
    {
      get => focusingViewModel;
      set => SetProperty(ref focusingViewModel, value);
    }

    private CapturingVM capturingViewModel;
    public CapturingVM CapturingViewModel
    {
      get => capturingViewModel;
      set => SetProperty(ref capturingViewModel, value);
    }

    public MainVM()
    {
      ConnectionViewModel = new ConnectionVM()
      {
        MainViewModel = this
      };
      
      FocusingViewModel = new FocusingVM()
      {
        MainViewModel = this
      };

      CapturingViewModel = new CapturingVM()
      {
        MainViewModel = this
      };
    }

    public void OnStart()
    {
      bluetoothCommunicator = DependencyService.Resolve<IBluetoothCommunicator>();
    }

    public void OnSleep()
    {
    }

    public void OnResume()
    {
    }
  }
}
