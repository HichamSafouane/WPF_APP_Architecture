// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Interface for RegionBehaviorFactories. This factory allows the registration of the default set of RegionBehaviors, that will
  ///             be added to the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehaviorCollection"/>s of all <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>s, unless overridden on a 'per-region' basis.
  /// 
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "It is more of a factory than a collection")]
  public interface IRegionBehaviorFactory : IEnumerable<string>, IEnumerable
  {
    /// <summary>
    /// Adds a particular type of RegionBehavior if it was not already registered. the <paramref name="behaviorKey"/> string is used to check if the behavior is already present
    /// 
    /// </summary>
    /// <param name="behaviorKey">The behavior key that's used to find if a certain behavior is already added.</param><param name="behaviorType">Type of the behavior to add. .</param>
    void AddIfMissing(string behaviorKey, Type behaviorType);

    /// <summary>
    /// Determines whether a behavior with the specified key already exists
    /// 
    /// </summary>
    /// <param name="behaviorKey">The behavior key.</param>
    /// <returns>
    /// <see langword="true"/> if a behavior with the specified key is present; otherwise, <see langword="false"/>.
    /// 
    /// </returns>
    bool ContainsKey(string behaviorKey);

    /// <summary>
    /// Creates an instance of the Behaviortype that's registered using the specified key.
    /// 
    /// </summary>
    /// <param name="key">The key that's used to register a behavior type.</param>
    /// <returns>
    /// The created behavior.
    /// </returns>
    IRegionBehavior CreateFromKey(string key);
  }
}
