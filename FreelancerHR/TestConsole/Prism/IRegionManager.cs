// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IRegionManager
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Defines an interface to manage a set of <see cref="T:Microsoft.Practices.Prism.Regions.IRegion">regions</see> and to attach regions to objects (typically controls).
  /// 
  /// </summary>
  public interface IRegionManager
  {
    /// <summary>
    /// Gets a collection of <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> that identify each region by name. You can use this collection to add or remove regions to the current region manager.
    /// 
    /// </summary>
    IRegionCollection Regions { get; }

    /// <summary>
    /// Creates a new region manager.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A new region manager that can be used as a different scope from the current region manager.
    /// </returns>
    IRegionManager CreateRegionManager();
  }
}
