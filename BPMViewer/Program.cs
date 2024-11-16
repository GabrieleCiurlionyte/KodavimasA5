using BPMViewer;

namespace BMPViewer
{
    public partial class MainForm : Form
    {
        private UIHelper uiHelper;
        private Image originalImage;

        public MainForm()
        {
            uiHelper = new UIHelper(this);
            uiHelper.LoadButton.Click += LoadButton_Click; // Attach event to LoadButton
        }

        private async void LoadButton_Click(object sender, EventArgs e)
        {
            string filePathInput = uiHelper.InputPath.Text.Trim();
            var filePath = PathHelper.GetProjectPath(filePathInput);

            int m = 3;
            int percentageOfMistake = 5;

            if (filePath != null)
            {
                try
                {
                    // Disable the Load Button while loading
                    uiHelper.LoadButton.Enabled = false;

                    ShowOriginalImage(filePath, m);
                    await ShowNotEncodedImageAsync(m, percentageOfMistake);
                    await ShowEncodedImageAsync(m, percentageOfMistake);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Enable the Load Button after processing
                    uiHelper.LoadButton.Enabled = true;
                }
            }
        }

        private void ShowOriginalImage(string filePath, int m)
        {
            Image image = Image.FromFile(filePath);

            if (!ImageHelper.isImageLengthValid(image, m))
            {
                return;
            }
            uiHelper.PictureBoxOriginalImage.Image = image;
            originalImage = image;
        }

        private async Task ShowNotEncodedImageAsync(int m, int percentageOfMistake)
        {
            uiHelper.PictureBoxNotEncodedImage.Image = uiHelper.CreateLoadingImage(uiHelper.PictureBoxNotEncodedImage.Width, uiHelper.PictureBoxNotEncodedImage.Height);
            uiHelper.PictureBoxNotEncodedImage.Refresh();

            var notEncodedImage = await Task.Run(() => ScenarioHelper.SendImageWithoutEncoding(originalImage, m, percentageOfMistake));

            if (notEncodedImage == null)
            {
                MessageBox.Show("Could not send not encoded image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                uiHelper.PictureBoxNotEncodedImage.Image = notEncodedImage;
            }
        }

        private async Task ShowEncodedImageAsync(int m, int percentageOfMistake)
        {
            uiHelper.PictureBoxEncodedImage.Image = uiHelper.CreateLoadingImage(uiHelper.PictureBoxEncodedImage.Width, uiHelper.PictureBoxEncodedImage.Height);
            uiHelper.PictureBoxEncodedImage.Refresh();

            var encodedImage = await Task.Run(() => ScenarioHelper.SendImageWithEncoding(originalImage, m, percentageOfMistake));

            if (encodedImage == null)
            {
                MessageBox.Show("Could not send encoded image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                uiHelper.PictureBoxEncodedImage.Image = encodedImage;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
