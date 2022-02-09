namespace Froom.Package.Helpers;

public static class Extensions
{
    public static bool LineHasAllColumns(this string[] cols, int count, string separator = ",")
    {
        return cols.Length == count;
    }

    public static string Column(this string[] columns, int index)
    {
        return columns[index].Replace("\"", "").Trim();
    }
}