namespace BPMViewer
{
    public static class PathHelper
    {
        
        public static string? GetProjectPath(string filePath) {

            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.Combine(GetProjectDirectory(), filePath);
            }

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Invalid file path. Please enter a valid BMP file path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Validate the file extension
            if (Path.GetExtension(filePath).ToLower() != ".bmp")
            {
                MessageBox.Show("Invalid file format. Please select a .bmp file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return filePath;
        }

        private static string GetProjectDirectory()
        {
            // Start from the base directory
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Traverse up three levels to get to the project folder
            string projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\Photos"));
            return projectDir;
        }
    }
}
