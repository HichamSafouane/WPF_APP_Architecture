// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.IHostAwareRegionBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Regions;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Defines a <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> that not allows extensible behaviors on regions which also interact
  ///             with the target element that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
  /// 
  /// </summary>
  public interface IHostAwareRegionBehavior : IRegionBehavior
  {
    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.DependencyObject"/> that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="T:System.Windows.DependencyObject"/> that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
    ///             This is usually a <see cref="T:System.Windows.FrameworkElement"/> that is part of the tree.
    /// </value>
    DependencyObject HostControl { get; set; }
  }
}
