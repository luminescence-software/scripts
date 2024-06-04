// This script checks for the values of the "Album" tag and the "Disc Number" tag.
// If the "Album" tag contains "CD#" or "CD #" where # is a number between 1-99,
// it populates the "Disc Number" tag with that number if the "Disc Number" tag is null or empty.

using System.Linq;
using System.IO;
using Metatogger.Data;
using System.Collections.Generic;
using System;  // Ensure we include System for Exception
using System.Text.RegularExpressions;

// Uncomment the following line to get a better coding experience with VS Code, but DO NOT forget to comment it before running the script into host
// System.Collections.Generic.IEnumerable<Metatogger.Data.AudioFile> files = new System.Collections.Generic.List<Metatogger.Data.AudioFile>(); // list of checked audio files loaded in the workspace

try
{
    // Define the regex pattern to find "CD#" or "CD #"
    var cdPattern = new Regex(@"\bCD\s*(\d{1,2})\b", RegexOptions.IgnoreCase);

    foreach (var file in files)
    {
        var allTags = file.GetAllTags();

        // Convert tags to a dictionary with lowercase keys for case-insensitive access
        var tagsDictionary = allTags.ToDictionary(tag => tag.Key.ToLower(), tag => tag.Value);

        // Check if the "Album" tag exists and contains the pattern
        if (tagsDictionary.TryGetValue("album", out var albumTags))
        {
            var albumTag = albumTags.FirstOrDefault();
            if (albumTag != null)
            {
                var match = cdPattern.Match(albumTag);
                if (match.Success && int.TryParse(match.Groups[1].Value, out int discNumber))
                {
                    // Check if the "Disc Number" tag is null or empty
                    if (!tagsDictionary.TryGetValue("disc number", out var discNumberTags) || discNumberTags.Count == 0)
                    {
                        // Use Metatogger's predefined tag names if available
                        file.SetTag(TagName.DiscNumber, discNumber.ToString(), true);  // Assuming TagName.DiscNumber is the correct predefined tag name
                    }
                }
            }
        }
    }
}
catch (Exception ex)
{
    // Handle exception if needed
    Console.WriteLine($"Error during script execution: {ex.Message}");
}

// The "files" variable contains the collection of audio files checked in Metatogger
// Dictionary<string, List<string>> AudioFile.GetAllTags() => returns all tags of an audio file
// List<string> AudioFile.GetValues(string tag) => returns all values of a tag
// string AudioFile.GetFirstValue(string tag) => returns first tag value for a tag
// AudioFile.SetTag(string tag, string value, bool overwrite = true, bool addIfExists = false) => add, overwrite or remove tag values (if value = null)
// AudioFile.SetTagValue(string tag, string oldValue, string newValue) => replaces the value of a tag by a new value
