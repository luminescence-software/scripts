# Scripts repository for Metatogger

Scripts for Metatogger are written is C# and executed by Roslyn.

The coding experience is much better under Visual Studio Code than Metatogger.

The API documentation for the main classes available for scripting in Metatogger can be read online at [luminescence-software.github.io/scripts-doc/](https://luminescence-software.github.io/scripts-doc/).

## Editing in Visual Studio Code

Install the .NET 10 SDK for Windows and the Microsoft C# extension (C# Dev Kit is optional), then open this directory in VS Code. Use the default Roslyn language server (`dotnet.server.useOmnisharp` must be false). VS Code loads `Scripts.IntelliSense.csproj` and restores its SDK references.

The project includes a Metatogger reference assembly; a local Metatogger installation and an installation-specific binary path are not required for IntelliSense. `ScriptContext.cs` supplies the type of the host-provided `files` variable only for editing, so no temporary declaration needs to be added to the scripts.

Run the scripts in Metatogger. This project provides editor context and does not reproduce CSX execution: multiple independent scripts produce C# top-level-statement diagnostics (error CS8802), and `#load`/`#r` (error CS8097) are not treated as scripting directives in an ordinary C# project. The .NET and Metatogger type information remains useful despite those diagnostics, which can be safely ignored.

Here is a basic script template:

```cs
// Describe here what the script does

using System.Linq;
using Metatogger.Data;

 // enter tags name to process => eg. ["TAGNAME1", "TAGNAME2", TagName.TrackNumber]
 // leave the array empty to process all tags => []
string[] tagsToProcess = [];

string NewValue(string oldValue)
{
   return oldValue; // change here to return a custom tag value
}

foreach (var file in files)
   foreach (var tag in file.GetAllTags().Where(kvp => tagsToProcess.Length == 0 || tagsToProcess.Contains(kvp.Key)))
     foreach (string tagValue in tag.Value)
        file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));


// The "files" variable contains the collection of audio files checked in Metatogger : IList<Metatogger.Data.AudioFile> files;
// Dictionary<string, List<string>> AudioFile.GetAllTags() => returns all tags of an audio file
// List<string> AudioFile.GetValues(string tag) => returns all values of a tag
// string AudioFile.GetFirstValue(string tag) => returns first tag value for a tag
// AudioFile.SetTag(string tag, string value, bool overwrite = true, bool addIfExists = false) => add, overwrite or remove tag values (if value = null)
// AudioFile.SetTagValue(string tag, string oldValue, string newValue) => replaces the value of a tag by a new value
```

Feel free to submit improvement or new script with "pull request".
