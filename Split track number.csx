// This script replaces track number format used in iTunes "track number/total number of tracks" ("3/10" -> "3")

using Metatogger.Data;

string NewValue(string oldValue)
{
	int index = oldValue.IndexOf('/');
	return index == -1 ? oldValue : oldValue.Substring(0, index);
}

foreach (var file in files)
{
	string trackNumber = file.GetFirstValue(TagName.TrackNumber);
	if (trackNumber != null)
		file.SetTag(TagName.TrackNumber, NewValue(trackNumber));
}