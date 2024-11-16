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

            var results = new string[listOfChunks.Count];
            Parallel.ForEach(listOfChunks, (chunk, state, index) =>
            {
                var intChunk = ConversionHelper.ConvertStringToIntArray(chunk);
                var intResult = MathHelper.MultiplyMatrixWithOneDimensionArray(generativeMatrix, intChunk);
                var encodedResult = ConversionHelper.ConvertIntArrayToBinaryStringArray(intResult);

                results[index] = string.Join("", encodedResult); // Store result at the correct index
            });

            // Concatenate the ordered results
            return string.Concat(results);


        }
    }
}
