using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class FocusingViewModel : BaseViewModel
  {
    private MainViewModel mainViewModel;
    public MainViewModel MainViewModel
    {
      get => mainViewModel;
      set
      {
        if (mainViewModel != value)
        {
          mainViewModel = value;
          OnPropertyChanged();
        }
      }
    }
  }
}
