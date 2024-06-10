// This script checks for the values of the "Album" tag and the "Disc Number" tag.
// If the "Album" tag contains "CD#" or "CD #" where # is a number between 1-99,
// it populates the "Disc Number" tag with that number, if the "Disc Number" tag is empty.
// Optionally, it can also remove the disc number from the album tag.

using System;
using System.Text.RegularExpressions;
using Metatogger.Data;

bool cleanAlbumTag = true;
var cdPattern = new Regex(@"\bCD\s*(\d{1,2})\b", RegexOptions.IgnoreCase);

foreach (var file in files)
{
	string album = file.GetFirstValue(TagName.Album);
	if (album != null)
	{
		Match match = cdPattern.Match(album);
		if (match.Success && Int32.TryParse(match.Groups[1].Value, out int discNumber))
		{
			file.SetTag(TagName.DiscNumber, discNumber.ToString(), false);
			if (cleanAlbumTag)
				file.SetTagValue(TagName.Album, album, album.Replace(match.Value, null).Replace("  ", " "));
		}
	}
}