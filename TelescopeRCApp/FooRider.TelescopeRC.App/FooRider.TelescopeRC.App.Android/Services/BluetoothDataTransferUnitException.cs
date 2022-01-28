using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FooRider.TelescopeRC.App.Droid.Services
{
  [Serializable]
  public class BluetoothDataTransferUnitException : Exception
  {
    public BluetoothDataTransferUnitException()
    {
    }

    public BluetoothDataTransferUnitException(string message) : base(message)
    {
    }

    public BluetoothDataTransferUnitException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BluetoothDataTransferUnitException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}