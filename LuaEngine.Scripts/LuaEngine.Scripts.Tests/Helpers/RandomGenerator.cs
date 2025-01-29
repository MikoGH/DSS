namespace LuaEngine.Scripts.Tests.Helpers;

public static class RandomGenerator
{
    private const string Chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZzАаБбВвГгДдЕеЖжЗзИиКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя0123456789-.\":()";
    private const int MaxStringLength = 10;

    /// <summary>
    /// Сгенерировать случайную строку.
    /// </summary>
    /// <param name="sporadic">Генератор случайных чисел.</param>
    /// <returns>Случайная строка.</returns>
    public static string GenerateString(Random sporadic)
    {
        return new string(Enumerable.Repeat(Chars, sporadic.Next(MaxStringLength)).Select(s => s[sporadic.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Сгенерировать случайную дату.
    /// </summary>
    /// <param name="sporadic">Генератор случайных чисел.</param>
    /// <param name="minYear">Минимальная дата.</param>
    /// <param name="maxYear">Максимальная дата.</param>
    /// <returns>Случайная дата.</returns>
    public static DateTime GenerateDate(Random sporadic, int minYear, int maxYear)
    {
        if (minYear >= maxYear)
            (minYear, maxYear) = (maxYear, minYear);

        return new DateTime(
            sporadic.Next(minYear, maxYear),
            sporadic.Next(1, 13),
            sporadic.Next(1, 29),
            23,
            59,
            59);
    }

    /// <summary>
    /// Сгенерировать коллекцию случайных строк.
    /// </summary>
    /// <param name="sporadic">Генератор случайных чисел.</param>
    /// <param name="collectionCount">Кол-во элементов коллекции.</param>
    /// <returns>Коллекция случайных строк.</returns>
    public static IEnumerable<string> GenerateCollectionsOfUniqueStrings(Random sporadic, int? collectionCount = null)
    {
        int count = collectionCount.HasValue ? collectionCount.Value : sporadic.Next() + 1;
        HashSet<string> strs = new HashSet<string>();
        for (int i = 0; i < count; i++)
        {
            string str = GenerateString(sporadic);
            if (!strs.Add(str))
                i--;
        }
        return strs.ToList();
    }

    /// <summary>
    /// Сгенерировать коллекцию случайных дат.
    /// </summary>
    /// <param name="sporadic">Генератор случайных чисел.</param>
    /// <param name="minYear">Минимальная дата.</param>
    /// <param name="maxYear">Максимальная дата.</param>
    /// <param name="collectionCount">Кол-во элементов коллекции.</param>
    /// <returns>Коллекция случайных дат.</returns>
    public static IEnumerable<DateTime> GenerateCollectionsOfUniqueDates(Random sporadic, int minYear, int maxYear, int? collectionCount = null)
    {
        int count = collectionCount.HasValue ? collectionCount.Value : sporadic.Next() + 1;
        HashSet<DateTime> dateTimes = new HashSet<DateTime>();
        for (int i = 0; i < count; i++)
        {
            DateTime date = GenerateDate(sporadic, minYear, maxYear);
            if (!dateTimes.Add(date))
                i--;
        }
        return dateTimes.ToList();
    }

    public static Result<T> GenerateEnumValue<T>(Random sporadic) where T : struct, Enum
    {
        var values = Enum.GetValues(typeof(T));
        if (values.Length <= 0)
            return Result<T>.Empty;

        T randomValue = (T)values.GetValue(sporadic.Next(values.Length))!;
        return randomValue;
    }

    /// <summary>
    /// Сгенерировать случайный Guid используя генератор случайных чисел.
    /// </summary>
    /// <param name="sporadic">Генератор случайных чисел.</param>
    /// <returns>Guid.</returns>
    public static Guid GenerateGuid(Random sporadic)
    {
        var guidBytes = new byte[16];
        
        sporadic.NextBytes(guidBytes);

        return new Guid(guidBytes);
    }

    /// <summary>
    /// Получить случайный элемент из коллекции.
    /// </summary>
    /// <typeparam name="T">Тип элементов.</typeparam>
    /// <param name="values">Коллекция элементов.</param>
    /// <param name="sporadic">Генератор случайных чисел.</param>
    /// <returns>Случайный элемент из коллекции.</returns>
    public static Result<T> PickOne<T>(IEnumerable<T> values, Random sporadic) =>
        PickOne(sporadic, values.ToArray());

    /// <summary>
    /// Получить случайный элемент из коллекции.
    /// </summary>
    /// <typeparam name="T">Тип элементов.</typeparam>
    /// <param name="sporadic">Генератор случайных чисел.</param>
    /// <param name="values">Коллекция элементов.</param>
    /// <returns>Случайный элемент из коллекции.</returns>
    public static Result<T> PickOne<T>(Random sporadic, params T[] values)
    {
        var count = values.Length;
        if (count <= 0)
            return Result<T>.Empty;

        return values.ElementAt(sporadic.Next(0, count));
    }
}
