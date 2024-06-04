// This script checks for the values of the "Artist" tag and the "Album Artist" tag.
// If the "Album Artist" tag is empty or null but the "Artist" tag exists and is the same for all files,
// it populates the "Album Artist" tag with the value from the "Artist" tag.

using System.Linq;
using System.IO;
using Metatogger.Data;
using System.Collections.Generic;
using System;  // Ensure we include System for Exception

// Uncomment the following line to get a better coding experience with VS Code, but DO NOT forget to comment it before running the script into host
// System.Collections.Generic.IEnumerable<Metatogger.Data.AudioFile> files = new System.Collections.Generic.List<Metatogger.Data.AudioFile>(); // list of checked audio files loaded in the workspace

try
{
    // Collect all "Artist" tags
    var artistTagsList = new List<string>();

    foreach (var file in files)
    {
        var allTags = file.GetAllTags();

        // Convert tags to a dictionary with lowercase keys for case-insensitive access
        var tagsDictionary = allTags.ToDictionary(tag => tag.Key.ToLower(), tag => tag.Value);

        tagsDictionary.TryGetValue("artist", out var artistTags);

        if (artistTags != null && artistTags.Count > 0)
        {
            artistTagsList.Add(artistTags.FirstOrDefault());
        }
    }

    // Check if all "Artist" tags are the same
    bool allArtistsSame = artistTagsList.Distinct().Count() == 1;
    string commonArtist = allArtistsSame ? artistTagsList.FirstOrDefault() : null;

    if (allArtistsSame && !string.IsNullOrEmpty(commonArtist))
    {
        foreach (var file in files)
        {
            var allTags = file.GetAllTags();

            // Convert tags to a dictionary with lowercase keys for case-insensitive access
            var tagsDictionary = allTags.ToDictionary(tag => tag.Key.ToLower(), tag => tag.Value);

            tagsDictionary.TryGetValue("album artist", out var albumArtistTags);

            if (albumArtistTags == null || albumArtistTags.Count == 0)
            {
                // Use Metatogger's predefined tag names if available
                file.SetTag(TagName.AlbumArtist, commonArtist, true);  // Assuming TagName.AlbumArtist is the correct predefined tag name
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
