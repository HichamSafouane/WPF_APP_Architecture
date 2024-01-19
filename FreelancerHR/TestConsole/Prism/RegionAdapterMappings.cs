// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionAdapterMappings
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// This class maps <see cref="T:System.Type"/> with <see cref="T:Microsoft.Practices.Prism.Regions.IRegionAdapter"/>.
  /// 
  /// </summary>
  public class RegionAdapterMappings
  {
    private readonly Dictionary<Type, IRegionAdapter> mappings = new Dictionary<Type, IRegionAdapter>();

    /// <summary>
    /// Registers the mapping between a type and an adapter.
    /// 
    /// </summary>
    /// <param name="controlType">The type of the control.</param><param name="adapter">The adapter to use with the <paramref name="controlType"/> type.</param><exception cref="T:System.ArgumentNullException">When any of <paramref name="controlType"/> or <paramref name="adapter"/> are <see langword="null"/>.</exception><exception cref="T:System.InvalidOperationException">If a mapping for <paramref name="controlType"/> already exists.</exception>
    public void RegisterMapping(Type controlType, IRegionAdapter adapter)
    {
      if (controlType == (Type) null)
        throw new ArgumentNullException("controlType");
      if (adapter == null)
        throw new ArgumentNullException("adapter");
      if (this.mappings.ContainsKey(controlType))
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.MappingExistsException, new object[1]
        {
          (object) controlType.Name
        }));
      this.mappings.Add(controlType, adapter);
    }

    /// <summary>
    /// Returns the adapter associated with the type provided.
    /// 
    /// </summary>
    /// <param name="controlType">The type to obtain the <see cref="T:Microsoft.Practices.Prism.Regions.IRegionAdapter"/> mapped.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.Practices.Prism.Regions.IRegionAdapter"/> mapped to the <paramref name="controlType"/>.
    /// </returns>
    /// 
    /// <remarks>
    /// This class will look for a registered type for <paramref name="controlType"/> and if there is not any,
    ///             it will look for a registered type for any of its ancestors in the class hierarchy.
    ///             If there is no registered type for <paramref name="controlType"/> or any of its ancestors,
    ///             an exception will be thrown.
    /// </remarks>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">When there is no registered type for <paramref name="controlType"/> or any of its ancestors.</exception>
    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "controlType")]
    public IRegionAdapter GetMapping(Type controlType)
    {
      for (Type key = controlType; key != (Type) null; key = key.BaseType)
      {
        if (this.mappings.ContainsKey(key))
          return this.mappings[key];
      }
      throw new KeyNotFoundException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.NoRegionAdapterException, new object[1]
      {
        (object) controlType
      }));
    }
  }
}
