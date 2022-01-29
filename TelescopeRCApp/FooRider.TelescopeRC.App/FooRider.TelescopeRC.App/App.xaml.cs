using FooRider.TelescopeRC.App.Services;
using FooRider.TelescopeRC.App.ViewModels;
using FooRider.TelescopeRC.App.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FooRider.TelescopeRC.App
{
  public partial class App : Application
  {

    public App()
    {
      InitializeComponent();
      
      MainPage = new AppShell();
    }

    protected override void OnStart()
    {
      ((MainVM)Resources["MainVM"]).OnStart();
    }

    protected override void OnSleep()
    {
      ((MainVM)Resources["MainVM"]).OnSleep();
    }

    protected override void OnResume()
    {
      ((MainVM)Resources["MainVM"]).OnResume();
    }
  }
}
