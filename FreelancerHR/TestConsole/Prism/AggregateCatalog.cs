// Decompiled with JetBrains decompiler
// Type: System.ComponentModel.Composition.Hosting.AggregateCatalog
// Assembly: System.ComponentModel.Composition, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: CCFB32A4-85CC-452F-B89A-7104D83C3E11
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ComponentModel.Composition.dll

using Microsoft.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Threading;

namespace System.ComponentModel.Composition.Hosting
{
  /// <summary>
  /// A catalog that combines the elements of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> objects.
  /// </summary>
  public class AggregateCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
  {
    private ComposablePartCatalogCollection _catalogs;
    private volatile int _isDisposed;

    /// <summary>
    /// Gets the underlying catalogs of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> objects that underlie the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> object.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> object has been disposed of.</exception>
    public ICollection<ComposablePartCatalog> Catalogs
    {
      get
      {
        this.ThrowIfDisposed();
        return (ICollection<ComposablePartCatalog>) this._catalogs;
      }
    }

    /// <summary>
    /// Occurs when the contents of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> object have changed.
    /// </summary>
    public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed
    {
      add
      {
        this._catalogs.Changed += value;
      }
      remove
      {
        this._catalogs.Changed -= value;
      }
    }

    /// <summary>
    /// Occurs when the contents of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> object are changing.
    /// </summary>
    public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing
    {
      add
      {
        this._catalogs.Changing += value;
      }
      remove
      {
        this._catalogs.Changing -= value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> class.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public AggregateCatalog()
      : this((IEnumerable<ComposablePartCatalog>) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> class with the specified catalogs.
    /// </summary>
    /// <param name="catalogs">A array of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="catalogs"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="catalogs"/> contains an element that is null.</exception>
    public AggregateCatalog(params ComposablePartCatalog[] catalogs)
      : this((IEnumerable<ComposablePartCatalog>) catalogs)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> class with the specified catalogs.
    /// </summary>
    /// <param name="catalogs">A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> or null to create an empty <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/>. </param><exception cref="T:System.ArgumentException"><paramref name="catalogs"/> contains an element that is null.</exception>
    public AggregateCatalog(IEnumerable<ComposablePartCatalog> catalogs)
    {
      Requires.NullOrNotNullElements<ComposablePartCatalog>(catalogs, "catalogs");
      this._catalogs = new ComposablePartCatalogCollection(catalogs, new Action<ComposablePartCatalogChangeEventArgs>(this.OnChanged), new Action<ComposablePartCatalogChangeEventArgs>(this.OnChanging));
    }

    /// <summary>
    /// Gets the export definitions that match the constraint expressed by the specified definition.
    /// </summary>
    /// 
    /// <returns>
    /// A collection of <see cref="T:System.Tuple`2"/> containing the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition"/> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> objects for objects that match the constraint specified by <paramref name="definition"/>.
    /// </returns>
    /// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition"/> objects to be returned.</param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> object has been disposed of.</exception><exception cref="T:System.ArgumentNullException"><paramref name="definition"/> is null.</exception>
    public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
    {
      this.ThrowIfDisposed();
      Requires.NotNull<ImportDefinition>(definition, "definition");
      IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> collection = (IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>>) null;
      List<Tuple<ComposablePartDefinition, ExportDefinition>> list = (List<Tuple<ComposablePartDefinition, ExportDefinition>>) null;
      foreach (ComposablePartCatalog composablePartCatalog in this._catalogs)
      {
        IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> exports = composablePartCatalog.GetExports(definition);
        if (exports != ComposablePartCatalog._EmptyExportsList)
        {
          if (collection == null)
          {
            collection = exports;
          }
          else
          {
            if (list == null)
            {
              list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>(collection);
              collection = (IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>>) list;
            }
            list.AddRange(exports);
          }
        }
      }
      return collection ?? (IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>>) ComposablePartCatalog._EmptyExportsList;
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing || Interlocked.CompareExchange(ref this._isDisposed, 1, 0) != 0)
          return;
        this._catalogs.Dispose();
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the catalog.
    /// </summary>
    /// 
    /// <returns>
    /// An enumerator that can be used to iterate through the catalog.
    /// </returns>
    public override IEnumerator<ComposablePartDefinition> GetEnumerator()
    {
      return Enumerable.SelectMany<ComposablePartCatalog, ComposablePartDefinition>((IEnumerable<ComposablePartCatalog>) this._catalogs, (Func<ComposablePartCatalog, IEnumerable<ComposablePartDefinition>>) (catalog => (IEnumerable<ComposablePartDefinition>) catalog)).GetEnumerator();
    }

    /// <summary>
    /// Raises the <see cref="E:System.ComponentModel.Composition.Hosting.AggregateCatalog.Changed"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartCatalogChangeEventArgs"/> object that contains the event data. </param>
    protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
    {
      this._catalogs.OnChanged((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.ComponentModel.Composition.Hosting.AggregateCatalog.Changing"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartCatalogChangeEventArgs"/> object that contains the event data. </param>
    protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
    {
      this._catalogs.OnChanging((object) this, e);
    }

    [DebuggerStepThrough]
    private void ThrowIfDisposed()
    {
      if (this._isDisposed == 1)
        throw ExceptionBuilder.CreateObjectDisposed((object) this);
    }
  }
}
