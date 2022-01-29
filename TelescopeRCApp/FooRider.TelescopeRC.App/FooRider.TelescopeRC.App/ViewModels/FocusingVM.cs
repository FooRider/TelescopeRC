using FooRider.TelescopeRC.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

    private int stepDuration = 100;
    public int StepDuration
    {
      get => stepDuration;
      set => SetProperty(ref stepDuration, value);
    }

    private List<SpeedVM> speeds = new List<SpeedVM>();
    public IEnumerable<SpeedVM> Speeds => speeds;

    private SpeedVM selectedSpeed;
    public SpeedVM SelectedSpeed
    {
      get => selectedSpeed;
      set => SetProperty(ref selectedSpeed, value);
    }

    private ICommand sendStepCmd;
    public ICommand SendStepCmd => sendStepCmd ?? (sendStepCmd = new Command<Direction>(async (dir) => await SendStepAsync(dir)));

    public FocusingVM()
    {
      speeds.Add(new SpeedVM() { Speed = 16 });
      //speeds.Add(new SpeedVM() { Speed = 32 });
      speeds.Add(new SpeedVM() { Speed = 64 });
      //speeds.Add(new SpeedVM() { Speed = 128 });
      speeds.Add(new SpeedVM() { Speed = 255 });

      SelectedSpeed = speeds.FirstOrDefault();
    }

    private string ConstructCommand(Direction direction, byte speed, int durationMs)
    {
      var sb = new StringBuilder();
      sb.Append("F ");
      if (direction == Direction.Forward)
        sb.Append('+');
      else
        sb.Append('-');
      sb.Append(speed.ToString());
      sb.Append(' ');
      sb.Append(durationMs.ToString());
      sb.Append("\r\n");
      return sb.ToString();
    }

    private async Task SendStepAsync(Direction direction)
    {
      System.Diagnostics.Debug.WriteLine($"Sending step {direction}, intensity {SelectedSpeed?.Speed}");

      var cmd = ConstructCommand(direction, SelectedSpeed?.Speed ?? 0, StepDuration);
      await MainViewModel.BluetoothCommunicator.SendCommandAsync(cmd);
    }

    public async Task SendContinuousStartAsync(Direction direction)
    {
      System.Diagnostics.Debug.WriteLine($"Starting continuous {direction}, intensity {SelectedSpeed?.Speed}");

      var cmd = ConstructCommand(direction, SelectedSpeed?.Speed ?? 0, 5000);
      await MainViewModel.BluetoothCommunicator.SendCommandAsync(cmd);
    }

    public async Task SendContinuousStopAsync()
    {
      System.Diagnostics.Debug.WriteLine($"Stopping continuous");

      var cmd = ConstructCommand(Direction.Forward, 0, 1);
      await MainViewModel.BluetoothCommunicator.SendCommandAsync(cmd);
    }
  }
}
