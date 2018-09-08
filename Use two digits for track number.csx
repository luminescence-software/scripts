// This script add a zero in front of track number if it is less than ten

using Metatogger.Data;
using System;

foreach (var file in files)
{
	// Get the current track number
	string trackNumber = file.GetFirstValue(TagName.TrackNumber);
	if (trackNumber.Contains("/"))
	{
		// If track contains '/' (e.g. "1/10"), only takes the text before the "/"
		int charLocation = trackNumber.IndexOf("/", StringComparison.Ordinal);
		trackNumber = trackNumber.Substring(0, charLocation);
	}
	
	// Adds leading zero
	if (trackNumber != null && trackNumber.Length == 1)
	{
		trackNumber = $"0{trackNumber}";
	}
	
	// Finally, sets the track number
	file.SetTag(TagName.TrackNumber, trackNumber);
}
