namespace KodavimasA5.Models
{
    public class Matrix
    {
        public dynamic Value { get; set; }
        public int Heigth { get; }
        public int Width { get; }

        public Matrix(int[,] value)
        {
            Value = value;
            Heigth = value.GetLength(0);
            Width = value.GetLength(1);
        }

        public Matrix(int[] value)
        {
            Value = value;
            Heigth = 1;
            Width = value.GetLength(0);
        }

        public static Matrix Concatenate(Matrix topLeft, Matrix topRight, Matrix bottomLeft, Matrix bottomRight)
        {
            int rows = topLeft.Heigth + bottomLeft.Heigth;
            int cols = topLeft.Width + topRight.Width;
            int[,] result = new int[rows, cols];

            // Copy topLeft
            for (int i = 0; i < topLeft.Heigth; i++)
                for (int j = 0; j < topLeft.Width; j++) {

                    if (topLeft.Value is int[,])
                    {
                        result[i, j] = topLeft.Value[i, j];
                    }
                    else
                    {
                        result[i, j] = topLeft.Value[i];
                    }
                }

            // Copy topRight
            for (int i = 0; i < topRight.Heigth; i++)
                for (int j = 0; j < topRight.Width; j++) {

                    if (topRight.Value is int[,])
                    {
                        result[i, j + topLeft.Width] = topRight.Value[i, j];
                    }
                    else
                    {
                        result[i, j + topLeft.Width] = topRight.Value[i];
                    }
                }

            // Copy bottomLeft
            for (int i = 0; i < bottomLeft.Heigth; i++)
                for (int j = 0; j < bottomLeft.Width; j++)
                {
                    if (bottomLeft.Value is int[,])
                    {
                        result[i + topLeft.Heigth, j] = bottomLeft.Value[i, j];
                    }
                    else
                    {
                        result[i + topLeft.Heigth, j] = bottomLeft.Value[i];
                    }
                }

            // Copy bottomRight
            for (int i = 0; i < bottomRight.Heigth; i++)
                for (int j = 0; j < bottomRight.Width; j++)
                {
                    if (bottomRight.Value is int[,])
                    {
                        result[i + topLeft.Heigth, j + topLeft.Width] = bottomRight.Value[i, j];
                    }
                    else
                    {
                        result[i + topLeft.Heigth, j + topLeft.Width] = bottomRight.Value[i];
                    }
                }

            return new Matrix(result);
        }
    }
}
