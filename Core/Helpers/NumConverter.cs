using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Helpers;

public static class NumConverter
{
    //public static byte[] GetNibblesBytes(char symbol) =>
    //    Encoding.ASCII.GetBytes(DecToHex((byte)symbol));

    public static byte[] GetNibblesBytes(string symbols) =>
        Encoding.ASCII.GetBytes(symbols);

    public static string DecimalToHexAsString(byte[] numbers) => 
        string.Join("", DecimalToHex(numbers));

    public static int[] DecimalToHex(byte[] numbers) =>
    numbers.Select(x => DecToHex(x)).ToArray();

    public static int StringToHex(string number) => Convert.ToInt32(number, 16);

    public static int DecToHex(int number)
    {
        if (number < 16) return Convert.ToInt32(HexComparer(number), 16);

        int value = number / 16;
        int result = number - value * 16;

        if (value < 16) return Convert.ToInt32(HexComparer(value) + HexComparer(result), 16);

        return Convert.ToInt32(DecToHex(value).ToString("X") + HexComparer(result), 16);
    }

    private static string HexComparer(int number) => 
        number switch
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
