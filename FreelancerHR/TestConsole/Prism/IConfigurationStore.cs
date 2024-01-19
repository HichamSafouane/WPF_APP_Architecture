// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Modularity.IConfigurationStore
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Modularity
{
  /// <summary>
  /// Defines a store for the module metadata.
  /// 
  /// </summary>
  public interface IConfigurationStore
  {
    /// <summary>
    /// Gets the module configuration data.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:Microsoft.Practices.Prism.Modularity.ModulesConfigurationSection"/> instance.
    /// </returns>
    ModulesConfigurationSection RetrieveModuleConfigurationSection();
  }
}
