// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IRegionMemberLifetime
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// When implemented, allows an instance placed in a <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>
  ///             that uses a <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionMemberLifetimeBehavior"/> to indicate
  ///             it should be removed when it transitions from an activated to deactived state.
  /// 
  /// </summary>
  public interface IRegionMemberLifetime
  {
    /// <summary>
    /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
    /// 
    /// </summary>
    bool KeepAlive { get; }
  }
}
