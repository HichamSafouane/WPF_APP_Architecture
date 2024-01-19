// Decompiled with JetBrains decompiler
// Type: System.ComponentModel.Composition.Primitives.ComposablePartCatalog
// Assembly: System.ComponentModel.Composition, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: CCFB32A4-85CC-452F-B89A-7104D83C3E11
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ComponentModel.Composition.dll

using Microsoft.Internal;
using Microsoft.Internal.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Threading;

namespace System.ComponentModel.Composition.Primitives
{
  /// <summary>
  /// Represents the abstract base class for composable part catalogs, which collect and return <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> objects.
  /// </summary>
  [DebuggerTypeProxy(typeof (ComposablePartCatalogDebuggerProxy))]
  public abstract class ComposablePartCatalog : IEnumerable<ComposablePartDefinition>, IEnumerable, IDisposable
  {
    internal static readonly List<Tuple<ComposablePartDefinition, ExportDefinition>> _EmptyExportsList = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
    private bool _isDisposed;
    private volatile IQueryable<ComposablePartDefinition> _queryableParts;

    /// <summary>
    /// Gets the part definitions that are contained in the catalog.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> contained in the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/>.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> object has been disposed of.</exception>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual IQueryable<ComposablePartDefinition> Parts
    {
      get
      {
        this.ThrowIfDisposed();
        if (this._queryableParts == null)
        {
          Interlocked.CompareExchange<IQueryable<ComposablePartDefinition>>(ref this._queryableParts, Queryable.AsQueryable<ComposablePartDefinition>((IEnumerable<ComposablePartDefinition>) this), (IQueryable<ComposablePartDefinition>) null);
          Assumes.NotNull<IQueryable<ComposablePartDefinition>>(this._queryableParts);
        }
        return this._queryableParts;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> class.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    protected ComposablePartCatalog()
    {
    }

    /// <summary>
    /// Gets a list of export definitions that match the constraint defined by the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// A collection of <see cref="T:System.Tuple`2"/> containing the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition"/> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> objects for objects that match the constraint specified by <paramref name="definition"/>.
    /// </returns>
    /// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition"/> objects to be returned.</param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> object has been disposed of.</exception><exception cref="T:System.ArgumentNullException"><paramref name="definition"/> is null.</exception>
    public virtual IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
    {
      this.ThrowIfDisposed();
      Requires.NotNull<ImportDefinition>(definition, "definition");
      List<Tuple<ComposablePartDefinition, ExportDefinition>> source = (List<Tuple<ComposablePartDefinition, ExportDefinition>>) null;
      IEnumerable<ComposablePartDefinition> candidateParts = this.GetCandidateParts(definition);
      if (candidateParts != null)
      {
        foreach (ComposablePartDefinition composablePartDefinition in candidateParts)
        {
          Tuple<ComposablePartDefinition, ExportDefinition> singleMatch;
          IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> multipleMatches;
          if (composablePartDefinition.TryGetExports(definition, out singleMatch, out multipleMatches))
            source = CollectionServices.FastAppendToListAllowNulls<Tuple<ComposablePartDefinition, ExportDefinition>>(source, singleMatch, multipleMatches);
        }
      }
      return (IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>>) source ?? (IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>>) ComposablePartCatalog._EmptyExportsList;
    }

    internal virtual IEnumerable<ComposablePartDefinition> GetCandidateParts(ImportDefinition definition)
    {
      return (IEnumerable<ComposablePartDefinition>) this;
    }

    /// <summary>
    /// Releases all resources used by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/>.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
    protected virtual void Dispose(bool disposing)
    {
      this._isDisposed = true;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the catalog.
    /// </summary>
    /// 
    /// <returns>
    /// An enumerator that can be used to iterate through the catalog.
    /// </returns>
    public virtual IEnumerator<ComposablePartDefinition> GetEnumerator()
    {
      IQueryable<ComposablePartDefinition> parts = this.Parts;
      if (object.ReferenceEquals((object) parts, (object) this._queryableParts))
        return Enumerable.Empty<ComposablePartDefinition>().GetEnumerator();
      return parts.GetEnumerator();
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    [DebuggerStepThrough]
    private void ThrowIfDisposed()
    {
      if (this._isDisposed)
        throw ExceptionBuilder.CreateObjectDisposed((object) this);
    }
  }
}
