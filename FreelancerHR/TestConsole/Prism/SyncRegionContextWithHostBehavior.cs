// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.SyncRegionContextWithHostBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.Prism.Regions;
using System;
using System.ComponentModel;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Behavior that synchronizes the <see cref="P:Microsoft.Practices.Prism.Regions.IRegion.Context"/> property of a <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> with
  ///             the control that hosts the Region. It does this by setting the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionContextProperty"/>
  ///             Dependency Property on the host control.
  /// 
  ///             This behavior allows the usage of two way databinding of the RegionContext from XAML.
  /// 
  /// </summary>
  public class SyncRegionContextWithHostBehavior : RegionBehavior, IHostAwareRegionBehavior, IRegionBehavior
  {
    /// <summary>
    /// Name that identifies the SyncRegionContextWithHostBehavior behavior in a collection of RegionsBehaviors.
    /// 
    /// </summary>
    public static readonly string BehaviorKey = "SyncRegionContextWithHost";
    private const string RegionContextPropertyName = "Context";
    private DependencyObject hostControl;

    private ObservableObject<object> HostControlRegionContext
    {
      get
      {
        return RegionContext.GetObservableContext(this.hostControl);
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.DependencyObject"/> that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="T:System.Windows.DependencyObject"/> that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
    ///             This is usually a <see cref="T:System.Windows.FrameworkElement"/> that is part of the tree.
    /// 
    /// </value>
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
    /// Override this method to perform the logic after the behavior has been attached.
    /// 
    /// </summary>
    protected override void OnAttach()
    {
      if (this.HostControl == null)
        return;
      this.SynchronizeRegionContext();
      this.HostControlRegionContext.PropertyChanged += new PropertyChangedEventHandler(this.RegionContextObservableObject_PropertyChanged);
      this.Region.PropertyChanged += new PropertyChangedEventHandler(this.Region_PropertyChanged);
    }

    private void Region_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Context") || RegionManager.GetRegionContext(this.HostControl) == this.Region.Context)
        return;
      RegionManager.SetRegionContext(this.hostControl, this.Region.Context);
    }

    private void RegionContextObservableObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Value"))
        return;
      this.SynchronizeRegionContext();
    }

    private void SynchronizeRegionContext()
    {
      if (this.Region.Context != this.HostControlRegionContext.Value)
        this.Region.Context = this.HostControlRegionContext.Value;
      if (RegionManager.GetRegionContext(this.HostControl) == this.HostControlRegionContext.Value)
        return;
      RegionManager.SetRegionContext(this.HostControl, this.HostControlRegionContext.Value);
    }
  }
}
