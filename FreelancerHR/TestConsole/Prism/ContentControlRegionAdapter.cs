// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.ContentControlRegionAdapter
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Adapter that creates a new <see cref="T:Microsoft.Practices.Prism.Regions.SingleActiveRegion"/> and monitors its
  ///             active view to set it on the adapted <see cref="T:System.Windows.Controls.ContentControl"/>.
  /// 
  /// </summary>
  public class ContentControlRegionAdapter : RegionAdapterBase<ContentControl>
  {
    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.ContentControlRegionAdapter"/>.
    /// 
    /// </summary>
    /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
    public ContentControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
      : base(regionBehaviorFactory)
    {
    }

    /// <summary>
    /// Adapts a <see cref="T:System.Windows.Controls.ContentControl"/> to an <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
    /// 
    /// </summary>
    /// <param name="region">The new region being used.</param><param name="regionTarget">The object to adapt.</param>
    protected override void Adapt(IRegion region, ContentControl regionTarget)
    {
      if (regionTarget == null)
        throw new ArgumentNullException("regionTarget");
      if (regionTarget.Content != null || BindingOperations.GetBinding((DependencyObject) regionTarget, ContentControl.ContentProperty) != null)
        throw new InvalidOperationException(Resources.ContentControlHasContentException);
      region.ActiveViews.CollectionChanged += (NotifyCollectionChangedEventHandler) ((param0, param1) => regionTarget.Content = Enumerable.FirstOrDefault<object>((IEnumerable<object>) region.ActiveViews));
      region.Views.CollectionChanged += (NotifyCollectionChangedEventHandler) ((sender, e) =>
      {
        if (e.Action != NotifyCollectionChangedAction.Add || Enumerable.Count<object>((IEnumerable<object>) region.ActiveViews) != 0)
          return;
        region.Activate(e.NewItems[0]);
      });
    }

    /// <summary>
    /// Creates a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.SingleActiveRegion"/>.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A new instance of <see cref="T:Microsoft.Practices.Prism.Regions.SingleActiveRegion"/>.
    /// </returns>
    protected override IRegion CreateRegion()
    {
      return (IRegion) new SingleActiveRegion();
    }
  }
}
