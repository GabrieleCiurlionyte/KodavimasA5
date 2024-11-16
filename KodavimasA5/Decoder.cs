using KodavimasA5.Helpers;
using System.Text;

namespace KodavimasA5;

public static class Decoder
{
    public static string Decode(string w, int m) 
    {

        //First we have to separate into chunks of size 2^m
        var chunkSize = GenerativeMatrixConstructor.GetReedMullerCodeLength(m);

        var listOfChunks = StringHelper.SplitStringIntoChunks(w, chunkSize);

        StringBuilder binaryBuilder = new StringBuilder();

        foreach (var chunk in listOfChunks) 
        {
            var intArray = ConversionHelper.ConvertStringToIntArray(chunk);

            //2nd step of Fast decoding for RM(1,m) algorithm
            var computeLargestW = ComputeLargestW(m, intArray);

            //3rd step of Fast decoding for RM(1,m) algorithm
            var index = FindIndexOfTheLargestComponentInW(computeLargestW);

            //Find binary representation of j
            var binaryIndex = ConversionHelper.ConvertIndexToBinaryStringRepresentation(index, m);

            var decodedChunk =  GetDecodedMessage(binaryIndex, computeLargestW[index] > 0);

            binaryBuilder.Append(decodedChunk);
        }

        return binaryBuilder.ToString();
    }

    private static string GetDecodedMessage(string indexBinaryString, bool largestWIsPositive)
    {
        return indexBinaryString.PadLeft(1+indexBinaryString.Length, largestWIsPositive ? '1' : '0');
    }

    public static int[] ModifyWVector(int[] w)
    {
        int[] intArray = new int[w.Length];
        for (int i = 0; i < w.Length; i++)
        {
            intArray[i] = (w[i] == 0) ? -1 : w[i];
        }
        return intArray;
    }

    public static int[] ComputeLargestW(int m, int[] w)
    {
        return ComputeRecursiveW(m, m, w);
    }

    public static int FindIndexOfTheLargestComponentInW(int[] w)
    {
        int[] absoluteValueW = ConversionHelper.ConvertArrayToAbsoluteValue(w);

        int largestComponentPosition = 0;
        int largestComponentValue = absoluteValueW[0];

        for (int i = 1; i < absoluteValueW.Length; i++)
        {
            if (absoluteValueW[i] > largestComponentValue)
            {
                largestComponentValue = absoluteValueW[i];
                largestComponentPosition = i;
            }
        }
        return largestComponentPosition;
    }

    public static int[] ComputeW1(int[] w, int m) 
    {
        //1st step of Fast decoding for RM(1,m) algorithm
        var modifyW = ModifyWVector(w);
        var hMatrix = MathHelper.CalculateH(1, m);
        return MathHelper.MultiplyMatrixWithOneDimensionArray(hMatrix, modifyW);
    }

    public static int[] ComputeRecursiveW(int index, int m, int[] w) 
    {
        if (index == 1) 
        {
            return ComputeW1(w, m);
        }

        return MathHelper.MultiplyMatrixWithOneDimensionArray(MathHelper.CalculateH(index,m),
            ComputeRecursiveW(index - 1, m, w)); 
    }
}
