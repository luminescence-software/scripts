// This script allows to transliterate cyrillic characters to latin and vice versa

#load ".CyrillicTransliteration.csx"

using System.Linq;
using Metatogger.Data;

// enter tags name to process => eg. { "TAGNAME1", "TAGNAME2", TagName.TrackNumber }
// leave the array empty to process all tags => { }

string[] tagsToProcess = {  };

// Available transliteration methods:
// Transliteration.CyrillicToLatin(string cyrillicSource, Language language = Language.Unknown)
// Transliteration.LatinToCyrillic(string latinSource, Language language = Language.Unknown)

// Available Language enumeration:
// Unknown (Most common rules will be used for transliteration), Russian, Belorussian, Ukrainian, Bulgarian, Macedonian

foreach (var file in files)
	foreach (var tag in file.GetAllTags().Where(kvp => tagsToProcess.Length == 0 || tagsToProcess.Contains(kvp.Key)))
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, Transliteration.CyrillicToLatin(tagValue, Language.Unknown));