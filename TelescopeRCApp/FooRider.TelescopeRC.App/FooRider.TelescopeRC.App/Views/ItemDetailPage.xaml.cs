using FooRider.TelescopeRC.App.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FooRider.TelescopeRC.App.Views
{
  public partial class ItemDetailPage : ContentPage
  {
    public ItemDetailPage()
    {
      InitializeComponent();
      BindingContext = new ItemDetailViewModel();
    }
  }
}