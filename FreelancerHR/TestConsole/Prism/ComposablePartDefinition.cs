// Decompiled with JetBrains decompiler
// Type: System.ComponentModel.Composition.Primitives.ComposablePartDefinition
// Assembly: System.ComponentModel.Composition, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: CCFB32A4-85CC-452F-B89A-7104D83C3E11
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ComponentModel.Composition.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime;

namespace System.ComponentModel.Composition.Primitives
{
  /// <summary>
  /// Defines an abstract base class for composable part definitions, which describe and enable the creation of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart"/> objects.
  /// </summary>
  public abstract class ComposablePartDefinition
  {
    internal static readonly IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> _EmptyExports = Enumerable.Empty<Tuple<ComposablePartDefinition, ExportDefinition>>();

    /// <summary>
    /// Gets a collection of <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition"/> objects that describe the objects exported by the part defined by this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition"/> objects that describe the exported objects provided by <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart"/> objects created by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/>.
    /// </returns>
    public abstract IEnumerable<ExportDefinition> ExportDefinitions { get; }

    /// <summary>
    /// Gets a collection of <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition"/> objects that describe the imports required by the part defined by this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition"/> objects that describe the imports required by <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart"/> objects created by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/>.
    /// </returns>
    public abstract IEnumerable<ImportDefinition> ImportDefinitions { get; }

    /// <summary>
    /// Gets a collection of the metadata for this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> object.
    /// </summary>
    /// 
    /// <returns>
    /// A collection that contains the metadata for the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/>. The default is an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2"/> object.
    /// </returns>
    public virtual IDictionary<string, object> Metadata
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return MetadataServices.EmptyMetadata;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> class.
    /// </summary>
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    protected ComposablePartDefinition()
    {
    }

    /// <summary>
    /// Creates a new instance of a part that the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition"/> describes.
    /// </summary>
    /// 
    /// <returns>
    /// The created part.
    /// </returns>
    public abstract ComposablePart CreatePart();

    internal virtual bool TryGetExports(ImportDefinition definition, out Tuple<ComposablePartDefinition, ExportDefinition> singleMatch, out IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> multipleMatches)
    {
      singleMatch = (Tuple<ComposablePartDefinition, ExportDefinition>) null;
      multipleMatches = (IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>>) null;
      List<Tuple<ComposablePartDefinition, ExportDefinition>> list = (List<Tuple<ComposablePartDefinition, ExportDefinition>>) null;
      Tuple<ComposablePartDefinition, ExportDefinition> tuple = (Tuple<ComposablePartDefinition, ExportDefinition>) null;
      bool flag = false;
      foreach (ExportDefinition exportDefinition in this.ExportDefinitions)
      {
        if (definition.IsConstraintSatisfiedBy(exportDefinition))
        {
          flag = true;
          if (tuple == null)
          {
            tuple = new Tuple<ComposablePartDefinition, ExportDefinition>(this, exportDefinition);
          }
          else
          {
            if (list == null)
            {
              list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
              list.Add(tuple);
            }
            list.Add(new Tuple<ComposablePartDefinition, ExportDefinition>(this, exportDefinition));
          }
        }
      }
      if (!flag)
        return false;
      if (list != null)
        multipleMatches = (IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>>) list;
      else
        singleMatch = tuple;
      return true;
    }

    internal virtual ComposablePartDefinition GetGenericPartDefinition()
    {
      return (ComposablePartDefinition) null;
    }
  }
}
