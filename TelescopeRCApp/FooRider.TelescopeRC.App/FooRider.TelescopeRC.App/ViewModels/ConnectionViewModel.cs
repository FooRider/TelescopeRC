using FooRider.TelescopeRC.App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FooRider.TelescopeRC.App.ViewModels
{
  public class ConnectionViewModel : BaseViewModel
  {
    public string Title => "Connection";

    private string test1 = "a";
    public string Test1
    {
      get => test1;
      set
      {
        if (test1 != value)
        {
          test1 = value;
          OnPropertyChanged();
        }
      }
    }

    private ICommand testCmd;
    public ICommand TestCmd => testCmd ?? (testCmd = new Command(async () => await Test()));

    private async Task Test()
    {
      var bc = DependencyService.Resolve<IBluetoothCom>();

      Test1 = await bc.DoSomething("ahoj");

      //Test1 = DateTime.Now.ToString();
      //await Task.Delay(1000);
      //Test1 = DateTime.Now.ToString();
      //await Task.Delay(1000);
      //Test1 = DateTime.Now.ToString();
    }
  }
}
