// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.GridViewColumn
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C1D3DCCB-C99A-4167-B947-733D30DFAA08
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\WPF\PresentationFramework.dll

using MS.Internal;
using System;
using System.ComponentModel;
using System.Runtime;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace System.Windows.Controls
{
  /// <summary>
  /// Represents a column that displays data.
  /// </summary>
  [StyleTypedProperty(Property = "HeaderContainerStyle", StyleTargetType = typeof (GridViewColumnHeader))]
  [ContentProperty("Header")]
  [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
  public class GridViewColumn : DependencyObject, INotifyPropertyChanged
  {
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.GridViewColumn.Header"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof (object), typeof (GridViewColumn), (PropertyMetadata) new FrameworkPropertyMetadata(new PropertyChangedCallback(GridViewColumn.OnHeaderChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.GridViewColumn.HeaderContainerStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderContainerStyleProperty = DependencyProperty.Register("HeaderContainerStyle", typeof (Style), typeof (GridViewColumn), (PropertyMetadata) new FrameworkPropertyMetadata(new PropertyChangedCallback(GridViewColumn.OnHeaderContainerStyleChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.GridViewColumn.HeaderTemplate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof (DataTemplate), typeof (GridViewColumn), (PropertyMetadata) new FrameworkPropertyMetadata(new PropertyChangedCallback(GridViewColumn.OnHeaderTemplateChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.GridViewColumn.HeaderTemplateSelector"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderTemplateSelectorProperty = DependencyProperty.Register("HeaderTemplateSelector", typeof (DataTemplateSelector), typeof (GridViewColumn), (PropertyMetadata) new FrameworkPropertyMetadata(new PropertyChangedCallback(GridViewColumn.OnHeaderTemplateSelectorChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.GridViewColumn.HeaderStringFormat"/> dependency property.
    /// </summary>
    /// 
    /// <returns>
    /// The identifier for the <see cref="P:System.Windows.Controls.GridViewColumn.HeaderStringFormat"/> dependency property.
    /// </returns>
    public static readonly DependencyProperty HeaderStringFormatProperty = DependencyProperty.Register("HeaderStringFormat", typeof (string), typeof (GridViewColumn), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(GridViewColumn.OnHeaderStringFormatChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.GridViewColumn.CellTemplate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof (DataTemplate), typeof (GridViewColumn), new PropertyMetadata(new PropertyChangedCallback(GridViewColumn.OnCellTemplateChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.GridViewColumn.CellTemplateSelector"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CellTemplateSelectorProperty = DependencyProperty.Register("CellTemplateSelector", typeof (DataTemplateSelector), typeof (GridViewColumn), new PropertyMetadata(new PropertyChangedCallback(GridViewColumn.OnCellTemplateSelectorChanged)));
    /// <summary>
    /// Identifies the <see cref="P:System.Windows.Controls.GridViewColumn.Width"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WidthProperty = FrameworkElement.WidthProperty.AddOwner(typeof (GridViewColumn), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(GridViewColumn.OnWidthChanged)));
    internal const string c_DisplayMemberBindingName = "DisplayMemberBinding";
    internal const string c_ActualWidthName = "ActualWidth";
    private BindingBase _displayMemberBinding;
    private DependencyObject _inheritanceContext;
    private double _desiredWidth;
    private int _actualIndex;
    private double _actualWidth;
    private ColumnMeasureState _state;

    /// <summary>
    /// Gets or sets the content of the header of a <see cref="T:System.Windows.Controls.GridViewColumn"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The object to use for the column header. The default is null.
    /// </returns>
    public object Header
    {
      get
      {
        return this.GetValue(GridViewColumn.HeaderProperty);
      }
      set
      {
        this.SetValue(GridViewColumn.HeaderProperty, value);
      }
    }

    /// <summary>
    /// Gets or sets the style to use for the header of the <see cref="T:System.Windows.Controls.GridViewColumn"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.Windows.Style"/> that defines the display properties for the column header. The default is null.
    /// </returns>
    public Style HeaderContainerStyle
    {
      get
      {
        return (Style) this.GetValue(GridViewColumn.HeaderContainerStyleProperty);
      }
      set
      {
        this.SetValue(GridViewColumn.HeaderContainerStyleProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the template to use to display the content of the column header.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Windows.DataTemplate"/> to use to display the column header. The default is null.
    /// </returns>
    public DataTemplate HeaderTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridViewColumn.HeaderTemplateProperty);
      }
      set
      {
        this.SetValue(GridViewColumn.HeaderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Controls.DataTemplateSelector"/> that provides logic to select the template to use to display the column header.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.Windows.Controls.DataTemplateSelector"/> object that provides data template selection for each <see cref="T:System.Windows.Controls.GridViewColumn"/>. The default is null.
    /// </returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTemplateSelector HeaderTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridViewColumn.HeaderTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(GridViewColumn.HeaderTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets a composite string that specifies how to format the <see cref="P:System.Windows.Controls.GridViewColumn.Header"/> property if it is displayed as a string.
    /// </summary>
    /// 
    /// <returns>
    /// A composite string that specifies how to format the <see cref="P:System.Windows.Controls.GridViewColumn.Header"/> property if it is displayed as a string. The default is null.
    /// </returns>
    public string HeaderStringFormat
    {
      get
      {
        return (string) this.GetValue(GridViewColumn.HeaderStringFormatProperty);
      }
      set
      {
        this.SetValue(GridViewColumn.HeaderStringFormatProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the data item to bind to for this column.
    /// </summary>
    /// 
    /// <returns>
    /// The specified data item type that displays in the column. The default is null.
    /// </returns>
    public BindingBase DisplayMemberBinding
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._displayMemberBinding;
      }
      set
      {
        if (this._displayMemberBinding == value)
          return;
        this._displayMemberBinding = value;
        this.OnDisplayMemberBindingChanged();
      }
    }

    /// <summary>
    /// Gets or sets the template to use to display the contents of a column cell.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Windows.DataTemplate"/> that is used to format a column cell. The default is null.
    /// </returns>
    public DataTemplate CellTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridViewColumn.CellTemplateProperty);
      }
      set
      {
        this.SetValue(GridViewColumn.CellTemplateProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets a <see cref="T:System.Windows.Controls.DataTemplateSelector"/> that determines the template to use to display cells in a column.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.Windows.Controls.DataTemplateSelector"/> that provides <see cref="T:System.Windows.DataTemplate"/> selection for column cells. The default is null.
    /// </returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTemplateSelector CellTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridViewColumn.CellTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(GridViewColumn.CellTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets the width of the column.
    /// </summary>
    /// 
    /// <returns>
    /// The width of the column. The default is <see cref="F:System.Double.NaN"/>, which automatically sizes to the largest column item that is not the column header.
    /// </returns>
    [TypeConverter(typeof (LengthConverter))]
    public double Width
    {
      get
      {
        return (double) this.GetValue(GridViewColumn.WidthProperty);
      }
      set
      {
        this.SetValue(GridViewColumn.WidthProperty, (object) value);
      }
    }

    /// <summary>
    /// Gets the actual width of a <see cref="T:System.Windows.Controls.GridViewColumn"/>.
    /// </summary>
    /// 
    /// <returns>
    /// The current width of the column. The default is zero (0.0).
    /// </returns>
    public double ActualWidth
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._actualWidth;
      }
      private set
      {
        if (double.IsNaN(value) || double.IsInfinity(value) || (value < 0.0 || this._actualWidth == value))
          return;
        this._actualWidth = value;
        this.OnPropertyChanged("ActualWidth");
      }
    }

    internal ColumnMeasureState State
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._state;
      }
      set
      {
        if (this._state != value)
        {
          this._state = value;
          if (value != ColumnMeasureState.Init)
            this.UpdateActualWidth();
          else
            this.DesiredWidth = 0.0;
        }
        else
        {
          if (value != ColumnMeasureState.SpecificWidth)
            return;
          this.UpdateActualWidth();
        }
      }
    }

    internal int ActualIndex
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._actualIndex;
      }
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set
      {
        this._actualIndex = value;
      }
    }

    internal double DesiredWidth
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._desiredWidth;
      }
      private set
      {
        this._desiredWidth = value;
      }
    }

    internal override DependencyObject InheritanceContext
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._inheritanceContext;
      }
    }

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] add
      {
        this._propertyChanged += value;
      }
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] remove
      {
        this._propertyChanged -= value;
      }
    }

    private event PropertyChangedEventHandler _propertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Controls.GridViewColumn"/> class.
    /// </summary>
    public GridViewColumn()
    {
      this.ResetPrivateData();
      this._state = double.IsNaN(this.Width) ? ColumnMeasureState.Init : ColumnMeasureState.SpecificWidth;
    }

    /// <summary>
    /// Creates a string representation of the <see cref="T:System.Windows.Controls.GridViewColumn"/>.
    /// </summary>
    /// 
    /// <returns>
    /// A string that identifies the object as a <see cref="T:System.Windows.Controls.GridViewColumn"/> object and displays the value of the <see cref="P:System.Windows.Controls.GridViewColumn.Header"/> property.
    /// </returns>
    public override string ToString()
    {
      return System.Windows.SR.Get("ToStringFormatString_GridViewColumn", (object) this.GetType(), this.Header);
    }

    /// <summary>
    /// Occurs when the <see cref="P:System.Windows.Controls.GridViewColumn.HeaderStringFormat"/> property changes.
    /// </summary>
    /// <param name="oldHeaderStringFormat">The old value of the <see cref="P:System.Windows.Controls.GridViewColumn.HeaderStringFormat"/> property.</param><param name="newHeaderStringFormat">The new value of the <see cref="P:System.Windows.Controls.GridViewColumn.HeaderStringFormat"/> property.</param>
    protected virtual void OnHeaderStringFormatChanged(string oldHeaderStringFormat, string newHeaderStringFormat)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Controls.GridViewColumn.System#ComponentModel#INotifyPropertyChanged#PropertyChanged"/> event.
    /// </summary>
    /// <param name="e">The event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this._propertyChanged == null)
        return;
      this._propertyChanged((object) this, e);
    }

    internal void OnThemeChanged()
    {
      if (this.Header == null)
        return;
      DependencyObject d = this.Header as DependencyObject;
      if (d == null)
        return;
      FrameworkElement fe;
      FrameworkContentElement fce;
      Helper.DowncastToFEorFCE(d, out fe, out fce, false);
      if (fe == null && fce == null)
        return;
      TreeWalkHelper.InvalidateOnResourcesChange(fe, fce, ResourcesChangeInfo.ThemeChangeInfo);
    }

    internal double EnsureWidth(double width)
    {
      if (width > this.DesiredWidth)
        this.DesiredWidth = width;
      return this.DesiredWidth;
    }

    internal void ResetPrivateData()
    {
      this._actualIndex = -1;
      this._desiredWidth = 0.0;
      this._state = double.IsNaN(this.Width) ? ColumnMeasureState.Init : ColumnMeasureState.SpecificWidth;
    }

    internal override void AddInheritanceContext(DependencyObject context, DependencyProperty property)
    {
      if (this._inheritanceContext != null || context == null)
        return;
      this._inheritanceContext = context;
      this.OnInheritanceContextChanged(EventArgs.Empty);
    }

    internal override void RemoveInheritanceContext(DependencyObject context, DependencyProperty property)
    {
      if (this._inheritanceContext != context)
        return;
      this._inheritanceContext = (DependencyObject) null;
      this.OnInheritanceContextChanged(EventArgs.Empty);
    }

    private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((GridViewColumn) d).OnPropertyChanged(GridViewColumn.HeaderProperty.Name);
    }

    private static void OnHeaderContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((GridViewColumn) d).OnPropertyChanged(GridViewColumn.HeaderContainerStyleProperty.Name);
    }

    private static void OnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      GridViewColumn gridViewColumn = (GridViewColumn) d;
      Helper.CheckTemplateAndTemplateSelector("Header", GridViewColumn.HeaderTemplateProperty, GridViewColumn.HeaderTemplateSelectorProperty, (DependencyObject) gridViewColumn);
      gridViewColumn.OnPropertyChanged(GridViewColumn.HeaderTemplateProperty.Name);
    }

    private static void OnHeaderTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      GridViewColumn gridViewColumn = (GridViewColumn) d;
      Helper.CheckTemplateAndTemplateSelector("Header", GridViewColumn.HeaderTemplateProperty, GridViewColumn.HeaderTemplateSelectorProperty, (DependencyObject) gridViewColumn);
      gridViewColumn.OnPropertyChanged(GridViewColumn.HeaderTemplateSelectorProperty.Name);
    }

    private static void OnHeaderStringFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((GridViewColumn) d).OnHeaderStringFormatChanged((string) e.OldValue, (string) e.NewValue);
    }

    private void OnDisplayMemberBindingChanged()
    {
      this.OnPropertyChanged("DisplayMemberBinding");
    }

    private static void OnCellTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((GridViewColumn) d).OnPropertyChanged(GridViewColumn.CellTemplateProperty.Name);
    }

    private static void OnCellTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((GridViewColumn) d).OnPropertyChanged(GridViewColumn.CellTemplateSelectorProperty.Name);
    }

    private static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      GridViewColumn gridViewColumn = (GridViewColumn) d;
      double d1 = (double) e.NewValue;
      gridViewColumn.State = double.IsNaN(d1) ? ColumnMeasureState.Init : ColumnMeasureState.SpecificWidth;
      gridViewColumn.OnPropertyChanged(GridViewColumn.WidthProperty.Name);
    }

    private void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    private void UpdateActualWidth()
    {
      this.ActualWidth = this.State == ColumnMeasureState.SpecificWidth ? this.Width : this.DesiredWidth;
    }
  }
}
