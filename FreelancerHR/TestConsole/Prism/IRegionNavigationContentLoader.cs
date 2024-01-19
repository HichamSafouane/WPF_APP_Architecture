// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IRegionNavigationContentLoader
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Identifies the view in a region that is the target of a navigation request.
  /// 
  /// </summary>
  public interface IRegionNavigationContentLoader
  {
    /// <summary>
    /// Gets the content to which the navigation request represented by <paramref name="navigationContext"/> applies.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// If none of the items in the region match the target of the navigation request, a new item
    ///             will be created and added to the region.
    /// 
    /// </remarks>
    /// <param name="region">The region.</param><param name="navigationContext">The context representing the navigation request.</param>
    /// <returns>
    /// The item to be the target of the navigation request.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">when a new item cannot be created for the navigation request.</exception>
    object LoadContent(IRegion region, NavigationContext navigationContext);
  }
}
