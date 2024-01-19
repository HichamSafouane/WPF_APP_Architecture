// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.ItemsControl
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C1D3DCCB-C99A-4167-B947-733D30DFAA08
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\WPF\PresentationFramework.dll

using MS.Internal;
using MS.Internal.Controls;
using MS.Internal.Data;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationFramework;
using MS.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace System.Windows.Controls
{
  /// <summary>
  /// Represents a control that can be used to present a collection of items.
  /// </summary>
  [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (FrameworkElement))]
  [ContentProperty("Items")]
  [DefaultEvent("OnItemsChanged")]
  [DefaultProperty("Items")]
  [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
  public class ItemsControl : Control, IAddChild, IGeneratorHost, IContainItemStorage
  {
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.ItemsSource"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.ItemsSource"/> dependency property.
    /// </returns>
    [CommonDependencyProperty]
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof (IEnumerable), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ItemsControl.OnItemsSourceChanged)));
    internal static readonly DependencyPropertyKey HasItemsPropertyKey = DependencyProperty.RegisterReadOnly("HasItems", typeof (bool), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(Control.OnVisualStatePropertyChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.HasItems"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.HasItems"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty HasItemsProperty = ItemsControl.HasItemsPropertyKey.DependencyProperty;
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.DisplayMemberPath"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.DisplayMemberPath"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty DisplayMemberPathProperty = DependencyProperty.Register("DisplayMemberPath", typeof (string), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, new PropertyChangedCallback(ItemsControl.OnDisplayMemberPathChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplate"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplate"/> dependency property.
    /// </returns>
    [CommonDependencyProperty]
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof (DataTemplate), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ItemsControl.OnItemTemplateChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplateSelector"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplateSelector"/> dependency property.
    /// </returns>
    [CommonDependencyProperty]
    public static readonly DependencyProperty ItemTemplateSelectorProperty = DependencyProperty.Register("ItemTemplateSelector", typeof (DataTemplateSelector), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ItemsControl.OnItemTemplateSelectorChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.ItemStringFormat"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.ItemStringFormat"/> dependency property.
    /// </returns>
    [CommonDependencyProperty]
    public static readonly DependencyProperty ItemStringFormatProperty = DependencyProperty.Register("ItemStringFormat", typeof (string), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ItemsControl.OnItemStringFormatChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.ItemBindingGroup"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.ItemBindingGroup"/> dependency property.
    /// </returns>
    [CommonDependencyProperty]
    public static readonly DependencyProperty ItemBindingGroupProperty = DependencyProperty.Register("ItemBindingGroup", typeof (BindingGroup), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ItemsControl.OnItemBindingGroupChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyle"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyle"/> dependency property.
    /// </returns>
    [CommonDependencyProperty]
    public static readonly DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register("ItemContainerStyle", typeof (Style), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ItemsControl.OnItemContainerStyleChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyleSelector"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyleSelector"/> dependency property.
    /// </returns>
    [CommonDependencyProperty]
    public static readonly DependencyProperty ItemContainerStyleSelectorProperty = DependencyProperty.Register("ItemContainerStyleSelector", typeof (StyleSelector), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ItemsControl.OnItemContainerStyleSelectorChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.ItemsPanel"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.ItemsPanel"/> dependency property.
    /// </returns>
    [CommonDependencyProperty]
    public static readonly DependencyProperty ItemsPanelProperty = DependencyProperty.Register("ItemsPanel", typeof (ItemsPanelTemplate), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) ItemsControl.GetDefaultItemsPanelTemplate(), new PropertyChangedCallback(ItemsControl.OnItemsPanelChanged)));
    private static readonly DependencyPropertyKey IsGroupingPropertyKey = DependencyProperty.RegisterReadOnly("IsGrouping", typeof (bool), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(ItemsControl.OnIsGroupingChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.IsGrouping"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.IsGrouping"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty IsGroupingProperty = ItemsControl.IsGroupingPropertyKey.DependencyProperty;
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.GroupStyleSelector"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.GroupStyleSelector"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty GroupStyleSelectorProperty = DependencyProperty.Register("GroupStyleSelector", typeof (GroupStyleSelector), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(ItemsControl.OnGroupStyleSelectorChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.AlternationCount"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.AlternationCount"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty AlternationCountProperty = DependencyProperty.Register("AlternationCount", typeof (int), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0, new PropertyChangedCallback(ItemsControl.OnAlternationCountChanged)));
    private static readonly DependencyPropertyKey AlternationIndexPropertyKey = DependencyProperty.RegisterAttachedReadOnly("AlternationIndex", typeof (int), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.AlternationIndex"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.AlternationIndex"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty AlternationIndexProperty = ItemsControl.AlternationIndexPropertyKey.DependencyProperty;
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.IsTextSearchEnabled"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.IsTextSearchEnabled"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty IsTextSearchEnabledProperty = DependencyProperty.Register("IsTextSearchEnabled", typeof (bool), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.ItemsControl.IsTextSearchCaseSensitive"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.ItemsControl.IsTextSearchCaseSensitive"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty IsTextSearchCaseSensitiveProperty = DependencyProperty.Register("IsTextSearchCaseSensitive", typeof (bool), typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));
    private static readonly UncommonField<bool> ShouldCoerceScrollUnitField = new UncommonField<bool>();
    private static readonly UncommonField<bool> ShouldCoerceCacheSizeField = new UncommonField<bool>();
    private ObservableCollection<System.Windows.Controls.GroupStyle> _groupStyle = new ObservableCollection<System.Windows.Controls.GroupStyle>();
    private ItemsControl.ItemInfo _focusedInfo;
    private ItemCollection _items;
    private ItemContainerGenerator _itemContainerGenerator;
    private Panel _itemsHost;
    private ScrollViewer _scrollHost;
    private static DependencyObjectType _dType;

    /// <summary>
    /// Gets the collection used to generate the content of the <see cref="T:System.Windows.Controls.ItemsControl"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The collection that is used to generate the content of the <see cref="T:System.Windows.Controls.ItemsControl"/>. The default is an empty collection.
    /// </returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Bindable(true)]
    [CustomCategory("Content")]
    public ItemCollection Items
    {
      get
      {
        if (this._items == null)
          this.CreateItemCollectionAndGenerator();
        return this._items;
      }
    }

    /// <summary>
    /// Gets or sets a collection used to generate the content of the <see cref="T:System.Windows.Controls.ItemsControl"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A collection that is used to generate the content of the <see cref="T:System.Windows.Controls.ItemsControl"/>. The default is null.
    /// </returns>
    [Bindable(true)]
    [CustomCategory("Content")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IEnumerable ItemsSource
    {
      get
      {
        return this.Items.ItemsSource;
      }
      set
      {
        if (value == null)
          this.ClearValue(ItemsControl.ItemsSourceProperty);
        else
          this.SetValue(ItemsControl.ItemsSourceProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets the <see cref="T:System.Windows.Controls.ItemContainerGenerator"/> that is associated with the control.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.Windows.Controls.ItemContainerGenerator"/> that is associated with the control. The default is null.
    /// </returns>
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public ItemContainerGenerator ItemContainerGenerator
    {
      get
      {
        if (this._itemContainerGenerator == null)
          this.CreateItemCollectionAndGenerator();
        return this._itemContainerGenerator;
      }
    }

    /// <summary>
    /// Gets an enumerator for the logical child objects of the <see cref="T:System.Windows.Controls.ItemsControl"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// An enumerator for the logical child objects of the <see cref="T:System.Windows.Controls.ItemsControl"/> object. The default is null.
    /// </returns>
    protected internal override IEnumerator LogicalChildren
    {
      get
      {
        if (!this.HasItems)
          return EmptyEnumerator.Instance;
        return this.Items.LogicalChildren;
      }
    }

    /// <summary>
    /// Gets a value that indicates whether the <see cref="T:System.Windows.Controls.ItemsControl"/> contains items.
    /// </summary>
    /// 
    /// <returns>
    /// true if the items count is greater than 0; otherwise, false.The default is false.
    /// </returns>
    [Browsable(false)]
    [Bindable(false)]
    public bool HasItems
    {
      get
      {
        return (bool) this.GetValue(ItemsControl.HasItemsProperty);
      }
    }

    /// <summary>
    /// Gets or sets a path to a value on the source object to serve as the visual representation of the object.
    /// </summary>
    /// 
    /// <returns>
    /// The path to a value on the source object. This can be any path, or an XPath such as "@Name". The default is an empty string ("").
    /// </returns>
    [CustomCategory("Content")]
    [Bindable(true)]
    public string DisplayMemberPath
    {
      get
      {
        return (string) this.GetValue(ItemsControl.DisplayMemberPathProperty);
      }
      set
      {
        this.SetValue(ItemsControl.DisplayMemberPathProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.DataTemplate"/> used to display each item.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Windows.DataTemplate"/> that specifies the visualization of the data objects. The default is null.
    /// </returns>
    [Bindable(true)]
    [CustomCategory("Content")]
    public DataTemplate ItemTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(ItemsControl.ItemTemplateProperty);
      }
      set
      {
        this.SetValue(ItemsControl.ItemTemplateProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the custom logic for choosing a template used to display each item.
    /// </summary>
    /// 
    /// <returns>
    /// A custom <see cref="T:System.Windows.Controls.DataTemplateSelector"/> object that provides logic and returns a <see cref="T:System.Windows.DataTemplate"/>. The default is null.
    /// </returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    [CustomCategory("Content")]
    public DataTemplateSelector ItemTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(ItemsControl.ItemTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(ItemsControl.ItemTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets a composite string that specifies how to format the items in the <see cref="T:System.Windows.Controls.ItemsControl"/> if they are displayed as strings.
    /// </summary>
    /// 
    /// <returns>
    /// A composite string that specifies how to format the items in the <see cref="T:System.Windows.Controls.ItemsControl"/> if they are displayed as strings.
    /// </returns>
    [CustomCategory("Content")]
    [Bindable(true)]
    public string ItemStringFormat
    {
      get
      {
        return (string) this.GetValue(ItemsControl.ItemStringFormatProperty);
      }
      set
      {
        this.SetValue(ItemsControl.ItemStringFormatProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Data.BindingGroup"/> that is copied to each item in the <see cref="T:System.Windows.Controls.ItemsControl"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.Windows.Data.BindingGroup"/> that is copied to each item in the <see cref="T:System.Windows.Controls.ItemsControl"/>.
    /// </returns>
    [Bindable(true)]
    [CustomCategory("Content")]
    public BindingGroup ItemBindingGroup
    {
      get
      {
        return (BindingGroup) this.GetValue(ItemsControl.ItemBindingGroupProperty);
      }
      set
      {
        this.SetValue(ItemsControl.ItemBindingGroupProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Style"/> that is applied to the container element generated for each item.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.Windows.Style"/> that is applied to the container element generated for each item. The default is null.
    /// </returns>
    [Bindable(true)]
    [Category("Content")]
    public Style ItemContainerStyle
    {
      get
      {
        return (Style) this.GetValue(ItemsControl.ItemContainerStyleProperty);
      }
      set
      {
        this.SetValue(ItemsControl.ItemContainerStyleProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets custom style-selection logic for a style that can be applied to each generated container element.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Windows.Controls.StyleSelector"/> object that contains logic that chooses the style to use as the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyle"/>. The default is null.
    /// </returns>
    [Bindable(true)]
    [Category("Content")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public StyleSelector ItemContainerStyleSelector
    {
      get
      {
        return (StyleSelector) this.GetValue(ItemsControl.ItemContainerStyleSelectorProperty);
      }
      set
      {
        this.SetValue(ItemsControl.ItemContainerStyleSelectorProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the template that defines the panel that controls the layout of items.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="T:System.Windows.Controls.ItemsPanelTemplate"/> that defines the panel to use for the layout of the items. The default value for the <see cref="T:System.Windows.Controls.ItemsControl"/> is an <see cref="T:System.Windows.Controls.ItemsPanelTemplate"/> that specifies a <see cref="T:System.Windows.Controls.StackPanel"/>.
    /// </returns>
    [Bindable(false)]
    public ItemsPanelTemplate ItemsPanel
    {
      get
      {
        return (ItemsPanelTemplate) this.GetValue(ItemsControl.ItemsPanelProperty);
      }
      set
      {
        this.SetValue(ItemsControl.ItemsPanelProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets a value that indicates whether the control is using grouping.
    /// </summary>
    /// 
    /// <returns>
    /// true if a control is using grouping; otherwise, false.
    /// </returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(false)]
    public bool IsGrouping
    {
      get
      {
        return (bool) this.GetValue(ItemsControl.IsGroupingProperty);
      }
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Windows.Controls.GroupStyle"/> objects that define the appearance of each level of groups.
    /// </summary>
    /// 
    /// <returns>
    /// A collection of <see cref="T:System.Windows.Controls.GroupStyle"/> objects that define the appearance of each level of groups.
    /// </returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ObservableCollection<System.Windows.Controls.GroupStyle> GroupStyle
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._groupStyle;
      }
    }

    /// <summary>
    /// Gets or sets a method that enables you to provide custom selection logic for a <see cref="T:System.Windows.Controls.GroupStyle"/> to apply to each group in a collection.
    /// </summary>
    /// 
    /// <returns>
    /// A method that enables you to provide custom selection logic for a <see cref="T:System.Windows.Controls.GroupStyle"/> to apply to each group in a collection.
    /// </returns>
    [CustomCategory("Content")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    public GroupStyleSelector GroupStyleSelector
    {
      get
      {
        return (GroupStyleSelector) this.GetValue(ItemsControl.GroupStyleSelectorProperty);
      }
      set
      {
        this.SetValue(ItemsControl.GroupStyleSelectorProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the number of alternating item containers in the <see cref="T:System.Windows.Controls.ItemsControl"/>, which enables alternating containers to have a unique appearance.
    /// </summary>
    /// 
    /// <returns>
    /// The number of alternating item containers in the <see cref="T:System.Windows.Controls.ItemsControl"/>.
    /// </returns>
    [CustomCategory("Content")]
    [Bindable(true)]
    public int AlternationCount
    {
      get
      {
        return (int) this.GetValue(ItemsControl.AlternationCountProperty);
      }
      set
      {
        this.SetValue(ItemsControl.AlternationCountProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether <see cref="T:System.Windows.Controls.TextSearch"/> is enabled on the <see cref="T:System.Windows.Controls.ItemsControl"/> instance.
    /// </summary>
    /// 
    /// <returns>
    /// true if <see cref="T:System.Windows.Controls.TextSearch"/> is enabled; otherwise, false. The default is false.
    /// </returns>
    public bool IsTextSearchEnabled
    {
      get
      {
        return (bool) this.GetValue(ItemsControl.IsTextSearchEnabledProperty);
      }
      set
      {
        this.SetValue(ItemsControl.IsTextSearchEnabledProperty, BooleanBoxes.Box(value));
      }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether case is a condition when searching for items.
    /// </summary>
    /// 
    /// <returns>
    /// true if text searches are case-sensitive; otherwise, false.
    /// </returns>
    public bool IsTextSearchCaseSensitive
    {
      get
      {
        return (bool) this.GetValue(ItemsControl.IsTextSearchCaseSensitiveProperty);
      }
      set
      {
        this.SetValue(ItemsControl.IsTextSearchCaseSensitiveProperty, BooleanBoxes.Box(value));
      }
    }

    ItemCollection IGeneratorHost.View
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.Items;
      }
    }

    int IGeneratorHost.AlternationCount
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.AlternationCount;
      }
    }

    private bool IsInitPending
    {
      get
      {
        return this.ReadInternalFlag(InternalFlags.InitPending);
      }
    }

    internal Panel ItemsHost
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._itemsHost;
      }
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set
      {
        this._itemsHost = value;
      }
    }

    internal ItemsControl.ItemInfo FocusedInfo
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._focusedInfo;
      }
    }

    internal bool IsLogicalVertical
    {
      get
      {
        if (this.ItemsHost != null && this.ItemsHost.HasLogicalOrientation && (this.ItemsHost.LogicalOrientation == Orientation.Vertical && this.ScrollHost != null) && this.ScrollHost.CanContentScroll)
          return VirtualizingPanel.GetScrollUnit((DependencyObject) this) == ScrollUnit.Item;
        return false;
      }
    }

    internal bool IsLogicalHorizontal
    {
      get
      {
        if (this.ItemsHost != null && this.ItemsHost.HasLogicalOrientation && (this.ItemsHost.LogicalOrientation == Orientation.Horizontal && this.ScrollHost != null) && this.ScrollHost.CanContentScroll)
          return VirtualizingPanel.GetScrollUnit((DependencyObject) this) == ScrollUnit.Item;
        return false;
      }
    }

    internal ScrollViewer ScrollHost
    {
      get
      {
        if (!this.ReadControlFlag(Control.ControlBoolFlags.ScrollHostValid))
        {
          if (this._itemsHost == null)
            return (ScrollViewer) null;
          for (DependencyObject reference = (DependencyObject) this._itemsHost; reference != this && reference != null; reference = VisualTreeHelper.GetParent(reference))
          {
            ScrollViewer scrollViewer = reference as ScrollViewer;
            if (scrollViewer != null)
            {
              this._scrollHost = scrollViewer;
              break;
            }
          }
          this.WriteControlFlag(Control.ControlBoolFlags.ScrollHostValid, true);
        }
        return this._scrollHost;
      }
    }

    internal static TimeSpan AutoScrollTimeout
    {
      get
      {
        return TimeSpan.FromMilliseconds((double) SafeNativeMethods.GetDoubleClickTime() * 0.8);
      }
    }

    internal override DependencyObjectType DTypeThemeStyleKey
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return ItemsControl._dType;
      }
    }

    static ItemsControl()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (ItemsControl)));
      ItemsControl._dType = DependencyObjectType.FromSystemTypeInternal(typeof (ItemsControl));
      EventManager.RegisterClassHandler(typeof (ItemsControl), Keyboard.GotKeyboardFocusEvent, (Delegate) new KeyboardFocusChangedEventHandler(ItemsControl.OnGotFocus));
      VirtualizingPanel.ScrollUnitProperty.OverrideMetadata(typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata(new PropertyChangedCallback(ItemsControl.OnScrollingModeChanged), new CoerceValueCallback(ItemsControl.CoerceScrollingMode)));
      VirtualizingPanel.CacheLengthProperty.OverrideMetadata(typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata(new PropertyChangedCallback(ItemsControl.OnCacheSizeChanged)));
      VirtualizingPanel.CacheLengthUnitProperty.OverrideMetadata(typeof (ItemsControl), (PropertyMetadata) new FrameworkPropertyMetadata(new PropertyChangedCallback(ItemsControl.OnCacheSizeChanged), new CoerceValueCallback(ItemsControl.CoerceVirtualizationCacheLengthUnit)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Controls.ItemsControl"/> class.
    /// </summary>
    public ItemsControl()
    {
      ItemsControl.ShouldCoerceCacheSizeField.SetValue((DependencyObject) this, true);
      this.CoerceValue(VirtualizingPanel.CacheLengthUnitProperty);
    }

    /// <summary>
    /// Returns a value that indicates whether serialization processes should serialize the effective value of the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> property.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> property value should be serialized; otherwise, false.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public bool ShouldSerializeItems()
    {
      return this.HasItems;
    }

    /// <summary>
    /// Called when the <see cref="P:System.Windows.Controls.ItemsControl.ItemsSource"/> property changes.
    /// </summary>
    /// <param name="oldValue">Old value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemsSource"/> property.</param><param name="newValue">New value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemsSource"/> property.</param>
    protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> property changes.
    /// </summary>
    /// <param name="e">Information about the change.</param>
    protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
    }

    internal virtual void AdjustItemInfoOverride(NotifyCollectionChangedEventArgs e)
    {
      this.AdjustItemInfo(e, this._focusedInfo);
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.DisplayMemberPath"/> property changes.
    /// </summary>
    /// <param name="oldDisplayMemberPath">The old value of the <see cref="P:System.Windows.Controls.ItemsControl.DisplayMemberPath"/> property.</param><param name="newDisplayMemberPath">New value of the <see cref="P:System.Windows.Controls.ItemsControl.DisplayMemberPath"/> property.</param>
    protected virtual void OnDisplayMemberPathChanged(string oldDisplayMemberPath, string newDisplayMemberPath)
    {
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplate"/> property changes.
    /// </summary>
    /// <param name="oldItemTemplate">The old <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplate"/> property value.</param><param name="newItemTemplate">The new <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplate"/> property value.</param>
    protected virtual void OnItemTemplateChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate)
    {
      this.CheckTemplateSource();
      if (this._itemContainerGenerator == null)
        return;
      this._itemContainerGenerator.Refresh();
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplateSelector"/> property changes.
    /// </summary>
    /// <param name="oldItemTemplateSelector">Old value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplateSelector"/> property.</param><param name="newItemTemplateSelector">New value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplateSelector"/> property.</param>
    protected virtual void OnItemTemplateSelectorChanged(DataTemplateSelector oldItemTemplateSelector, DataTemplateSelector newItemTemplateSelector)
    {
      this.CheckTemplateSource();
      if (this._itemContainerGenerator == null || this.ItemTemplate != null)
        return;
      this._itemContainerGenerator.Refresh();
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.ItemStringFormat"/> property changes.
    /// </summary>
    /// <param name="oldItemStringFormat">The old value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemStringFormat"/> property.</param><param name="newItemStringFormat">The new value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemStringFormat"/> property.</param>
    protected virtual void OnItemStringFormatChanged(string oldItemStringFormat, string newItemStringFormat)
    {
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.ItemBindingGroup"/> property changes.
    /// </summary>
    /// <param name="oldItemBindingGroup">The old value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemBindingGroup"/>.</param><param name="newItemBindingGroup">The new value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemBindingGroup"/>.</param>
    protected virtual void OnItemBindingGroupChanged(BindingGroup oldItemBindingGroup, BindingGroup newItemBindingGroup)
    {
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyle"/> property changes.
    /// </summary>
    /// <param name="oldItemContainerStyle">Old value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyle"/> property.</param><param name="newItemContainerStyle">New value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyle"/> property.</param>
    protected virtual void OnItemContainerStyleChanged(Style oldItemContainerStyle, Style newItemContainerStyle)
    {
      MS.Internal.Helper.CheckStyleAndStyleSelector("ItemContainer", ItemsControl.ItemContainerStyleProperty, ItemsControl.ItemContainerStyleSelectorProperty, (DependencyObject) this);
      if (this._itemContainerGenerator == null)
        return;
      this._itemContainerGenerator.Refresh();
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyleSelector"/> property changes.
    /// </summary>
    /// <param name="oldItemContainerStyleSelector">Old value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyleSelector"/> property.</param><param name="newItemContainerStyleSelector">New value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyleSelector"/> property.</param>
    protected virtual void OnItemContainerStyleSelectorChanged(StyleSelector oldItemContainerStyleSelector, StyleSelector newItemContainerStyleSelector)
    {
      MS.Internal.Helper.CheckStyleAndStyleSelector("ItemContainer", ItemsControl.ItemContainerStyleProperty, ItemsControl.ItemContainerStyleSelectorProperty, (DependencyObject) this);
      if (this._itemContainerGenerator == null || this.ItemContainerStyle != null)
        return;
      this._itemContainerGenerator.Refresh();
    }

    /// <summary>
    /// Returns the <see cref="T:System.Windows.Controls.ItemsControl"/> that the specified element hosts items for.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.Windows.Controls.ItemsControl"/> that the specified element hosts items for, or null.
    /// </returns>
    /// <param name="element">The host element.</param>
    public static ItemsControl GetItemsOwner(DependencyObject element)
    {
      ItemsControl itemsControl = (ItemsControl) null;
      Panel panel = element as Panel;
      if (panel != null && panel.IsItemsHost)
      {
        ItemsPresenter itemsPresenter = ItemsPresenter.FromPanel(panel);
        itemsControl = itemsPresenter == null ? panel.TemplatedParent as ItemsControl : itemsPresenter.Owner;
      }
      return itemsControl;
    }

    internal static DependencyObject GetItemsOwnerInternal(DependencyObject element)
    {
      ItemsControl itemsControl;
      return ItemsControl.GetItemsOwnerInternal(element, out itemsControl);
    }

    internal static DependencyObject GetItemsOwnerInternal(DependencyObject element, out ItemsControl itemsControl)
    {
      DependencyObject dependencyObject = (DependencyObject) null;
      Panel panel = element as Panel;
      itemsControl = (ItemsControl) null;
      if (panel != null && panel.IsItemsHost)
      {
        ItemsPresenter itemsPresenter = ItemsPresenter.FromPanel(panel);
        if (itemsPresenter != null)
        {
          dependencyObject = itemsPresenter.TemplatedParent;
          itemsControl = itemsPresenter.Owner;
        }
        else
        {
          dependencyObject = panel.TemplatedParent;
          itemsControl = dependencyObject as ItemsControl;
        }
      }
      return dependencyObject;
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.ItemsPanel"/> property changes.
    /// </summary>
    /// <param name="oldItemsPanel">Old value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemsPanel"/> property.</param><param name="newItemsPanel">New value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemsPanel"/> property.</param>
    protected virtual void OnItemsPanelChanged(ItemsPanelTemplate oldItemsPanel, ItemsPanelTemplate newItemsPanel)
    {
      this.ItemContainerGenerator.OnPanelChanged();
    }

    internal virtual void OnIsGroupingChanged(DependencyPropertyChangedEventArgs e)
    {
      ItemsControl.ShouldCoerceScrollUnitField.SetValue((DependencyObject) this, true);
      this.CoerceValue(VirtualizingPanel.ScrollUnitProperty);
      ItemsControl.ShouldCoerceCacheSizeField.SetValue((DependencyObject) this, true);
      this.CoerceValue(VirtualizingPanel.CacheLengthUnitProperty);
      ((IContainItemStorage) this).Clear();
    }

    /// <summary>
    /// Returns a value that indicates whether serialization processes should serialize the effective value of the <see cref="P:System.Windows.Controls.ItemsControl.GroupStyle"/> property.
    /// </summary>
    /// 
    /// <returns>
    /// true if the <see cref="P:System.Windows.Controls.ItemsControl.GroupStyle"/> property value should be serialized; otherwise, false.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeGroupStyle()
    {
      return this.GroupStyle.Count > 0;
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.GroupStyleSelector"/> property changes.
    /// </summary>
    /// <param name="oldGroupStyleSelector">Old value of the <see cref="P:System.Windows.Controls.ItemsControl.GroupStyleSelector"/> property.</param><param name="newGroupStyleSelector">New value of the <see cref="P:System.Windows.Controls.ItemsControl.GroupStyleSelector"/> property.</param>
    protected virtual void OnGroupStyleSelectorChanged(GroupStyleSelector oldGroupStyleSelector, GroupStyleSelector newGroupStyleSelector)
    {
      if (this._itemContainerGenerator == null)
        return;
      this._itemContainerGenerator.Refresh();
    }

    /// <summary>
    /// Invoked when the <see cref="P:System.Windows.Controls.ItemsControl.AlternationCount"/> property changes.
    /// </summary>
    /// <param name="oldAlternationCount">The old value of <see cref="P:System.Windows.Controls.ItemsControl.AlternationCount"/>.</param><param name="newAlternationCount">The new value of <see cref="P:System.Windows.Controls.ItemsControl.AlternationCount"/>.</param>
    protected virtual void OnAlternationCountChanged(int oldAlternationCount, int newAlternationCount)
    {
      this.ItemContainerGenerator.ChangeAlternationCount();
    }

    /// <summary>
    /// Gets the <see cref="P:System.Windows.Controls.ItemsControl.AlternationIndex"/> for the specified object.
    /// </summary>
    /// 
    /// <returns>
    /// The value of the <see cref="P:System.Windows.Controls.ItemsControl.AlternationIndex"/>.
    /// </returns>
    /// <param name="element">The object from which to get the <see cref="P:System.Windows.Controls.ItemsControl.AlternationIndex"/>.</param>
    public static int GetAlternationIndex(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (int) element.GetValue(ItemsControl.AlternationIndexProperty);
    }

    internal static void SetAlternationIndex(DependencyObject d, int value)
    {
      d.SetValue(ItemsControl.AlternationIndexPropertyKey, (object) value);
    }

    internal static void ClearAlternationIndex(DependencyObject d)
    {
      d.ClearValue(ItemsControl.AlternationIndexPropertyKey);
    }

    /// <summary>
    /// Returns the <see cref="T:System.Windows.Controls.ItemsControl"/> that owns the specified container element.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.Windows.Controls.ItemsControl"/> that owns the specified container element.
    /// </returns>
    /// <param name="container">The container element to return the <see cref="T:System.Windows.Controls.ItemsControl"/> for.</param>
    public static ItemsControl ItemsControlFromItemContainer(DependencyObject container)
    {
      UIElement uiElement = container as UIElement;
      if (uiElement == null)
        return (ItemsControl) null;
      ItemsControl itemsControl = LogicalTreeHelper.GetParent((DependencyObject) uiElement) as ItemsControl;
      if (itemsControl == null)
        return ItemsControl.GetItemsOwner((DependencyObject) (VisualTreeHelper.GetParent((DependencyObject) uiElement) as UIElement));
      if (((IGeneratorHost) itemsControl).IsItemItsOwnContainer((object) uiElement))
        return itemsControl;
      return (ItemsControl) null;
    }

    /// <summary>
    /// Returns the container that belongs to the specified <see cref="T:System.Windows.Controls.ItemsControl"/> that owns the given container element.
    /// </summary>
    /// 
    /// <returns>
    /// The container that belongs to the specified <see cref="T:System.Windows.Controls.ItemsControl"/> that owns the given element, if <paramref name="itemsControl"/> is not null. If <paramref name="itemsControl"/> is null, returns the closest container that belongs to any <see cref="T:System.Windows.Controls.ItemsControl"/>.
    /// </returns>
    /// <param name="itemsControl">The <see cref="T:System.Windows.Controls.ItemsControl"/> to return the container for.</param><param name="element">The element to return the container for.</param>
    public static DependencyObject ContainerFromElement(ItemsControl itemsControl, DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      if (ItemsControl.IsContainerForItemsControl(element, itemsControl))
        return element;
      FrameworkObject frameworkObject = new FrameworkObject(element);
      frameworkObject.Reset(frameworkObject.GetPreferVisualParent(true).DO);
      while (frameworkObject.DO != null && !ItemsControl.IsContainerForItemsControl(frameworkObject.DO, itemsControl))
        frameworkObject.Reset(frameworkObject.PreferVisualParent.DO);
      return frameworkObject.DO;
    }

    /// <summary>
    /// Returns the container that belongs to the current <see cref="T:System.Windows.Controls.ItemsControl"/> that owns the given element.
    /// </summary>
    /// 
    /// <returns>
    /// The container that belongs to the current <see cref="T:System.Windows.Controls.ItemsControl"/> that owns the given element or null if no such container exists.
    /// </returns>
    /// <param name="element">The element to return the container for.</param>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public DependencyObject ContainerFromElement(DependencyObject element)
    {
      return ItemsControl.ContainerFromElement(this, element);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    void IAddChild.AddChild(object value)
    {
      this.AddChild(value);
    }

    /// <summary>
    /// Adds the specified object as the child of the <see cref="T:System.Windows.Controls.ItemsControl"/> object.
    /// </summary>
    /// <param name="value">The object to add as a child.</param>
    protected virtual void AddChild(object value)
    {
      this.Items.Add(value);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    void IAddChild.AddText(string text)
    {
      this.AddText(text);
    }

    /// <summary>
    /// Adds the specified text string to the <see cref="T:System.Windows.Controls.ItemsControl"/> object.
    /// </summary>
    /// <param name="text">The string to add.</param>
    protected virtual void AddText(string text)
    {
      this.Items.Add((object) text);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    bool IGeneratorHost.IsItemItsOwnContainer(object item)
    {
      return this.IsItemItsOwnContainer(item);
    }

    DependencyObject IGeneratorHost.GetContainerForItem(object item)
    {
      DependencyObject dependencyObject = !this.IsItemItsOwnContainerOverride(item) ? this.GetContainerForItemOverride() : item as DependencyObject;
      Visual visual1 = dependencyObject as Visual;
      if (visual1 != null)
      {
        Visual visual2 = VisualTreeHelper.GetParent((DependencyObject) visual1) as Visual;
        if (visual2 != null)
        {
          Invariant.Assert(visual2 is FrameworkElement, System.Windows.SR.Get("ItemsControl_ParentNotFrameworkElement"));
          Panel panel = visual2 as Panel;
          if (panel != null && visual1 is UIElement)
            panel.Children.RemoveNoVerify((UIElement) visual1);
          else
            ((FrameworkElement) visual2).TemplateChild = (UIElement) null;
        }
      }
      return dependencyObject;
    }

    void IGeneratorHost.PrepareItemContainer(DependencyObject container, object item)
    {
      GroupItem groupItem = container as GroupItem;
      if (groupItem != null)
      {
        groupItem.PrepareItemContainer(item, this);
      }
      else
      {
        if (this.ShouldApplyItemContainerStyle(container, item))
          this.ApplyItemContainerStyle(container, item);
        this.PrepareContainerForItemOverride(container, item);
        if (!MS.Internal.Helper.HasUnmodifiedDefaultValue((DependencyObject) this, ItemsControl.ItemBindingGroupProperty) && MS.Internal.Helper.HasUnmodifiedDefaultOrInheritedValue(container, FrameworkElement.BindingGroupProperty))
        {
          BindingGroup itemBindingGroup = this.ItemBindingGroup;
          BindingGroup bindingGroup = itemBindingGroup != null ? new BindingGroup(itemBindingGroup) : (BindingGroup) null;
          container.SetValue(FrameworkElement.BindingGroupProperty, (object) bindingGroup);
        }
        if (container == item && TraceData.IsEnabled && (this.ItemTemplate != null || this.ItemTemplateSelector != null))
          TraceData.Trace(TraceEventType.Error, TraceData.ItemTemplateForDirectItem, (object) AvTrace.TypeName(item));
        TreeViewItem treeViewItem = container as TreeViewItem;
        if (treeViewItem == null)
          return;
        treeViewItem.PrepareItemContainer(item, this);
      }
    }

    void IGeneratorHost.ClearContainerForItem(DependencyObject container, object item)
    {
      GroupItem groupItem = container as GroupItem;
      if (groupItem == null)
      {
        this.ClearContainerForItemOverride(container, item);
        TreeViewItem treeViewItem = container as TreeViewItem;
        if (treeViewItem == null)
          return;
        treeViewItem.ClearItemContainer(item, this);
      }
      else
        groupItem.ClearItemContainer(item, this);
    }

    bool IGeneratorHost.IsHostForItemContainer(DependencyObject container)
    {
      ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(container);
      if (itemsControl != null)
        return itemsControl == this;
      if (LogicalTreeHelper.GetParent(container) == null && this.IsItemItsOwnContainerOverride((object) container) && this.HasItems)
        return this.Items.Contains((object) container);
      return false;
    }

    System.Windows.Controls.GroupStyle IGeneratorHost.GetGroupStyle(CollectionViewGroup group, int level)
    {
      System.Windows.Controls.GroupStyle groupStyle = (System.Windows.Controls.GroupStyle) null;
      if (this.GroupStyleSelector != null)
        groupStyle = this.GroupStyleSelector(group, level);
      if (groupStyle == null)
      {
        if (level >= this.GroupStyle.Count)
          level = this.GroupStyle.Count - 1;
        if (level >= 0)
          groupStyle = this.GroupStyle[level];
      }
      return groupStyle;
    }

    void IGeneratorHost.SetIsGrouping(bool isGrouping)
    {
      this.SetValue(ItemsControl.IsGroupingPropertyKey, BooleanBoxes.Box(isGrouping));
    }

    /// <summary>
    /// Indicates that the initialization of the <see cref="T:System.Windows.Controls.ItemsControl"/> object is about to start.
    /// </summary>
    public override void BeginInit()
    {
      base.BeginInit();
      if (this._items == null)
        return;
      this._items.BeginInit();
    }

    /// <summary>
    /// Indicates that the initialization of the <see cref="T:System.Windows.Controls.ItemsControl"/> object is complete.
    /// </summary>
    public override void EndInit()
    {
      if (!this.IsInitPending)
        return;
      if (this._items != null)
        this._items.EndInit();
      base.EndInit();
    }

    /// <summary>
    /// Determines if the specified item is (or is eligible to be) its own container.
    /// </summary>
    /// 
    /// <returns>
    /// true if the item is (or is eligible to be) its own container; otherwise, false.
    /// </returns>
    /// <param name="item">The item to check.</param>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public bool IsItemItsOwnContainer(object item)
    {
      return this.IsItemItsOwnContainerOverride(item);
    }

    /// <summary>
    /// Determines if the specified item is (or is eligible to be) its own container.
    /// </summary>
    /// 
    /// <returns>
    /// true if the item is (or is eligible to be) its own container; otherwise, false.
    /// </returns>
    /// <param name="item">The item to check.</param>
    protected virtual bool IsItemItsOwnContainerOverride(object item)
    {
      return item is UIElement;
    }

    /// <summary>
    /// Creates or identifies the element that is used to display the given item.
    /// </summary>
    /// 
    /// <returns>
    /// The element that is used to display the given item.
    /// </returns>
    protected virtual DependencyObject GetContainerForItemOverride()
    {
      return (DependencyObject) new ContentPresenter();
    }

    /// <summary>
    /// Prepares the specified element to display the specified item.
    /// </summary>
    /// <param name="element">Element used to display the specified item.</param><param name="item">Specified item.</param>
    protected virtual void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
      HeaderedContentControl headeredContentControl;
      if ((headeredContentControl = element as HeaderedContentControl) != null)
      {
        headeredContentControl.PrepareHeaderedContentControl(item, this.ItemTemplate, this.ItemTemplateSelector, this.ItemStringFormat);
      }
      else
      {
        ContentControl contentControl;
        if ((contentControl = element as ContentControl) != null)
        {
          contentControl.PrepareContentControl(item, this.ItemTemplate, this.ItemTemplateSelector, this.ItemStringFormat);
        }
        else
        {
          ContentPresenter contentPresenter;
          if ((contentPresenter = element as ContentPresenter) != null)
          {
            contentPresenter.PrepareContentPresenter(item, this.ItemTemplate, this.ItemTemplateSelector, this.ItemStringFormat);
          }
          else
          {
            HeaderedItemsControl headeredItemsControl;
            if ((headeredItemsControl = element as HeaderedItemsControl) != null)
            {
              headeredItemsControl.PrepareHeaderedItemsControl(item, this);
            }
            else
            {
              ItemsControl itemsControl;
              if ((itemsControl = element as ItemsControl) == null || itemsControl == this)
                return;
              itemsControl.PrepareItemsControl(item, this);
            }
          }
        }
      }
    }

    /// <summary>
    /// When overridden in a derived class, undoes the effects of the <see cref="M:System.Windows.Controls.ItemsControl.PrepareContainerForItemOverride(System.Windows.DependencyObject,System.Object)"/> method.
    /// </summary>
    /// <param name="element">The container element.</param><param name="item">The item.</param>
    protected virtual void ClearContainerForItemOverride(DependencyObject element, object item)
    {
      HeaderedContentControl headeredContentControl;
      if ((headeredContentControl = element as HeaderedContentControl) != null)
      {
        headeredContentControl.ClearHeaderedContentControl(item);
      }
      else
      {
        ContentControl contentControl;
        if ((contentControl = element as ContentControl) != null)
        {
          contentControl.ClearContentControl(item);
        }
        else
        {
          ContentPresenter contentPresenter;
          if ((contentPresenter = element as ContentPresenter) != null)
          {
            contentPresenter.ClearContentPresenter(item);
          }
          else
          {
            HeaderedItemsControl headeredItemsControl;
            if ((headeredItemsControl = element as HeaderedItemsControl) != null)
            {
              headeredItemsControl.ClearHeaderedItemsControl(item);
            }
            else
            {
              ItemsControl itemsControl;
              if ((itemsControl = element as ItemsControl) == null || itemsControl == this)
                return;
              itemsControl.ClearItemsControl(item);
            }
          }
        }
      }
    }

    /// <summary>
    /// Invoked when the <see cref="E:System.Windows.UIElement.TextInput"/> event is received.
    /// </summary>
    /// <param name="e">Information about the event.</param>
    protected override void OnTextInput(TextCompositionEventArgs e)
    {
      base.OnTextInput(e);
      if (string.IsNullOrEmpty(e.Text) || !this.IsTextSearchEnabled || e.OriginalSource != this && ItemsControl.ItemsControlFromItemContainer(e.OriginalSource as DependencyObject) != this)
        return;
      TextSearch textSearch = TextSearch.EnsureInstance(this);
      if (textSearch == null)
        return;
      textSearch.DoSearch(e.Text);
      e.Handled = true;
    }

    /// <summary>
    /// Invoked when the <see cref="E:System.Windows.UIElement.KeyDown"/> event is received.
    /// </summary>
    /// <param name="e">Information about the event.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (!this.IsTextSearchEnabled || e.Key != System.Windows.Input.Key.Back)
        return;
      TextSearch textSearch = TextSearch.EnsureInstance(this);
      if (textSearch == null)
        return;
      textSearch.DeleteLastCharacter();
    }

    internal override void OnTemplateChangedInternal(FrameworkTemplate oldTemplate, FrameworkTemplate newTemplate)
    {
      this._itemsHost = (Panel) null;
      this._scrollHost = (ScrollViewer) null;
      this.WriteControlFlag(Control.ControlBoolFlags.ScrollHostValid, false);
      base.OnTemplateChangedInternal(oldTemplate, newTemplate);
    }

    /// <summary>
    /// Returns a value that indicates whether to apply the style from the <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyle"/> or <see cref="P:System.Windows.Controls.ItemsControl.ItemContainerStyleSelector"/> property to the container element of the specified item.
    /// </summary>
    /// 
    /// <returns>
    /// Always true for the base implementation.
    /// </returns>
    /// <param name="container">The container element.</param><param name="item">The item of interest.</param>
    protected virtual bool ShouldApplyItemContainerStyle(DependencyObject container, object item)
    {
      return true;
    }

    internal void PrepareItemsControl(object item, ItemsControl parentItemsControl)
    {
      if (item == this)
        return;
      DataTemplate itemTemplate = parentItemsControl.ItemTemplate;
      DataTemplateSelector templateSelector = parentItemsControl.ItemTemplateSelector;
      string itemStringFormat = parentItemsControl.ItemStringFormat;
      Style itemContainerStyle = parentItemsControl.ItemContainerStyle;
      StyleSelector containerStyleSelector = parentItemsControl.ItemContainerStyleSelector;
      int alternationCount = parentItemsControl.AlternationCount;
      BindingGroup itemBindingGroup = parentItemsControl.ItemBindingGroup;
      if (itemTemplate != null)
        this.SetValue(ItemsControl.ItemTemplateProperty, (object) itemTemplate);
      if (templateSelector != null)
        this.SetValue(ItemsControl.ItemTemplateSelectorProperty, (object) templateSelector);
      if (itemStringFormat != null && MS.Internal.Helper.HasDefaultValue((DependencyObject) this, ItemsControl.ItemStringFormatProperty))
        this.SetValue(ItemsControl.ItemStringFormatProperty, (object) itemStringFormat);
      if (itemContainerStyle != null && MS.Internal.Helper.HasDefaultValue((DependencyObject) this, ItemsControl.ItemContainerStyleProperty))
        this.SetValue(ItemsControl.ItemContainerStyleProperty, (object) itemContainerStyle);
      if (containerStyleSelector != null && MS.Internal.Helper.HasDefaultValue((DependencyObject) this, ItemsControl.ItemContainerStyleSelectorProperty))
        this.SetValue(ItemsControl.ItemContainerStyleSelectorProperty, (object) containerStyleSelector);
      if (alternationCount != 0 && MS.Internal.Helper.HasDefaultValue((DependencyObject) this, ItemsControl.AlternationCountProperty))
        this.SetValue(ItemsControl.AlternationCountProperty, (object) alternationCount);
      if (itemBindingGroup == null || !MS.Internal.Helper.HasDefaultValue((DependencyObject) this, ItemsControl.ItemBindingGroupProperty))
        return;
      this.SetValue(ItemsControl.ItemBindingGroupProperty, (object) itemBindingGroup);
    }

    internal void ClearItemsControl(object item)
    {
      ItemsControl itemsControl = this;
    }

    internal object OnBringItemIntoView(object arg)
    {
      ItemsControl.ItemInfo info = arg as ItemsControl.ItemInfo;
      if (info == (ItemsControl.ItemInfo) null)
        info = this.NewItemInfo(arg, (DependencyObject) null, -1);
      return this.OnBringItemIntoView(info);
    }

    internal object OnBringItemIntoView(ItemsControl.ItemInfo info)
    {
      FrameworkElement frameworkElement = info.Container as FrameworkElement;
      if (frameworkElement != null)
        frameworkElement.BringIntoView();
      else if ((info = this.LeaseItemInfo(info, true)).Index >= 0)
      {
        if (!FrameworkCompatibilityPreferences.GetVSP45Compat())
          this.UpdateLayout();
        VirtualizingPanel virtualizingPanel = this.ItemsHost as VirtualizingPanel;
        if (virtualizingPanel != null)
          virtualizingPanel.BringIndexIntoView(info.Index);
      }
      return (object) null;
    }

    internal bool NavigateByLine(FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      return this.NavigateByLine(this.FocusedInfo, Keyboard.FocusedElement as FrameworkElement, direction, itemNavigateArgs);
    }

    internal void PrepareNavigateByLine(ItemsControl.ItemInfo startingInfo, FrameworkElement startingElement, FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs, out FrameworkElement container)
    {
      container = (FrameworkElement) null;
      if (this.ItemsHost == null)
        return;
      if (startingElement != null)
        this.MakeVisible(startingElement, direction, false);
      else
        this.MakeVisible(startingInfo, direction, out startingElement);
      this.NavigateByLineInternal(startingInfo != (ItemsControl.ItemInfo) null ? startingInfo.Item : (object) null, direction, startingElement, itemNavigateArgs, false, out container);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal bool NavigateByLine(ItemsControl.ItemInfo startingInfo, FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      return this.NavigateByLine(startingInfo, (FrameworkElement) null, direction, itemNavigateArgs);
    }

    internal bool NavigateByLine(ItemsControl.ItemInfo startingInfo, FrameworkElement startingElement, FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      if (this.ItemsHost == null)
        return false;
      if (startingElement != null)
        this.MakeVisible(startingElement, direction, false);
      else
        this.MakeVisible(startingInfo, direction, out startingElement);
      FrameworkElement container;
      return this.NavigateByLineInternal(startingInfo != (ItemsControl.ItemInfo) null ? startingInfo.Item : (object) null, direction, startingElement, itemNavigateArgs, true, out container);
    }

    internal void PrepareToNavigateByPage(ItemsControl.ItemInfo startingInfo, FrameworkElement startingElement, FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs, out FrameworkElement container)
    {
      container = (FrameworkElement) null;
      if (this.ItemsHost == null)
        return;
      if (startingElement != null)
        this.MakeVisible(startingElement, direction, false);
      else
        this.MakeVisible(startingInfo, direction, out startingElement);
      this.NavigateByPageInternal(startingInfo != (ItemsControl.ItemInfo) null ? startingInfo.Item : (object) null, direction, startingElement, itemNavigateArgs, false, out container);
    }

    internal bool NavigateByPage(FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      return this.NavigateByPage(this.FocusedInfo, Keyboard.FocusedElement as FrameworkElement, direction, itemNavigateArgs);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal bool NavigateByPage(ItemsControl.ItemInfo startingInfo, FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      return this.NavigateByPage(startingInfo, (FrameworkElement) null, direction, itemNavigateArgs);
    }

    internal bool NavigateByPage(ItemsControl.ItemInfo startingInfo, FrameworkElement startingElement, FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      if (this.ItemsHost == null)
        return false;
      if (startingElement != null)
        this.MakeVisible(startingElement, direction, false);
      else
        this.MakeVisible(startingInfo, direction, out startingElement);
      FrameworkElement container;
      return this.NavigateByPageInternal(startingInfo != (ItemsControl.ItemInfo) null ? startingInfo.Item : (object) null, direction, startingElement, itemNavigateArgs, true, out container);
    }

    internal void NavigateToStart(ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      FrameworkElement container;
      this.NavigateToStartInternal(itemNavigateArgs, true, out container);
    }

    internal bool NavigateToStartInternal(ItemsControl.ItemNavigateArgs itemNavigateArgs, bool shouldFocus, out FrameworkElement container)
    {
      container = (FrameworkElement) null;
      if (this.ItemsHost != null)
      {
        if (this.ScrollHost != null)
        {
          bool flag = this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Horizontal;
          double horizontalOffset;
          double verticalOffset;
          do
          {
            horizontalOffset = this.ScrollHost.HorizontalOffset;
            verticalOffset = this.ScrollHost.VerticalOffset;
            if (flag)
              this.ScrollHost.ScrollToLeftEnd();
            else
              this.ScrollHost.ScrollToTop();
            this.ItemsHost.UpdateLayout();
          }
          while (!DoubleUtil.AreClose(horizontalOffset, this.ScrollHost.HorizontalOffset) || !DoubleUtil.AreClose(verticalOffset, this.ScrollHost.VerticalOffset));
        }
        FrameworkElement firstElement;
        object itemOnCurrentPage = this.GetFirstItemOnCurrentPage(this.FindEndFocusableLeafContainer(this.ItemsHost, false), FocusNavigationDirection.Up, out firstElement);
        container = firstElement;
        if (shouldFocus)
        {
          if (firstElement != null && (itemOnCurrentPage == DependencyProperty.UnsetValue || itemOnCurrentPage is CollectionViewGroupInternal))
            return firstElement.Focus();
          ItemsControl encapsulatingItemsControl = ItemsControl.GetEncapsulatingItemsControl(firstElement);
          if (encapsulatingItemsControl != null)
            return encapsulatingItemsControl.FocusItem(this.NewItemInfo(itemOnCurrentPage, (DependencyObject) firstElement, -1), itemNavigateArgs);
        }
      }
      return false;
    }

    internal void NavigateToEnd(ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      FrameworkElement container;
      this.NavigateToEndInternal(itemNavigateArgs, true, out container);
    }

    internal bool NavigateToEndInternal(ItemsControl.ItemNavigateArgs itemNavigateArgs, bool shouldFocus, out FrameworkElement container)
    {
      container = (FrameworkElement) null;
      if (this.ItemsHost != null)
      {
        if (this.ScrollHost != null)
        {
          bool flag = this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Horizontal;
          double horizontalOffset;
          double verticalOffset;
          do
          {
            horizontalOffset = this.ScrollHost.HorizontalOffset;
            verticalOffset = this.ScrollHost.VerticalOffset;
            if (flag)
              this.ScrollHost.ScrollToRightEnd();
            else
              this.ScrollHost.ScrollToBottom();
            this.ItemsHost.UpdateLayout();
          }
          while (!DoubleUtil.AreClose(horizontalOffset, this.ScrollHost.HorizontalOffset) || !DoubleUtil.AreClose(verticalOffset, this.ScrollHost.VerticalOffset));
        }
        FrameworkElement firstElement;
        object itemOnCurrentPage = this.GetFirstItemOnCurrentPage(this.FindEndFocusableLeafContainer(this.ItemsHost, true), FocusNavigationDirection.Down, out firstElement);
        container = firstElement;
        if (shouldFocus)
        {
          if (firstElement != null && (itemOnCurrentPage == DependencyProperty.UnsetValue || itemOnCurrentPage is CollectionViewGroupInternal))
            return firstElement.Focus();
          ItemsControl encapsulatingItemsControl = ItemsControl.GetEncapsulatingItemsControl(firstElement);
          if (encapsulatingItemsControl != null)
            return encapsulatingItemsControl.FocusItem(this.NewItemInfo(itemOnCurrentPage, (DependencyObject) firstElement, -1), itemNavigateArgs);
        }
      }
      return false;
    }

    internal void NavigateToItem(ItemsControl.ItemInfo info, ItemsControl.ItemNavigateArgs itemNavigateArgs, bool alwaysAtTopOfViewport = false)
    {
      if (!(info != (ItemsControl.ItemInfo) null))
        return;
      this.NavigateToItem(info.Item, info.Index, itemNavigateArgs, alwaysAtTopOfViewport);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal void NavigateToItem(object item, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      this.NavigateToItem(item, -1, itemNavigateArgs, false);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal void NavigateToItem(object item, int itemIndex, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      this.NavigateToItem(item, itemIndex, itemNavigateArgs, false);
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal void NavigateToItem(object item, ItemsControl.ItemNavigateArgs itemNavigateArgs, bool alwaysAtTopOfViewport)
    {
      this.NavigateToItem(item, -1, itemNavigateArgs, alwaysAtTopOfViewport);
    }

    private void NavigateToItem(object item, int elementIndex, ItemsControl.ItemNavigateArgs itemNavigateArgs, bool alwaysAtTopOfViewport)
    {
      if (item == DependencyProperty.UnsetValue)
        return;
      if (elementIndex == -1)
      {
        elementIndex = this.Items.IndexOf(item);
        if (elementIndex == -1)
          return;
      }
      bool flag = false;
      if (this.ItemsHost != null)
        flag = this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Horizontal;
      FocusNavigationDirection direction = flag ? FocusNavigationDirection.Right : FocusNavigationDirection.Down;
      FrameworkElement container;
      this.MakeVisible(elementIndex, direction, alwaysAtTopOfViewport, out container);
      this.FocusItem(this.NewItemInfo(item, (DependencyObject) container, -1), itemNavigateArgs);
    }

    internal void MakeVisible(FrameworkElement container, FocusNavigationDirection direction, bool alwaysAtTopOfViewport)
    {
      if (this.ScrollHost == null || this.ItemsHost == null)
        return;
      FrameworkElement viewportElement = this.GetViewportElement();
      while (container != null && !this.IsOnCurrentPage(viewportElement, container, direction, false))
      {
        double horizontalOffset = this.ScrollHost.HorizontalOffset;
        double verticalOffset = this.ScrollHost.VerticalOffset;
        container.BringIntoView();
        this.ItemsHost.UpdateLayout();
        if (DoubleUtil.AreClose(horizontalOffset, this.ScrollHost.HorizontalOffset) && DoubleUtil.AreClose(verticalOffset, this.ScrollHost.VerticalOffset))
          break;
      }
      if (container == null || !alwaysAtTopOfViewport)
        return;
      bool flag = this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Horizontal;
      FrameworkElement firstElement;
      this.GetFirstItemOnCurrentPage(container, FocusNavigationDirection.Up, out firstElement);
      while (firstElement != container)
      {
        double horizontalOffset = this.ScrollHost.HorizontalOffset;
        double verticalOffset = this.ScrollHost.VerticalOffset;
        if (flag)
          this.ScrollHost.LineRight();
        else
          this.ScrollHost.LineDown();
        this.ScrollHost.UpdateLayout();
        if (DoubleUtil.AreClose(horizontalOffset, this.ScrollHost.HorizontalOffset) && DoubleUtil.AreClose(verticalOffset, this.ScrollHost.VerticalOffset))
          break;
        this.GetFirstItemOnCurrentPage(container, FocusNavigationDirection.Up, out firstElement);
      }
    }

    internal FrameworkElement GetViewportElement()
    {
      FrameworkElement frameworkElement = (FrameworkElement) this.ScrollHost;
      if (frameworkElement == null)
      {
        frameworkElement = (FrameworkElement) this.ItemsHost;
      }
      else
      {
        ScrollContentPresenter contentPresenter = frameworkElement.GetTemplateChild("PART_ScrollContentPresenter") as ScrollContentPresenter;
        if (contentPresenter != null)
          frameworkElement = (FrameworkElement) contentPresenter;
      }
      return frameworkElement;
    }

    internal static ElementViewportPosition GetElementViewportPosition(FrameworkElement viewPort, UIElement element, FocusNavigationDirection axis, bool fullyVisible)
    {
      Rect elementRect;
      return ItemsControl.GetElementViewportPosition(viewPort, element, axis, fullyVisible, out elementRect);
    }

    internal static ElementViewportPosition GetElementViewportPosition(FrameworkElement viewPort, UIElement element, FocusNavigationDirection axis, bool fullyVisible, out Rect elementRect)
    {
      elementRect = Rect.Empty;
      if (viewPort == null || element == null || !viewPort.IsAncestorOf((DependencyObject) element))
        return ElementViewportPosition.None;
      Rect viewportRect = new Rect(new Point(), viewPort.RenderSize);
      Rect rect1 = new Rect(new Point(), element.RenderSize);
      Rect rect2 = element.TransformToAncestor((Visual) viewPort).TransformBounds(rect1);
      bool flag1 = axis == FocusNavigationDirection.Up || axis == FocusNavigationDirection.Down;
      bool flag2 = axis == FocusNavigationDirection.Left || axis == FocusNavigationDirection.Right;
      elementRect = rect2;
      if (fullyVisible)
      {
        if (viewportRect.Contains(rect2))
          return ElementViewportPosition.CompletelyInViewport;
      }
      else if (flag1)
      {
        if (DoubleUtil.LessThanOrClose(viewportRect.Top, rect2.Top) && DoubleUtil.LessThanOrClose(rect2.Bottom, viewportRect.Bottom))
          return ElementViewportPosition.CompletelyInViewport;
      }
      else if (flag2 && DoubleUtil.LessThanOrClose(viewportRect.Left, rect2.Left) && DoubleUtil.LessThanOrClose(rect2.Right, viewportRect.Right))
        return ElementViewportPosition.CompletelyInViewport;
      if (ItemsControl.ElementIntersectsViewport(viewportRect, rect2))
        return ElementViewportPosition.PartiallyInViewport;
      if (flag1 && DoubleUtil.LessThanOrClose(rect2.Bottom, viewportRect.Top) || flag2 && DoubleUtil.LessThanOrClose(rect2.Right, viewportRect.Left))
        return ElementViewportPosition.BeforeViewport;
      return flag1 && DoubleUtil.LessThanOrClose(viewportRect.Bottom, rect2.Top) || flag2 && DoubleUtil.LessThanOrClose(viewportRect.Right, rect2.Left) ? ElementViewportPosition.AfterViewport : ElementViewportPosition.None;
    }

    internal virtual bool FocusItem(ItemsControl.ItemInfo info, ItemsControl.ItemNavigateArgs itemNavigateArgs)
    {
      object obj = info.Item;
      bool flag = false;
      if (obj != null)
      {
        UIElement uiElement = info.Container as UIElement;
        if (uiElement != null)
          flag = uiElement.Focus();
      }
      if (itemNavigateArgs.DeviceUsed is KeyboardDevice)
        KeyboardNavigation.ShowFocusVisual();
      return flag;
    }

    internal void DoAutoScroll()
    {
      this.DoAutoScroll(this.FocusedInfo);
    }

    internal void DoAutoScroll(ItemsControl.ItemInfo startingInfo)
    {
      FrameworkElement element = this.ScrollHost != null ? (FrameworkElement) this.ScrollHost : (FrameworkElement) this.ItemsHost;
      if (element == null)
        return;
      Point position = Mouse.GetPosition((IInputElement) element);
      Rect rect = new Rect(new Point(), element.RenderSize);
      bool flag = false;
      if (position.Y < rect.Top)
      {
        this.NavigateByLine(startingInfo, FocusNavigationDirection.Up, new ItemsControl.ItemNavigateArgs((InputDevice) Mouse.PrimaryDevice, Keyboard.Modifiers));
        flag = startingInfo != this.FocusedInfo;
      }
      else if (position.Y >= rect.Bottom)
      {
        this.NavigateByLine(startingInfo, FocusNavigationDirection.Down, new ItemsControl.ItemNavigateArgs((InputDevice) Mouse.PrimaryDevice, Keyboard.Modifiers));
        flag = startingInfo != this.FocusedInfo;
      }
      if (flag)
        return;
      if (position.X < rect.Left)
      {
        FocusNavigationDirection direction = FocusNavigationDirection.Left;
        if (this.IsRTL(element))
          direction = FocusNavigationDirection.Right;
        this.NavigateByLine(startingInfo, direction, new ItemsControl.ItemNavigateArgs((InputDevice) Mouse.PrimaryDevice, Keyboard.Modifiers));
      }
      else
      {
        if (position.X < rect.Right)
          return;
        FocusNavigationDirection direction = FocusNavigationDirection.Right;
        if (this.IsRTL(element))
          direction = FocusNavigationDirection.Left;
        this.NavigateByLine(startingInfo, direction, new ItemsControl.ItemNavigateArgs((InputDevice) Mouse.PrimaryDevice, Keyboard.Modifiers));
      }
    }

    internal static DependencyObject TryGetTreeViewItemHeader(DependencyObject element)
    {
      TreeViewItem treeViewItem = element as TreeViewItem;
      if (treeViewItem != null)
        return (DependencyObject) treeViewItem.TryGetHeaderElement();
      return element;
    }

    internal object GetItemOrContainerFromContainer(DependencyObject container)
    {
      object obj = this.ItemContainerGenerator.ItemFromContainer(container);
      if (obj == DependencyProperty.UnsetValue && ItemsControl.ItemsControlFromItemContainer(container) == this && ((IGeneratorHost) this).IsItemItsOwnContainer((object) container))
        obj = (object) container;
      return obj;
    }

    internal static bool EqualsEx(object o1, object o2)
    {
      if (DependencyProperty.UnsetValue == o1)
        return o1 == o2;
      if (DependencyProperty.UnsetValue == o2)
        return false;
      return object.Equals(o1, o2);
    }

    internal ItemsControl.ItemInfo NewItemInfo(object item, DependencyObject container = null, int index = -1)
    {
      return new ItemsControl.ItemInfo(item, container, index).Refresh(this.ItemContainerGenerator);
    }

    internal ItemsControl.ItemInfo ItemInfoFromContainer(DependencyObject container)
    {
      return this.NewItemInfo(this.ItemContainerGenerator.ItemFromContainer(container), container, this.ItemContainerGenerator.IndexFromContainer(container));
    }

    internal ItemsControl.ItemInfo ItemInfoFromIndex(int index)
    {
      if (index < 0)
        return (ItemsControl.ItemInfo) null;
      return this.NewItemInfo(this.Items[index], this.ItemContainerGenerator.ContainerFromIndex(index), index);
    }

    internal ItemsControl.ItemInfo NewUnresolvedItemInfo(object item)
    {
      return new ItemsControl.ItemInfo(item, ItemsControl.ItemInfo.UnresolvedContainer, -1);
    }

    internal DependencyObject ContainerFromItemInfo(ItemsControl.ItemInfo info)
    {
      DependencyObject dependencyObject = info.Container;
      if (dependencyObject == null)
      {
        if (info.Index >= 0)
        {
          dependencyObject = this.ItemContainerGenerator.ContainerFromIndex(info.Index);
          info.Container = dependencyObject;
        }
        else
          dependencyObject = this.ItemContainerGenerator.ContainerFromItem(info.Item);
      }
      return dependencyObject;
    }

    internal void AdjustItemInfoAfterGeneratorChange(ItemsControl.ItemInfo info)
    {
      if (!(info != (ItemsControl.ItemInfo) null))
        return;
      this.AdjustItemInfosAfterGeneratorChange((IEnumerable<ItemsControl.ItemInfo>) new ItemsControl.ItemInfo[1]
      {
        info
      }, false);
    }

    internal void AdjustItemInfosAfterGeneratorChange(IEnumerable<ItemsControl.ItemInfo> list, bool claimUniqueContainer)
    {
      bool flag = false;
      foreach (ItemsControl.ItemInfo itemInfo in list)
      {
        DependencyObject container = itemInfo.Container;
        if (container == null)
          flag = true;
        else if (!ItemsControl.EqualsEx(itemInfo.Item, container.ReadLocalValue(ItemContainerGenerator.ItemForItemContainerProperty)))
        {
          itemInfo.Container = (DependencyObject) null;
          flag = true;
        }
      }
      if (!flag)
        return;
      List<DependencyObject> claimedContainers = new List<DependencyObject>();
      if (claimUniqueContainer)
      {
        foreach (ItemsControl.ItemInfo itemInfo in list)
        {
          DependencyObject container = itemInfo.Container;
          if (container != null)
            claimedContainers.Add(container);
        }
      }
      foreach (ItemsControl.ItemInfo itemInfo in list)
      {
        DependencyObject container = itemInfo.Container;
        if (container == null)
        {
          int itemIndex = itemInfo.Index;
          if (itemIndex >= 0)
          {
            container = this.ItemContainerGenerator.ContainerFromIndex(itemIndex);
          }
          else
          {
            object item = itemInfo.Item;
            this.ItemContainerGenerator.FindItem((Func<object, DependencyObject, bool>) ((o, d) =>
            {
              if (object.Equals(o, item))
                return !claimedContainers.Contains(d);
              return false;
            }), out container, out itemIndex);
          }
          if (container != null)
          {
            itemInfo.Container = container;
            itemInfo.Index = itemIndex;
            if (claimUniqueContainer)
              claimedContainers.Add(container);
          }
        }
      }
    }

    internal void AdjustItemInfo(NotifyCollectionChangedEventArgs e, ItemsControl.ItemInfo info)
    {
      if (!(info != (ItemsControl.ItemInfo) null))
        return;
      ItemsControl.ItemInfo[] itemInfoArray = new ItemsControl.ItemInfo[1]
      {
        info
      };
      this.AdjustItemInfos(e, (IEnumerable<ItemsControl.ItemInfo>) itemInfoArray);
    }

    internal void AdjustItemInfos(NotifyCollectionChangedEventArgs e, IEnumerable<ItemsControl.ItemInfo> list)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          using (IEnumerator<ItemsControl.ItemInfo> enumerator = list.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ItemsControl.ItemInfo current = enumerator.Current;
              int index = current.Index;
              if (index >= e.NewStartingIndex)
                current.Index = index + 1;
            }
            break;
          }
        case NotifyCollectionChangedAction.Remove:
          using (IEnumerator<ItemsControl.ItemInfo> enumerator = list.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ItemsControl.ItemInfo current = enumerator.Current;
              int index = current.Index;
              if (index > e.OldStartingIndex)
                current.Index = index - 1;
              else if (index == e.OldStartingIndex)
                current.Index = -1;
            }
            break;
          }
        case NotifyCollectionChangedAction.Move:
          int num1;
          int num2;
          int num3;
          if (e.OldStartingIndex < e.NewStartingIndex)
          {
            num1 = e.OldStartingIndex + 1;
            num2 = e.NewStartingIndex;
            num3 = -1;
          }
          else
          {
            num1 = e.NewStartingIndex;
            num2 = e.OldStartingIndex - 1;
            num3 = 1;
          }
          using (IEnumerator<ItemsControl.ItemInfo> enumerator = list.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ItemsControl.ItemInfo current = enumerator.Current;
              int index = current.Index;
              if (index == e.OldStartingIndex)
                current.Index = e.NewStartingIndex;
              else if (num1 <= index && index <= num2)
                current.Index = index + num3;
            }
            break;
          }
        case NotifyCollectionChangedAction.Reset:
          using (IEnumerator<ItemsControl.ItemInfo> enumerator = list.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.Index = -1;
            break;
          }
      }
    }

    internal ItemsControl.ItemInfo LeaseItemInfo(ItemsControl.ItemInfo info, bool ensureIndex = false)
    {
      if (info.Index < 0)
      {
        info = this.NewItemInfo(info.Item, (DependencyObject) null, -1);
        if (ensureIndex && info.Index < 0)
          info.Index = this.Items.IndexOf(info.Item);
      }
      return info;
    }

    internal void RefreshItemInfo(ItemsControl.ItemInfo info)
    {
      if (!(info != (ItemsControl.ItemInfo) null))
        return;
      info.Refresh(this.ItemContainerGenerator);
    }

    object IContainItemStorage.ReadItemValue(object item, DependencyProperty dp)
    {
      return MS.Internal.Helper.ReadItemValue((DependencyObject) this, item, dp.GlobalIndex);
    }

    void IContainItemStorage.StoreItemValue(object item, DependencyProperty dp, object value)
    {
      MS.Internal.Helper.StoreItemValue((DependencyObject) this, item, dp.GlobalIndex, value);
    }

    void IContainItemStorage.ClearItemValue(object item, DependencyProperty dp)
    {
      MS.Internal.Helper.ClearItemValue((DependencyObject) this, item, dp.GlobalIndex);
    }

    void IContainItemStorage.ClearValue(DependencyProperty dp)
    {
      MS.Internal.Helper.ClearItemValueStorage((DependencyObject) this, new int[1]
      {
        dp.GlobalIndex
      });
    }

    void IContainItemStorage.Clear()
    {
      MS.Internal.Helper.ClearItemValueStorage((DependencyObject) this);
    }

    /// <summary>
    /// Provides a string representation of the <see cref="T:System.Windows.Controls.ItemsControl"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// The string representation of the object.
    /// </returns>
    public override string ToString()
    {
      return System.Windows.SR.Get("ToStringFormatString_ItemsControl", (object) this.GetType(), (object) (this.HasItems ? this.Items.Count : 0));
    }

    private static void OnScrollingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ItemsControl.ShouldCoerceScrollUnitField.SetValue(d, true);
      d.CoerceValue(VirtualizingPanel.ScrollUnitProperty);
    }

    private static object CoerceScrollingMode(DependencyObject d, object baseValue)
    {
      if (ItemsControl.ShouldCoerceScrollUnitField.GetValue(d))
      {
        ItemsControl.ShouldCoerceScrollUnitField.SetValue(d, false);
        BaseValueSource baseValueSource = DependencyPropertyHelper.GetValueSource(d, VirtualizingPanel.ScrollUnitProperty).BaseValueSource;
        if (((ItemsControl) d).IsGrouping && baseValueSource == BaseValueSource.Default)
          return (object) ScrollUnit.Pixel;
      }
      return baseValue;
    }

    private static void OnCacheSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ItemsControl.ShouldCoerceCacheSizeField.SetValue(d, true);
      d.CoerceValue(e.Property);
    }

    private static object CoerceVirtualizationCacheLengthUnit(DependencyObject d, object baseValue)
    {
      if (ItemsControl.ShouldCoerceCacheSizeField.GetValue(d))
      {
        ItemsControl.ShouldCoerceCacheSizeField.SetValue(d, false);
        BaseValueSource baseValueSource = DependencyPropertyHelper.GetValueSource(d, VirtualizingPanel.CacheLengthUnitProperty).BaseValueSource;
        if (!((ItemsControl) d).IsGrouping && !(d is TreeView) && baseValueSource == BaseValueSource.Default)
          return (object) VirtualizationCacheLengthUnit.Item;
      }
      return baseValue;
    }

    private void CreateItemCollectionAndGenerator()
    {
      this._items = new ItemCollection((DependencyObject) this);
      this._items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemCollectionChanged1);
      this._itemContainerGenerator = new ItemContainerGenerator((IGeneratorHost) this);
      this._itemContainerGenerator.ChangeAlternationCount();
      this._items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemCollectionChanged2);
      if (this.IsInitPending)
        this._items.BeginInit();
      else if (this.IsInitialized)
      {
        this._items.BeginInit();
        this._items.EndInit();
      }
      this._groupStyle.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnGroupStyleChanged);
    }

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ItemsControl itemsControl = (ItemsControl) d;
      IEnumerable oldValue = (IEnumerable) e.OldValue;
      IEnumerable newValue = (IEnumerable) e.NewValue;
      ((IContainItemStorage) itemsControl).Clear();
      BindingExpressionBase beb = BindingOperations.GetBindingExpressionBase(d, ItemsControl.ItemsSourceProperty);
      if (beb != null)
        itemsControl.Items.SetItemsSource(newValue, (Func<object, object>) (x => beb.GetSourceItem(x)));
      else if (e.NewValue != null)
        itemsControl.Items.SetItemsSource(newValue, (Func<object, object>) null);
      else
        itemsControl.Items.ClearItemsSource();
      itemsControl.OnItemsSourceChanged(oldValue, newValue);
    }

    private void OnItemCollectionChanged1(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.AdjustItemInfoOverride(e);
    }

    private void OnItemCollectionChanged2(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.SetValue(ItemsControl.HasItemsPropertyKey, this._items != null && !this._items.IsEmpty);
      if (this._focusedInfo != (ItemsControl.ItemInfo) null && this._focusedInfo.Index < 0)
        this._focusedInfo = (ItemsControl.ItemInfo) null;
      if (e.Action == NotifyCollectionChangedAction.Reset)
        ((IContainItemStorage) this).Clear();
      this.OnItemsChanged(e);
    }

    private static void OnDisplayMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ItemsControl itemsControl = (ItemsControl) d;
      itemsControl.OnDisplayMemberPathChanged((string) e.OldValue, (string) e.NewValue);
      itemsControl.UpdateDisplayMemberTemplateSelector();
    }

    private void UpdateDisplayMemberTemplateSelector()
    {
      if (!string.IsNullOrEmpty(this.DisplayMemberPath) || !string.IsNullOrEmpty(this.ItemStringFormat))
      {
        DataTemplateSelector templateSelector = this.ItemTemplateSelector;
        if (templateSelector != null && !(templateSelector is DisplayMemberTemplateSelector) && (this.ReadLocalValue(ItemsControl.ItemTemplateSelectorProperty) != DependencyProperty.UnsetValue || this.ReadLocalValue(ItemsControl.DisplayMemberPathProperty) == DependencyProperty.UnsetValue))
          throw new InvalidOperationException(System.Windows.SR.Get("DisplayMemberPathAndItemTemplateSelectorDefined"));
        this.ItemTemplateSelector = (DataTemplateSelector) new DisplayMemberTemplateSelector(this.DisplayMemberPath, this.ItemStringFormat);
      }
      else
      {
        if (!(this.ItemTemplateSelector is DisplayMemberTemplateSelector))
          return;
        this.ClearValue(ItemsControl.ItemTemplateSelectorProperty);
      }
    }

    private static void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnItemTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue);
    }

    private static void OnItemTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnItemTemplateSelectorChanged((DataTemplateSelector) e.OldValue, (DataTemplateSelector) e.NewValue);
    }

    private static void OnItemStringFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ItemsControl itemsControl = (ItemsControl) d;
      itemsControl.OnItemStringFormatChanged((string) e.OldValue, (string) e.NewValue);
      itemsControl.UpdateDisplayMemberTemplateSelector();
    }

    private static void OnItemBindingGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnItemBindingGroupChanged((BindingGroup) e.OldValue, (BindingGroup) e.NewValue);
    }

    private void CheckTemplateSource()
    {
      if (string.IsNullOrEmpty(this.DisplayMemberPath))
      {
        MS.Internal.Helper.CheckTemplateAndTemplateSelector("Item", ItemsControl.ItemTemplateProperty, ItemsControl.ItemTemplateSelectorProperty, (DependencyObject) this);
      }
      else
      {
        if (!(this.ItemTemplateSelector is DisplayMemberTemplateSelector))
          throw new InvalidOperationException(System.Windows.SR.Get("ItemTemplateSelectorBreaksDisplayMemberPath"));
        if (MS.Internal.Helper.IsTemplateDefined(ItemsControl.ItemTemplateProperty, (DependencyObject) this))
          throw new InvalidOperationException(System.Windows.SR.Get("DisplayMemberPathAndItemTemplateDefined"));
      }
    }

    private static void OnItemContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnItemContainerStyleChanged((Style) e.OldValue, (Style) e.NewValue);
    }

    private static void OnItemContainerStyleSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnItemContainerStyleSelectorChanged((StyleSelector) e.OldValue, (StyleSelector) e.NewValue);
    }

    private static ItemsPanelTemplate GetDefaultItemsPanelTemplate()
    {
      ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate(new FrameworkElementFactory(typeof (StackPanel)));
      itemsPanelTemplate.Seal();
      return itemsPanelTemplate;
    }

    private static void OnItemsPanelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnItemsPanelChanged((ItemsPanelTemplate) e.OldValue, (ItemsPanelTemplate) e.NewValue);
    }

    private static void OnIsGroupingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnIsGroupingChanged(e);
    }

    private void OnGroupStyleChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this._itemContainerGenerator == null)
        return;
      this._itemContainerGenerator.Refresh();
    }

    private static void OnGroupStyleSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnGroupStyleSelectorChanged((GroupStyleSelector) e.OldValue, (GroupStyleSelector) e.NewValue);
    }

    private static void OnAlternationCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ItemsControl) d).OnAlternationCountChanged((int) e.OldValue, (int) e.NewValue);
    }

    private static bool IsContainerForItemsControl(DependencyObject element, ItemsControl itemsControl)
    {
      return element.ContainsValue(ItemContainerGenerator.ItemForItemContainerProperty) && (itemsControl == null || itemsControl == ItemsControl.ItemsControlFromItemContainer(element));
    }

    private bool NavigateByLineInternal(object startingItem, FocusNavigationDirection direction, FrameworkElement startingElement, ItemsControl.ItemNavigateArgs itemNavigateArgs, bool shouldFocus, out FrameworkElement container)
    {
      container = (FrameworkElement) null;
      if (startingItem == null && (startingElement == null || startingElement == this))
        return this.NavigateToStartInternal(itemNavigateArgs, shouldFocus, out container);
      if (startingElement == null || !this.ItemsHost.IsAncestorOf((DependencyObject) startingElement))
      {
        startingElement = (FrameworkElement) this.ScrollHost;
      }
      else
      {
        for (DependencyObject parent = VisualTreeHelper.GetParent((DependencyObject) startingElement); parent != null && parent != this.ItemsHost; parent = VisualTreeHelper.GetParent(parent))
        {
          switch (KeyboardNavigation.GetDirectionalNavigation(parent))
          {
            case KeyboardNavigationMode.Contained:
            case KeyboardNavigationMode.Cycle:
              return false;
            default:
              goto default;
          }
        }
      }
      bool isHorizontal = this.ItemsHost != null && this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Horizontal;
      bool treeViewNavigation = this is TreeView;
      FrameworkElement element = KeyboardNavigation.Current.PredictFocusedElement((DependencyObject) startingElement, direction, treeViewNavigation) as FrameworkElement;
      if (this.ScrollHost != null)
      {
        bool flag1 = false;
        FrameworkElement viewportElement = this.GetViewportElement();
        VirtualizingPanel virtualizingPanel = this.ItemsHost as VirtualizingPanel;
        bool flag2 = KeyboardNavigation.GetDirectionalNavigation((DependencyObject) this) == KeyboardNavigationMode.Cycle;
        while (true)
        {
          if (element != null)
          {
            if (virtualizingPanel != null && this.ScrollHost.CanContentScroll && VirtualizingPanel.GetIsVirtualizing((DependencyObject) this))
            {
              Rect elementRect1;
              switch (ItemsControl.GetElementViewportPosition(viewportElement, (UIElement) (ItemsControl.TryGetTreeViewItemHeader((DependencyObject) element) as FrameworkElement), direction, false, out elementRect1))
              {
                case ElementViewportPosition.CompletelyInViewport:
                case ElementViewportPosition.PartiallyInViewport:
                  if (flag2)
                  {
                    Rect elementRect2;
                    int num = (int) ItemsControl.GetElementViewportPosition(viewportElement, (UIElement) startingElement, direction, false, out elementRect2);
                    if (this.IsInDirectionForLineNavigation(elementRect2, elementRect1, direction, isHorizontal))
                      goto label_31;
                    else
                      break;
                  }
                  else
                    goto label_31;
              }
              element = (FrameworkElement) null;
            }
            else
              goto label_31;
          }
          double horizontalOffset = this.ScrollHost.HorizontalOffset;
          double verticalOffset = this.ScrollHost.VerticalOffset;
          switch (direction)
          {
            case FocusNavigationDirection.Up:
              flag1 = true;
              if (isHorizontal)
              {
                this.ScrollHost.LineLeft();
                break;
              }
              this.ScrollHost.LineUp();
              break;
            case FocusNavigationDirection.Down:
              flag1 = true;
              if (isHorizontal)
              {
                this.ScrollHost.LineRight();
                break;
              }
              this.ScrollHost.LineDown();
              break;
          }
          this.ScrollHost.UpdateLayout();
          if ((!DoubleUtil.AreClose(horizontalOffset, this.ScrollHost.HorizontalOffset) || !DoubleUtil.AreClose(verticalOffset, this.ScrollHost.VerticalOffset)) && (direction != FocusNavigationDirection.Down || this.ScrollHost.VerticalOffset <= this.ScrollHost.ExtentHeight && this.ScrollHost.HorizontalOffset <= this.ScrollHost.ExtentWidth) && (direction != FocusNavigationDirection.Up || this.ScrollHost.VerticalOffset >= 0.0 && this.ScrollHost.HorizontalOffset >= 0.0))
            element = KeyboardNavigation.Current.PredictFocusedElement((DependencyObject) startingElement, direction, treeViewNavigation) as FrameworkElement;
          else
            break;
        }
        if (flag2)
        {
          if (direction == FocusNavigationDirection.Up)
            return this.NavigateToEndInternal(itemNavigateArgs, true, out container);
          if (direction == FocusNavigationDirection.Down)
            return this.NavigateToStartInternal(itemNavigateArgs, true, out container);
        }
label_31:
        if (flag1 && element != null && this.ItemsHost.IsAncestorOf((DependencyObject) element))
          this.AdjustOffsetToAlignWithEdge(element, direction);
      }
      if (element != null && this.ItemsHost.IsAncestorOf((DependencyObject) element))
      {
        ItemsControl itemsControl = (ItemsControl) null;
        object encapsulatingItem = ItemsControl.GetEncapsulatingItem(element, out container, out itemsControl);
        container = element;
        if (!shouldFocus)
          return false;
        if (encapsulatingItem == DependencyProperty.UnsetValue || encapsulatingItem is CollectionViewGroupInternal)
          return element.Focus();
        if (itemsControl != null)
          return itemsControl.FocusItem(this.NewItemInfo(encapsulatingItem, (DependencyObject) container, -1), itemNavigateArgs);
      }
      return false;
    }

    private bool NavigateByPageInternal(object startingItem, FocusNavigationDirection direction, FrameworkElement startingElement, ItemsControl.ItemNavigateArgs itemNavigateArgs, bool shouldFocus, out FrameworkElement container)
    {
      container = (FrameworkElement) null;
      if (startingItem == null && (startingElement == null || startingElement == this))
        return this.NavigateToFirstItemOnCurrentPage(startingItem, direction, itemNavigateArgs, shouldFocus, out container);
      FrameworkElement firstElement;
      object itemOnCurrentPage = this.GetFirstItemOnCurrentPage(startingElement, direction, out firstElement);
      if ((object.Equals(startingItem, itemOnCurrentPage) || object.Equals((object) startingElement, (object) firstElement)) && this.ScrollHost != null)
      {
        bool flag = this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Horizontal;
        do
        {
          double horizontalOffset = this.ScrollHost.HorizontalOffset;
          double verticalOffset = this.ScrollHost.VerticalOffset;
          switch (direction)
          {
            case FocusNavigationDirection.Up:
              if (flag)
              {
                this.ScrollHost.PageLeft();
                break;
              }
              this.ScrollHost.PageUp();
              break;
            case FocusNavigationDirection.Down:
              if (flag)
              {
                this.ScrollHost.PageRight();
                break;
              }
              this.ScrollHost.PageDown();
              break;
          }
          this.ScrollHost.UpdateLayout();
          if (!DoubleUtil.AreClose(horizontalOffset, this.ScrollHost.HorizontalOffset) || !DoubleUtil.AreClose(verticalOffset, this.ScrollHost.VerticalOffset))
            itemOnCurrentPage = this.GetFirstItemOnCurrentPage(startingElement, direction, out firstElement);
          else
            break;
        }
        while (itemOnCurrentPage == DependencyProperty.UnsetValue);
      }
      container = firstElement;
      if (shouldFocus)
      {
        if (firstElement != null && (itemOnCurrentPage == DependencyProperty.UnsetValue || itemOnCurrentPage is CollectionViewGroupInternal))
          return firstElement.Focus();
        ItemsControl encapsulatingItemsControl = ItemsControl.GetEncapsulatingItemsControl(firstElement);
        if (encapsulatingItemsControl != null)
          return encapsulatingItemsControl.FocusItem(this.NewItemInfo(itemOnCurrentPage, (DependencyObject) firstElement, -1), itemNavigateArgs);
      }
      return false;
    }

    private FrameworkElement FindEndFocusableLeafContainer(Panel itemsHost, bool last)
    {
      if (itemsHost == null)
        return (FrameworkElement) null;
      UIElementCollection children = itemsHost.Children;
      if (children != null)
      {
        int count = children.Count;
        int index = last ? count - 1 : 0;
        int num = last ? -1 : 1;
        while (index >= 0 && index < count)
        {
          FrameworkElement frameworkElement1 = children[index] as FrameworkElement;
          if (frameworkElement1 != null)
          {
            ItemsControl itemsControl = frameworkElement1 as ItemsControl;
            FrameworkElement frameworkElement2 = (FrameworkElement) null;
            if (itemsControl != null)
            {
              if (itemsControl.ItemsHost != null)
                frameworkElement2 = this.FindEndFocusableLeafContainer(itemsControl.ItemsHost, last);
            }
            else
            {
              GroupItem groupItem = frameworkElement1 as GroupItem;
              if (groupItem != null && groupItem.ItemsHost != null)
                frameworkElement2 = this.FindEndFocusableLeafContainer(groupItem.ItemsHost, last);
            }
            if (frameworkElement2 != null)
              return frameworkElement2;
            if (FrameworkElement.KeyboardNavigation.IsFocusableInternal((DependencyObject) frameworkElement1))
              return frameworkElement1;
          }
          index += num;
        }
      }
      return (FrameworkElement) null;
    }

    private object FindFocusable(int startIndex, int direction, out int foundIndex, out FrameworkElement foundContainer)
    {
      if (this.HasItems)
      {
        int count = this.Items.Count;
        while (startIndex >= 0 && startIndex < count)
        {
          FrameworkElement frameworkElement = this.ItemContainerGenerator.ContainerFromIndex(startIndex) as FrameworkElement;
          if (frameworkElement == null || Keyboard.IsFocusable((DependencyObject) frameworkElement))
          {
            foundIndex = startIndex;
            foundContainer = frameworkElement;
            return this.Items[startIndex];
          }
          startIndex += direction;
        }
      }
      foundIndex = -1;
      foundContainer = (FrameworkElement) null;
      return (object) null;
    }

    private void AdjustOffsetToAlignWithEdge(FrameworkElement element, FocusNavigationDirection direction)
    {
      if (VirtualizingPanel.GetScrollUnit((DependencyObject) this) == ScrollUnit.Item)
        return;
      ScrollViewer scrollHost = this.ScrollHost;
      FrameworkElement viewportElement = this.GetViewportElement();
      element = ItemsControl.TryGetTreeViewItemHeader((DependencyObject) element) as FrameworkElement;
      Rect rect1 = new Rect(new Point(), element.RenderSize);
      Rect rect2 = element.TransformToAncestor((Visual) viewportElement).TransformBounds(rect1);
      bool flag = this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Horizontal;
      if (direction == FocusNavigationDirection.Down)
      {
        if (flag)
          scrollHost.ScrollToHorizontalOffset(scrollHost.HorizontalOffset - scrollHost.ViewportWidth + rect2.Right);
        else
          scrollHost.ScrollToVerticalOffset(scrollHost.VerticalOffset - scrollHost.ViewportHeight + rect2.Bottom);
      }
      else
      {
        if (direction != FocusNavigationDirection.Up)
          return;
        if (flag)
          scrollHost.ScrollToHorizontalOffset(scrollHost.HorizontalOffset + rect2.Left);
        else
          scrollHost.ScrollToVerticalOffset(scrollHost.VerticalOffset + rect2.Top);
      }
    }

    private void MakeVisible(int index, FocusNavigationDirection direction, bool alwaysAtTopOfViewport, out FrameworkElement container)
    {
      container = (FrameworkElement) null;
      if (index < 0)
        return;
      container = this.ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
      if (container == null)
      {
        VirtualizingPanel virtualizingPanel = this.ItemsHost as VirtualizingPanel;
        if (virtualizingPanel != null)
        {
          virtualizingPanel.BringIndexIntoView(index);
          this.UpdateLayout();
          container = this.ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
        }
      }
      this.MakeVisible(container, direction, alwaysAtTopOfViewport);
    }

    private void MakeVisible(ItemsControl.ItemInfo info, FocusNavigationDirection direction, out FrameworkElement container)
    {
      if (info != (ItemsControl.ItemInfo) null)
      {
        this.MakeVisible(info.Index, direction, false, out container);
        info.Container = (DependencyObject) container;
      }
      else
        this.MakeVisible(-1, direction, false, out container);
    }

    private bool NavigateToFirstItemOnCurrentPage(object startingItem, FocusNavigationDirection direction, ItemsControl.ItemNavigateArgs itemNavigateArgs, bool shouldFocus, out FrameworkElement container)
    {
      object itemOnCurrentPage = this.GetFirstItemOnCurrentPage(this.ItemContainerGenerator.ContainerFromItem(startingItem) as FrameworkElement, direction, out container);
      if (itemOnCurrentPage != DependencyProperty.UnsetValue && shouldFocus)
        return this.FocusItem(this.NewItemInfo(itemOnCurrentPage, (DependencyObject) container, -1), itemNavigateArgs);
      return false;
    }

    private object GetFirstItemOnCurrentPage(FrameworkElement startingElement, FocusNavigationDirection direction, out FrameworkElement firstElement)
    {
      bool flag1 = this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Horizontal;
      bool flag2 = this.ItemsHost.HasLogicalOrientation && this.ItemsHost.LogicalOrientation == Orientation.Vertical;
      if (this.ScrollHost != null && this.ScrollHost.CanContentScroll && (VirtualizingPanel.GetScrollUnit((DependencyObject) this) == ScrollUnit.Item && !(this is TreeView)) && !this.IsGrouping)
      {
        int foundIndex = -1;
        if (flag2)
        {
          if (direction == FocusNavigationDirection.Up)
            return this.FindFocusable((int) this.ScrollHost.VerticalOffset, 1, out foundIndex, out firstElement);
          return this.FindFocusable((int) (this.ScrollHost.VerticalOffset + Math.Max(this.ScrollHost.ViewportHeight - 1.0, 0.0)), -1, out foundIndex, out firstElement);
        }
        if (flag1)
        {
          if (direction == FocusNavigationDirection.Up)
            return this.FindFocusable((int) this.ScrollHost.HorizontalOffset, 1, out foundIndex, out firstElement);
          return this.FindFocusable((int) (this.ScrollHost.HorizontalOffset + Math.Max(this.ScrollHost.ViewportWidth - 1.0, 0.0)), -1, out foundIndex, out firstElement);
        }
      }
      if (startingElement != null)
      {
        if (flag1)
        {
          if (direction == FocusNavigationDirection.Up)
            direction = FocusNavigationDirection.Left;
          else if (direction == FocusNavigationDirection.Down)
            direction = FocusNavigationDirection.Right;
        }
        FrameworkElement viewportElement = this.GetViewportElement();
        bool treeViewNavigation = this is TreeView;
        FrameworkElement element = KeyboardNavigation.Current.PredictFocusedElementAtViewportEdge((DependencyObject) startingElement, direction, treeViewNavigation, viewportElement, (DependencyObject) viewportElement) as FrameworkElement;
        object obj = (object) null;
        firstElement = (FrameworkElement) null;
        if (element != null)
          obj = ItemsControl.GetEncapsulatingItem(element, out firstElement);
        if (element == null || obj == DependencyProperty.UnsetValue)
        {
          switch (ItemsControl.GetElementViewportPosition(viewportElement, (UIElement) startingElement, direction, false))
          {
            case ElementViewportPosition.CompletelyInViewport:
            case ElementViewportPosition.PartiallyInViewport:
              element = startingElement;
              obj = ItemsControl.GetEncapsulatingItem(element, out firstElement);
              break;
          }
        }
        if (obj != null && obj is CollectionViewGroupInternal)
          firstElement = element;
        return obj;
      }
      firstElement = (FrameworkElement) null;
      return (object) null;
    }

    private bool IsOnCurrentPage(object item, FocusNavigationDirection axis)
    {
      FrameworkElement frameworkElement = this.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
      if (frameworkElement == null)
        return false;
      return ItemsControl.GetElementViewportPosition(this.GetViewportElement(), (UIElement) frameworkElement, axis, false) == ElementViewportPosition.CompletelyInViewport;
    }

    private bool IsOnCurrentPage(FrameworkElement element, FocusNavigationDirection axis)
    {
      return ItemsControl.GetElementViewportPosition(this.GetViewportElement(), (UIElement) element, axis, false) == ElementViewportPosition.CompletelyInViewport;
    }

    private bool IsOnCurrentPage(FrameworkElement viewPort, FrameworkElement element, FocusNavigationDirection axis, bool fullyVisible)
    {
      return ItemsControl.GetElementViewportPosition(viewPort, (UIElement) element, axis, fullyVisible) == ElementViewportPosition.CompletelyInViewport;
    }

    private static bool ElementIntersectsViewport(Rect viewportRect, Rect elementRect)
    {
      return !viewportRect.IsEmpty && !elementRect.IsEmpty && (!DoubleUtil.LessThan(elementRect.Right, viewportRect.Left) && !LayoutDoubleUtil.AreClose(elementRect.Right, viewportRect.Left)) && (!DoubleUtil.GreaterThan(elementRect.Left, viewportRect.Right) && !LayoutDoubleUtil.AreClose(elementRect.Left, viewportRect.Right) && (!DoubleUtil.LessThan(elementRect.Bottom, viewportRect.Top) && !LayoutDoubleUtil.AreClose(elementRect.Bottom, viewportRect.Top))) && (!DoubleUtil.GreaterThan(elementRect.Top, viewportRect.Bottom) && !LayoutDoubleUtil.AreClose(elementRect.Top, viewportRect.Bottom));
    }

    private bool IsInDirectionForLineNavigation(Rect fromRect, Rect toRect, FocusNavigationDirection direction, bool isHorizontal)
    {
      if (direction == FocusNavigationDirection.Down)
      {
        if (isHorizontal)
          return DoubleUtil.GreaterThanOrClose(toRect.Left, fromRect.Left);
        return DoubleUtil.GreaterThanOrClose(toRect.Top, fromRect.Top);
      }
      if (direction != FocusNavigationDirection.Up)
        return false;
      if (isHorizontal)
        return DoubleUtil.LessThanOrClose(toRect.Right, fromRect.Right);
      return DoubleUtil.LessThanOrClose(toRect.Bottom, fromRect.Bottom);
    }

    private static void OnGotFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
      ItemsControl itemsControl = (ItemsControl) sender;
      UIElement uiElement1 = e.OriginalSource as UIElement;
      if (uiElement1 == null || uiElement1 == itemsControl)
        return;
      object obj = itemsControl.ItemContainerGenerator.ItemFromContainer((DependencyObject) uiElement1);
      if (obj != DependencyProperty.UnsetValue)
      {
        itemsControl._focusedInfo = itemsControl.NewItemInfo(obj, (DependencyObject) uiElement1, -1);
      }
      else
      {
        if (!(itemsControl._focusedInfo != (ItemsControl.ItemInfo) null))
          return;
        UIElement uiElement2 = itemsControl._focusedInfo.Container as UIElement;
        if (uiElement2 != null && MS.Internal.Helper.IsAnyAncestorOf((DependencyObject) uiElement2, (DependencyObject) uiElement1))
          return;
        itemsControl._focusedInfo = (ItemsControl.ItemInfo) null;
      }
    }

    private bool IsRTL(FrameworkElement element)
    {
      return element.FlowDirection == FlowDirection.RightToLeft;
    }

    private static ItemsControl GetEncapsulatingItemsControl(FrameworkElement element)
    {
      for (; element != null; element = VisualTreeHelper.GetParent((DependencyObject) element) as FrameworkElement)
      {
        ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer((DependencyObject) element);
        if (itemsControl != null)
          return itemsControl;
      }
      return (ItemsControl) null;
    }

    private static object GetEncapsulatingItem(FrameworkElement element, out FrameworkElement container)
    {
      ItemsControl itemsControl = (ItemsControl) null;
      return ItemsControl.GetEncapsulatingItem(element, out container, out itemsControl);
    }

    private static object GetEncapsulatingItem(FrameworkElement element, out FrameworkElement container, out ItemsControl itemsControl)
    {
      object obj = DependencyProperty.UnsetValue;
      itemsControl = (ItemsControl) null;
      for (; element != null; element = VisualTreeHelper.GetParent((DependencyObject) element) as FrameworkElement)
      {
        itemsControl = ItemsControl.ItemsControlFromItemContainer((DependencyObject) element);
        if (itemsControl != null)
        {
          obj = itemsControl.ItemContainerGenerator.ItemFromContainer((DependencyObject) element);
          if (obj != DependencyProperty.UnsetValue)
            break;
        }
      }
      container = element;
      return obj;
    }

    private void ApplyItemContainerStyle(DependencyObject container, object item)
    {
      FrameworkObject frameworkObject = new FrameworkObject(container);
      if (!frameworkObject.IsStyleSetFromGenerator && container.ReadLocalValue(FrameworkElement.StyleProperty) != DependencyProperty.UnsetValue)
        return;
      Style style = this.ItemContainerStyle;
      if (style == null && this.ItemContainerStyleSelector != null)
        style = this.ItemContainerStyleSelector.SelectStyle(item, container);
      if (style != null)
      {
        if (!style.TargetType.IsInstanceOfType((object) container))
          throw new InvalidOperationException(System.Windows.SR.Get("StyleForWrongType", (object) style.TargetType.Name, (object) container.GetType().Name));
        frameworkObject.Style = style;
        frameworkObject.IsStyleSetFromGenerator = true;
      }
      else
      {
        if (!frameworkObject.IsStyleSetFromGenerator)
          return;
        frameworkObject.IsStyleSetFromGenerator = false;
        container.ClearValue(FrameworkElement.StyleProperty);
      }
    }

    private void RemoveItemContainerStyle(DependencyObject container)
    {
      if (!new FrameworkObject(container).IsStyleSetFromGenerator)
        return;
      container.ClearValue(FrameworkElement.StyleProperty);
    }

    internal class ItemNavigateArgs
    {
      private InputDevice _deviceUsed;
      private ModifierKeys _modifierKeys;
      private static ItemsControl.ItemNavigateArgs _empty;

      public InputDevice DeviceUsed
      {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
        {
          return this._deviceUsed;
        }
      }

      public static ItemsControl.ItemNavigateArgs Empty
      {
        get
        {
          if (ItemsControl.ItemNavigateArgs._empty == null)
            ItemsControl.ItemNavigateArgs._empty = new ItemsControl.ItemNavigateArgs((InputDevice) null, ModifierKeys.None);
          return ItemsControl.ItemNavigateArgs._empty;
        }
      }

      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
      public ItemNavigateArgs(InputDevice deviceUsed, ModifierKeys modifierKeys)
      {
        this._deviceUsed = deviceUsed;
        this._modifierKeys = modifierKeys;
      }
    }

    [DebuggerDisplay("Index: {Index}  Item: {Item}")]
    internal class ItemInfo
    {
      internal static readonly DependencyObject SentinelContainer = new DependencyObject();
      internal static readonly DependencyObject UnresolvedContainer = new DependencyObject();
      internal static readonly DependencyObject KeyContainer = new DependencyObject();
      internal static readonly DependencyObject RemovedContainer = new DependencyObject();

      internal object Item { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; private set; }

      internal DependencyObject Container { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

      internal int Index { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

      internal bool IsResolved
      {
        get
        {
          return this.Container != ItemsControl.ItemInfo.UnresolvedContainer;
        }
      }

      internal bool IsKey
      {
        get
        {
          return this.Container == ItemsControl.ItemInfo.KeyContainer;
        }
      }

      internal bool IsRemoved
      {
        get
        {
          return this.Container == ItemsControl.ItemInfo.RemovedContainer;
        }
      }

      public ItemInfo(object item, DependencyObject container = null, int index = -1)
      {
        this.Item = item;
        this.Container = container;
        this.Index = index;
      }

      public static bool operator ==(ItemsControl.ItemInfo info1, ItemsControl.ItemInfo info2)
      {
        return object.Equals((object) info1, (object) info2);
      }

      public static bool operator !=(ItemsControl.ItemInfo info1, ItemsControl.ItemInfo info2)
      {
        return !object.Equals((object) info1, (object) info2);
      }

      internal ItemsControl.ItemInfo Clone()
      {
        return new ItemsControl.ItemInfo(this.Item, this.Container, this.Index);
      }

      internal static ItemsControl.ItemInfo Key(ItemsControl.ItemInfo info)
      {
        if (info.Container != ItemsControl.ItemInfo.UnresolvedContainer)
          return info;
        return new ItemsControl.ItemInfo(info.Item, ItemsControl.ItemInfo.KeyContainer, -1);
      }

      public override int GetHashCode()
      {
        if (this.Item == null)
          return 314159;
        return this.Item.GetHashCode();
      }

      public override bool Equals(object o)
      {
        if (o == this)
          return true;
        ItemsControl.ItemInfo that = o as ItemsControl.ItemInfo;
        if (that == (ItemsControl.ItemInfo) null)
          return false;
        return this.Equals(that, false);
      }

      internal bool Equals(ItemsControl.ItemInfo that, bool matchUnresolved)
      {
        if (this.IsRemoved || that.IsRemoved || !ItemsControl.EqualsEx(this.Item, that.Item))
          return false;
        if (this.Container == ItemsControl.ItemInfo.KeyContainer)
        {
          if (!matchUnresolved)
            return that.Container != ItemsControl.ItemInfo.UnresolvedContainer;
          return true;
        }
        if (that.Container == ItemsControl.ItemInfo.KeyContainer)
        {
          if (!matchUnresolved)
            return this.Container != ItemsControl.ItemInfo.UnresolvedContainer;
          return true;
        }
        if (this.Container == ItemsControl.ItemInfo.UnresolvedContainer || that.Container == ItemsControl.ItemInfo.UnresolvedContainer)
          return false;
        if (this.Container != that.Container)
        {
          if (this.Container == ItemsControl.ItemInfo.SentinelContainer || that.Container == ItemsControl.ItemInfo.SentinelContainer)
            return true;
          if (this.Container != null && that.Container != null)
            return false;
          if (this.Index >= 0 && that.Index >= 0)
            return this.Index == that.Index;
          return true;
        }
        if (this.Container != ItemsControl.ItemInfo.SentinelContainer && (this.Index < 0 || that.Index < 0))
          return true;
        return this.Index == that.Index;
      }

      internal ItemsControl.ItemInfo Refresh(ItemContainerGenerator generator)
      {
        if (this.Container == null && this.Index < 0)
          this.Container = generator.ContainerFromItem(this.Item);
        if (this.Index < 0 && this.Container != null)
          this.Index = generator.IndexFromContainer(this.Container);
        if (this.Container == null && this.Index >= 0)
          this.Container = generator.ContainerFromIndex(this.Index);
        if (this.Container == ItemsControl.ItemInfo.SentinelContainer && this.Index >= 0)
          this.Container = (DependencyObject) null;
        return this;
      }

      internal void Reset(object item)
      {
        this.Item = item;
      }
    }
  }
}
