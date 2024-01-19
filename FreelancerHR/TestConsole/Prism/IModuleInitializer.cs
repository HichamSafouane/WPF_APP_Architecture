// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Modularity.IModuleInitializer
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Modularity
{
  /// <summary>
  /// Declares a service which initializes the modules into the application.
  /// 
  /// </summary>
  public interface IModuleInitializer
  {
    /// <summary>
    /// Initializes the specified module.
    /// 
    /// </summary>
    /// <param name="moduleInfo">The module to initialize</param>
    void Initialize(ModuleInfo moduleInfo);
  }
}
