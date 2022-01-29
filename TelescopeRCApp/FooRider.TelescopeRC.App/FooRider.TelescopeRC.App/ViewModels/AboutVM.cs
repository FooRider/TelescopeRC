using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class AboutVM : BaseViewModel
  {
    public AboutVM()
    {
      OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
    }

    public ICommand OpenWebCommand { get; }
  }
}