// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionBehaviorFactory
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Defines a factory that allows the registration of the default set of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehavior"/>, that will
  ///             be added to the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehaviorCollection"/> of all <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>s, unless overridden on a 'per-region' basis.
  /// 
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "It is more of a factory than a collection")]
  public class RegionBehaviorFactory : IRegionBehaviorFactory, IEnumerable<string>, IEnumerable
  {
    private readonly Dictionary<string, Type> registeredBehaviors = new Dictionary<string, Type>();
    private readonly IServiceLocator serviceLocator;

    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.RegionBehaviorFactory"/>.
    /// 
    /// </summary>
    /// <param name="serviceLocator"><see cref="T:Microsoft.Practices.ServiceLocation.IServiceLocator"/> used to create the instance of the behavior from its <see cref="T:System.Type"/>.</param>
    public RegionBehaviorFactory(IServiceLocator serviceLocator)
    {
      this.serviceLocator = serviceLocator;
    }

    /// <summary>
    /// Adds a particular type of RegionBehavior if it was not already registered. The <paramref name="behaviorKey"/> string is used to check if the behavior is already present
    /// 
    /// </summary>
    /// <param name="behaviorKey">The behavior key that's used to find if a certain behavior is already added.</param><param name="behaviorType">Type of the behavior to add.</param>
    public void AddIfMissing(string behaviorKey, Type behaviorType)
    {
      if (behaviorKey == null)
        throw new ArgumentNullException("behaviorKey");
      if (behaviorType == (Type) null)
        throw new ArgumentNullException("behaviorType");
      if (!typeof (IRegionBehavior).IsAssignableFrom(behaviorType))
        throw new ArgumentException(string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, Resources.CanOnlyAddTypesThatInheritIFromRegionBehavior, new object[1]
        {
          (object) behaviorType.Name
        }), "behaviorType");
      if (this.registeredBehaviors.ContainsKey(behaviorKey))
        return;
      this.registeredBehaviors.Add(behaviorKey, behaviorType);
    }

    /// <summary>
    /// Creates an instance of the behavior <see cref="T:System.Type"/> that is registered using the specified key.
    /// 
    /// </summary>
    /// <param name="key">The key that is used to register a behavior type.</param>
    /// <returns>
    /// A new instance of the behavior.
    /// </returns>
    public IRegionBehavior CreateFromKey(string key)
    {
      if (!this.ContainsKey(key))
        throw new ArgumentException(string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, Resources.TypeWithKeyNotRegistered, new object[1]
        {
          (object) key
        }), "key");
      return (IRegionBehavior) this.serviceLocator.GetInstance((Type) this.registeredBehaviors[key]);
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
    /// <filterpriority>1</filterpriority>
    public IEnumerator<string> GetEnumerator()
    {
      return (IEnumerator<string>) this.registeredBehaviors.Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    /// <summary>
    /// Determines whether a behavior with the specified key already exists.
    /// 
    /// </summary>
    /// <param name="behaviorKey">The behavior key.</param>
    /// <returns>
    /// <see langword="true"/> if a behavior with the specified key is present; otherwise, <see langword="false"/>.
    /// 
    /// </returns>
    public bool ContainsKey(string behaviorKey)
    {
      return this.registeredBehaviors.ContainsKey(behaviorKey);
    }
  }
}
