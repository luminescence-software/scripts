// This script capitalize your tag value with the capitalization rules from the common English title case style ("a day in the life" -> "A Day in the Life")

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

public static class EnglishTitleCapitalizer
{
	public readonly static string[] Prepositions = new[] { "about", "above", "across", "after", "against", "along", "among", "around", "at", "before", "behind", "below", "beneath", "beside", "between", "beyond", "but", "by", "despite", "down", "during", "except", "for", "from", "in", "inside", "into", "like", "near", "of", "off", "on", "onto", "out", "outside", "over", "past", "per", "since", "through", "throughout", "till", "to", "toward", "under", "underneath", "until", "up", "upon", "via", "with", "within", "without", "versus" };
	public readonly static string[] Articles = new[] { "a", "an", "the" };
	public readonly static string[] Conjunctions = new[] { "and", "but", "or", "nor", "for", "yet", "so" };
	public readonly static char[] EndingPunctuations = new[] { '.', '?', '!', ':' };

	public readonly static HashSet<string> MustBeUpperCase = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "DJ", "MC", "TV", "MTV", "EP", "LP", "YMCA", "NYC", "NY", "USSR", "USA", "R&B", "BBC", "FM", "AC", "DC", "AC/DC", "UK", "BPM", "OK", "Q&A", "FAQ" };
	public readonly static HashSet<string> MustBeLowerCase = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "ft", "ft.", "feat", "feat.", "n'", "'n'", "no." };
	public readonly static HashSet<string> TheGroups = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "Beatles", "Police", "Corrs", "Doors", "Beach Boys" };
	public readonly static Dictionary<string, string> SpecialMixedCase = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
	{
		["rock'n'roll"] = "Rock 'n' Roll",
		["o'clock"] = "O'Clock",
		["djs"] = "DJs"
	};

	private static int GetFirstCharIndex(string s)
	{
		for (int i = 0; i < s.Length; i++)
		{
			if (Char.IsLetterOrDigit(s[i]))
				return i;
		}

		return -1;
	}

	private static int GetLastCharIndex(string s)
	{
		for (int i = s.Length - 1; i >= 0; i--)
		{
			if (Char.IsLetterOrDigit(s[i]))
				return i;
		}

		return -1;
	}

	private static string GetWordCore(string s)
	{
		int firstCharIndex = GetFirstCharIndex(s);
		if (firstCharIndex == -1)
			return s;

		return s.Substring(firstCharIndex, GetLastCharIndex(s) + 1 - firstCharIndex);
	}

	private static string UpperCase(string s)
	{
		int firstCharIndex = GetFirstCharIndex(s);
		if (firstCharIndex == -1)
			return s;

		if (SpecialMixedCase.ContainsKey(s))
			return SpecialMixedCase[s];

		var sb = new StringBuilder(s);
		sb[firstCharIndex] = Char.ToUpper(sb[firstCharIndex]);

		for (int i = 0; i < sb.Length - 1; i++)
		{
			if (Char.GetUnicodeCategory(sb[i]) == UnicodeCategory.DashPunctuation)
				sb[i + 1] = Char.ToUpper(sb[i + 1]);
		}

		return sb.ToString();
	}

	public static string Capitalize(string title, bool resetUpperCase = false, bool useTitleCase = true, int maxPrepositionLength = 3)
	{
		if (String.IsNullOrWhiteSpace(title))
			return null;

		var lowerCaseWords = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
		if (useTitleCase)
		{
			lowerCaseWords.UnionWith(Prepositions.Where(word => word.Length <= maxPrepositionLength));
			lowerCaseWords.UnionWith(Articles.Where(word => word.Length <= maxPrepositionLength));
			lowerCaseWords.UnionWith(Conjunctions.Where(word => word.Length <= maxPrepositionLength));
		}

		if (resetUpperCase)
			title = title.ToLower();

		string[] words = title.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

		for (int i = 0; i < words.Length; i++)
		{
			string word = words[i];
			string wordCore = GetWordCore(word);

			if (MustBeUpperCase.Contains(wordCore) || MustBeUpperCase.Contains(word.Replace(".", null)))
			{
				words[i] = word.ToUpper();
			}
			else if (MustBeLowerCase.Contains(word) || MustBeLowerCase.Contains(wordCore))
			{
				words[i] = resetUpperCase ? word : word.ToLower();
			}
			else if (lowerCaseWords.Contains(wordCore)) // the word should be lower case...
			{
				if (i == 0 // ...unless it's the first word
					|| i == words.Length - 1 // ...unless it's the last word
					|| Char.GetUnicodeCategory(word[0]) == UnicodeCategory.OpenPunctuation // ...unless the word begins with '('
					|| word[0] == '"' // ...unless the word begins with '"'
					|| Char.GetUnicodeCategory(word[word.Length - 1]) == UnicodeCategory.ClosePunctuation // ...unless the word ends with ')'
					|| word[word.Length - 1] == '"' // ...unless the word ends with '"'
					|| Char.GetUnicodeCategory(words[i - 1][words[i - 1].Length - 1]) == UnicodeCategory.ClosePunctuation // ...unless the previous word ends with ')'
					|| Char.GetUnicodeCategory(words[i - 1][words[i - 1].Length - 1]) == UnicodeCategory.DashPunctuation // ...unless the previous word ends with '-'
					|| EndingPunctuations.Contains(words[i - 1][words[i - 1].Length - 1]) // ...unless the previous word ends with '.'
					|| (String.Equals(wordCore, "The", StringComparison.InvariantCultureIgnoreCase) && TheGroups.Contains(GetWordCore(words[i + 1])))) // ...unless "the" is a part of band's name
					words[i] = UpperCase(word);
				else
					words[i] = resetUpperCase ? word : word.ToLower();
			}
			else
			{
				words[i] = UpperCase(word);
			}
		}

		return String.Join(" ", words);
	}
}

foreach (var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, EnglishTitleCapitalizer.Capitalize(tagValue));