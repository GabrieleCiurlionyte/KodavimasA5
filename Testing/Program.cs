using System.Diagnostics;
using KodavimasA5;

public class Program
{
    public static void Main(string[] args)
    {
        RunExperiment(m: 2, percentageOfMistake: 10, input: "111", repetitions: 300);
        RunExperiment(m: 3, percentageOfMistake: 10, input: "1101", repetitions: 300);
        RunExperiment(m: 4, percentageOfMistake: 10, input: "11111", repetitions: 300);
        RunExperiment(m: 5, percentageOfMistake: 10, input: "111111", repetitions: 300);
    }

    private static void RunExperiment(int m, int percentageOfMistake, string input, int repetitions)
    {
        int passCount = 0;
        int failCount = 0;
        double totalExecutionTime = 0;

        Console.WriteLine($"\nStarting experiment with m = {m}, percentageOfMistake = {percentageOfMistake}%, input = '{input}', repetitions = {repetitions}\n");

        for (int i = 0; i < repetitions; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                ExecuteDecodeTest(m, percentageOfMistake, input);
                passCount++; // Increment pass count if no exception is thrown
            }
            catch (Exception ex)
            {
                // Count failures if there's any mismatch or exception in decoding
                failCount++;
                Console.WriteLine($"Test failed on iteration {i + 1}: {ex.Message}");
            }
            stopwatch.Stop();
            totalExecutionTime += stopwatch.Elapsed.TotalMilliseconds;
        }

        // Calculate and display results
        double averageExecutionTime = totalExecutionTime / repetitions;
        Console.WriteLine($"Average Execution Time: {averageExecutionTime} ms");
        Console.WriteLine($"Pass Count: {passCount}");
        Console.WriteLine($"Fail Count: {failCount}");

        // Optional assertion
        if (passCount <= repetitions * 0.75)
        {
            Console.WriteLine("Warning: Expected more than 75% passes.");
        }
    }

    private static void ExecuteDecodeTest(int m, double percentageOfMistake, string input)
    {
        var encodedVector = Encoder.Encode(input, m);
        string channelVector = Channel.SendThroughChannel(encodedVector, percentageOfMistake);
        var decodedString = Decoder.Decode(new Random(), channelVector, m);

        if (decodedString != input)
        {
            throw new Exception("Decoded string does not match the expected input.");
        }
    }
}
