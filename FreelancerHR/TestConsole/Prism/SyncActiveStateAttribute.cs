// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.SyncActiveStateAttribute
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Defines that a view is synchronized with its parent view's Active state.
  /// 
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public sealed class SyncActiveStateAttribute : Attribute
  {
  }
}
