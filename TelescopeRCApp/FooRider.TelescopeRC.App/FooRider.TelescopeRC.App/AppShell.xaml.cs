using FooRider.TelescopeRC.App.ViewModels;
using FooRider.TelescopeRC.App.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FooRider.TelescopeRC.App
{
  public partial class AppShell : Xamarin.Forms.Shell
  {
    public AppShell()
    {
      InitializeComponent();
      Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
      Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
    }

  }
}
