// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IRegionBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Interface for allowing extensible behavior on regions.
  /// 
  /// </summary>
  public interface IRegionBehavior
  {
    /// <summary>
    /// The region that this behavior is extending.
    /// 
    /// </summary>
    IRegion Region { get; set; }

    /// <summary>
    /// Attaches the behavior to the specified region.
    /// 
    /// </summary>
    void Attach();
  }
}
