// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionContext
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using System;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Class that holds methods to Set and Get the RegionContext from a DependencyObject.
  /// 
  ///             RegionContext allows sharing of contextual information between the view that's hosting a <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>
  ///             and any views that are inside the Region.
  /// 
  /// </summary>
  public static class RegionContext
  {
    private static readonly DependencyProperty ObservableRegionContextProperty = DependencyProperty.RegisterAttached("ObservableRegionContext", typeof (ObservableObject<object>), typeof (RegionContext), (PropertyMetadata) null);

    /// <summary>
    /// Returns an <see cref="T:Microsoft.Practices.Prism.ObservableObject`1"/> wrapper around the RegionContext value. The RegionContext
    ///             will be set on any views (dependency objects) that are inside the <see cref="P:Microsoft.Practices.Prism.Regions.IRegion.Views"/> collection by
    ///             the <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.BindRegionContextToDependencyObjectBehavior"/> Behavior.
    ///             The RegionContext will also be set to the control that hosts the Region, by the <see cref="T:Microsoft.Practices.Prism.Regions.Behaviors.SyncRegionContextWithHostBehavior"/> Behavior.
    /// 
    ///             If the <see cref="T:Microsoft.Practices.Prism.ObservableObject`1"/> wrapper does not already exist, an empty one will be created. This way, an observer can
    ///             notify when the value is set for the first time.
    /// 
    /// </summary>
    /// <param name="view">Any view that hold the RegionContext value. </param>
    /// <returns>
    /// Wrapper around the Regioncontext value.
    /// </returns>
    public static ObservableObject<object> GetObservableContext(DependencyObject view)
    {
      if (view == null)
        throw new ArgumentNullException("view");
      ObservableObject<object> observableObject = view.GetValue(RegionContext.ObservableRegionContextProperty) as ObservableObject<object>;
      if (observableObject == null)
      {
        observableObject = new ObservableObject<object>();
        view.SetValue(RegionContext.ObservableRegionContextProperty, (object) observableObject);
      }
      return observableObject;
    }
  }
}
