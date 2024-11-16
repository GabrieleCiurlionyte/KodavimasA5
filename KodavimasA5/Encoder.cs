using KodavimasA5.Helpers;
using System.Text;

namespace KodavimasA5
{
    public static class Encoder
    {
        public static string Encode(string input, int m) 
        {
            var chunkSize = GenerativeMatrixConstructor.GetReedMullerCodeDimension(m, 1);

            var listOfChunks = StringHelper.SplitStringIntoChunks(input, chunkSize);

            var generativeMatrix = GenerativeMatrixConstructor.ConstructGenerativeMatrix(1, m);

            StringBuilder binaryBuilder = new StringBuilder();

            foreach (var chunk in listOfChunks) 
            {
                var intChunk = ConversionHelper.ConvertStringToIntArray(chunk);
                var intResult = MathHelper.MultiplyMatrixWithOneDimensionArray(generativeMatrix, intChunk);
                var encodedResult = ConversionHelper.ConvertIntArrayToBinaryStringArray(intResult);
                binaryBuilder.Append(string.Join("", encodedResult));
            }
            return binaryBuilder.ToString();


        }
    }
}
