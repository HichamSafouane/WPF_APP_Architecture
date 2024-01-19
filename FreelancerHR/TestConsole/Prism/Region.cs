// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Region
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Implementation of <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> that allows multiple active views.
  /// 
  /// </summary>
  public class Region : IRegion, INavigateAsync, INotifyPropertyChanged
  {
    private ObservableCollection<ItemMetadata> itemMetadataCollection;
    private string name;
    private ViewsCollection views;
    private ViewsCollection activeViews;
    private object context;
    private IRegionManager regionManager;
    private IRegionNavigationService regionNavigationService;
    private Comparison<object> sort;

    /// <summary>
    /// Gets the collection of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/>s that can extend the behavior of regions.
    /// 
    /// </summary>
    public IRegionBehaviorCollection Behaviors { get; private set; }

    /// <summary>
    /// Gets or sets a context for the region. This value can be used by the user to share context with the views.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The context value to be shared.
    /// </value>
    public object Context
    {
      get
      {
        return this.context;
      }
      set
      {
        if (this.context == value)
          return;
        this.context = value;
        this.OnPropertyChanged("Context");
      }
    }

    /// <summary>
    /// Gets the name of the region that uniequely identifies the region within a <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/>.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The name of the region.
    /// </value>
    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        if (this.name != null && this.name != value)
          throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.CannotChangeRegionNameException, new object[1]
          {
            (object) this.name
          }));
        if (string.IsNullOrEmpty(value))
          throw new ArgumentException(Resources.RegionNameCannotBeEmptyException);
        this.name = value;
        this.OnPropertyChanged("Name");
      }
    }

    /// <summary>
    /// Gets a readonly view of the collection of views in the region.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="T:Microsoft.Practices.Prism.Regions.IViewsCollection"/> of all the added views.
    /// </value>
    public virtual IViewsCollection Views
    {
      get
      {
        if (this.views == null)
        {
          this.views = new ViewsCollection(this.ItemMetadataCollection, (Predicate<ItemMetadata>) (x => true));
          this.views.SortComparison = this.sort;
        }
        return (IViewsCollection) this.views;
      }
    }

    /// <summary>
    /// Gets a readonly view of the collection of all the active views in the region.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="T:Microsoft.Practices.Prism.Regions.IViewsCollection"/> of all the active views.
    /// </value>
    public virtual IViewsCollection ActiveViews
    {
      get
      {
        if (this.activeViews == null)
        {
          this.activeViews = new ViewsCollection(this.ItemMetadataCollection, (Predicate<ItemMetadata>) (x => x.IsActive));
          this.activeViews.SortComparison = this.sort;
        }
        return (IViewsCollection) this.activeViews;
      }
    }

    /// <summary>
    /// Gets or sets the comparison used to sort the views.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The comparison to use.
    /// </value>
    public Comparison<object> SortComparison
    {
      get
      {
        return this.sort;
      }
      set
      {
        this.sort = value;
        if (this.activeViews != null)
          this.activeViews.SortComparison = this.sort;
        if (this.views == null)
          return;
        this.views.SortComparison = this.sort;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> that will be passed to the views when adding them to the region, unless the view is added by specifying createRegionManagerScope as <see langword="true"/>.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> where this <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is registered.
    /// </value>
    /// 
    /// <remarks>
    /// This is usually used by implementations of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> and should not be
    ///             used by the developer explicitely.
    /// </remarks>
    public IRegionManager RegionManager
    {
      get
      {
        return this.regionManager;
      }
      set
      {
        if (this.regionManager == value)
          return;
        this.regionManager = value;
        this.OnPropertyChanged("RegionManager");
      }
    }

    /// <summary>
    /// Gets the navigation service.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The navigation service.
    /// </value>
    public IRegionNavigationService NavigationService
    {
      get
      {
        if (this.regionNavigationService == null)
        {
          this.regionNavigationService = ServiceLocator.Current.GetInstance<IRegionNavigationService>();
          this.regionNavigationService.Region = (IRegion) this;
        }
        return this.regionNavigationService;
      }
      set
      {
        this.regionNavigationService = value;
      }
    }

    /// <summary>
    /// Gets the collection with all the views along with their metadata.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/> of <see cref="T:Microsoft.Practices.Prism.Regions.ItemMetadata"/> with all the added views.
    /// </value>
    protected virtual ObservableCollection<ItemMetadata> ItemMetadataCollection
    {
      get
      {
        if (this.itemMetadataCollection == null)
          this.itemMetadataCollection = new ObservableCollection<ItemMetadata>();
        return this.itemMetadataCollection;
      }
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// 
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.Region"/>.
    /// 
    /// </summary>
    public Region()
    {
      this.Behaviors = (IRegionBehaviorCollection) new RegionBehaviorCollection((IRegion) this);
      this.sort = new Comparison<object>(Region.DefaultSortComparison);
    }

    /// <overloads>Adds a new view to the region.</overloads>
    /// <summary>
    /// Adds a new view to the region.
    /// 
    /// </summary>
    /// <param name="view">The view to add.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> that is set on the view if it is a <see cref="T:System.Windows.DependencyObject"/>. It will be the current region manager when using this overload.
    /// </returns>
    public IRegionManager Add(object view)
    {
      return this.Add(view, (string) null, false);
    }

    /// <summary>
    /// Adds a new view to the region.
    /// 
    /// </summary>
    /// <param name="view">The view to add.</param><param name="viewName">The name of the view. This can be used to retrieve it later by calling <see cref="M:Microsoft.Practices.Prism.Regions.IRegion.GetView(System.String)"/>.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> that is set on the view if it is a <see cref="T:System.Windows.DependencyObject"/>. It will be the current region manager when using this overload.
    /// </returns>
    public IRegionManager Add(object view, string viewName)
    {
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.StringCannotBeNullOrEmpty, new object[1]
        {
          (object) "viewName"
        }));
      return this.Add(view, viewName, false);
    }

    /// <summary>
    /// Adds a new view to the region.
    /// 
    /// </summary>
    /// <param name="view">The view to add.</param><param name="viewName">The name of the view. This can be used to retrieve it later by calling <see cref="M:Microsoft.Practices.Prism.Regions.IRegion.GetView(System.String)"/>.</param><param name="createRegionManagerScope">When <see langword="true"/>, the added view will receive a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/>, otherwise it will use the current region manager for this region.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> that is set on the view if it is a <see cref="T:System.Windows.DependencyObject"/>.
    /// </returns>
    public virtual IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
    {
      IRegionManager scopedRegionManager = createRegionManagerScope ? this.RegionManager.CreateRegionManager() : this.RegionManager;
      this.InnerAdd(view, viewName, scopedRegionManager);
      return scopedRegionManager;
    }

    /// <summary>
    /// Removes the specified view from the region.
    /// 
    /// </summary>
    /// <param name="view">The view to remove.</param>
    public virtual void Remove(object view)
    {
      this.ItemMetadataCollection.Remove(this.GetItemMetadataOrThrow(view));
      DependencyObject target = view as DependencyObject;
      if (target == null || Microsoft.Practices.Prism.Regions.RegionManager.GetRegionManager(target) != this.RegionManager)
        return;
      target.ClearValue(Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty);
    }

    /// <summary>
    /// Marks the specified view as active.
    /// 
    /// </summary>
    /// <param name="view">The view to activate.</param>
    public virtual void Activate(object view)
    {
      ItemMetadata itemMetadataOrThrow = this.GetItemMetadataOrThrow(view);
      if (itemMetadataOrThrow.IsActive)
        return;
      itemMetadataOrThrow.IsActive = true;
    }

    /// <summary>
    /// Marks the specified view as inactive.
    /// 
    /// </summary>
    /// <param name="view">The view to deactivate.</param>
    public virtual void Deactivate(object view)
    {
      ItemMetadata itemMetadataOrThrow = this.GetItemMetadataOrThrow(view);
      if (!itemMetadataOrThrow.IsActive)
        return;
      itemMetadataOrThrow.IsActive = false;
    }

    /// <summary>
    /// Returns the view instance that was added to the region using a specific name.
    /// 
    /// </summary>
    /// <param name="viewName">The name used when adding the view to the region.</param>
    /// <returns>
    /// Returns the named view or <see langword="null"/> if the view with <paramref name="viewName"/> does not exist in the current region.
    /// </returns>
    public virtual object GetView(string viewName)
    {
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.StringCannotBeNullOrEmpty, new object[1]
        {
          (object) "viewName"
        }));
      ItemMetadata itemMetadata = Enumerable.FirstOrDefault<ItemMetadata>((IEnumerable<ItemMetadata>) this.ItemMetadataCollection, (Func<ItemMetadata, bool>) (x => x.Name == viewName));
      if (itemMetadata != null)
        return itemMetadata.Item;
      return (object) null;
    }

    /// <summary>
    /// Initiates navigation to the specified target.
    /// 
    /// </summary>
    /// <param name="target">The target.</param><param name="navigationCallback">A callback to execute when the navigation request is completed.</param>
    public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback)
    {
      this.RequestNavigate(target, navigationCallback, (NavigationParameters) null);
    }

    /// <summary>
    /// Initiates navigation to the specified target.
    /// 
    /// </summary>
    /// <param name="target">The target.</param><param name="navigationCallback">A callback to execute when the navigation request is completed.</param><param name="navigationParameters">The navigation parameters specific to the navigation request.</param>
    public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
    {
      this.NavigationService.RequestNavigate(target, navigationCallback, navigationParameters);
    }

    private void InnerAdd(object view, string viewName, IRegionManager scopedRegionManager)
    {
      if (Enumerable.FirstOrDefault<ItemMetadata>((IEnumerable<ItemMetadata>) this.ItemMetadataCollection, (Func<ItemMetadata, bool>) (x => x.Item == view)) != null)
        throw new InvalidOperationException(Resources.RegionViewExistsException);
      ItemMetadata itemMetadata = new ItemMetadata(view);
      if (!string.IsNullOrEmpty(viewName))
      {
        if (Enumerable.FirstOrDefault<ItemMetadata>((IEnumerable<ItemMetadata>) this.ItemMetadataCollection, (Func<ItemMetadata, bool>) (x => x.Name == viewName)) != null)
          throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.RegionViewNameExistsException, new object[1]
          {
            (object) viewName
          }));
        itemMetadata.Name = viewName;
      }
      DependencyObject target = view as DependencyObject;
      if (target != null)
        Microsoft.Practices.Prism.Regions.RegionManager.SetRegionManager(target, scopedRegionManager);
      this.ItemMetadataCollection.Add(itemMetadata);
    }

    private ItemMetadata GetItemMetadataOrThrow(object view)
    {
      if (view == null)
        throw new ArgumentNullException("view");
      ItemMetadata itemMetadata = Enumerable.FirstOrDefault<ItemMetadata>((IEnumerable<ItemMetadata>) this.ItemMetadataCollection, (Func<ItemMetadata, bool>) (x => x.Item == view));
      if (itemMetadata == null)
        throw new ArgumentException(Resources.ViewNotInRegionException, "view");
      return itemMetadata;
    }

    private void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// The default sort algorithm.
    /// 
    /// </summary>
    /// <param name="x">The first view to compare.</param><param name="y">The second view to compare.</param>
    /// <returns/>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
    public static int DefaultSortComparison(object x, object y)
    {
      if (x == null)
        return y == null ? 0 : -1;
      if (y == null)
        return 1;
      return Region.ViewSortHintAttributeComparison(Enumerable.FirstOrDefault<object>((IEnumerable<object>) x.GetType().GetCustomAttributes(typeof (ViewSortHintAttribute), true)) as ViewSortHintAttribute, Enumerable.FirstOrDefault<object>((IEnumerable<object>) y.GetType().GetCustomAttributes(typeof (ViewSortHintAttribute), true)) as ViewSortHintAttribute);
    }

    private static int ViewSortHintAttributeComparison(ViewSortHintAttribute x, ViewSortHintAttribute y)
    {
      if (x == null)
        return y == null ? 0 : -1;
      if (y == null)
        return 1;
      return string.Compare(x.Hint, y.Hint, StringComparison.Ordinal);
    }
  }
}
