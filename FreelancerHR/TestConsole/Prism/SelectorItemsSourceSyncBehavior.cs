// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.Behaviors.SelectorItemsSourceSyncBehavior
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Microsoft.Practices.Prism.Regions.Behaviors
{
  /// <summary>
  /// Defines the attached behavior that keeps the items of the <see cref="T:System.Windows.Controls.Primitives.Selector"/> host control in synchronization with the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
  /// 
  ///             This behavior also makes sure that, if you activate a view in a region, the SelectedItem is set. If you set the SelectedItem or SelectedItems (ListBox)
  ///             then this behavior will also call Activate on the selected items.
  /// 
  /// <remarks>
  /// When calling Activate on a view, you can only select a single active view at a time. By setting the SelectedItems property of a listbox, you can set
  ///             multiple views to active.
  /// 
  /// </remarks>
  /// 
  /// </summary>
  public class SelectorItemsSourceSyncBehavior : RegionBehavior, IHostAwareRegionBehavior, IRegionBehavior
  {
    /// <summary>
    /// Name that identifies the SelectorItemsSourceSyncBehavior behavior in a collection of RegionsBehaviors.
    /// 
    /// </summary>
    public static readonly string BehaviorKey = "SelectorItemsSourceSyncBehavior";
    private bool updatingActiveViewsInHostControlSelectionChanged;
    private Selector hostControl;

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.DependencyObject"/> that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="T:System.Windows.DependencyObject"/> that the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> is attached to.
    /// 
    /// </value>
    /// 
    /// <remarks>
    /// For this behavior, the host control must always be a <see cref="T:System.Windows.Controls.Primitives.Selector"/> or an inherited class.
    /// </remarks>
    public DependencyObject HostControl
    {
      get
      {
        return (DependencyObject) this.hostControl;
      }
      set
      {
        this.hostControl = value as Selector;
      }
    }

    /// <summary>
    /// Starts to monitor the <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> to keep it in synch with the items of the <see cref="P:Microsoft.Practices.Prism.Regions.Behaviors.SelectorItemsSourceSyncBehavior.HostControl"/>.
    /// 
    /// </summary>
    protected override void OnAttach()
    {
      if (this.hostControl.ItemsSource != null || BindingOperations.GetBinding((DependencyObject) this.hostControl, ItemsControl.ItemsSourceProperty) != null)
        throw new InvalidOperationException(Resources.ItemsControlHasItemsSourceException);
      this.SynchronizeItems();
      this.hostControl.SelectionChanged += new SelectionChangedEventHandler(this.HostControlSelectionChanged);
      this.Region.ActiveViews.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ActiveViews_CollectionChanged);
      this.Region.Views.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Views_CollectionChanged);
    }

    private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        int newStartingIndex = e.NewStartingIndex;
        foreach (object insertItem in (IEnumerable) e.NewItems)
          this.hostControl.Items.Insert(newStartingIndex++, insertItem);
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Remove)
          return;
        foreach (object removeItem in (IEnumerable) e.OldItems)
          this.hostControl.Items.Remove(removeItem);
      }
    }

    private void SynchronizeItems()
    {
      List<object> list = new List<object>();
      foreach (object obj in (IEnumerable) this.hostControl.Items)
        list.Add(obj);
      foreach (object newItem in (IEnumerable<object>) this.Region.Views)
        this.hostControl.Items.Add(newItem);
      foreach (object view in list)
        this.Region.Add(view);
    }

    private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this.updatingActiveViewsInHostControlSelectionChanged)
        return;
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        if (this.hostControl.SelectedItem != null && this.hostControl.SelectedItem != e.NewItems[0] && this.Region.ActiveViews.Contains(this.hostControl.SelectedItem))
          this.Region.Deactivate(this.hostControl.SelectedItem);
        this.hostControl.SelectedItem = e.NewItems[0];
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Remove || !e.OldItems.Contains(this.hostControl.SelectedItem))
          return;
        this.hostControl.SelectedItem = (object) null;
      }
    }

    private void HostControlSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      try
      {
        this.updatingActiveViewsInHostControlSelectionChanged = true;
        if (e.OriginalSource != sender)
          return;
        foreach (object view in (IEnumerable) e.RemovedItems)
        {
          if (this.Region.Views.Contains(view) && this.Region.ActiveViews.Contains(view))
            this.Region.Deactivate(view);
        }
        foreach (object view in (IEnumerable) e.AddedItems)
        {
          if (this.Region.Views.Contains(view) && !this.Region.ActiveViews.Contains(view))
            this.Region.Activate(view);
        }
      }
      finally
      {
        this.updatingActiveViewsInHostControlSelectionChanged = false;
      }
    }
  }
}
