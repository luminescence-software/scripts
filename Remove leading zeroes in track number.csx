// This script removes the leading zeroes in the track number, the opposite of "Use two digits for track number"

using System;
using Metatogger.Data;

foreach (var file in files)
{
	if (Int32.TryParse(file.GetFirstValue(TagName.TrackNumber), out int trackNumber))
		file.SetTag(TagName.TrackNumber, trackNumber.ToString());
}