using FooRider.TelescopeRC.App.Models;
using FooRider.TelescopeRC.App.ViewModels;
using FooRider.TelescopeRC.App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FooRider.TelescopeRC.App.Views
{
  public partial class ItemsPage : ContentPage
  {
    ItemsViewModel _viewModel;

    public ItemsPage()
    {
      InitializeComponent();

      BindingContext = _viewModel = new ItemsViewModel();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      _viewModel.OnAppearing();
    }
  }
}