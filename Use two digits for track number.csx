// This script add a zero in front of track number if it is less than ten

using Metatogger.Data;

foreach (var file in files)
{
	string trackNumber = file.GetFirstValue(TagName.TrackNumber);
	if (trackNumber != null && trackNumber.Length == 1)
		file.SetTag(TagName.TrackNumber, $"0{trackNumber}");
}