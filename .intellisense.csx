using System.Collections.ObjectModel;

//namespace Metatogger.Data
//{
   public interface IAudioFile
   {
      string Bitrate { get; }
      string BitsPerSample { get; }
      byte BitsSample { get; }
      string ChannelMode { get; }
      byte Channels { get; }
      void ClearArts();
      string Codec { get; }
      string CodecVersion { get; }
      string Cover { get; set; }
      string CoverDescription { get; }
      string Duration { get; }
      string ExternalCover { get; set; }
      string ExternalCoverDescription { get; }
      FileProcess FileProcess { get; set; }
      int[] Fingerprint { get; }
      string FullPath { get; }
      Dictionary<string, string> GetAllFirstTagsWithExtendedMetadata();
      Dictionary<string, List<string>> GetAllTags();
      string GetFirstValue(string tag);
      string GetPreviousTagValue(string tag);
      List<string> GetValues(string tag);
      bool HasChange(string tag);
      bool HasChanges { get; }
      bool HasCoverChanged { get; }
      bool HasCoverSupport { get; }
      bool HasOutputFilenameChanged { get; }
      bool HasOutputPathChanged { get; }
      bool HasPathChanges { get; }
      bool HasPathError { get; }
      bool HasTagChanges { get; }
      string InputFilename { get; }
      string InputPath { get; }
      bool IsPlaying { get; }
      int NominalBitrate { get; }
      string OutputFilename { get; set; }
      string OutputPath { get; set; }
      string PathError { get; }
      void RejectChange(string tag);
      void RejectChanges();
      string SampleRate { get; }
      void SaveChanges();
      float Seconds { get; }
      string SetTag(string tag, string value, bool overwrite = true, bool addIfExists = false);
      string SetTagValue(string name, string oldValue, string newValue);
      int SimilarityGroupId { get; }
   }
   
   public enum FileProcess
   {
      Copy,
      Move,
      SoftLink,
      Rename
   }

   public static class ExtendedMetadataName
   {
      public const string CodecVersion = "CODECVERSION";
      public const string Codec = "CODEC";
      public const string SampleRate = "SAMPLERATE";
      public const string Duration = "DURATION";
      public const string Bitrate = "BITRATE";
      public const string BitsPerSample = "BITSPERSAMPLE";
      public const string ChannelMode = "CHANNELMODE";
      public const string Channels = "CHANNELS";
      public const string Seconds = "SECONDS";
      public const string TwoDigitsTrackNumber = "TWODIGITSTRACKNB";
      public const string Filename = "FILENAME";
   }

   public static class TagName
   {
      public const string DefaultName = "TAGNAME";

      public const string Artist = "ARTIST";
      public const string Title = "TITLE";
      public const string Date = "DATE";
      public const string Album = "ALBUM";
      public const string Genre = "GENRE";
      public const string TrackNumber = "TRACKNUMBER";

      public const string AlbumArtist = "ALBUMARTIST";
      public const string DiscNumber = "DISCNUMBER";
      public const string Lyricist = "LYRICIST";
      public const string Composer = "COMPOSER";
      public const string Language = "LANGUAGE";
      public const string OriginalAlbum = "ORIGINALALBUM";
      public const string OriginalArtist = "ORIGINALARTIST";
      public const string OriginalDate = "ORIGINALDATE";
      public const string AlbumSort = "ALBUMSORT";
      public const string ArtistSort = "ARTISTSORT";
      public const string AlbumArtistSort = "ALBUMARTISTSORT";
      public const string TitleSort = "TITLESORT";
      public const string Label = "LABEL";
      public const string Comment = "COMMENT";
      public const string Lyrics = "LYRICS";

      private static readonly string[] commonTags = new[] { Artist, Title, Date, Album, Genre, TrackNumber };
      private static readonly string[] uncommonTags = new[] { AlbumArtist, Lyricist, Composer, Language, DiscNumber, OriginalAlbum, OriginalArtist, OriginalDate, AlbumSort, ArtistSort, TitleSort, AlbumArtistSort, Label, Comment, Lyrics };
      //private static readonly string[] fileSystem = new[] { "InputPath", "InputFilename", "OutputPath", "OutputFilename" };
      //private static readonly string[] technical = new[] { "CodecVersion", "Bitrate", "SampleRate", "Duration", "BitsPerSample", "ChannelMode" };

      public static readonly ReadOnlyCollection<string> CommonTags = Array.AsReadOnly(commonTags);
      public static readonly ReadOnlyCollection<string> UncommonTags = Array.AsReadOnly(uncommonTags);
      public static readonly ReadOnlyCollection<string> AllSupportedTags = commonTags.Concat(uncommonTags).ToList().AsReadOnly();
   }
//}

var files = new List<IAudioFile>();