using Importer.Models;

namespace Importer.ImportProcessors
{
    public interface IBaseImportItemProcessor<out TItem, TImportObj> where TItem : IGlassBase
    {
    }
}
