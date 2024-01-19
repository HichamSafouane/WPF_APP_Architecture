// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Modularity.IModule
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

namespace Microsoft.Practices.Prism.Modularity
{
  /// <summary>
  /// Defines the contract for the modules deployed in the application.
  /// 
  /// </summary>
  public interface IModule
  {
    /// <summary>
    /// Notifies the module that it has be initialized.
    /// 
    /// </summary>
    void Initialize();
  }
}
