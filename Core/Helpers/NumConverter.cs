using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers;

public static class NumConverter
{
    public static byte[] GetNibblesBytes(char symbol) =>
        Encoding.ASCII.GetBytes(DecimalToHex((byte)symbol).ToCharArray());

    public static byte[] GetNibblesBytes(string symbols) =>
        Encoding.ASCII.GetBytes(symbols);

    public static string DecimalToHexAsString(byte[] numbers) => 
        string.Join("", DecimalToHex(numbers));

    public static string[] DecimalToHex(byte[] numbers)
    {
        if (numbers is null) return [string.Empty];

        return numbers.Select(x => DecimalToHex(x)).ToArray();
    }

    public static string DecimalToHex(int? number)
    {
        if (number is null) return string.Empty;

        if (number < 16) return HexComparer(number);

        int? value = number / 16;
        int? result = number - value * 16;

        if (value < 16) return HexComparer(value) + HexComparer(result);

        return DecimalToHex(value) + HexComparer(result);
    }

    private static string HexComparer(int? number)
    {
        return number switch
        {
            10 => "a",
            11 => "b",
            12 => "c",
            13 => "d",
            14 => "e",
            15 => "f",
            _ => number.ToString(),
        };
    }
}
