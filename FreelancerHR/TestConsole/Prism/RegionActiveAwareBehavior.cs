// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.RegionActiveAwareBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Behavior that monitors a <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> object and
  ///             changes the value for the <see cref="P:Microsoft.Practices.Prism.IActiveAware.IsActive"/> property when
  ///             an object that implements <see cref="T:Microsoft.Practices.Prism.IActiveAware"/> gets added or removed
  ///             from the collection.
  /// 
  /// </summary>
  /// 
  /// <remarks>
  /// This class can also sync the active state for any scoped regions directly on the view based on the <see cref="T:Microsoft.Practices.Prism.Regions.SyncActiveStateAttribute"/>.
  ///             If you use the <see cref="M:Microsoft.Practices.Prism.Regions.Region.Add(System.Object,System.String,System.Boolean)"/> method with the createRegionManagerScope option, the scoped manager will be attached to the view.
  /// 
  /// </remarks>
  public class RegionActiveAwareBehavior : IRegionBehavior
  {
    /// <summary>
    /// Name that identifies the <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionActiveAwareBehavior"/> behavior in a collection of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/>.
    /// 
    /// </summary>
    public const string BehaviorKey = "ActiveAware";

    /// <summary>
    /// The region that this behavior is extending
    /// 
    /// </summary>
    public IRegion Region { get; set; }

    /// <summary>
    /// Attaches the behavior to the specified region
    /// 
    /// </summary>
    public void Attach()
    {
      INotifyCollectionChanged collection = this.GetCollection();
      if (collection == null)
        return;
      collection.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
    }

    /// <summary>
    /// Detaches the behavior from the <see cref="T:System.Collections.Specialized.INotifyCollectionChanged"/>.
    /// 
    /// </summary>
    public void Detach()
    {
      INotifyCollectionChanged collection = this.GetCollection();
      if (collection == null)
        return;
      collection.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
    }

    private static void InvokeOnActiveAwareElement(object item, Action<IActiveAware> invocation)
    {
      IActiveAware activeAware1 = item as IActiveAware;
      if (activeAware1 != null)
        invocation(activeAware1);
      FrameworkElement frameworkElement = item as FrameworkElement;
      if (frameworkElement == null)
        return;
      IActiveAware activeAware2 = frameworkElement.DataContext as IActiveAware;
      if (activeAware2 != null)
        invocation(activeAware2);
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        foreach (object obj in (IEnumerable) e.NewItems)
        {
          Action<IActiveAware> invocation = (Action<IActiveAware>) (activeAware => activeAware.IsActive = true);
          RegionActiveAwareBehavior.InvokeOnActiveAwareElement(obj, invocation);
          this.InvokeOnSynchronizedActiveAwareChildren(obj, invocation);
        }
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Remove)
          return;
        foreach (object obj in (IEnumerable) e.OldItems)
        {
          Action<IActiveAware> invocation = (Action<IActiveAware>) (activeAware => activeAware.IsActive = false);
          RegionActiveAwareBehavior.InvokeOnActiveAwareElement(obj, invocation);
          this.InvokeOnSynchronizedActiveAwareChildren(obj, invocation);
        }
      }
    }

    private void InvokeOnSynchronizedActiveAwareChildren(object item, Action<IActiveAware> invocation)
    {
      DependencyObject target = item as DependencyObject;
      if (target == null)
        return;
      IRegionManager regionManager = RegionManager.GetRegionManager(target);
      if (regionManager == null || regionManager == this.Region.RegionManager)
        return;
      foreach (object obj in Enumerable.Where<object>(Enumerable.SelectMany<IRegion, object>((IEnumerable<IRegion>) regionManager.Regions, (Func<IRegion, IEnumerable<object>>) (e => (IEnumerable<object>) e.ActiveViews)), new Func<object, bool>(this.ShouldSyncActiveState)))
        RegionActiveAwareBehavior.InvokeOnActiveAwareElement(obj, invocation);
    }

    private bool ShouldSyncActiveState(object view)
    {
      if (Attribute.IsDefined((MemberInfo) view.GetType(), typeof (SyncActiveStateAttribute)))
        return true;
      FrameworkElement frameworkElement = view as FrameworkElement;
      if (frameworkElement == null)
        return false;
      object dataContext = frameworkElement.DataContext;
      return dataContext != null && Attribute.IsDefined((MemberInfo) dataContext.GetType(), typeof (SyncActiveStateAttribute));
    }

    private INotifyCollectionChanged GetCollection()
    {
      return (INotifyCollectionChanged) this.Region.ActiveViews;
    }
  }
}
