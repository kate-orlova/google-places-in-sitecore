using System.Collections.Generic;
using GooglePlacesImport.Interfaces;
using Importer.Models;
using Sitecore.Globalization;

namespace GooglePlacesImport.Processors
{
    public class GooglePlacesItemProcessor : IGooglePlacesItemProcessor
    {
        public Item ProcessItem(ItemDto importObj, IEnumerable<Language> languageVersions, string pathOverride = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Item> GetExistItems(Language language = null)
        {
            throw new System.NotImplementedException();
        }
    }
}