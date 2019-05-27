[![GitHub release](https://img.shields.io/github/release-date/kate-orlova/google-places-in-sitecore.svg?style=flat)](https://github.com/kate-orlova/google-places-in-sitecore/releases/tag/v1.0)
[![GitHub license](https://img.shields.io/github/license/kate-orlova/google-places-in-sitecore.svg)](https://github.com/kate-orlova/google-places-in-sitecore/blob/master/LICENSE)
![GitHub language count](https://img.shields.io/github/languages/count/kate-orlova/google-places-in-sitecore.svg?style=flat)
![GitHub top language](https://img.shields.io/github/languages/top/kate-orlova/google-places-in-sitecore.svg?style=flat)
![HitHub repo size](https://img.shields.io/github/repo-size/kate-orlova/google-places-in-sitecore.svg?style=flat)

# Google Places in Sitecore
Google Places in Sitecore module is an open source project about [Google Places](https://cloud.google.com/maps-platform/places/) integration with Sitecore.

The taken approach is based on [SiteCron](https://www.nuget.org/packages/SiteCron) module providing an advanced execution of scheduled tasks in Sitecore. The solution adheres to [Helix](https://helix.sitecore.net/) principles and consists of:
1. **Foundation layer** with an _Importer_ project implementing the core models, repositories and import processors;
2. **Project layer** with a _GooglePlacesImport_ project implementing the main SiteCron task _..\src\Project\GooglePlacesImport\SitecronTasks\ImportGooglePlaces.cs_ for importing data from [Google Places](https://cloud.google.com/maps-platform/places/) to a Sitecore website.

## Configuration
Configuration settings are defined via a Sitecore Item _..\src\Project\GooglePlacesImport\Models\GooglePlacesSettings.cs_, so that one can easy adjust them for their needs:
* **GoogleApiKey** - your Google API Key
* **GooglePlaceSearchRequest**, for example, _https://maps.googleapis.com/maps/api/place/findplacefromtext/json?key={0}&input={1}&inputtype=textquery&fields=place_id&locationbias=circle:1000@{2},{3}_
* **GooglePlaceDetailsByIdRequest**, for example, _https://maps.googleapis.com/maps/api/place/details/json?key={0}&placeid={1}&fields={2}_
* **GooglePlaceBasicDataFields**, for example, _"name,url,formatted_address"_
* **GooglePlaceBasicDataCacheMinutes**, for example, _10080_
* **GooglePlaceContactDataFields**, for example, _"formatted_phone_number,opening_hours"_
* **GooglePlaceContactDataCacheMinutes**, for example, _10080_
* **GooglePlaceAtmosphereDataFields**, for example, _"rating"_
* **GooglePlaceAtmosphereDataCacheMinutes**, for example, _10080_

# Contribution
Hope you found this module useful, your contributions and suggestions will be very much appreciated. Please submit a pull request.

# License
The Google Places in Sitecore module is released under the MIT license what means that you can modify and use it how you want even for commercial use. Please give it a star if you like it and your experience was positive.
