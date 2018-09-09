// This script add a zero in front of track number if it is less than ten

using Metatogger.Data;

foreach (var file in files)
{
	string currentTrackNumber = file.GetFirstValue(TagName.TrackNumber);
	if (currentTrackNumber != null)
	{
		string trackNumber = currentTrackNumber;

		// If track number contains '/' (e.g. "1/10"), only takes the text before the '/'
		int slashIndex = trackNumber.IndexOf('/');
		if (slashIndex != -1)
			trackNumber = trackNumber.Substring(0, slashIndex);

		if (trackNumber.Length == 1)
			file.SetTag(TagName.TrackNumber, $"0{currentTrackNumber}");
	}
}