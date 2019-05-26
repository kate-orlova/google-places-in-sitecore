using GooglePlacesImport.Importers;
using GooglePlacesImport.Interfaces;
using GooglePlacesImport.Processors;
using GooglePlacesImport.Services;
using Importer.ImportProcessors;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace GooglePlacesImport.DependencyInjection
{
    public class RegisterServices : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGooglePlacesImporter, GooglePlacesImporter>();
            serviceCollection.AddTransient<IGooglePlacesItemProcessor, GooglePlacesItemProcessor>();
            serviceCollection.AddTransient<IGooglePlacesService, GooglePlacesService>();
        }
    }
}