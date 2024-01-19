// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionManager
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.Prism.Regions.Behaviors;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// This class is responsible for maintaining a collection of regions and attaching regions to controls.
  /// 
  /// </summary>
  /// 
  /// <remarks>
  /// This class supplies the attached properties that can be used for simple region creation from XAML.
  /// 
  /// </remarks>
  public class RegionManager : IRegionManager
  {
    private static readonly WeakDelegatesManager updatingRegionsListeners = new WeakDelegatesManager();
    /// <summary>
    /// Identifies the RegionName attached property.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// When a control has both the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/> and
    ///             <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> attached properties set to
    ///             a value different than <see langword="null"/> and there is a
    ///             <see cref="T:Microsoft.Practices.Prism.Regions.IRegionAdapter"/> mapping registered for the control, it
    ///             will create and adapt a new region for that control, and register it
    ///             in the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> with the specified region name.
    /// 
    /// </remarks>
    public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached("RegionName", typeof (string), typeof (RegionManager), new PropertyMetadata(new PropertyChangedCallback(RegionManager.OnSetRegionNameCallback)));
    private static readonly DependencyProperty ObservableRegionProperty = DependencyProperty.RegisterAttached("ObservableRegion", typeof (ObservableObject<IRegion>), typeof (RegionManager), (PropertyMetadata) null);
    /// <summary>
    /// Identifies the RegionManager attached property.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// When a control has both the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/> and
    ///             <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> attached properties set to
    ///             a value different than <see langword="null"/> and there is a
    ///             <see cref="T:Microsoft.Practices.Prism.Regions.IRegionAdapter"/> mapping registered for the control, it
    ///             will create and adapt a new region for that control, and register it
    ///             in the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> with the specified region name.
    /// 
    /// </remarks>
    public static readonly DependencyProperty RegionManagerProperty = DependencyProperty.RegisterAttached("RegionManager", typeof (IRegionManager), typeof (RegionManager), (PropertyMetadata) null);
    /// <summary>
    /// Identifies the RegionContext attached property.
    /// 
    /// </summary>
    public static readonly DependencyProperty RegionContextProperty = DependencyProperty.RegisterAttached("RegionContext", typeof (object), typeof (RegionManager), new PropertyMetadata(new PropertyChangedCallback(RegionManager.OnRegionContextChanged)));
    private readonly RegionManager.RegionCollection regionCollection;

    /// <summary>
    /// Gets a collection of <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> that identify each region by name. You can use this collection to add or remove regions to the current region manager.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="T:Microsoft.Practices.Prism.Regions.IRegionCollection"/> with all the registered regions.
    /// </value>
    public IRegionCollection Regions
    {
      get
      {
        return (IRegionCollection) this.regionCollection;
      }
    }

    /// <summary>
    /// Notification used by attached behaviors to update the region managers appropriatelly if needed to.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This event uses weak references to the event handler to prevent this static event of keeping the
    ///             target element longer than expected.
    /// </remarks>
    public static event EventHandler UpdatingRegions
    {
      add
      {
        RegionManager.updatingRegionsListeners.AddListener((Delegate) value);
      }
      remove
      {
        RegionManager.updatingRegionsListeners.RemoveListener((Delegate) value);
      }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.RegionManager"/>.
    /// 
    /// </summary>
    public RegionManager()
    {
      this.regionCollection = new RegionManager.RegionCollection((IRegionManager) this);
    }

    /// <summary>
    /// Sets the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/> attached property.
    /// 
    /// </summary>
    /// <param name="regionTarget">The object to adapt. This is typically a container (i.e a control).</param><param name="regionName">The name of the region to register.</param>
    public static void SetRegionName(DependencyObject regionTarget, string regionName)
    {
      if (regionTarget == null)
        throw new ArgumentNullException("regionTarget");
      regionTarget.SetValue(RegionManager.RegionNameProperty, (object) regionName);
    }

    /// <summary>
    /// Gets the value for the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/> attached property.
    /// 
    /// </summary>
    /// <param name="regionTarget">The object to adapt. This is typically a container (i.e a control).</param>
    /// <returns>
    /// The name of the region that should be created when
    ///             <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> is also set in this element.
    /// </returns>
    public static string GetRegionName(DependencyObject regionTarget)
    {
      if (regionTarget == null)
        throw new ArgumentNullException("regionTarget");
      return regionTarget.GetValue(RegionManager.RegionNameProperty) as string;
    }

    /// <summary>
    /// Returns an <see cref="T:Microsoft.Practices.Prism.ObservableObject`1"/> wrapper that can hold an <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>. Using this wrapper
    ///             you can detect when an <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> has been created by the <see cref="T:Microsoft.Practices.Prism.Regions.RegionAdapterBase`1"/>.
    /// 
    ///             If the <see cref="T:Microsoft.Practices.Prism.ObservableObject`1"/> wrapper does not yet exist, a new wrapper will be created. When the region
    ///             gets created and assigned to the wrapper, you can use the <see cref="E:Microsoft.Practices.Prism.ObservableObject`1.PropertyChanged"/> event
    ///             to get notified of that change.
    /// 
    /// </summary>
    /// <param name="view">The view that will host the region. </param>
    /// <returns>
    /// Wrapper that can hold an <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> value and can notify when the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> value changes.
    /// </returns>
    public static ObservableObject<IRegion> GetObservableRegion(DependencyObject view)
    {
      if (view == null)
        throw new ArgumentNullException("view");
      ObservableObject<IRegion> observableObject = view.GetValue(RegionManager.ObservableRegionProperty) as ObservableObject<IRegion>;
      if (observableObject == null)
      {
        observableObject = new ObservableObject<IRegion>();
        view.SetValue(RegionManager.ObservableRegionProperty, (object) observableObject);
      }
      return observableObject;
    }

    private static void OnSetRegionNameCallback(DependencyObject element, DependencyPropertyChangedEventArgs args)
    {
      if (RegionManager.IsInDesignMode(element))
        return;
      RegionManager.CreateRegion(element);
    }

    private static void CreateRegion(DependencyObject element)
    {
      DelayedRegionCreationBehavior instance = ServiceLocator.Current.GetInstance<DelayedRegionCreationBehavior>();
      instance.TargetElement = element;
      instance.Attach();
    }

    /// <summary>
    /// Gets the value of the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/> attached property.
    /// 
    /// </summary>
    /// <param name="target">The target element.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> attached to the <paramref name="target"/> element.
    /// </returns>
    public static IRegionManager GetRegionManager(DependencyObject target)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      return (IRegionManager) target.GetValue(RegionManager.RegionManagerProperty);
    }

    /// <summary>
    /// Sets the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> attached property.
    /// 
    /// </summary>
    /// <param name="target">The target element.</param><param name="value">The value.</param>
    public static void SetRegionManager(DependencyObject target, IRegionManager value)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      target.SetValue(RegionManager.RegionManagerProperty, (object) value);
    }

    private static void OnRegionContextChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
    {
      if (RegionContext.GetObservableContext(depObj).Value == e.NewValue)
        return;
      RegionContext.GetObservableContext(depObj).Value = e.NewValue;
    }

    /// <summary>
    /// Gets the value of the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionContextProperty"/> attached property.
    /// 
    /// </summary>
    /// <param name="target">The target element.</param>
    /// <returns>
    /// The region context to pass to the contained views.
    /// </returns>
    public static object GetRegionContext(DependencyObject target)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      return target.GetValue(RegionManager.RegionContextProperty);
    }

    /// <summary>
    /// Sets the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionContextProperty"/> attached property.
    /// 
    /// </summary>
    /// <param name="target">The target element.</param><param name="value">The value.</param>
    public static void SetRegionContext(DependencyObject target, object value)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      target.SetValue(RegionManager.RegionContextProperty, value);
    }

    /// <summary>
    /// Notifies attached behaviors to update the region managers appropriatelly if needed to.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This method is normally called internally, and there is usually no need to call this from user code.
    /// 
    /// </remarks>
    public static void UpdateRegions()
    {
      try
      {
        RegionManager.updatingRegionsListeners.Raise(null, (object) EventArgs.Empty);
      }
      catch (TargetInvocationException ex)
      {
        throw new UpdateRegionsException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.UpdateRegionException, new object[1]
        {
          (object) ExceptionExtensions.GetRootException((Exception) ex)
        }), ex.InnerException);
      }
    }

    private static bool IsInDesignMode(DependencyObject element)
    {
      return DesignerProperties.GetIsInDesignMode(element);
    }

    /// <summary>
    /// Creates a new region manager.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A new region manager that can be used as a different scope from the current region manager.
    /// </returns>
    public IRegionManager CreateRegionManager()
    {
      return (IRegionManager) new RegionManager();
    }

    private class RegionCollection : IRegionCollection, IEnumerable<IRegion>, IEnumerable, INotifyCollectionChanged
    {
      private readonly IRegionManager regionManager;
      private readonly List<IRegion> regions;

      public IRegion this[string regionName]
      {
        get
        {
          RegionManager.UpdateRegions();
          IRegion regionByName = this.GetRegionByName(regionName);
          if (regionByName == null)
            throw new KeyNotFoundException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Resources.RegionNotInRegionManagerException, new object[1]
            {
              (object) regionName
            }));
          return regionByName;
        }
      }

      public event NotifyCollectionChangedEventHandler CollectionChanged;

      public RegionCollection(IRegionManager regionManager)
      {
        this.regionManager = regionManager;
        this.regions = new List<IRegion>();
      }

      public IEnumerator<IRegion> GetEnumerator()
      {
        RegionManager.UpdateRegions();
        return (IEnumerator<IRegion>) this.regions.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this.GetEnumerator();
      }

      public void Add(IRegion region)
      {
        if (region == null)
          throw new ArgumentNullException("region");
        RegionManager.UpdateRegions();
        if (region.Name == null)
          throw new InvalidOperationException(Resources.RegionNameCannotBeEmptyException);
        if (this.GetRegionByName(region.Name) != null)
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.RegionNameExistsException, new object[1]
          {
            (object) region.Name
          }));
        this.regions.Add(region);
        region.RegionManager = this.regionManager;
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) region, 0));
      }

      public bool Remove(string regionName)
      {
        RegionManager.UpdateRegions();
        bool flag = false;
        IRegion regionByName = this.GetRegionByName(regionName);
        if (regionByName != null)
        {
          flag = true;
          this.regions.Remove(regionByName);
          regionByName.RegionManager = (IRegionManager) null;
          this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) regionByName, 0));
        }
        return flag;
      }

      public bool ContainsRegionWithName(string regionName)
      {
        RegionManager.UpdateRegions();
        return this.GetRegionByName(regionName) != null;
      }

      private IRegion GetRegionByName(string regionName)
      {
        return Enumerable.FirstOrDefault<IRegion>((IEnumerable<IRegion>) this.regions, (Func<IRegion, bool>) (r => r.Name == regionName));
      }

      private void OnCollectionChanged(NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
      {
        NotifyCollectionChangedEventHandler changedEventHandler = this.CollectionChanged;
        if (changedEventHandler == null)
          return;
        changedEventHandler((object) this, notifyCollectionChangedEventArgs);
      }
    }
  }
}
