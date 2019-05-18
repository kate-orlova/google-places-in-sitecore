using Glass.Mapper.Sc;
using GooglePlacesImport.Interfaces;
using Importer.Importers;
using Importer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using Importer.Enums;
using log4net;

namespace GooglePlacesImport.Importers
{
    public class GooglePlacesImporter : BaseImporter, IGooglePlacesImporter
    {
        protected readonly ILog Logger;
        private readonly ISitecoreContext _sitecoreContext;
        private readonly IGooglePlacesItemProcessor _googlePlacesItemProcessor;
        private readonly IMapper _mapper;
        private readonly IGooglePlacesService _googlePlacesService;

        public GooglePlacesImporter(ISitecoreContext sitecoreContext, IGooglePlacesItemProcessor googlePlacesItemProcessor, IMapper mapper, IGooglePlacesService googlePlacesService) : base(sitecoreContext)
        {
            this._sitecoreContext = sitecoreContext;
           this._googlePlacesItemProcessor = googlePlacesItemProcessor;
            this._mapper = mapper;
            this._googlePlacesService = googlePlacesService;
        }

        public ImportLog Run()
        {
            var log = new ImportLog();
            var existingItems = _googlePlacesItemProcessor.GetExistItems();
            var existingItemsDtos = _mapper.Map<IEnumerable<ItemDto>>(existingItems);
            var placesSearchLog = new List<ImportLogEntry>();
            var itemsWithPlaceId = _googlePlacesService.PopulateGooglePlacesIds(existingItemsDtos, true, ref placesSearchLog);
            var itemsWithPlaceData = _googlePlacesService.PopulateGooglePlacesData(itemsWithPlaceId, ref placesSearchLog);
            log.Entries.AddRange(placesSearchLog);

            foreach (var itemDto in itemsWithPlaceData)
            {
                var sw = new Stopwatch();
                sw.Start();

                var item = _googlePlacesItemProcessor.ProcessItem(itemDto);
                sw.Stop();
                var importLogEntry = new ImportLogEntry
                {
                    Level = MessageLevel.Info,
                    Action = ImportAction.Imported,
                    Id = item.Id,
                    Message = $"{itemDto.CompanyName} - Google Place data has been imported successfully"
                };
                WriteToLog(importLogEntry);
                log.Entries.Add(importLogEntry);
            }

            return log;
        }
        private void WriteToLog(ImportLogEntry importLogEntry)
        {
            switch (importLogEntry.Level)
            {
                case MessageLevel.Error:
                    Logger.Error(importLogEntry.Message);
                    break;
                case MessageLevel.Critical:
                    Logger.Fatal(importLogEntry.Message);
                    break;
                case MessageLevel.Info:
                default:
                    Logger.Info(importLogEntry.Message);
                    break;
            }
        }
    }
}