using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.MefExtensions;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition;
using FreelancerHR.Repository;
using System.ComponentModel.Composition.Registration;
using System.Reflection;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using FreelancerHR.Service.Mapping;

namespace FreelancerHR.Main
{
    public  class FreelancerHRBootstrapper : MefBootstrapper
    {
        
        protected override void ConfigureAggregateCatalog()
        {
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(FreelancerHRBootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(Shell), typeof(FreelancerHRBootstrapper)));
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell) this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.GetExportedValue<IAutoMapperServiceConfiguration>().Configure();
        }

        protected override AggregateCatalog CreateAggregateCatalog()
        {
            AggregateCatalog aggregateCatalog = base.CreateAggregateCatalog();
            RegistrationBuilder registrationBuilder = new RegistrationBuilder();

            //registrationBuilder.ForType<MainWindow>().ImportProperty<MainWindowViewModel>(p => p.DataContext);
            ////add current assembly to catalog
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly, registrationBuilder));

        //    .SetCreationPolicy(CreationPolicy.NonShared).Export();
 
        //registrationBuilder.ForTypesDerivedFrom<ApiController>()
        //    .SetCreationPolicy(CreationPolicy.NonShared).Export();
 
        //registrationBuilder
        //    .ForTypesMatching(t =>
        //        t.FullName.StartsWith(
        //            Assembly
        //                .GetExecutingAssembly()
        //                .GetName().Name + ".Parts."))
        //    .SetCreationPolicy(CreationPolicy.NonShared)
        //    .ExportInterfaces(x => x.IsPublic);
 
        //var aggregateCatalog = new AggregateCatalog();
 
        //aggregateCatalog.Catalogs.Add(
        //    new AssemblyCatalog(Assembly.GetExecutingAssembly(), registrationBuilder));
 
        //// Set up all the Mef conventions for our repository assembly
        //registrationBuilder = new RegistrationBuilder();
 
        //registrationBuilder.ForTypesDerivedFrom<IUnitOfWork>().Export<IUnitOfWork>();
 
        //aggregateCatalog.Catalogs.Add(
        //    new AssemblyCatalog(typeof(IUnitOfWork).Assembly, registrationBuilder));
 
        //// Set up all the Mef conventions for our data assembly
        //registrationBuilder = new RegistrationBuilder();
 
        //registrationBuilder.ForTypesDerivedFrom<IDbContext>().Export<IDbContext>();
 
        //aggregateCatalog.Catalogs.Add(
        //    new AssemblyCatalog(typeof(IDbContext).Assembly, registrationBuilder));
 
        // Add all our catalogs with Mef conventions to our container
        //MefMvcConfig.RegisterMef(new CompositionContainer(aggregateCatalog));

            AggregateCatalog childAggregateCatalog = new AggregateCatalog();
            compositionScopeDefinition = aggregateCatalog.AsScope(childAggregateCatalog.AsScope());


            childAggregateCatalog.Catalogs.Add(new AssemblyCatalog(@"FreelancerHR.Service.dll"));
            childAggregateCatalog.Catalogs.Add(new AssemblyCatalog(@"FreelancerHR.Repository.dll"));



            var managerCatalog = new TypeCatalog(typeof(ViewModel));
            var partCatalog = new TypeCatalog(typeof(Service), typeof(Repository), typeof(Repository1), typeof(DatabaseConnection));

            aggregateCatalog.Catalogs.Add(managerCatalog);
            aggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IAutoMapperServiceConfiguration).Assembly));
            childAggregateCatalog.Catalogs.Add(partCatalog);

            return aggregateCatalog;
        }

        CompositionScopeDefinition compositionScopeDefinition;

        protected override CompositionContainer CreateContainer()
        {
            CompositionContainer container = new CompositionContainer(compositionScopeDefinition);

            //Not needed!
            container.ComposeExportedValue<CompositionContainer>(container);

            //var viewModel = container.GetExportedValue<ViewModel>();
            //viewModel.HandleRequest();
        
            return container;
        }
    }

    interface IViewModel
    {
        ExportLifetimeContext<Service> CreateCarHost();
        void HandleRequest();
    }

    [Export]
    public class ViewModel : FreelancerHR.Main.IViewModel
    {
        [Import]
        ExportFactory<Service> _factory;


        static int Coun = 0;
        int Counter = 0;

        [ImportingConstructor]
        public ViewModel()
        {
            Counter = ++Coun;
            Debug.WriteLine("ViewModel#####################" + Counter);


        }

        public ExportLifetimeContext<Service> CreateCarHost()
        {
            ExportLifetimeContext<Service> service = _factory.CreateExport();
            return service;
        }

        public void HandleRequest()
        {
            using (var instance = _factory.CreateExport())
            {
                instance.Value.Repository.Process();
            }
        }
    }

    interface IService
    {
        void Proccess();
    }

    [Export]
    //[PartCreationPolicy(CreationPolicy.Shared)]
    public class Service : IDisposable, IService
    {
        [Import]
        public Repository1 Repository1 { get; set; }


        private Repository _Repository;

        [Import]
        public Repository Repository
        {
            get { return _Repository; }
            set 
            { 
                _Repository = value;
            }
        }

        static int Coun = 0;
        int Counter = 0;

        public Service()
        {
            Counter = ++Coun;
            Debug.WriteLine("Service " + Counter);
        }

        public void Proccess()
        {
            Repository.Process();
        }


        public void Dispose()
        {

        }
    }

    interface IRepository
    {
        void Process();
    }

        [Export]
    public class Repository : IDisposable, IRepository
        {
            static int Coun = 0;
            int Counter = 0;

            DatabaseConnection connection;

            [ImportingConstructor]
            public Repository(DatabaseConnection connection)
            {
                Counter = ++Coun;
                this.connection = connection;
                Debug.WriteLine("Repository " + Counter);
            }

            public void Process()
            {
                this.connection.DBProcess();
            }

            ~Repository()
            {

            }

            public void Dispose()
            {

            }
        }

        [Export]
        public class Repository1 : IDisposable, IRepository
        {
            static int Coun = 0;
            int Counter = 0;

            DatabaseConnection connection;

            [ImportingConstructor]
            public Repository1(DatabaseConnection connection)
            {
                Counter = ++Coun;
                this.connection = connection;
                Debug.WriteLine("RepositoryII " + Counter);
            }

            public void Process()
            {
                this.connection.DBProcess();
            }

            ~Repository1()
            {

            }

            public void Dispose()
            {
            }
        }

        [Export]
        public class DatabaseConnection : IDisposable
        {
            static int Coun = 0;
            int Counter = 0;

            public DatabaseConnection()
            {
                Counter = ++Coun;
                Debug.WriteLine("DatabaseConnection " + Counter);

            }

            public void DBProcess()
            {
                Debug.WriteLine("Result DBProcess " + Counter + "\r\n");
            }

            ~DatabaseConnection()
            {
                Debug.WriteLine("~DatabaseConnection()!!!!!!!!!!!!!!!!");
            }

            public void Dispose()
            {

            }
        }





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
}
