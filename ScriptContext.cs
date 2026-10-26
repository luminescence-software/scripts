// Declaration reserved for the editor. Metatogger provides its own files variable at runtime.

global using static MetatoggerScriptEditorContext;

internal static class MetatoggerScriptEditorContext
{
    /// <summary>
    /// The collection of audio files to process.
    /// </summary>
    public static System.Collections.Generic.IList<Metatogger.Data.AudioFile> files
        => throw new System.InvalidOperationException("This context is intended solely for IntelliSense. Run the scripts in Metatogger.");
}