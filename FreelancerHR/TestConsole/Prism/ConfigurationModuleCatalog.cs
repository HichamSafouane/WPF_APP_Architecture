// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Modularity.ConfigurationModuleCatalog
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Microsoft.Practices.Prism.Modularity
{
  /// <summary>
  /// A catalog built from a configuration file.
  /// 
  /// </summary>
  public class ConfigurationModuleCatalog : ModuleCatalog
  {
    /// <summary>
    /// Gets or sets the store where the configuration is kept.
    /// 
    /// </summary>
    public IConfigurationStore Store { get; set; }

    /// <summary>
    /// Builds an instance of ConfigurationModuleCatalog with a <see cref="T:Microsoft.Practices.Prism.Modularity.ConfigurationStore"/> as the default store.
    /// 
    /// </summary>
    public ConfigurationModuleCatalog()
    {
      this.Store = (IConfigurationStore) new ConfigurationStore();
    }

    /// <summary>
    /// Loads the catalog from the configuration.
    /// 
    /// </summary>
    protected override void InnerLoad()
    {
      if (this.Store == null)
        throw new InvalidOperationException(Resources.ConfigurationStoreCannotBeNull);
      this.EnsureModulesDiscovered();
    }

    private static string GetFileAbsoluteUri(string filePath)
    {
      return new UriBuilder()
      {
        Host = string.Empty,
        Scheme = Uri.UriSchemeFile,
        Path = Path.GetFullPath(filePath)
      }.Uri.ToString();
    }

    private void EnsureModulesDiscovered()
    {
      ModulesConfigurationSection configurationSection = this.Store.RetrieveModuleConfigurationSection();
      if (configurationSection == null)
        return;
      foreach (ModuleConfigurationElement configurationElement1 in (ConfigurationElementCollection) configurationSection.Modules)
      {
        IList<string> list = (IList<string>) new List<string>();
        if (configurationElement1.Dependencies.Count > 0)
        {
          foreach (ModuleDependencyConfigurationElement configurationElement2 in (ConfigurationElementCollection) configurationElement1.Dependencies)
            list.Add(configurationElement2.ModuleName);
        }
        ModuleInfo moduleInfo = new ModuleInfo(configurationElement1.ModuleName, configurationElement1.ModuleType)
        {
          Ref = ConfigurationModuleCatalog.GetFileAbsoluteUri(configurationElement1.AssemblyFile),
          InitializationMode = configurationElement1.StartupLoaded ? InitializationMode.WhenAvailable : InitializationMode.OnDemand
        };
        CollectionExtensions.AddRange<string>(moduleInfo.DependsOn, (IEnumerable<string>) Enumerable.ToArray<string>((IEnumerable<string>) list));
        this.AddModule(moduleInfo);
      }
    }
  }
}
