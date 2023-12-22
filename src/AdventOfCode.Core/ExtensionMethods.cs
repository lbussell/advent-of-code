namespace AdventOfCode.Core;

using System.Numerics;

public static class ExtensionMethods
{
    public static int Product(this IEnumerable<int> source) => Product<int, int>(source);

    public static int LCM(this IEnumerable<int> source) => source.Aggregate(LCM);

    public static long LCM(this IEnumerable<long> source) => source.Aggregate(LCM);

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

    private static int LCM(int a, int b) => a / GCD(a, b) * b;

    private static int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);

    private static long LCM(long a, long b) => a / GCD(a, b) * b;

    private static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);
}
