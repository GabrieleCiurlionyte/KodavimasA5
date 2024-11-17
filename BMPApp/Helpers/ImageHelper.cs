using KodavimasA5.Helpers;

namespace BMPApp.Helpers
{
    public static class ImageHelper
    {
        public static string ConvertImageToBinary(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                byte[] imageBytes = ms.ToArray();

                string[] binaryStrings = new string[imageBytes.Length];

                // Parallel conversion of each byte to a binary string
                Parallel.ForEach(Enumerable.Range(0, imageBytes.Length), i =>
                {
                    binaryStrings[i] = Convert.ToString(imageBytes[i], 2).PadLeft(8, '0');
                });

                return string.Concat(binaryStrings);
            }
        }

        public static Image ConvertBinaryToImage(string binaryString)
        {
            int byteCount = binaryString.Length / 8;
            byte[] imageBytes = new byte[byteCount];

            // Parallel processing of binary string segments into bytes
            Parallel.For(0, byteCount, i =>
            {
                imageBytes[i] = Convert.ToByte(binaryString.Substring(i * 8, 8), 2);
            });

            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Image image = Image.FromStream(ms);
                return (Image)image.Clone();
            }
        }

        public static bool isImageLengthValid(Image image, int m)
        {
            var binaryVector = ConvertImageToBinary(image);

            if (!ValidatorHelper.IsBinaryVectorLengthCorrect(binaryVector, m))
            {
                MessageBox.Show("Input image cannot be encoded because of incorrect length.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
