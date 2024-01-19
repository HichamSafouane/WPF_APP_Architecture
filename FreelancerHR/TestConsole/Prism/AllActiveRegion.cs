// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.AllActiveRegion
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Region that keeps all the views in it as active. Deactivation of views is not allowed.
  /// 
  /// </summary>
  public class AllActiveRegion : Region
  {
    /// <summary>
    /// Gets a readonly view of the collection of all the active views in the region. These are all the added views.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="T:Microsoft.Practices.Prism.Regions.IViewsCollection"/> of all the active views.
    /// </value>
    public override IViewsCollection ActiveViews
    {
      get
      {
        return this.Views;
      }
    }

    /// <summary>
    /// Deactive is not valid in this Region. This method will always throw <see cref="T:System.InvalidOperationException"/>.
    /// 
    /// </summary>
    /// <param name="view">The view to deactivate.</param><exception cref="T:System.InvalidOperationException">Every time this method is called.</exception>
    public override void Deactivate(object view)
    {
      throw new InvalidOperationException(Resources.DeactiveNotPossibleException);
    }
  }
}
