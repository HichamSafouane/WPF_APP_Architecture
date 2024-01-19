// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.DelayedRegionCreationBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Behavior that creates a new <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>, when the control that will host the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> (see <see cref="P:Microsoft.Practices.Prism.Regions.Behaviors.DelayedRegionCreationBehavior.TargetElement"/>)
  ///             is added to the VisualTree. This behavior will use the <see cref="T:Microsoft.Practices.Prism.Regions.RegionAdapterMappings"/> class to find the right type of adapter to create
  ///             the region. After the region is created, this behavior will detach.
  /// 
  /// </summary>
  /// 
  /// <remarks>
  /// Attached property value inheritance is not available in Silverlight, so the current approach walks up the visual tree when requesting a region from a region manager.
  ///             The <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.RegionManagerRegistrationBehavior"/> is now responsible for walking up the Tree.
  /// 
  /// </remarks>
  public class DelayedRegionCreationBehavior
  {
    private readonly RegionAdapterMappings regionAdapterMappings;
    private WeakReference elementWeakReference;
    private bool regionCreated;

    /// <summary>
    /// Sets a class that interfaces between the <see cref="T:Microsoft.Practices.Prism.Regions.RegionManager"/> 's static properties/events and this behavior,
    ///             so this behavior can be tested in isolation.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The region manager accessor.
    /// </value>
    public IRegionManagerAccessor RegionManagerAccessor { get; set; }

    /// <summary>
    /// The element that will host the Region.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The target element.
    /// </value>
    public DependencyObject TargetElement
    {
      get
      {
        return this.elementWeakReference != null ? this.elementWeakReference.Target as DependencyObject : (DependencyObject) null;
      }
      set
      {
        this.elementWeakReference = new WeakReference((object) value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.DelayedRegionCreationBehavior"/> class.
    /// 
    /// </summary>
    /// <param name="regionAdapterMappings">The region adapter mappings, that are used to find the correct adapter for
    ///             a given controltype. The controltype is determined by the <see name="TargetElement"/> value.
    ///             </param>
    public DelayedRegionCreationBehavior(RegionAdapterMappings regionAdapterMappings)
    {
      this.regionAdapterMappings = regionAdapterMappings;
      this.RegionManagerAccessor = (IRegionManagerAccessor) new DefaultRegionManagerAccessor();
    }

    /// <summary>
    /// Start monitoring the <see cref="T:Microsoft.Practices.Prism.Regions.RegionManager"/> and the <see cref="P:Microsoft.Practices.Prism.Regions.Behaviors.DelayedRegionCreationBehavior.TargetElement"/> to detect when the <see cref="P:Microsoft.Practices.Prism.Regions.Behaviors.DelayedRegionCreationBehavior.TargetElement"/> becomes
    ///             part of the Visual Tree. When that happens, the Region will be created and the behavior will <see cref="M:Microsoft.Practices.Prism.Regions.Behaviors.DelayedRegionCreationBehavior.Detach"/>.
    /// 
    /// </summary>
    public void Attach()
    {
      this.RegionManagerAccessor.UpdatingRegions += new EventHandler(this.OnUpdatingRegions);
      this.WireUpTargetElement();
    }

    /// <summary>
    /// Stop monitoring the <see cref="T:Microsoft.Practices.Prism.Regions.RegionManager"/> and the  <see cref="P:Microsoft.Practices.Prism.Regions.Behaviors.DelayedRegionCreationBehavior.TargetElement"/>, so that this behavior can be garbage collected.
    /// 
    /// </summary>
    public void Detach()
    {
      this.RegionManagerAccessor.UpdatingRegions -= new EventHandler(this.OnUpdatingRegions);
      this.UnWireTargetElement();
    }

    /// <summary>
    /// Called when the <see cref="T:Microsoft.Practices.Prism.Regions.RegionManager"/> is updating it's <see cref="P:Microsoft.Practices.Prism.Regions.RegionManager.Regions"/> collection.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This method has to be public, because it has to be callable using weak references in silverlight and other partial trust environments.
    /// 
    /// </remarks>
    /// <param name="sender">The <see cref="T:Microsoft.Practices.Prism.Regions.RegionManager"/>. </param><param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "This has to be public in order to work with weak references in partial trust or Silverlight environments.")]
    public void OnUpdatingRegions(object sender, EventArgs e)
    {
      this.TryCreateRegion();
    }

    private void TryCreateRegion()
    {
      DependencyObject targetElement = this.TargetElement;
      if (targetElement == null)
      {
        this.Detach();
      }
      else
      {
        if (!targetElement.CheckAccess())
          return;
        this.Detach();
        if (!this.regionCreated)
        {
          string regionName = this.RegionManagerAccessor.GetRegionName(targetElement);
          this.CreateRegion(targetElement, regionName);
          this.regionCreated = true;
        }
      }
    }

    /// <summary>
    /// Method that will create the region, by calling the right <see cref="T:Microsoft.Practices.Prism.Regions.IRegionAdapter"/>.
    /// 
    /// </summary>
    /// <param name="targetElement">The target element that will host the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.</param><param name="regionName">Name of the region.</param>
    /// <returns>
    /// The created <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>
    /// </returns>
    protected virtual IRegion CreateRegion(DependencyObject targetElement, string regionName)
    {
      if (targetElement == null)
        throw new ArgumentNullException("targetElement");
      try
      {
        return this.regionAdapterMappings.GetMapping(targetElement.GetType()).Initialize((object) targetElement, regionName);
      }
      catch (Exception ex)
      {
        throw new RegionCreationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.RegionCreationException, new object[2]
        {
          (object) regionName,
          (object) ex
        }), ex);
      }
    }

    private void ElementLoaded(object sender, RoutedEventArgs e)
    {
      this.UnWireTargetElement();
      this.TryCreateRegion();
    }

    private void WireUpTargetElement()
    {
      FrameworkElement frameworkElement = this.TargetElement as FrameworkElement;
      if (frameworkElement == null)
        return;
      frameworkElement.Loaded += new RoutedEventHandler(this.ElementLoaded);
    }

    private void UnWireTargetElement()
    {
      FrameworkElement frameworkElement = this.TargetElement as FrameworkElement;
      if (frameworkElement == null)
        return;
      frameworkElement.Loaded -= new RoutedEventHandler(this.ElementLoaded);
    }
  }
}
