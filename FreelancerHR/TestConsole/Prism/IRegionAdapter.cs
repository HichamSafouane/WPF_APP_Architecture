// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IRegionAdapter
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Defines an interfaces to adapt an object and bind it to a new <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
  /// 
  /// </summary>
  public interface IRegionAdapter
  {
    /// <summary>
    /// Adapts an object and binds it to a new <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
    /// 
    /// </summary>
    /// <param name="regionTarget">The object to adapt.</param><param name="regionName">The name of the region to be created.</param>
    /// <returns>
    /// The new instance of <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> that the <paramref name="regionTarget"/> is bound to.
    /// </returns>
    IRegion Initialize(object regionTarget, string regionName);
  }
}
