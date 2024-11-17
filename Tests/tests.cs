using KodavimasA5;
using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

public class ReedMullerDecoderTests : IDisposable
{
    private readonly ITestOutputHelper _output;

    public ReedMullerDecoderTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(2, 10, "111")]
    public void Decode_Decoding_MultipleExecutions(int m, int percentageOfMistake, string input)
    {
        int passCount = 0;
        int failCount = 0;
        double totalExecutionTime = 0;

        for (int i = 0; i < 300; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                ExecuteDecodeTest(m, percentageOfMistake, input);
                passCount++;
            }
            catch (Xunit.Sdk.EqualException)
            {
                failCount++;
            }
            stopwatch.Stop();
            totalExecutionTime += stopwatch.Elapsed.TotalMilliseconds;
        }

        double averageExecutionTime = totalExecutionTime / 300;

        // Output results using _output helper
        _output.WriteLine($"Average Execution Time: {averageExecutionTime} ms");
        _output.WriteLine($"Pass Count: {passCount}");
        _output.WriteLine($"Fail Count: {failCount}");

        Assert.True(passCount > 100, "Expected more than 250 passes.");
    }

    private void ExecuteDecodeTest(int m, int percentageOfMistake, string input)
    {
        var encodedVector = Encoder.Encode(input, m);
        string channelVector = Channel.SendThroughChannel(encodedVector, percentageOfMistake);
        var decodedString = Decoder.Decode(new Random(), channelVector, m);
        Assert.Equal(input, decodedString);
    }

    public void Dispose()
    {
        // Any cleanup code if needed
    }
}
