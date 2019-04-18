using Importer.Models;
using System;

namespace Importer.Search
{
    public interface IBaseSearchManager<out TItem> where TItem : class, IGlassBase
    {
        TItem FindItem(Guid itemGuid);
    }
}
