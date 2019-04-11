using Importer.Importers;
using Importer.Models;

namespace GooglePlacesImport.Interfaces
{
    public interface IGooglePlacesImporter: IImporter
    {
        ImportLog Run();
    }
}