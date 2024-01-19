// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.ClearChildViewsRegionBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Behavior that removes the RegionManager attached property of all the views in a region once the RegionManager property of a region becomes null.
  ///             This is useful when removing views with nested regions, to ensure these nested regions get removed from the RegionManager as well.
  /// 
  /// <remarks>
  /// This behavior does not apply by default.
  ///             In order to activate it, the ClearChildViews attached property must be set to True in the view containing the affected child regions.
  /// 
  /// </remarks>
  /// 
  /// </summary>
  public class ClearChildViewsRegionBehavior : RegionBehavior
  {
    /// <summary>
    /// This attached property can be defined on a view to indicate that regions defined in it must be removed from the region manager when the parent view gets removed from a region.
    /// 
    /// </summary>
    public static readonly DependencyProperty ClearChildViewsProperty = DependencyProperty.RegisterAttached("ClearChildViews", typeof (bool), typeof (ClearChildViewsRegionBehavior), new PropertyMetadata((object) false));
    /// <summary>
    /// The behavior key.
    /// 
    /// </summary>
    public const string BehaviorKey = "ClearChildViews";

    /// <summary>
    /// Gets the ClearChildViews attached property from a DependencyObject.
    /// 
    /// </summary>
    /// <param name="target">The object from which to get the value.</param>
    /// <returns>
    /// The value of the ClearChildViews attached property in the target specified.
    /// </returns>
    public static bool GetClearChildViews(DependencyObject target)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      return (bool) target.GetValue(ClearChildViewsRegionBehavior.ClearChildViewsProperty);
    }

    /// <summary>
    /// Sets the ClearChildViews attached property in a DependencyObject.
    /// 
    /// </summary>
    /// <param name="target">The object in which to set the value.</param><param name="value">The value of to set in the target object's ClearChildViews attached property.</param>
    public static void SetClearChildViews(DependencyObject target, bool value)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      target.SetValue(ClearChildViewsRegionBehavior.ClearChildViewsProperty, (object) (bool) (value ? 1 : 0));
    }

    /// <summary>
    /// Subscribes to the <see cref="T:Microsoft.Practices.Prism.Regions.Region"/>'s PropertyChanged method to monitor its RegionManager property.
    /// 
    /// </summary>
    protected override void OnAttach()
    {
      this.Region.PropertyChanged += new PropertyChangedEventHandler(this.Region_PropertyChanged);
    }

    private static void ClearChildViews(IRegion region)
    {
      foreach (object obj in (IEnumerable<object>) region.Views)
      {
        DependencyObject target = obj as DependencyObject;
        if (target != null && ClearChildViewsRegionBehavior.GetClearChildViews(target))
          target.ClearValue(RegionManager.RegionManagerProperty);
      }
    }

    private void Region_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "RegionManager") || this.Region.RegionManager != null)
        return;
      ClearChildViewsRegionBehavior.ClearChildViews(this.Region);
    }
  }
}
