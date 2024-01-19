// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.ViewSortHintAttribute
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Provides a hint from a view to a region on how to sort the view.
  /// 
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public sealed class ViewSortHintAttribute : Attribute
  {
    /// <summary>
    /// Gets  the hint.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The hint to use for sorting.
    /// </value>
    public string Hint { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.ViewSortHintAttribute"/> class.
    /// 
    /// </summary>
    /// <param name="hint">The hint to use for sorting.</param>
    public ViewSortHintAttribute(string hint)
    {
      this.Hint = hint;
    }
  }
}
