// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.RegionMemberLifetimeBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// The RegionMemberLifetimeBehavior determines if items should be removed from the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>
  ///             when they are deactivated.
  /// 
  /// </summary>
  /// 
  /// <remarks>
  /// The <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionMemberLifetimeBehavior"/> monitors the <see cref="P:Microsoft.Practices.Prism.Regions.IRegion.ActiveViews"/>
  ///             collection to discover items that transition into a deactivated state.
  ///             <p/>
  ///             The behavior checks the removed items for either the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionMemberLifetime"/>
  ///             or the <see cref="T:Microsoft.Practices.Prism.Regions.RegionMemberLifetimeAttribute"/> (in that order) to determine if it should be kept
  ///             alive on removal.
  ///             <p/>
  ///             If the item in the collection is a <see cref="T:System.Windows.FrameworkElement"/>, it will
  ///             also check it's DataContext for <see cref="T:Microsoft.Practices.Prism.Regions.IRegionMemberLifetime"/> or the <see cref="T:Microsoft.Practices.Prism.Regions.RegionMemberLifetimeAttribute"/>.
  ///             <p/>
  ///             The order of checks are:
  /// 
  /// <list type="number">
  /// 
  /// <item>
  /// Region Item's IRegionMemberLifetime.KeepAlive value.
  /// </item>
  /// 
  /// <item>
  /// Region Item's DataContext's IRegionMemberLifetime.KeepAlive value.
  /// </item>
  /// 
  /// <item>
  /// Region Item's RegionMemberLifetimeAttribute.KeepAlive value.
  /// </item>
  /// 
  /// <item>
  /// Region Item's DataContext's RegionMemberLifetimeAttribute.KeepAlive value.
  /// </item>
  /// 
  /// </list>
  /// 
  /// </remarks>
  public class RegionMemberLifetimeBehavior : RegionBehavior
  {
    /// <summary>
    /// The key for this behavior.
    /// 
    /// </summary>
    public const string BehaviorKey = "RegionMemberLifetimeBehavior";

    /// <summary>
    /// Override this method to perform the logic after the behavior has been attached.
    /// 
    /// </summary>
    protected override void OnAttach()
    {
      this.Region.ActiveViews.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnActiveViewsChanged);
    }

    private void OnActiveViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action != NotifyCollectionChangedAction.Remove)
        return;
      foreach (object obj in (IEnumerable) e.OldItems)
      {
        if (!RegionMemberLifetimeBehavior.ShouldKeepAlive(obj))
          this.Region.Remove(obj);
      }
    }

    private static bool ShouldKeepAlive(object inactiveView)
    {
      IRegionMemberLifetime orContextLifetime = RegionMemberLifetimeBehavior.GetItemOrContextLifetime(inactiveView);
      if (orContextLifetime != null)
        return orContextLifetime.KeepAlive;
      RegionMemberLifetimeAttribute lifetimeAttribute = RegionMemberLifetimeBehavior.GetItemOrContextLifetimeAttribute(inactiveView);
      if (lifetimeAttribute != null)
        return lifetimeAttribute.KeepAlive;
      return true;
    }

    private static RegionMemberLifetimeAttribute GetItemOrContextLifetimeAttribute(object inactiveView)
    {
      RegionMemberLifetimeAttribute lifetimeAttribute = Enumerable.FirstOrDefault<RegionMemberLifetimeAttribute>(RegionMemberLifetimeBehavior.GetCustomAttributes<RegionMemberLifetimeAttribute>(inactiveView.GetType()));
      if (lifetimeAttribute != null)
        return lifetimeAttribute;
      FrameworkElement frameworkElement = inactiveView as FrameworkElement;
      if (frameworkElement != null && frameworkElement.DataContext != null)
        return Enumerable.FirstOrDefault<RegionMemberLifetimeAttribute>(RegionMemberLifetimeBehavior.GetCustomAttributes<RegionMemberLifetimeAttribute>(frameworkElement.DataContext.GetType()));
      return (RegionMemberLifetimeAttribute) null;
    }

    private static IRegionMemberLifetime GetItemOrContextLifetime(object inactiveView)
    {
      IRegionMemberLifetime regionMemberLifetime = inactiveView as IRegionMemberLifetime;
      if (regionMemberLifetime != null)
        return regionMemberLifetime;
      FrameworkElement frameworkElement = inactiveView as FrameworkElement;
      if (frameworkElement != null)
        return frameworkElement.DataContext as IRegionMemberLifetime;
      return (IRegionMemberLifetime) null;
    }

    private static IEnumerable<T> GetCustomAttributes<T>(Type type)
    {
      return Enumerable.OfType<T>((IEnumerable) type.GetCustomAttributes(typeof (T), true));
    }
  }
}
