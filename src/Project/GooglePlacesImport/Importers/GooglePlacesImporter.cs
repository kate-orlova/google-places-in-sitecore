using Glass.Mapper.Sc;
using GooglePlacesImport.Interfaces;
using Importer.Importers;
using Importer.Models;
using System;
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