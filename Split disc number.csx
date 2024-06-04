// This script replaces disc number format used in iTunes "disc number/total number of discs" ("1/2" -> "1")

using Metatogger.Data;

foreach (var file in files)
{
  string discNumber = file.GetFirstValue(TagName.DiscNumber);
  if (discNumber != null)
  {
    int index = discNumber.IndexOf('/');
    if (index != -1)
      file.SetTag(TagName.DiscNumber, discNumber.Substring(0, index));
  }
}
