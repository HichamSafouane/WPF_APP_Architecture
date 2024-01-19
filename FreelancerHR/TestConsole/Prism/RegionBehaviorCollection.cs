// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionBehaviorCollection
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// A collection of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> instances, that are stored and retrieved by Key.
  /// 
  /// </summary>
  public class RegionBehaviorCollection : IRegionBehaviorCollection, IEnumerable<KeyValuePair<string, IRegionBehavior>>, IEnumerable
  {
    private readonly Dictionary<string, IRegionBehavior> behaviors = new Dictionary<string, IRegionBehavior>();
    private readonly IRegion region;

    /// <summary>
    /// Gets the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> with the specified key.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The RegionBehavior that's registered with the key.
    /// </value>
    public IRegionBehavior this[string key]
    {
      get
      {
        return this.behaviors[key];
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.RegionBehaviorCollection"/> class and associates it with a region.
    /// 
    /// </summary>
    /// <param name="region">The region to associate the behavior collection with.</param>
    public RegionBehaviorCollection(IRegion region)
    {
      this.region = region;
    }

    /// <summary>
    /// Adds a <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> to the collection, using the specified key as an indexer.
    /// 
    /// </summary>
    /// <param name="key">The key that specifies the type of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> that's added.</param><param name="regionBehavior">The <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> to add.</param><exception cref="T:System.ArgumentNullException">Thrown is the <paramref name="key"/> parameter is Null,
    ///             or if the <paramref name="regionBehavior"/> parameter is Null.
    ///             </exception><exception cref="T:System.ArgumentException">Thrown if a behavior with the specified Key parameter already exists.</exception>
    public void Add(string key, IRegionBehavior regionBehavior)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      if (regionBehavior == null)
        throw new ArgumentNullException("regionBehavior");
      if (this.behaviors.ContainsKey(key))
        throw new ArgumentException("Could not add duplicate behavior with same key.", "key");
      this.behaviors.Add(key, regionBehavior);
      regionBehavior.Region = this.region;
      regionBehavior.Attach();
    }

    /// <summary>
    /// Checks if a <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/> with the specified key is already present.
    /// 
    /// </summary>
    /// <param name="key">The key to use to find a particular <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/>.</param>
    /// <returns/>
    public bool ContainsKey(string key)
    {
      return this.behaviors.ContainsKey(key);
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
    public IEnumerator<KeyValuePair<string, IRegionBehavior>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, IRegionBehavior>>) this.behaviors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.behaviors.GetEnumerator();
    }
  }
}
