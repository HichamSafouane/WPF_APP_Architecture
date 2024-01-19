// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.BindRegionContextToDependencyObjectBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Defines a behavior that forwards the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionContextProperty"/>
  ///             to the views in the region.
  /// 
  /// </summary>
  public class BindRegionContextToDependencyObjectBehavior : IRegionBehavior
  {
    /// <summary>
    /// The key of this behavior.
    /// 
    /// </summary>
    public const string BehaviorKey = "ContextToDependencyObject";

    /// <summary>
    /// Behavior's attached region.
    /// 
    /// </summary>
    public IRegion Region { get; set; }

    /// <summary>
    /// Attaches the behavior to the specified region.
    /// 
    /// </summary>
    public void Attach()
    {
      this.Region.Views.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Views_CollectionChanged);
      this.Region.PropertyChanged += new PropertyChangedEventHandler(this.Region_PropertyChanged);
      BindRegionContextToDependencyObjectBehavior.SetContextToViews((IEnumerable) this.Region.Views, this.Region.Context);
      this.AttachNotifyChangeEvent((IEnumerable) this.Region.Views);
    }

    private static void SetContextToViews(IEnumerable views, object context)
    {
      foreach (object obj in views)
      {
        DependencyObject view = obj as DependencyObject;
        if (view != null)
          RegionContext.GetObservableContext(view).Value = context;
      }
    }

    private void AttachNotifyChangeEvent(IEnumerable views)
    {
      foreach (object obj in views)
      {
        DependencyObject view = obj as DependencyObject;
        if (view != null)
          RegionContext.GetObservableContext(view).PropertyChanged += new PropertyChangedEventHandler(this.ViewRegionContext_OnPropertyChangedEvent);
      }
    }

    private void DetachNotifyChangeEvent(IEnumerable views)
    {
      foreach (object obj in views)
      {
        DependencyObject view = obj as DependencyObject;
        if (view != null)
          RegionContext.GetObservableContext(view).PropertyChanged -= new PropertyChangedEventHandler(this.ViewRegionContext_OnPropertyChangedEvent);
      }
    }

    private void ViewRegionContext_OnPropertyChangedEvent(object sender, PropertyChangedEventArgs args)
    {
      if (!(args.PropertyName == "Value"))
        return;
      this.Region.Context = ((ObservableObject<object>) sender).Value;
    }

    private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        BindRegionContextToDependencyObjectBehavior.SetContextToViews((IEnumerable) e.NewItems, this.Region.Context);
        this.AttachNotifyChangeEvent((IEnumerable) e.NewItems);
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Remove || this.Region.Context == null)
          return;
        this.DetachNotifyChangeEvent((IEnumerable) e.OldItems);
        BindRegionContextToDependencyObjectBehavior.SetContextToViews((IEnumerable) e.OldItems, (object) null);
      }
    }

    private void Region_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Context"))
        return;
      BindRegionContextToDependencyObjectBehavior.SetContextToViews((IEnumerable) this.Region.Views, this.Region.Context);
    }
  }
}
