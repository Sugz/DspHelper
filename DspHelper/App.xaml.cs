﻿using DspHelper.Models;
using DspHelper.ViewModels;
using DspHelper.ViewModels.ItemsCollection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DspHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Viewmodels
            services.AddSingleton<DspComponentsCollectionViewModel>();
            services.AddSingleton<DspBuildingsCollectionViewModel>();
            services.AddSingleton<DspSourcesCollectionViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
