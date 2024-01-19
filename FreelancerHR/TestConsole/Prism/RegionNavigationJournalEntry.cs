// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.RegionNavigationJournalEntry
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;
using System.Globalization;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// An entry in an IRegionNavigationJournal representing the URI navigated to.
  /// 
  /// </summary>
  public class RegionNavigationJournalEntry : IRegionNavigationJournalEntry
  {
    /// <summary>
    /// Gets or sets the URI.
    /// 
    /// </summary>
    /// 
    /// <value>
    /// The URI.
    /// </value>
    public Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets the NavigationParameters instance.
    /// 
    /// </summary>
    public NavigationParameters Parameters { get; set; }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents this instance.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="T:System.String"/> that represents this instance.
    /// 
    /// </returns>
    public override string ToString()
    {
      if (!(this.Uri != (Uri) null))
        return base.ToString();
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "RegionNavigationJournalEntry:'{0}'", new object[1]
      {
        (object) this.Uri.ToString()
      });
    }
  }
}
