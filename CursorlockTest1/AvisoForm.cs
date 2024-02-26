using System;
using System.Windows.Forms;

namespace CursorlockTest1
{
    public partial class AvisoForm : Form
    {
        private CheckBox noMostrarCheckBox;

        public AvisoForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Aviso";
            this.Size = new System.Drawing.Size(300, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label avisoLabel = new Label();
            avisoLabel.Text = "Para salir del modo Lock, cambie de ventana.";
            avisoLabel.AutoSize = true;
            avisoLabel.Location = new System.Drawing.Point(20, 20);

            noMostrarCheckBox = new CheckBox();
            noMostrarCheckBox.Text = "No mostrar este aviso nuevamente";
            noMostrarCheckBox.AutoSize = true;
            noMostrarCheckBox.Location = new System.Drawing.Point(20, 60);

            Button aceptarButton = new Button();
            aceptarButton.Text = "Aceptar";
            aceptarButton.Location = new System.Drawing.Point(100, 100);
            aceptarButton.Click += AceptarButton_Click;

            this.Controls.Add(avisoLabel);
            this.Controls.Add(noMostrarCheckBox);
            this.Controls.Add(aceptarButton);
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public bool NoMostrarAviso
        {
            get { return noMostrarCheckBox.Checked; }
        }
    }
}
