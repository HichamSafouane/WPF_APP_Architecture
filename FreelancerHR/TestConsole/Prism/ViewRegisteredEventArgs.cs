// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.ViewRegisteredEventArgs
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Argument class used by the <see cref="E:Microsoft.Practices.Prism.Regions.IRegionViewRegistry.ContentRegistered"/> event when a new content is registered.
  /// 
  /// </summary>
  public class ViewRegisteredEventArgs : EventArgs
  {
    /// <summary>
    /// Gets the region name to which the content was registered.
    /// 
    /// </summary>
    public string RegionName { get; private set; }

    /// <summary>
    /// Gets the content which was registered.
    /// 
    /// </summary>
    public Func<object> GetView { get; private set; }

    /// <summary>
    /// Initializes the ViewRegisteredEventArgs class.
    /// 
    /// </summary>
    /// <param name="regionName">The region name to which the content was registered.</param><param name="getViewDelegate">The content which was registered.</param>
    public ViewRegisteredEventArgs(string regionName, Func<object> getViewDelegate)
    {
      this.GetView = getViewDelegate;
      this.RegionName = regionName;
    }
  }
}
