// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.ItemsControlRegionAdapter
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Adapter that creates a new <see cref="T:Microsoft.Practices.Prism.Regions.AllActiveRegion"/> and binds all
  ///             the views to the adapted <see cref="T:System.Windows.Controls.ItemsControl"/>.
  /// 
  /// </summary>
  public class ItemsControlRegionAdapter : RegionAdapterBase<ItemsControl>
  {
    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.ItemsControlRegionAdapter"/>.
    /// 
    /// </summary>
    /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
    public ItemsControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
      : base(regionBehaviorFactory)
    {
    }

    /// <summary>
    /// Adapts an <see cref="T:System.Windows.Controls.ItemsControl"/> to an <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
    /// 
    /// </summary>
    /// <param name="region">The new region being used.</param><param name="regionTarget">The object to adapt.</param>
    protected override void Adapt(IRegion region, ItemsControl regionTarget)
    {
      if (region == null)
        throw new ArgumentNullException("region");
      if (regionTarget == null)
        throw new ArgumentNullException("regionTarget");
      if (regionTarget.ItemsSource != null || BindingOperations.GetBinding((DependencyObject) regionTarget, ItemsControl.ItemsSourceProperty) != null)
        throw new InvalidOperationException(Resources.ItemsControlHasItemsSourceException);
      if (regionTarget.Items.Count > 0)
      {
        foreach (object view in (IEnumerable) regionTarget.Items)
          region.Add(view);
        regionTarget.Items.Clear();
      }
      regionTarget.ItemsSource = (IEnumerable) region.Views;
    }

    /// <summary>
    /// Creates a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.AllActiveRegion"/>.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A new instance of <see cref="T:Microsoft.Practices.Prism.Regions.AllActiveRegion"/>.
    /// </returns>
    protected override IRegion CreateRegion()
    {
      return (IRegion) new AllActiveRegion();
    }
  }
}
