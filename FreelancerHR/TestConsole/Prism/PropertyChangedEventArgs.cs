// Decompiled with JetBrains decompiler
// Type: System.ComponentModel.PropertyChangedEventArgs
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 5ABD58FD-DF31-44FD-A492-63F2B47CC9AF
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.dll

using System;
using System.Runtime;
using System.Security.Permissions;

namespace System.ComponentModel
{
  /// <summary>
  /// Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/> event.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
  public class PropertyChangedEventArgs : EventArgs
  {
    private readonly string propertyName;

    /// <summary>
    /// Gets the name of the property that changed.
    /// </summary>
    /// 
    /// <returns>
    /// The name of the property that changed.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string PropertyName
    {
      [__DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.propertyName;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangedEventArgs"/> class.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed. </param>
    [__DynamicallyInvokable]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public PropertyChangedEventArgs(string propertyName)
    {
      this.propertyName = propertyName;
    }
  }
}
