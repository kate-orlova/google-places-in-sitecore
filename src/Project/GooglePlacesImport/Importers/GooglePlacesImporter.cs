using Glass.Mapper.Sc;
using GooglePlacesImport.Interfaces;
using Importer.Importers;
using Importer.Models;
using System;

namespace GooglePlacesImport.Importers
{
    public class GooglePlacesImporter : BaseImporter, IGooglePlacesImporter
    {
        public GooglePlacesImporter(ISitecoreContext sitecoreContext) : base(sitecoreContext)
        {
        }

        public ImportLog Run()
        {
            throw new NotImplementedException();
        }
    }
}