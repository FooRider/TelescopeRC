using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class FocusingVM : BaseViewModel
  {
    private MainVM mainViewModel;
    public MainVM MainViewModel
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
