// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.SelectorRegionAdapter
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Regions.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Adapter that creates a new <see cref="T:Microsoft.Practices.Prism.Regions.Region"/> and binds all
  ///             the views to the adapted <see cref="T:System.Windows.Controls.Primitives.Selector"/>.
  ///             It also keeps the <see cref="P:Microsoft.Practices.Prism.Regions.IRegion.ActiveViews"/> and the selected items
  ///             of the <see cref="T:System.Windows.Controls.Primitives.Selector"/> in sync.
  /// 
  /// </summary>
  public class SelectorRegionAdapter : RegionAdapterBase<Selector>
  {
    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.SelectorRegionAdapter"/>.
    /// 
    /// </summary>
    /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
    public SelectorRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
      : base(regionBehaviorFactory)
    {
    }

    /// <summary>
    /// Adapts an <see cref="T:System.Windows.Controls.Primitives.Selector"/> to an <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
    /// 
    /// </summary>
    /// <param name="region">The new region being used.</param><param name="regionTarget">The object to adapt.</param>
    protected override void Adapt(IRegion region, Selector regionTarget)
    {
    }

    /// <summary>
    /// Attach new behaviors.
    /// 
    /// </summary>
    /// <param name="region">The region being used.</param><param name="regionTarget">The object to adapt.</param>
    /// <remarks>
    /// This class attaches the base behaviors and also listens for changes in the
    ///             activity of the region or the control selection and keeps the in sync.
    /// 
    /// </remarks>
    protected override void AttachBehaviors(IRegion region, Selector regionTarget)
    {
      if (region == null)
        throw new ArgumentNullException("region");
      region.Behaviors.Add(SelectorItemsSourceSyncBehavior.BehaviorKey, (IRegionBehavior) new SelectorItemsSourceSyncBehavior()
      {
        HostControl = (DependencyObject) regionTarget
      });
      this.AttachBehaviors(region, regionTarget);
    }

    /// <summary>
    /// Creates a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.Region"/>.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A new instance of <see cref="T:Microsoft.Practices.Prism.Regions.Region"/>.
    /// </returns>
    protected override IRegion CreateRegion()
    {
      return (IRegion) new Region();
    }
  }
}
