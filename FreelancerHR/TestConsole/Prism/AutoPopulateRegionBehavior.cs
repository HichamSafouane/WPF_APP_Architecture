// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.AutoPopulateRegionBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Populates the target region with the views registered to it in the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionViewRegistry"/>.
  /// 
  /// </summary>
  public class AutoPopulateRegionBehavior : RegionBehavior
  {
    /// <summary>
    /// The key of this behavior.
    /// 
    /// </summary>
    public const string BehaviorKey = "AutoPopulate";
    private readonly IRegionViewRegistry regionViewRegistry;

    /// <summary>
    /// Creates a new instance of the AutoPopulateRegionBehavior
    ///             associated with the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionViewRegistry"/> received.
    /// 
    /// </summary>
    /// <param name="regionViewRegistry"><see cref="T:Microsoft.Practices.Prism.Regions.IRegionViewRegistry"/> that the behavior will monitor for views to populate the region.</param>
    public AutoPopulateRegionBehavior(IRegionViewRegistry regionViewRegistry)
    {
      this.regionViewRegistry = regionViewRegistry;
    }

    /// <summary>
    /// Attaches the AutoPopulateRegionBehavior to the Region.
    /// 
    /// </summary>
    protected override void OnAttach()
    {
      if (string.IsNullOrEmpty(this.Region.Name))
        this.Region.PropertyChanged += new PropertyChangedEventHandler(this.Region_PropertyChanged);
      else
        this.StartPopulatingContent();
    }

    private void StartPopulatingContent()
    {
      foreach (object viewToAdd in this.CreateViewsToAutoPopulate())
        this.AddViewIntoRegion(viewToAdd);
      this.regionViewRegistry.ContentRegistered += new EventHandler<ViewRegisteredEventArgs>(this.OnViewRegistered);
    }

    /// <summary>
    /// Returns a collection of views that will be added to the
    ///             View collection.
    /// 
    /// </summary>
    /// 
    /// <returns/>
    protected virtual IEnumerable<object> CreateViewsToAutoPopulate()
    {
      return this.regionViewRegistry.GetContents(this.Region.Name);
    }

    /// <summary>
    /// Adds a view into the views collection of this region.
    /// 
    /// </summary>
    /// <param name="viewToAdd"/>
    protected virtual void AddViewIntoRegion(object viewToAdd)
    {
      this.Region.Add(viewToAdd);
    }

    private void Region_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Name") || string.IsNullOrEmpty(this.Region.Name))
        return;
      this.Region.PropertyChanged -= new PropertyChangedEventHandler(this.Region_PropertyChanged);
      this.StartPopulatingContent();
    }

    /// <summary>
    /// Handler of the event that fires when a new viewtype is registered to the registry.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// Although this is a public method to support Weak Delegates in Silverlight, it should not be called by the user.
    /// </remarks>
    /// <param name="sender"/><param name="e"/>
    [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "This has to be public in order to work with weak references in partial trust or Silverlight environments.")]
    public virtual void OnViewRegistered(object sender, ViewRegisteredEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      if (!(e.RegionName == this.Region.Name))
        return;
      this.AddViewIntoRegion(e.GetView());
    }
  }
}
