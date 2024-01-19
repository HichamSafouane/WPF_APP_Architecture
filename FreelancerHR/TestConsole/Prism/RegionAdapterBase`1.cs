// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionAdapterBase`1
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using Microsoft.Practices.Prism.Regions.Behaviors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Base class to facilitate the creation of <see cref="T:Microsoft.Practices.Prism.Regions.IRegionAdapter"/> implementations.
  /// 
  /// </summary>
  /// <typeparam name="T">Type of object to adapt.</typeparam>
  public abstract class RegionAdapterBase<T> : IRegionAdapter where T : class
  {
    /// <summary>
    /// Gets or sets the factory used to create the region behaviors to attach to the created regions.
    /// 
    /// </summary>
    protected IRegionBehaviorFactory RegionBehaviorFactory { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.RegionAdapterBase`1"/>.
    /// 
    /// </summary>
    /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
    protected RegionAdapterBase(IRegionBehaviorFactory regionBehaviorFactory)
    {
      this.RegionBehaviorFactory = regionBehaviorFactory;
    }

    /// <summary>
    /// Adapts an object and binds it to a new <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
    /// 
    /// </summary>
    /// <param name="regionTarget">The object to adapt.</param><param name="regionName">The name of the region to be created.</param>
    /// <returns>
    /// The new instance of <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/> that the <paramref name="regionTarget"/> is bound to.
    /// </returns>
    public IRegion Initialize(T regionTarget, string regionName)
    {
      if (regionName == null)
        throw new ArgumentNullException("regionName");
      IRegion region = this.CreateRegion();
      region.Name = regionName;
      RegionAdapterBase<T>.SetObservableRegionOnHostingControl(region, regionTarget);
      this.Adapt(region, regionTarget);
      this.AttachBehaviors(region, regionTarget);
      this.AttachDefaultBehaviors(region, regionTarget);
      return region;
    }

    IRegion IRegionAdapter.Initialize(object regionTarget, string regionName)
    {
      return this.Initialize(RegionAdapterBase<T>.GetCastedObject(regionTarget), regionName);
    }

    /// <summary>
    /// This method adds the default behaviors by using the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory"/> object.
    /// 
    /// </summary>
    /// <param name="region">The region being used.</param><param name="regionTarget">The object to adapt.</param>
    protected virtual void AttachDefaultBehaviors(IRegion region, T regionTarget)
    {
      if (region == null)
        throw new ArgumentNullException("region");
      if ((object) regionTarget == null)
        throw new ArgumentNullException("regionTarget");
      IRegionBehaviorFactory regionBehaviorFactory = this.RegionBehaviorFactory;
      if (regionBehaviorFactory == null)
        return;
      DependencyObject dependencyObject = (object) regionTarget as DependencyObject;
      foreach (string key in (IEnumerable<string>) regionBehaviorFactory)
      {
        if (!region.Behaviors.ContainsKey(key))
        {
          IRegionBehavior fromKey = regionBehaviorFactory.CreateFromKey(key);
          if (dependencyObject != null)
          {
            IHostAwareRegionBehavior awareRegionBehavior = fromKey as IHostAwareRegionBehavior;
            if (awareRegionBehavior != null)
              awareRegionBehavior.HostControl = dependencyObject;
          }
          region.Behaviors.Add(key, fromKey);
        }
      }
    }

    /// <summary>
    /// Template method to attach new behaviors.
    /// 
    /// </summary>
    /// <param name="region">The region being used.</param><param name="regionTarget">The object to adapt.</param>
    protected virtual void AttachBehaviors(IRegion region, T regionTarget)
    {
    }

    /// <summary>
    /// Template method to adapt the object to an <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
    /// 
    /// </summary>
    /// <param name="region">The new region being used.</param><param name="regionTarget">The object to adapt.</param>
    protected abstract void Adapt(IRegion region, T regionTarget);

    /// <summary>
    /// Template method to create a new instance of <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>
    ///             that will be used to adapt the object.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A new instance of <see cref="T:Microsoft.Practices.Prism.Regions.IRegion"/>.
    /// </returns>
    protected abstract IRegion CreateRegion();

    private static T GetCastedObject(object regionTarget)
    {
      if (regionTarget == null)
        throw new ArgumentNullException("regionTarget");
      T obj = regionTarget as T;
      if ((object) obj == null)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.AdapterInvalidTypeException, new object[1]
        {
          (object) typeof (T).Name
        }));
      return obj;
    }

    private static void SetObservableRegionOnHostingControl(IRegion region, T regionTarget)
    {
      DependencyObject view = (object) regionTarget as DependencyObject;
      if (view == null)
        return;
      RegionManager.GetObservableRegion(view).Value = region;
    }
  }
}
