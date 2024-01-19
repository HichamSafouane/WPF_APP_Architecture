// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Regions.NavigationParameters
// Assembly: Microsoft.Practices.Prism.Composition, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ADF29BAF-B8A8-43AD-B62C-39144F40570E
// Assembly location: C:\SubVersion\MLD\Main\IFace\Bin\Debug\Microsoft.Practices.Prism.Composition.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.Prism.Regions
{
  /// <summary>
  /// Represents Navigation parameters.
  /// 
  /// </summary>
  /// 
  /// <remarks>
  /// This class can be used to to pass object parameters during Navigation.
  /// 
  /// </remarks>
  public class NavigationParameters : IEnumerable<KeyValuePair<string, object>>, IEnumerable
  {
    private readonly List<KeyValuePair<string, object>> entries = new List<KeyValuePair<string, object>>();

    /// <summary>
    /// Gets the <see cref="T:System.String"/> with the specified key.
    /// 
    /// </summary>
    /// 
    /// <returns>
    /// The value for the specified key, or <see langword="null"/> if the query does not contain such a key.
    /// </returns>
    public object this[string key]
    {
      get
      {
        foreach (KeyValuePair<string, object> keyValuePair in this.entries)
        {
          if (string.Compare(keyValuePair.Key, key, StringComparison.Ordinal) == 0)
            return keyValuePair.Value;
        }
        return (object) null;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.NavigationParameters"/> class.
    /// 
    /// </summary>
    public NavigationParameters()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Practices.Prism.Regions.NavigationParameters"/> class with a query string.
    /// 
    /// </summary>
    /// <param name="query">The query string.</param>
    public NavigationParameters(string query)
    {
      if (query == null)
        return;
      int length = query.Length;
      for (int index = query.Length <= 0 || (int) query[0] != 63 ? 0 : 1; index < length; ++index)
      {
        int startIndex = index;
        int num = -1;
        for (; index < length; ++index)
        {
          switch (query[index])
          {
            case '=':
              if (num < 0)
              {
                num = index;
                break;
              }
              break;
            case '&':
              goto label_9;
          }
        }
label_9:
        string stringToUnescape1 = (string) null;
        string stringToUnescape2;
        if (num >= 0)
        {
          stringToUnescape1 = query.Substring(startIndex, num - startIndex);
          stringToUnescape2 = query.Substring(num + 1, index - num - 1);
        }
        else
          stringToUnescape2 = query.Substring(startIndex, index - startIndex);
        this.Add(stringToUnescape1 != null ? Uri.UnescapeDataString(stringToUnescape1) : (string) null, (object) Uri.UnescapeDataString(stringToUnescape2));
        if (index == length - 1 && (int) query[index] == 38)
          this.Add((string) null, (object) "");
      }
    }

    /// <summary>
    /// Gets the enumerator.
    /// 
    /// </summary>
    /// 
    /// <returns/>
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, object>>) this.entries.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    /// <summary>
    /// Adds the specified key and value.
    /// 
    /// </summary>
    /// <param name="key">The name.</param><param name="value">The value.</param>
    public void Add(string key, object value)
    {
      this.entries.Add(new KeyValuePair<string, object>(key, value));
    }

    /// <summary>
    /// Converts the list of key value pairs to a query string.
    /// 
    /// </summary>
    /// 
    /// <returns/>
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.entries.Count > 0)
      {
        stringBuilder.Append('?');
        bool flag = true;
        foreach (KeyValuePair<string, object> keyValuePair in this.entries)
        {
          if (!flag)
            stringBuilder.Append('&');
          else
            flag = false;
          stringBuilder.Append(Uri.EscapeDataString(keyValuePair.Key));
          stringBuilder.Append('=');
          stringBuilder.Append(Uri.EscapeDataString(keyValuePair.Value.ToString()));
        }
      }
      return stringBuilder.ToString();
    }
  }
}
