using System;
using System.Drawing;
using System.Windows.Forms;

namespace CursorlockTest1
{
    public partial class Form1 : Form
    {
        private bool cursorLocked = false;
        private bool isSelectingArea = false;
        private Point selectionStart;
        private Rectangle selectedArea;
        private Color areaColor = Color.Red;

        private Label coordinatesLabel;
        private PictureBox lockIcon;

        public Form1()
        {
            AvisoForm avisoForm = new AvisoForm();
            DialogResult avisoResult = avisoForm.ShowDialog();
            if (avisoResult == DialogResult.OK && avisoForm.NoMostrarAviso)
            {
            }

            InitializeComponent();
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Size = new Size(600, 400);

            coordinatesLabel = new Label();
            coordinatesLabel.Location = new Point(10, 10);
            this.Controls.Add(coordinatesLabel);

            Button colorButton = new Button();
            colorButton.Text = "Color";
            colorButton.Location = new Point(10, 40);
            colorButton.Click += ColorButton_Click;
            this.Controls.Add(colorButton);

            this.Controls.Add(lockIcon);

            this.MouseMove += (sender, e) =>
            {
                UpdateCoordinatesLabel(e.Location);

                if (isSelectingArea)
                {
                    Point currentPoint = e.Location;
                    selectedArea = new Rectangle(
                        Math.Min(selectionStart.X, currentPoint.X),
                        Math.Min(selectionStart.Y, currentPoint.Y),
                        Math.Abs(currentPoint.X - selectionStart.X),
                        Math.Abs(currentPoint.Y - selectionStart.Y)
                    );


                    this.Invalidate();
                }
            };

            this.MouseDown += (sender, e) =>
            {
                if (!cursorLocked)
                {
                    isSelectingArea = true;
                    selectionStart = e.Location;

                    UpdateCoordinatesLabel(selectionStart);
                }
            };

            this.MouseUp += (sender, e) =>
            {
                if (isSelectingArea)
                {
                    isSelectingArea = false;

                    UpdateCoordinatesLabel(e.Location);


                    if (selectedArea.Width > 10 && selectedArea.Height > 10)
                    {
                        Cursor.Clip = this.RectangleToScreen(selectedArea);
                    }
                    else
                    {
                        Cursor.Clip = Rectangle.Empty;
                    }
                }
            };

            this.Paint += (sender, e) =>
            {
                if (isSelectingArea)
                {
                    using (Pen pen = new Pen(areaColor, 2))
                    {
                        e.Graphics.DrawRectangle(pen, selectedArea);
                    }
                }
            };
        }

        private void UpdateCoordinatesLabel(Point coordinates)
        {
            coordinatesLabel.Text = $"X: {coordinates.X}, Y: {coordinates.Y}";
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                areaColor = colorDialog.Color;
            }
        }
    }
}
