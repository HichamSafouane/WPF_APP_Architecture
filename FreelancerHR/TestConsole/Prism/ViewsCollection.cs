// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.ViewsCollection
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Implementation of <see cref="T:Microsoft.Practices.Prism.Regions.IViewsCollection"/> that takes an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1"/> of <see cref="T:Microsoft.Practices.Prism.Regions.ItemMetadata"/>
  ///             and filters it to display an <see cref="T:System.Collections.Specialized.INotifyCollectionChanged"/> collection of
  ///             <see cref="T:System.Object"/> elements (the items which the <see cref="T:Microsoft.Practices.Prism.Regions.ItemMetadata"/> wraps).
  /// 
  /// </summary>
  public class ViewsCollection : IViewsCollection, IEnumerable<object>, IEnumerable, INotifyCollectionChanged
  {
    private readonly Dictionary<ItemMetadata, ViewsCollection.MonitorInfo> monitoredItems = new Dictionary<ItemMetadata, ViewsCollection.MonitorInfo>();
    private List<object> filteredItems = new List<object>();
    private readonly ObservableCollection<ItemMetadata> subjectCollection;
    private readonly Predicate<ItemMetadata> filter;
    private Comparison<object> sort;

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
        if (!(this.sort != value))
          return;
        this.sort = value;
        this.UpdateFilteredItemsList();
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      }
    }

    private IEnumerable<object> FilteredItems
    {
      get
      {
        return (IEnumerable<object>) this.filteredItems;
      }
    }

    /// <summary>
    /// Occurs when the collection changes.
    /// 
    /// </summary>
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.ViewsCollection"/> class.
    /// 
    /// </summary>
    /// <param name="list">The list to wrap and filter.</param><param name="filter">A predicate to filter the <paramref name="list"/> collection.</param>
    public ViewsCollection(ObservableCollection<ItemMetadata> list, Predicate<ItemMetadata> filter)
    {
      this.subjectCollection = list;
      this.filter = filter;
      this.MonitorAllMetadataItems();
      this.subjectCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SourceCollectionChanged);
      this.UpdateFilteredItemsList();
    }

    /// <summary>
    /// Determines whether the collection contains a specific value.
    /// 
    /// </summary>
    /// <param name="value">The object to locate in the collection.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is found in the collection; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(object value)
    {
      return Enumerable.Contains<object>(this.FilteredItems, value);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
    /// 
    /// </returns>
    public IEnumerator<object> GetEnumerator()
    {
      return this.FilteredItems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    /// <summary>
    /// Used to invoked the <see cref="E:Microsoft.Practices.Prism.Regions.ViewsCollection.CollectionChanged"/> event.
    /// 
    /// </summary>
    /// <param name="e"/>
    private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      NotifyCollectionChangedEventHandler changedEventHandler = this.CollectionChanged;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, e);
    }

    private void NotifyReset()
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    /// <summary>
    /// Removes all monitoring of underlying MetadataItems and re-adds them.
    /// 
    /// </summary>
    private void ResetAllMonitors()
    {
      this.RemoveAllMetadataMonitors();
      this.MonitorAllMetadataItems();
    }

    /// <summary>
    /// Adds all underlying MetadataItems to the list from the subjectCollection
    /// 
    /// </summary>
    private void MonitorAllMetadataItems()
    {
      foreach (ItemMetadata itemMetadata in (Collection<ItemMetadata>) this.subjectCollection)
        this.AddMetadataMonitor(itemMetadata, this.filter(itemMetadata));
    }

    /// <summary>
    /// Removes all monitored items from our monitoring list.
    /// 
    /// </summary>
    private void RemoveAllMetadataMonitors()
    {
      foreach (KeyValuePair<ItemMetadata, ViewsCollection.MonitorInfo> keyValuePair in this.monitoredItems)
        keyValuePair.Key.MetadataChanged -= new EventHandler(this.OnItemMetadataChanged);
      this.monitoredItems.Clear();
    }

    /// <summary>
    /// Adds handler to monitor the MetadatItem and adds it to our monitoring list.
    /// 
    /// </summary>
    /// <param name="itemMetadata"/><param name="isInList"/>
    private void AddMetadataMonitor(ItemMetadata itemMetadata, bool isInList)
    {
      itemMetadata.MetadataChanged += new EventHandler(this.OnItemMetadataChanged);
      this.monitoredItems.Add(itemMetadata, new ViewsCollection.MonitorInfo()
      {
        IsInList = isInList
      });
    }

    /// <summary>
    /// Unhooks from the MetadataItem change event and removes from our monitoring list.
    /// 
    /// </summary>
    /// <param name="itemMetadata"/>
    private void RemoveMetadataMonitor(ItemMetadata itemMetadata)
    {
      itemMetadata.MetadataChanged -= new EventHandler(this.OnItemMetadataChanged);
      this.monitoredItems.Remove(itemMetadata);
    }

    /// <summary>
    /// Invoked when any of the underlying ItemMetadata items we're monitoring changes.
    /// 
    /// </summary>
    /// <param name="sender"/><param name="e"/>
    private void OnItemMetadataChanged(object sender, EventArgs e)
    {
      ItemMetadata key = (ItemMetadata) sender;
      ViewsCollection.MonitorInfo monitorInfo;
      if (!this.monitoredItems.TryGetValue(key, out monitorInfo))
        return;
      if (this.filter(key))
      {
        if (monitorInfo.IsInList)
          return;
        monitorInfo.IsInList = true;
        this.UpdateFilteredItemsList();
        this.NotifyAdd(key.Item);
      }
      else
      {
        monitorInfo.IsInList = false;
        this.RemoveFromFilteredList(key.Item);
      }
    }

    /// <summary>
    /// The event handler due to changes in the underlying collection.
    /// 
    /// </summary>
    /// <param name="sender"/><param name="e"/>
    private void SourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          this.UpdateFilteredItemsList();
          foreach (ItemMetadata itemMetadata in (IEnumerable) e.NewItems)
          {
            bool isInList = this.filter(itemMetadata);
            this.AddMetadataMonitor(itemMetadata, isInList);
            if (isInList)
              this.NotifyAdd(itemMetadata.Item);
          }
          if (this.sort == null)
            break;
          this.NotifyReset();
          break;
        case NotifyCollectionChangedAction.Remove:
          IEnumerator enumerator = e.OldItems.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              ItemMetadata itemMetadata = (ItemMetadata) enumerator.Current;
              this.RemoveMetadataMonitor(itemMetadata);
              if (this.filter(itemMetadata))
                this.RemoveFromFilteredList(itemMetadata.Item);
            }
            break;
          }
          finally
          {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
              disposable.Dispose();
          }
        default:
          this.ResetAllMonitors();
          this.UpdateFilteredItemsList();
          this.NotifyReset();
          break;
      }
    }

    private void NotifyAdd(object item)
    {
      int newStartingIndex = this.filteredItems.IndexOf(item);
      this.NotifyAdd((IList) new object[1]
      {
        item
      }, newStartingIndex);
    }

    private void RemoveFromFilteredList(object item)
    {
      int originalIndex = this.filteredItems.IndexOf(item);
      this.UpdateFilteredItemsList();
      this.NotifyRemove((IList) new object[1]
      {
        item
      }, originalIndex);
    }

    private void UpdateFilteredItemsList()
    {
      this.filteredItems = Enumerable.ToList<object>((IEnumerable<object>) Enumerable.OrderBy<object, object>(Enumerable.Select<ItemMetadata, object>(Enumerable.Where<ItemMetadata>((IEnumerable<ItemMetadata>) this.subjectCollection, (Func<ItemMetadata, bool>) (i => this.filter(i))), (Func<ItemMetadata, object>) (i => i.Item)), (Func<object, object>) (o => o), (IComparer<object>) new ViewsCollection.RegionItemComparer(this.SortComparison)));
    }

    private void NotifyAdd(IList items, int newStartingIndex)
    {
      if (items.Count <= 0)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items, newStartingIndex));
    }

    private void NotifyRemove(IList items, int originalIndex)
    {
      if (items.Count <= 0)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items, originalIndex));
    }

    private class MonitorInfo
    {
      public bool IsInList { get; set; }
    }

    private class RegionItemComparer : Comparer<object>
    {
      private readonly Comparison<object> comparer;

      public RegionItemComparer(Comparison<object> comparer)
      {
        this.comparer = comparer;
      }

      public override int Compare(object x, object y)
      {
        if (this.comparer == null)
          return 0;
        return this.comparer(x, y);
      }
    }
  }
}
