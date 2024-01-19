// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.RegionManagerRegistrationBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.Prism.Regions;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Subscribes to a static event from the <see cref="T:Microsoft.Practices.Prism.Regions.RegionManager"/> in order to register the target <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>
  ///             in a <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> when one is available on the host control by walking up the tree and finding
  ///             a control whose <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> property is not <see langword="null"/>.
  /// 
  /// </summary>
  public class RegionManagerRegistrationBehavior : RegionBehavior, IHostAwareRegionBehavior, IRegionBehavior
  {
    /// <summary>
    /// The key of this behavior.
    /// 
    /// </summary>
    public static readonly string BehaviorKey = "RegionManagerRegistration";
    private WeakReference attachedRegionManagerWeakReference;
    private DependencyObject hostControl;

    /// <summary>
    /// Provides an abstraction on top of the RegionManager static members.
    /// 
    /// </summary>
    public IRegionManagerAccessor RegionManagerAccessor { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.DependencyObject"/> that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="T:System.Windows.DependencyObject"/> that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
    ///             This is usually a <see cref="T:System.Windows.FrameworkElement"/> that is part of the tree.
    /// </value>
    /// <exception cref="T:System.InvalidOperationException">When this member is set after the <see cref="M:Microsoft.Practices.Prism.Regions.IRegionBehavior.Attach"/> method has being called.</exception>
    public DependencyObject HostControl
    {
      get
      {
        return this.hostControl;
      }
      set
      {
        if (this.IsAttached)
          throw new InvalidOperationException(Resources.HostControlCannotBeSetAfterAttach);
        this.hostControl = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionManagerRegistrationBehavior"/>.
    /// 
    /// </summary>
    public RegionManagerRegistrationBehavior()
    {
      this.RegionManagerAccessor = (IRegionManagerAccessor) new DefaultRegionManagerAccessor();
    }

    /// <summary>
    /// When the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> has a name assigned, the behavior will start monitoring the ancestor controls in the element tree
    ///             to look for an <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> where to register the region in.
    /// 
    /// </summary>
    protected override void OnAttach()
    {
      if (string.IsNullOrEmpty(this.Region.Name))
        this.Region.PropertyChanged += new PropertyChangedEventHandler(this.Region_PropertyChanged);
      else
        this.StartMonitoringRegionManager();
    }

    private void Region_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Name") || string.IsNullOrEmpty(this.Region.Name))
        return;
      this.Region.PropertyChanged -= new PropertyChangedEventHandler(this.Region_PropertyChanged);
      this.StartMonitoringRegionManager();
    }

    private void StartMonitoringRegionManager()
    {
      this.RegionManagerAccessor.UpdatingRegions += new EventHandler(this.OnUpdatingRegions);
      this.TryRegisterRegion();
    }

    private void TryRegisterRegion()
    {
      DependencyObject hostControl = this.HostControl;
      if (!hostControl.CheckAccess())
        return;
      IRegionManager regionManager = this.FindRegionManager(hostControl);
      IRegionManager attachedRegionManager = this.GetAttachedRegionManager();
      if (regionManager != attachedRegionManager)
      {
        if (attachedRegionManager != null)
        {
          this.attachedRegionManagerWeakReference = (WeakReference) null;
          attachedRegionManager.Regions.Remove(this.Region.Name);
        }
        if (regionManager != null)
        {
          this.attachedRegionManagerWeakReference = new WeakReference((object) regionManager);
          regionManager.Regions.Add(this.Region);
        }
      }
    }

    /// <summary>
    /// This event handler gets called when a RegionManager is requering the instances of a region to be registered if they are not already.
    /// 
    /// <remarks>
    /// Although this is a public method to support Weak Delegates in Silverlight, it should not be called by the user.
    /// </remarks>
    /// 
    /// </summary>
    /// <param name="sender">The sender.</param><param name="e">The arguments.</param>
    [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "This has to be public in order to work with weak references in partial trust or Silverlight environments.")]
    public void OnUpdatingRegions(object sender, EventArgs e)
    {
      this.TryRegisterRegion();
    }

    private IRegionManager FindRegionManager(DependencyObject dependencyObject)
    {
      IRegionManager regionManager = this.RegionManagerAccessor.GetRegionManager(dependencyObject);
      if (regionManager != null)
        return regionManager;
      DependencyObject parent = LogicalTreeHelper.GetParent(dependencyObject);
      if (parent != null)
        return this.FindRegionManager(parent);
      return (IRegionManager) null;
    }

    private IRegionManager GetAttachedRegionManager()
    {
      if (this.attachedRegionManagerWeakReference != null)
        return this.attachedRegionManagerWeakReference.Target as IRegionManager;
      return (IRegionManager) null;
    }
  }
}
