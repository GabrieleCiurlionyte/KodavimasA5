namespace BMPViewer
{
    public class UIHelper
    {
        private readonly Form mainForm;

        // Controls for images and labels
        public PictureBox PictureBoxOriginalImage { get; private set; }
        public PictureBox PictureBoxNotEncodedImage { get; private set; }
        public PictureBox PictureBoxEncodedImage { get; private set; }
        public TextBox InputPath { get; private set; }
        public Button LoadButton { get; private set; }

        // Labels for each image
        private Label labelOriginalImage;
        private Label labelNotEncodedImage;
        private Label labelEncodedImage;

        public UIHelper(Form form)
        {
            mainForm = form;
            InitializeUI();
        }

        private void InitializeUI()
        {
            mainForm.Text = "BMP Image Viewer";
            mainForm.WindowState = FormWindowState.Maximized;
            mainForm.MinimumSize = new Size(1200, 600);

            InputPath = new TextBox { Left = 20, Top = 20, Width = 800 };
            LoadButton = new Button { Text = "Load BMP", Left = 830, Top = 18, Width = 100 };

            int spacing = 20;
            int pictureBoxWidth = (mainForm.ClientSize.Width - 4 * spacing) / 3;
            int pictureBoxHeight = mainForm.ClientSize.Height - 160;

            // Label for Original Image
            labelOriginalImage = new Label
            {
                Text = "Original Image",
                AutoSize = true,
                Top = 60,
                Left = spacing
            };

            // Original Image PictureBox
            PictureBoxOriginalImage = new PictureBox
            {
                Top = labelOriginalImage.Bottom + 5,
                Width = pictureBoxWidth,
                Height = pictureBoxHeight,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
            };

            // Label for Not Encoded Image
            labelNotEncodedImage = new Label
            {
                Text = "Without Encoding",
                AutoSize = true,
                Top = 60,
                Left = PictureBoxOriginalImage.Right + spacing
            };

            // Not Encoded Image PictureBox
            PictureBoxNotEncodedImage = new PictureBox
            {
                Top = labelNotEncodedImage.Bottom + 5,
                Width = pictureBoxWidth,
                Height = pictureBoxHeight,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
            };

            // Label for Encoded Image
            labelEncodedImage = new Label
            {
                Text = "With Encoding",
                AutoSize = true,
                Top = 60,
                Left = PictureBoxNotEncodedImage.Right + spacing
            };

            // Encoded Image PictureBox
            PictureBoxEncodedImage = new PictureBox
            {
                Top = labelEncodedImage.Bottom + 5,
                Width = pictureBoxWidth,
                Height = pictureBoxHeight,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            // Add controls to the form
            mainForm.Controls.Add(InputPath);
            mainForm.Controls.Add(LoadButton);
            mainForm.Controls.Add(labelOriginalImage);
            mainForm.Controls.Add(PictureBoxOriginalImage);
            mainForm.Controls.Add(labelNotEncodedImage);
            mainForm.Controls.Add(PictureBoxNotEncodedImage);
            mainForm.Controls.Add(labelEncodedImage);
            mainForm.Controls.Add(PictureBoxEncodedImage);

            mainForm.Resize += (s, e) => AdjustPictureBoxSizes();
        }

        public void AdjustPictureBoxSizes()
        {
            int spacing = 20;
            int pictureBoxWidth = (mainForm.ClientSize.Width - 4 * spacing) / 3;
            int pictureBoxHeight = mainForm.ClientSize.Height - 160;

            PictureBoxOriginalImage.Width = pictureBoxWidth;
            PictureBoxOriginalImage.Height = pictureBoxHeight;
            labelNotEncodedImage.Left = PictureBoxOriginalImage.Right + spacing;
            PictureBoxNotEncodedImage.Left = PictureBoxOriginalImage.Right + spacing;
            PictureBoxNotEncodedImage.Width = pictureBoxWidth;
            PictureBoxNotEncodedImage.Height = pictureBoxHeight;
            labelEncodedImage.Left = PictureBoxNotEncodedImage.Right + spacing;
            PictureBoxEncodedImage.Left = PictureBoxNotEncodedImage.Right + spacing;
            PictureBoxEncodedImage.Width = pictureBoxWidth;
            PictureBoxEncodedImage.Height = pictureBoxHeight;
        }

        public Image CreateLoadingImage(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                using (Font font = new Font("Arial", 20, FontStyle.Bold))
                {
                    var textSize = g.MeasureString("Loading...", font);
                    g.DrawString("Loading...", font, Brushes.Gray, (width - textSize.Width) / 2, (height - textSize.Height) / 2);
                }
            }
            return bitmap;
        }
    }
}
