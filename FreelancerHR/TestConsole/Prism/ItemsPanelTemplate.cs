// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.ItemsPanelTemplate
// Assembly: PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C1D3DCCB-C99A-4167-B947-733D30DFAA08
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\WPF\PresentationFramework.dll

using System;
using System.Runtime;
using System.Windows;
using System.Xaml;

namespace System.Windows.Controls
{
  /// <summary>
  /// Specifies the panel that the <see cref="T:System.Windows.Controls.ItemsPresenter"/> creates for the layout of the items of an <see cref="T:System.Windows.Controls.ItemsControl"/>.
  /// </summary>
  public class ItemsPanelTemplate : FrameworkTemplate
  {
    internal override Type TargetTypeInternal
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return ItemsPanelTemplate.DefaultTargetType;
      }
    }

    internal static Type DefaultTargetType
    {
      get
      {
        return typeof (ItemsPresenter);
      }
    }

    /// <summary>
    /// Initializes an instance of the <see cref="T:System.Windows.Controls.ItemsPanelTemplate"/> class.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public ItemsPanelTemplate()
    {
    }

    /// <summary>
    /// Initializes an instance of the <see cref="T:System.Windows.Controls.ItemsPanelTemplate"/> class with the specified template.
    /// </summary>
    /// <param name="root">The <see cref="T:System.Windows.FrameworkElementFactory"/> object that represents the template.</param>
    public ItemsPanelTemplate(FrameworkElementFactory root)
    {
      this.VisualTree = root;
    }

    internal override void SetTargetTypeInternal(Type targetType)
    {
      throw new InvalidOperationException(System.Windows.SR.Get("TemplateNotTargetType"));
    }

    internal override void ProcessTemplateBeforeSeal()
    {
      if (this.HasContent)
      {
        TemplateContent template = this.Template;
        XamlType xamlType = template.SchemaContext.GetXamlType(typeof (Panel));
        if (template.RootType == (XamlType) null || !template.RootType.CanAssignTo(xamlType))
          throw new InvalidOperationException(System.Windows.SR.Get("ItemsPanelNotAPanel", (object) template.RootType));
      }
      else
      {
        FrameworkElementFactory visualTree;
        if ((visualTree = this.VisualTree) == null)
          return;
        if (!typeof (Panel).IsAssignableFrom(visualTree.Type))
          throw new InvalidOperationException(System.Windows.SR.Get("ItemsPanelNotAPanel", (object) visualTree.Type));
        visualTree.SetValue(Panel.IsItemsHostProperty, (object) true);
      }
    }

    /// <summary>
    /// Checks that the templated parent is a non-null <see cref="T:System.Windows.Controls.ItemsPresenter"/> object.
    /// </summary>
    /// <param name="templatedParent">The element this template is applied to. This must be an <see cref="T:System.Windows.Controls.ItemsPresenter"/> object.</param><exception cref="T:System.ArgumentNullException"><paramref name="templatedParent"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="templatedParent"/> is not an <see cref="T:System.Windows.Controls.ItemsPresenter"/>.</exception>
    protected override void ValidateTemplatedParent(FrameworkElement templatedParent)
    {
      if (templatedParent == null)
        throw new ArgumentNullException("templatedParent");
      if (!(templatedParent is ItemsPresenter))
        throw new ArgumentException(System.Windows.SR.Get("TemplateTargetTypeMismatch", (object) "ItemsPresenter", (object) templatedParent.GetType().Name));
    }
  }
}
