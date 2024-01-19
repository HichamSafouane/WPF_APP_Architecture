// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IViewsCollection
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Defines a view of a collection.
  /// 
  /// </summary>
  public interface IViewsCollection : IEnumerable<object>, IEnumerable, INotifyCollectionChanged
  {
    /// <summary>
    /// Determines whether the collection contains a specific value.
    /// 
    /// </summary>
    /// <param name="value">The object to locate in the collection.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is found in the collection; otherwise, <see langword="false"/>.
    /// </returns>
    bool Contains(object value);
  }
}
