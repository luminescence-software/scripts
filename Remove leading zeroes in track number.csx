// This script removes the leading zeroes in the track number, the opposite of "Use two digits for track number"

using System;
using Metatogger.Data;

foreach (var file in files)
{
	int trackNumber;
	if (Int32.TryParse(file.GetFirstValue(TagName.TrackNumber), out trackNumber))
		file.SetTag(TagName.TrackNumber, $ "{trackNumber}");
}