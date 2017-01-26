// This script removes the leading zeroes in the track number, the opposite of "Use two digits for track number.csx"

using Metatogger.Data;
using System;

foreach	(var file in files)
{
	int trackNumber = Int32.Parse(file.GetFirstValue(TagName.TrackNumber));
	if (trackNumber != null && trackNumber < 10)
	{
		file.SetTag(TagName.TrackNumber, $"{trackNumber}");
	}
}
