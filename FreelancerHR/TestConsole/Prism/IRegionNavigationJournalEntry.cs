// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.IRegionNavigationJournalEntry
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// An entry in an IRegionNavigationJournal representing the URI navigated to.
  /// 
  /// </summary>
  public interface IRegionNavigationJournalEntry
  {
    /// <summary>
    /// Gets or sets the URI.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The URI.
    /// </value>
    Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets the NavigationParameters instance.
    /// 
    /// </summary>
    NavigationParameters Parameters { get; set; }
  }
}
