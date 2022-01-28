using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FooRider.TelescopeRC.App.Services
{
  public interface IBluetoothCom
  {
    Task<string> DoSomething(string arg);
  }
}
