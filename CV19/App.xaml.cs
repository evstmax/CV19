using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CV19.Services;

namespace CV19
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static bool InDesignMode { get; private set; } = true;


        protected override void OnStartup(StartupEventArgs e)
        {
            InDesignMode = false;
            base.OnStartup(e);
            var service_test = new DataService();
            var countries = service_test.GetData().ToArray();
        }
    }
}
