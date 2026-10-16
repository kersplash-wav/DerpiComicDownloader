using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public static class InputService
{
    public static bool YesOrNo(string query)
    {
        // Request Input //
        while (true)
        {
            // Query //
            Console.Write(query + " (y/n): ");
            // Input //
            char input = Console.ReadKey().KeyChar;
            // Conditions //
            if (input is not 'y' and not 'n' and not 'Y' and not 'N')
            {
                Console.WriteLine("Invalid Input!\n");
                continue;
            }
            // Make New Line //
            Console.WriteLine();
            // Checks //
            return input is 'y' or 'Y';
        }
    }

    public static string RequestString(string query)
    {
        // Variables //
        string? input;
        // State Query //
        Console.WriteLine(query);
        // Request //
        while (true)
        {
            // Request Input //
            input = Console.ReadLine();
            // Checks //
            if (input is null 
                || input.Contains('\\') 
                || input.Contains('/'))
            {
                // Debug //
                Console.WriteLine("Invalid Character!");
                // Restart //
                continue;
            }
            // Success //
            break;
        }
        // Success //
        return input;
    }

    public static int RequestInt(string query)
    {
        // Request Input //
        while (true)
        {

            // Query //
            Console.Write($"{query}: ");
            // Input //
            string? input = Console.ReadLine();
            // Conditions //
            if (input is null)
            {
                Console.WriteLine("Invalid Input!\n");
                continue;
            }
            if (!int.TryParse(input, out int chosenInt))
            {
                Console.WriteLine("Invalid Input!\n");
                continue;
            }
            // Checks //
            return chosenInt;
        }
    }

    public static byte RequestByte(string query, byte min = byte.MinValue, byte max = byte.MaxValue)
    {
        // Request Input //
        while (true)
        {
            // Query //
            Console.Write($"{query} ({min}-{max}): ");
            // Input //
            string? input = Console.ReadLine();
            // Conditions //
            if (input is null || !byte.TryParse(input, out byte chosenByte))
            {
                Console.WriteLine("Invalid Input!\n");
                continue;
            }
            if (chosenByte < min || chosenByte > max)
            {
                Console.WriteLine("Input out of Range!\n");
                continue;
            }
            // Checks //
            return chosenByte;
        }
    }
}
