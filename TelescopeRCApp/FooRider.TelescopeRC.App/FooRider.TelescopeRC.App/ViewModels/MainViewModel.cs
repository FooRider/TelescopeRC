using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
    private ConnectionViewModel connectionViewModel;
    public ConnectionViewModel ConnectionViewModel
    {
      get => connectionViewModel;
      set => SetProperty(ref connectionViewModel, value);
    }

    private FocusingViewModel focusingViewModel;
    public FocusingViewModel FocusingViewModel
    {
      get => focusingViewModel;
      set => SetProperty(ref focusingViewModel, value);
    }

    private CapturingViewModel capturingViewModel;
    public CapturingViewModel CapturingViewModel
    {
      get => capturingViewModel;
      set => SetProperty(ref capturingViewModel, value);
    }

    public MainViewModel()
    {
      ConnectionViewModel = new ConnectionViewModel()
      {
        MainViewModel = this
      };
      
      FocusingViewModel = new FocusingViewModel()
      {
        MainViewModel = this
      };

      CapturingViewModel = new CapturingViewModel()
      {
        MainViewModel = this
      };
    }
  }
}
