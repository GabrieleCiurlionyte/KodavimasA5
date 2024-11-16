using BPMViewer;

namespace BMPViewer
{
    public partial class MainForm : Form
    {
        private PictureBox pictureBoxOriginalImage;
        private PictureBox pictureBoxNotEncodedImage;
        private PictureBox pictureBoxEncodedImage;

        private TextBox inputPath;
        private Button loadButton;

        private Image originalImage;

        public MainForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "BMP Image Viewer";
            this.Size = new Size(1200, 500); // Increase form width to accommodate both images

            inputPath = new TextBox { Left = 20, Top = 20, Width = 800 };
            loadButton = new Button { Text = "Load BMP", Left = 830, Top = 18, Width = 100 };
            loadButton.Click += LoadButton_Click;

            // Original Image PictureBox
            pictureBoxOriginalImage = new PictureBox
            {
                Left = 20,
                Top = 60,
                Width = 540,
                Height = 360,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Displayed Image PictureBox
            pictureBoxNotEncodedImage = new PictureBox
            {
                Left = 600, // Positioned beside the original image PictureBox
                Top = 60,
                Width = 540,
                Height = 360,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Encoded Image PictureBox
            pictureBoxEncodedImage = new PictureBox
            {
                Left = 1180, // Positioned beside the not encoded image PictureBox
                Top = 60,
                Width = 540,
                Height = 360,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Add controls to the form
            Controls.Add(inputPath);
            Controls.Add(loadButton);
            Controls.Add(pictureBoxOriginalImage);
            Controls.Add(pictureBoxNotEncodedImage);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            string filePathInput = inputPath.Text.Trim();
            var filePath = PathHelper.GetProjectPath(filePathInput);

            //TODO: change hardcoded values
            int m = 3;
            int percentageOfMistake = 25;

            if (filePath != null)
            {
                try
                {
                    ShowOriginalImage(filePath, m);
                    ShowNotEncodedImage(m, percentageOfMistake);
                    ShowEncodedImage(m, percentageOfMistake);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowOriginalImage(string filePath, int m) {
            Image image = Image.FromFile(filePath);

            if (!ImageHelper.isImageLengthValid(image, m))
            {
                return;
            }
            // Display the original image in the first PictureBox
            pictureBoxOriginalImage.Image = image;
            originalImage = image;
        }

        private void ShowNotEncodedImage(int m, int percentageOfMistake) {
            var notEncodedImage = ScenarioHelper.SendImageWithoutEncoding(originalImage, m, percentageOfMistake);
            if (notEncodedImage == null)
            {
                MessageBox.Show($"Could not send not encoded image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                pictureBoxNotEncodedImage.Image = notEncodedImage;
            }
        }

        private void ShowEncodedImage(int m, int percentageOfMistake)
        {
            var encodedImage = ScenarioHelper.SendImageWithEncoding(originalImage, m, percentageOfMistake); // Assuming you have this method
            if (encodedImage == null)
            {
                MessageBox.Show("Could not send encoded image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                pictureBoxEncodedImage.Image = encodedImage;
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
