using System.Text;

namespace KodavimasA5.Helpers
{
    public static class ConversionHelper
    {
        public static string ConvertIndexToBinaryStringRepresentation(int? index, int maxLength)
        {
            if (index is null)
            {
                throw new ArgumentNullException(nameof(index));
            }

            string binaryString = Convert.ToString((int)index, 2).PadLeft(maxLength, '0');
            binaryString = new string(binaryString.Reverse().ToArray());

            if (binaryString.Length > maxLength)
            {
                throw new ArgumentException("Index cannot be represented within the specified maxLength.");
            }

            return binaryString;
        }

        public static string ConvertStringToBinary(string input)
        {
            StringBuilder binaryBuilder = new StringBuilder();

            foreach (char c in input)
            {
                string binary = Convert.ToString(c, 2).PadLeft(8, '0');
                binaryBuilder.Append(binary);
            }

            return binaryBuilder.ToString().Trim();
        }

        public static string ConvertBinaryToString(string binaryInput)
        {
            if (binaryInput.Length % 8 != 0)
            {
                throw new ArgumentException("Binary input length must be a multiple of 8.");
            }

            StringBuilder textBuilder = new StringBuilder();

            for (int i = 0; i < binaryInput.Length; i += 8)
            {
                string byteString = binaryInput.Substring(i, 8);
                char character = (char)Convert.ToInt32(byteString, 2);
                textBuilder.Append(character);
            }

            return textBuilder.ToString();
        }

        public static int[] ConvertStringToIntArray(string inputString)
        {
            return inputString.Select(c => int.Parse(c.ToString())).ToArray();
        }

        public static string[] ConvertIntArrayToBinaryStringArray(int[] intArray)
        {
            string[] stringArray = new string[intArray.Length];

            for (int i = 0; i < intArray.Length; i++)
            {
                stringArray[i] = (intArray[i]%2).ToString();
            }

            return stringArray;
        }

        public static int[] ConvertArrayToAbsoluteValue(int[] array)
        {
            int[] absArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                absArray[i] = Math.Abs(array[i]);
            }

            return absArray;
        }
    }
}
