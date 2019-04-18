using Importer.Models;

namespace Importer.Repositories
{
    public interface IGenericSitecoreItemRepository<TItem> where TItem : GlassBase, IGlassBase
    {
    }
}
