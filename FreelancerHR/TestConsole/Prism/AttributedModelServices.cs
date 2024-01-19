// Decompiled with JetBrains decompiler
// Type: System.ComponentModel.Composition.AttributedModelServices
// Assembly: System.ComponentModel.Composition, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: CCFB32A4-85CC-452F-B89A-7104D83C3E11
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ComponentModel.Composition.dll

using Microsoft.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.AttributedModel;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.Composition
{
  /// <summary>
  /// Contains helper methods for using the MEF attributed programming model with composition.
  /// </summary>
  public static class AttributedModelServices
  {
    /// <summary>
    /// Gets a metadata view object from a dictionary of loose metadata.
    /// </summary>
    /// 
    /// <returns>
    /// A metadata view containing the specified metadata.
    /// </returns>
    /// <param name="metadata">A collection of loose metadata.</param><typeparam name="TMetadataView">The type of the metadata view object to get.</typeparam>
    public static TMetadataView GetMetadataView<TMetadataView>(IDictionary<string, object> metadata)
    {
      Requires.NotNull<IDictionary<string, object>>(metadata, "metadata");
      return MetadataViewProvider.GetMetadataView<TMetadataView>(metadata);
    }

    /// <summary>
    /// Creates a composable part from the specified attributed object.
    /// </summary>
    /// 
    /// <returns>
    /// The created part.
    /// </returns>
    /// <param name="attributedPart">The attributed object.</param>
    public static ComposablePart CreatePart(object attributedPart)
    {
      Requires.NotNull<object>(attributedPart, "attributedPart");
      return (ComposablePart) AttributedModelDiscovery.CreatePart(attributedPart);
    }

    /// <summary>
    /// Creates a composable part from the specified attributed object, using the specified reflection context.
    /// </summary>
    /// 
    /// <returns>
    /// The created part.
    /// </returns>
    /// <param name="attributedPart">The attributed object.</param><param name="reflectionContext">The reflection context for the part.</param><exception cref="T:System.ArgumentNullException"><paramref name="reflectionContext"/> is null.</exception>
    public static ComposablePart CreatePart(object attributedPart, ReflectionContext reflectionContext)
    {
      Requires.NotNull<object>(attributedPart, "attributedPart");
      Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
      return (ComposablePart) AttributedModelDiscovery.CreatePart(attributedPart, reflectionContext);
    }

    /// <summary>
    /// Creates a composable part from the specified attributed object, using the specified part definition.
    /// </summary>
    /// 
    /// <returns>
    /// The created part.
    /// </returns>
    /// <param name="partDefinition">The definition of the new part.</param><param name="attributedPart">The attributed object.</param>
    public static ComposablePart CreatePart(ComposablePartDefinition partDefinition, object attributedPart)
    {
      Requires.NotNull<ComposablePartDefinition>(partDefinition, "partDefinition");
      Requires.NotNull<object>(attributedPart, "attributedPart");
      ReflectionComposablePartDefinition composablePartDefinition = partDefinition as ReflectionComposablePartDefinition;
      if (composablePartDefinition == null)
        throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType());
      return (ComposablePart) AttributedModelDiscovery.CreatePart((ComposablePartDefinition) composablePartDefinition, attributedPart);
    }

    /// <summary>
    /// Creates a part definition with the specified type and origin.
    /// </summary>
    /// 
    /// <returns>
    /// The new part definition.
    /// </returns>
    /// <param name="type">The type of the definition.</param><param name="origin">The origin of the definition.</param>
    public static ComposablePartDefinition CreatePartDefinition(Type type, ICompositionElement origin)
    {
      Requires.NotNull<Type>(type, "type");
      return AttributedModelServices.CreatePartDefinition(type, origin, false);
    }

    /// <summary>
    /// Creates a part definition with the specified type and origin.
    /// </summary>
    /// 
    /// <returns>
    /// The new part definition.
    /// </returns>
    /// <param name="type">The type of the definition.</param><param name="origin">The origin of the definition.</param><param name="ensureIsDiscoverable">A value indicating whether or not the new definition should be discoverable.</param>
    public static ComposablePartDefinition CreatePartDefinition(Type type, ICompositionElement origin, bool ensureIsDiscoverable)
    {
      Requires.NotNull<Type>(type, "type");
      if (ensureIsDiscoverable)
        return AttributedModelDiscovery.CreatePartDefinitionIfDiscoverable(type, origin);
      return (ComposablePartDefinition) AttributedModelDiscovery.CreatePartDefinition(type, (PartCreationPolicyAttribute) null, false, origin);
    }

    /// <summary>
    /// Gets the unique identifier for the specified type.
    /// </summary>
    /// 
    /// <returns>
    /// The unique identifier for the type.
    /// </returns>
    /// <param name="type">The type to examine.</param>
    public static string GetTypeIdentity(Type type)
    {
      Requires.NotNull<Type>(type, "type");
      return ContractNameServices.GetTypeIdentity(type);
    }

    /// <summary>
    /// Gets the unique identifier for the specified method.
    /// </summary>
    /// 
    /// <returns>
    /// The unique identifier for the method.
    /// </returns>
    /// <param name="method">The method to examine.</param>
    public static string GetTypeIdentity(MethodInfo method)
    {
      Requires.NotNull<MethodInfo>(method, "method");
      return ContractNameServices.GetTypeIdentityFromMethod(method);
    }

    /// <summary>
    /// Gets a canonical contract name for the specified type.
    /// </summary>
    /// 
    /// <returns>
    /// A contract name created from the specified type.
    /// </returns>
    /// <param name="type">The type to use.</param>
    public static string GetContractName(Type type)
    {
      Requires.NotNull<Type>(type, "type");
      return AttributedModelServices.GetTypeIdentity(type);
    }

    /// <summary>
    /// Creates a part from the specified value and adds it to the specified batch.
    /// </summary>
    /// 
    /// <returns>
    /// The new part.
    /// </returns>
    /// <param name="batch">The batch to add to.</param><param name="exportedValue">The value to add.</param><typeparam name="T">The type of the new part.</typeparam>
    public static ComposablePart AddExportedValue<T>(this CompositionBatch batch, T exportedValue)
    {
      Requires.NotNull<CompositionBatch>(batch, "batch");
      string contractName = AttributedModelServices.GetContractName(typeof (T));
      return AttributedModelServices.AddExportedValue<T>(batch, contractName, exportedValue);
    }

    /// <summary>
    /// Creates a part from the specified value and composes it in the specified composition container.
    /// </summary>
    /// <param name="container">The composition container to perform composition in.</param><param name="exportedValue">The value to compose.</param><typeparam name="T">The type of the new part.</typeparam>
    public static void ComposeExportedValue<T>(this CompositionContainer container, T exportedValue)
    {
      Requires.NotNull<CompositionContainer>(container, "container");
      CompositionBatch batch = new CompositionBatch();
      AttributedModelServices.AddExportedValue<T>(batch, exportedValue);
      container.Compose(batch);
    }

    /// <summary>
    /// Creates a part from the specified value and adds it to the specified batch with the specified contract name.
    /// </summary>
    /// 
    /// <returns>
    /// The new part.
    /// </returns>
    /// <param name="batch">The batch to add to.</param><param name="contractName">The contract name of the export.</param><param name="exportedValue">The value to add.</param><typeparam name="T">The type of the new part.</typeparam>
    public static ComposablePart AddExportedValue<T>(this CompositionBatch batch, string contractName, T exportedValue)
    {
      Requires.NotNull<CompositionBatch>(batch, "batch");
      string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof (T));
      IDictionary<string, object> metadata = (IDictionary<string, object>) new Dictionary<string, object>();
      metadata.Add("ExportTypeIdentity", (object) typeIdentity);
      return batch.AddExport(new Export(contractName, metadata, (Func<object>) (() => (object) (T) exportedValue)));
    }

    /// <summary>
    /// Creates a part from the specified object under the specified contract name and composes it in the specified composition container.
    /// </summary>
    /// <param name="container">The composition container to perform composition in.</param><param name="contractName">The contract name to export the part under.</param><param name="exportedValue">The value to compose.</param><typeparam name="T">The type of the new part.</typeparam>
    public static void ComposeExportedValue<T>(this CompositionContainer container, string contractName, T exportedValue)
    {
      Requires.NotNull<CompositionContainer>(container, "container");
      CompositionBatch batch = new CompositionBatch();
      AttributedModelServices.AddExportedValue<T>(batch, contractName, exportedValue);
      container.Compose(batch);
    }

    /// <summary>
    /// Creates a composable part from the specified attributed object, and adds it to the specified composition batch.
    /// </summary>
    /// 
    /// <returns>
    /// The new part.
    /// </returns>
    /// <param name="batch">The batch to add to.</param><param name="attributedPart">The object to add.</param>
    public static ComposablePart AddPart(this CompositionBatch batch, object attributedPart)
    {
      Requires.NotNull<CompositionBatch>(batch, "batch");
      Requires.NotNull<object>(attributedPart, "attributedPart");
      ComposablePart part = AttributedModelServices.CreatePart(attributedPart);
      batch.AddPart(part);
      return part;
    }

    /// <summary>
    /// Creates composable parts from an array of attributed objects and composes them in the specified composition container.
    /// </summary>
    /// <param name="container">The composition container to perform composition in.</param><param name="attributedParts">An array of attributed objects to compose.</param>
    public static void ComposeParts(this CompositionContainer container, params object[] attributedParts)
    {
      Requires.NotNull<CompositionContainer>(container, "container");
      Requires.NotNullOrNullElements<object>((IEnumerable<object>) attributedParts, "attributedParts");
      CompositionBatch batch = new CompositionBatch((IEnumerable<ComposablePart>) Enumerable.ToArray<ComposablePart>(Enumerable.Select<object, ComposablePart>((IEnumerable<object>) attributedParts, (Func<object, ComposablePart>) (attributedPart => AttributedModelServices.CreatePart(attributedPart)))), Enumerable.Empty<ComposablePart>());
      container.Compose(batch);
    }

    /// <summary>
    /// Composes the specified part by using the specified composition service, with recomposition disabled.
    /// </summary>
    /// 
    /// <returns>
    /// The composed part.
    /// </returns>
    /// <param name="compositionService">The composition service to use.</param><param name="attributedPart">The part to compose.</param>
    public static ComposablePart SatisfyImportsOnce(this ICompositionService compositionService, object attributedPart)
    {
      Requires.NotNull<ICompositionService>(compositionService, "compositionService");
      Requires.NotNull<object>(attributedPart, "attributedPart");
      ComposablePart part = AttributedModelServices.CreatePart(attributedPart);
      compositionService.SatisfyImportsOnce(part);
      return part;
    }

    /// <summary>
    /// Composes the specified part by using the specified composition service, with recomposition disabled and using the specified reflection context.
    /// </summary>
    /// 
    /// <returns>
    /// The composed part.
    /// </returns>
    /// <param name="compositionService">The composition service to use.</param><param name="attributedPart">The part to compose.</param><param name="reflectionContext">The reflection context for the part.</param><exception cref="T:System.ArgumentNullException"><paramref name="reflectionContext"/> is null.</exception>
    public static ComposablePart SatisfyImportsOnce(this ICompositionService compositionService, object attributedPart, ReflectionContext reflectionContext)
    {
      Requires.NotNull<ICompositionService>(compositionService, "compositionService");
      Requires.NotNull<object>(attributedPart, "attributedPart");
      Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
      ComposablePart part = AttributedModelServices.CreatePart(attributedPart, reflectionContext);
      compositionService.SatisfyImportsOnce(part);
      return part;
    }

    /// <summary>
    /// Returns a value that indicates whether the specified part contains an export that matches the specified contract type.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="part"/> contains an export definition that matches <paramref name="contractType"/>; otherwise, false.
    /// </returns>
    /// <param name="part">The part to search.</param><param name="contractType">The contract type.</param>
    public static bool Exports(this ComposablePartDefinition part, Type contractType)
    {
      Requires.NotNull<ComposablePartDefinition>(part, "part");
      Requires.NotNull<Type>(contractType, "contractType");
      return ScopingExtensions.Exports(part, AttributedModelServices.GetContractName(contractType));
    }

    /// <summary>
    /// Returns a value that indicates whether the specified part contains an export that matches the specified contract type.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="part"/> contains an export definition of type <paramref name="T"/>; otherwise, false.
    /// </returns>
    /// <param name="part">The part to search.</param><typeparam name="T">The contract type.</typeparam>
    public static bool Exports<T>(this ComposablePartDefinition part)
    {
      Requires.NotNull<ComposablePartDefinition>(part, "part");
      return AttributedModelServices.Exports(part, typeof (T));
    }

    /// <summary>
    /// Returns a value that indicates whether the specified part contains an import that matches the specified contract type.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="part"/> contains an import definition that matches <paramref name="contractType"/>; otherwise, false.
    /// </returns>
    /// <param name="part">The part to search.</param><param name="contractType">The contract type.</param>
    public static bool Imports(this ComposablePartDefinition part, Type contractType)
    {
      Requires.NotNull<ComposablePartDefinition>(part, "part");
      Requires.NotNull<Type>(contractType, "contractType");
      return ScopingExtensions.Imports(part, AttributedModelServices.GetContractName(contractType));
    }

    /// <summary>
    /// Returns a value that indicates whether the specified part contains an import that matches the specified contract type.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="part"/> contains an import definition of type <paramref name="T"/>; otherwise, false.
    /// </returns>
    /// <param name="part">The part to search.</param><typeparam name="T">The contract type.</typeparam>
    public static bool Imports<T>(this ComposablePartDefinition part)
    {
      Requires.NotNull<ComposablePartDefinition>(part, "part");
      return AttributedModelServices.Imports(part, typeof (T));
    }

    /// <summary>
    /// Returns a value that indicates whether the specified part contains an import that matches the specified contract type and import cardinality.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="part"/> contains an import definition that matches <paramref name="contractType"/> and <paramref name="importCardinality"/>; otherwise, false.
    /// </returns>
    /// <param name="part">The part to search.</param><param name="contractType">The contract type.</param><param name="importCardinality">The import cardinality.</param>
    public static bool Imports(this ComposablePartDefinition part, Type contractType, ImportCardinality importCardinality)
    {
      Requires.NotNull<ComposablePartDefinition>(part, "part");
      Requires.NotNull<Type>(contractType, "contractType");
      return ScopingExtensions.Imports(part, AttributedModelServices.GetContractName(contractType), importCardinality);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified part contains an import that matches the specified contract type and import cardinality.
    /// </summary>
    /// 
    /// <returns>
    /// true if <paramref name="part"/> contains an import definition of type <paramref name="T"/> that has the specified import cardinality; otherwise, false.
    /// </returns>
    /// <param name="part">The part to search.</param><param name="importCardinality">The import cardinality.</param><typeparam name="T">The contract type.</typeparam>
    public static bool Imports<T>(this ComposablePartDefinition part, ImportCardinality importCardinality)
    {
      Requires.NotNull<ComposablePartDefinition>(part, "part");
      return AttributedModelServices.Imports(part, typeof (T), importCardinality);
    }
  }
}
