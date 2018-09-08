using Metatogger.Data;
using System;

// If no track number, checks the first two 
// digits and adds them as the track number 

// NOTE: Assumes a 2-digit track number. 
// You may want to filter out songs with a 
// 3-digit track number first.

foreach (var file in files)
{
	string filename = file.OutputFilename;
	
	var trackNumber = filename.Substring(0,2);
	
	var trackIsEmpty = string.IsNullOrWhiteSpace(file.GetFirstValue(TagName.TrackNumber));
	
	int throwaway;
	bool trackNumberIsNumber = int.TryParse(trackNumber, out throwaway);
	if (trackIsEmpty && trackNumberIsNumber)
	{
		file.SetTag(TagName.TrackNumber, trackNumber);
	}
}
