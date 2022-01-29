using FooRider.TelescopeRC.App.Models;
using FooRider.TelescopeRC.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FooRider.TelescopeRC.App.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class FocusingPage : ContentPage
  {
    public FocusingPage()
    {
      InitializeComponent();
    }

    private async void Button_Pressed(object sender, EventArgs e)
    {
      var obj = (sender as Button)?.CommandParameter;
      if (!(obj is Direction))
        return;

      var direction = (Direction)obj;
      await ((MainVM)BindingContext).FocusingViewModel.SendContinuousStartAsync(direction);
    }

    private async void Button_Released(object sender, EventArgs e)
    {
      await ((MainVM)BindingContext).FocusingViewModel.SendContinuousStopAsync();
    }
  }
}