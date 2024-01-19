// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.SingleActiveRegion
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Region that allows a maximum of one active view at a time.
  /// 
  /// </summary>
  public class SingleActiveRegion : Region
  {
    /// <summary>
    /// Marks the specified view as active.
    /// 
    /// </summary>
    /// <param name="view">The view to activate.</param>
    /// <remarks>
    /// If there is an active view before calling this method,
    ///             that view will be deactivated automatically.
    /// </remarks>
    public override void Activate(object view)
    {
      object view1 = Enumerable.FirstOrDefault<object>((IEnumerable<object>) this.ActiveViews);
      if (view1 != null && view1 != view && this.Views.Contains(view1))
        this.Deactivate(view1);
      base.Activate(view);
    }
  }
}
