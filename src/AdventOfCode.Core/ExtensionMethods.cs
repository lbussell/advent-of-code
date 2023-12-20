namespace AdventOfCode.Core;

using System.Numerics;

public static class ExtensionMethods
{
    public static int Product(this IEnumerable<int> source) => Product<int, int>(source);

    private static TResult Product<TSource, TResult>(this IEnumerable<TSource> source)
        where TSource : struct, INumber<TSource>
        where TResult : struct, INumber<TResult>
    {
        TResult product = TResult.One;
        foreach (TSource value in source)
        {
            checked
            {
                product *= TResult.CreateChecked(value);
            }
        }

        return product;
    }
}
