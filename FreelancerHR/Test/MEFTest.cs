using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{

    [Export]
    public class Application1
    {
        [Import]
        private ExportFactory<ProcessA> ProcAFactory { get; set; }
        [Import]
        private ExportFactory<ProcessB> ProcBFactory { get; set; }
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
