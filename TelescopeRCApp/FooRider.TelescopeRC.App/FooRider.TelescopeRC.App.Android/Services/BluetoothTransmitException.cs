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
  public class BluetoothTransmitException : BluetoothDataTransferUnitException
  {
    public Memory<byte> Buffer { get; }

    public BluetoothTransmitException()
    {
    }

    public BluetoothTransmitException(string message) : this(message, null, null)
    {
    }

    public BluetoothTransmitException(string message, Exception innerException) : this(message, innerException, null)
    {
    }

    public BluetoothTransmitException(string message, Exception innerException, Memory<byte> buffer) : base(message, innerException)
    {
      this.Buffer = buffer;
    }

    protected BluetoothTransmitException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}