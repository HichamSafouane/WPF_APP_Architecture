// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Provides a base class for region's behaviors.
  /// 
  /// </summary>
  public abstract class RegionBehavior : IRegionBehavior
  {
    private IRegion region;

    /// <summary>
    /// Behavior's attached region.
    /// 
    /// </summary>
    public IRegion Region
    {
      get
      {
        return this.region;
      }
      set
      {
        if (this.IsAttached)
          throw new InvalidOperationException(Resources.RegionBehaviorRegionCannotBeSetAfterAttach);
        this.region = value;
      }
    }

    /// <summary>
    /// Returns <see langword="true"/> if the behavior is attached to a region, <see langword="false"/> otherwise.
    /// 
    /// </summary>
    public bool IsAttached { get; private set; }

    /// <summary>
    /// Attaches the behavior to the region.
    /// 
    /// </summary>
    public void Attach()
    {
      if (this.region == null)
        throw new InvalidOperationException(Resources.RegionBehaviorAttachCannotBeCallWithNullRegion);
      this.IsAttached = true;
      this.OnAttach();
    }

    /// <summary>
    /// Override this method to perform the logic after the behavior has been attached.
    /// 
    /// </summary>
    protected abstract void OnAttach();
  }
}
