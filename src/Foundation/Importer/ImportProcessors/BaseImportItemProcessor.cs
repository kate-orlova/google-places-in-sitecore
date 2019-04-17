using Importer.Models;

namespace Importer.ImportProcessors
{
    public abstract class BaseImportItemProcessor<TItem, TImportObj> : IBaseImportItemProcessor<TItem, TImportObj> where TItem : GlassBase, IGlassBase
    {
    }
}
