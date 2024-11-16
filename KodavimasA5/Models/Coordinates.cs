namespace KodavimasA5.Models
{
    public class Coordinates
    {
        private int _heigth = 0;
        private int _width = 0;

        public Coordinates(int height, int width)
        {
            _heigth = height;
            _width = width;
        }

        public int GetHeigth() 
        { 
            return _heigth;
        }

        public int GetWidth()
        { 
            return _width;
        }
    }
}
