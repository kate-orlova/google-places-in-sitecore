﻿using Glass.Mapper.Sc;
using Importer.Enums;
using Importer.ImportProcessors;
using Importer.Models;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using GooglePlacesImport.Interfaces;

namespace GooglePlacesImport.Processors
{
    public class GooglePlacesItemProcessor : BaseImportItemProcessor<Item, ItemDto>, IGooglePlacesItemProcessor
    {
        public GooglePlacesItemProcessor(
            ISitecoreContext sitecoreContext,
            string locationPathOverride = null) : base(sitecoreContext, locationPathOverride)
        {
        }

        protected override Func<ItemDto, string> IdStringFromImportObj { get; }
        protected override Func<Item, string> IdStringFromSitecoreItem { get; }
        protected override string DefaultLocation => string.Empty;
        protected override Func<ItemDto, string> ItemNameFromImportObj { get; }
        protected override bool MapDatabaseFields => true;

        public override Item ProcessItem(ItemDto importObj, IEnumerable<Language> languageVersions,
            string pathOverride = null)
        {
            if (importObj == null || languageVersions == null || !languageVersions.Any())
            {
                throw new ImportLogException
                {
                    Entry = new ImportLogEntry
                    {
                        Level = MessageLevel.Error,
                        Message = "No language versions specified"
                    }
                };
            }

            var defaultLanguage = languageVersions.First();

            var newId = this.CalculateItemId(importObj);
            var itemLocationInTargetContext = pathOverride ?? this.CalculateItemLocation(importObj);
            var defaultItem = this.GetItem(newId, importObj, itemLocationInTargetContext, defaultLanguage);
            defaultItem = this.MapDefaultVersionFields(defaultItem, importObj);
            foreach (var language in languageVersions)
            {
                defaultItem.Language = language.Name;
                this.SaveItem(defaultItem);
            }

            return defaultItem;
        }

        public IEnumerable<Item> GetExistItems(Language language = null)
        {
            return this.GetItems(this.CalculateItemLocation(null), language);
        }

        protected override Item MapDefaultVersionFields(Item item, ItemDto importObj)
        {
            item = base.MapDefaultVersionFields(item, importObj);

            item.PlaceId = importObj.GooglePlaceData?.PlaceId;

            item.BasicFormattedAddress = importObj.GooglePlaceData?.BasicFormattedAddress;
            item.BasicName = importObj.GooglePlaceData?.BasicName;
            item.BasicUrl = importObj.GooglePlaceData?.BasicUrl;
            item.BasicDataImported = importObj.GooglePlaceData?.BasicDataImported ?? new DateTime();

            item.ContactFormattedPhoneNumber = importObj.GooglePlaceData?.ContactFormattedPhoneNumber;
            item.ContactOpeningHours = importObj.GooglePlaceData?.ContactOpeningHours;
            item.ContactDataImported = importObj.GooglePlaceData?.ContactDataImported ?? new DateTime();

            item.AtmosphereRating = importObj.GooglePlaceData?.AtmosphereRating ?? Decimal.Zero;
            item.AtmosphereDataImported = importObj.GooglePlaceData?.AtmosphereDataImported ?? new DateTime();

            return item;
        }
    }
}