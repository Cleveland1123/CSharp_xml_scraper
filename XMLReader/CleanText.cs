


namespace XMLReader;

public static class CleanText
{
    public static string Clean(string str)
    {
        Regex regex = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        return regex.Replace(str, String.Empty);

    }


}
