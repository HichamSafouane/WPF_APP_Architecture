﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.ItemMetadata
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Defines a class that wraps an item and adds metadata for it.
  /// 
  /// </summary>
  public class ItemMetadata : DependencyObject
  {
    /// <summary>
    /// The name of the wrapped item.
    /// 
    /// </summary>
    public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof (string), typeof (ItemMetadata), (PropertyMetadata) null);
    /// <summary>
    /// Value indicating whether the wrapped item is considered active.
    /// 
    /// </summary>
    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof (bool), typeof (ItemMetadata), new PropertyMetadata(new PropertyChangedCallback(ItemMetadata.DependencyPropertyChanged)));

    /// <summary>
    /// Gets the wrapped item.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The wrapped item.
    /// </value>
    public object Item { get; private set; }

    /// <summary>
    /// Gets or sets a name for the wrapped item.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The name of the wrapped item.
    /// </value>
    public string Name
    {
      get
      {
        return (string) this.GetValue(ItemMetadata.NameProperty);
      }
      set
      {
        this.SetValue(ItemMetadata.NameProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the wrapped item is considered active.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true"/> if the item should be considered active; otherwise <see langword="false"/>.
    /// </value>
    public bool IsActive
    {
      get
      {
        return (bool) this.GetValue(ItemMetadata.IsActiveProperty);
      }
      set
      {
        this.SetValue(ItemMetadata.IsActiveProperty, (object) (bool) (value ? 1 : 0));
      }
    }

    /// <summary>
    /// Occurs when metadata on the item changes.
    /// 
    /// </summary>
    public event EventHandler MetadataChanged;

    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.ItemMetadata"/>.
    /// 
    /// </summary>
    /// <param name="item">The item to wrap.</param>
    public ItemMetadata(object item)
    {
      this.Item = item;
    }

    /// <summary>
    /// Explicitly invokes <see cref="E:Microsoft.Practices.Prism.Regions.ItemMetadata.MetadataChanged"/> to notify listeners.
    /// 
    /// </summary>
    public void InvokeMetadataChanged()
    {
      EventHandler eventHandler = this.MetadataChanged;
      if (eventHandler == null)
        return;
      eventHandler((object) this, EventArgs.Empty);
    }

    private static void DependencyPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
      ItemMetadata itemMetadata = dependencyObject as ItemMetadata;
      if (itemMetadata == null)
        return;
      itemMetadata.InvokeMetadataChanged();
    }
  }
}
