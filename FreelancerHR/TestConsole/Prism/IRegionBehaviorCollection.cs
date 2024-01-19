// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IRegionBehaviorCollection
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Defines the interface for a collection of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> classes on a Region.
  /// 
  /// </summary>
  public interface IRegionBehaviorCollection : IEnumerable<KeyValuePair<string, IRegionBehavior>>, IEnumerable
  {
    /// <summary>
    /// Gets the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> with the specified key.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The registered <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/>
    /// </value>
    IRegionBehavior this[string key] { get; }

    /// <summary>
    /// Adds a <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> to the collection, using the specified key as an indexer.
    /// 
    /// </summary>
    /// <param name="key">The key that specifies the type of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> that's added.
    ///             </param><param name="regionBehavior">The <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> to add.</param>
    void Add(string key, IRegionBehavior regionBehavior);

    /// <summary>
    /// Checks if a <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> with the specified key is already present.
    /// 
    /// </summary>
    /// <param name="key">The key to use to find a particular <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/>.</param>
    /// <returns/>
    bool ContainsKey(string key);
  }
}
