using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class SpeedVM : BaseViewModel
  {
    private byte speed;
    public byte Speed
    {
      get => speed;
      set => SetProperty(ref speed, value);
    }
  }
}
