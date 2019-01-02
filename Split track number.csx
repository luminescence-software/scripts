// This script replaces track number format used in iTunes "track number/total number of tracks" ("3/10" -> "3")

using Metatogger.Data;

foreach (var file in files)
{
	string trackNumber = file.GetFirstValue(TagName.TrackNumber);
	if (trackNumber != null)
	{
		int index = trackNumber.IndexOf('/');
		if (index != -1)
			file.SetTag(TagName.TrackNumber, trackNumber.Substring(0, index));
	}
}