using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using Importer.Models;

namespace GooglePlacesImport.Models
{
    [SitecoreType(TemplateId = "{TEMPLATE_ID}")]
    public class GooglePlacesSettings : GlassBase
    {
        /// <summary>
        /// Google Place Search Request field
        /// <para>Field Type: Single-Line Text</para>		
        /// </summary>
        [SitecoreField(FieldId = "{FIELD_ID}", FieldName = "Google Place Search Request")]
        public string GooglePlaceSearchRequest { get; set; }

        /// <summary>
        /// Google Place Details By Id Request field
        /// <para>Field Type: Single-Line Text</para>		
        /// </summary>
        [SitecoreField(FieldId = "{FIELD_ID}", FieldName = "Google Place Details By Id Request")]
        public string GooglePlaceDetailsByIdRequest { get; set; }

        /// <summary>
        /// Google Place Basic Data Fields field
        /// <para>Field Type: Single-Line Text</para>		
        /// </summary>
        [SitecoreField(FieldId = "{FIELD_ID}", FieldName = "Google Place Basic Data Fields")]
        public string GooglePlaceBasicDataFields { get; set; }

        /// <summary>
        /// Google Place Basic Data Cache Minutes field
        /// <para>Field Type: Integer</para>		
        /// </summary>
        [SitecoreField(FieldId = "{FIELD_ID}", FieldName = "Google Place Basic Data Cache Minutes")]
        public Int32 GooglePlaceBasicDataCacheMinutes { get; set; }

        /// <summary>
        /// Google Place Contact Data Fields field
        /// <para>Field Type: Single-Line Text</para>		
        /// </summary>
        [SitecoreField(FieldId = "{FIELD_ID}", FieldName = "Google Place Contact Data Fields")]
        public string GooglePlaceContactDataFields { get; set; }

        /// <summary>
        /// Google Place Contact Data Cache Minutes field
        /// <para>Field Type: Integer</para>		
        /// </summary>
        [SitecoreField(FieldId = "{FIELD_ID}", FieldName = "Google Place Contact Data Cache Minutes")]
        public Int32 GooglePlaceContactDataCacheMinutes { get; set; }

        /// <summary>
        /// Google Place Atmosphere Data Fields field
        /// <para>Field Type: Single-Line Text</para>		
        /// </summary>
        [SitecoreField(FieldId = "{FIELD_ID}", FieldName = "Google Place Atmosphere Data Fields")]
        public string GooglePlaceAtmosphereDataFields { get; set; }

        /// <summary>
        /// Google Place Atmosphere Data Cache Minutes field
        /// <para>Field Type: Integer</para>		
        /// </summary>
        [SitecoreField(FieldId = "{FIELD_ID}", FieldName = "Google Place Atmosphere Data Cache Minutes")]
        public Int32 GooglePlaceAtmosphereDataCacheMinutes { get; set; }
    }
}