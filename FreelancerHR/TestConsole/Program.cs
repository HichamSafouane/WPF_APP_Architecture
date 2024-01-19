using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //AggregateCatalog aggregateCatalog = new AggregateCatalog();
            //AggregateCatalog childAggregateCatalog = new AggregateCatalog();
            //CompositionScopeDefinition compositionScopeDefinition = aggregateCatalog.AsScope(childAggregateCatalog.AsScope());
            //CompositionContainer compositionContainer = new CompositionContainer(compositionScopeDefinition);

            //TypeCatalog globalParts = new TypeCatalog(typeof(Application));
            //TypeCatalog scopedParts = new TypeCatalog(typeof(ProcessA), typeof(ProcessA), typeof(ProcessB), typeof(PluginA), typeof(DAL));

            //aggregateCatalog.Catalogs.Add(globalParts);
            //childAggregateCatalog.Catalogs.Add(scopedParts);

            //Application requestListener = compositionContainer.GetExportedValue<Application>();


            var scopeDependentCatalog = new TypeCatalog(
                typeof(ProcessA),
                typeof(ProcessB),
                typeof(PluginA),
                typeof(PluginB),
                typeof(DAL));
            var scopeDefDependent = new CompositionScopeDefinition(scopeDependentCatalog, null);

            var appCatalog = new TypeCatalog(typeof(Application));
            var scopeDefRoot = new CompositionScopeDefinition(appCatalog, new[] { scopeDefDependent });


            var container = new CompositionContainer(scopeDefRoot);

            var app = container.GetExportedValue<Application>();

            app.WriteLayoutA();
            Console.WriteLine("————————————");
            app.WriteLayoutB();

        }
    }

    // Handy extension methods for dealing with CompositionScopeDefinition (Not relevant to this answer but useful).
    public static class ComposablePartCatalogExtensions
    {
        public static CompositionScopeDefinition AsScope(this ComposablePartCatalog catalog, params CompositionScopeDefinition[] children)
        {
            return new CompositionScopeDefinition(catalog, children);
        }

        public static CompositionScopeDefinition AsScopeWithPublicSurface<T>(this ComposablePartCatalog catalog, params CompositionScopeDefinition[] children)
        {
            IEnumerable<ExportDefinition> definitions = catalog.Parts.SelectMany((p) => p.ExportDefinitions.Where((e) => e.ContractName == AttributedModelServices.GetContractName(typeof(T))));
            return new CompositionScopeDefinition(catalog, children, definitions);
        }
    }


  [Export]
  public class Application
  {
      [Import]
      private ExportFactory<ProcessA> ProcAFactory { get; set;}
      [Import]
      private ExportFactory<ProcessB> ProcBFactory { get; set;}
 
      public void WriteLayoutA()
      {
          using (ExportLifetimeContext<ProcessA> lifeOfA = ProcAFactory.CreateExport())
          {
              ProcessA a = lifeOfA.Value;
              Console.WriteLine("Proc A");
              Console.WriteLine("\tPlug A: {0}", a.PlugA.GetHashCode());
              Console.WriteLine("\t\tDal: {0}", a.PlugA.Dal.GetHashCode());
              Console.WriteLine("\tPlug B: {0}", a.PlugB.GetHashCode());
              Console.WriteLine("\t\tDal: {0}", a.PlugB.Dal.GetHashCode());
          }
      }
 
      public void WriteLayoutB()
      {
          using (ExportLifetimeContext<ProcessB> lifeOfB = ProcBFactory.CreateExport())
          {
              ProcessB b = lifeOfB.Value;
              Console.WriteLine("Proc B");
              Console.WriteLine("\tPlug A: {0}", b.PlugA.GetHashCode());
              Console.WriteLine("\t\tDal: {0}", b.PlugA.Dal.GetHashCode());
              Console.WriteLine("\tPlug B: {0}", b.PlugB.GetHashCode());
              Console.WriteLine("\t\tDal: {0}", b.PlugB.Dal.GetHashCode());
          }
      }
  }


    [Export]
    public class ProcessA
    {
        [Import]
        public PluginA PlugA { get; private set; }
        [Import]
        public PluginB PlugB { get; private set; }
    }

    [Export]
    public class ProcessB
    {
        [Import]
        public PluginA PlugA { get; private set; }
        [Import]
        public PluginB PlugB { get; private set; }
    }

    [Export]
    public class PluginA
    {
        [Import]
        public DAL Dal { get; private set; }
    }

    [Export]
    public class PluginB
    {
        [Import]
        public DAL Dal { get; private set; }
    }

    [Export]
    public class DAL
    {
    }
}
