using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorMap
{
    public partial class Form1 : Form
    {
        private SolidBrush myBrush;
        private Graphics myGraphics;
        private bool isDrawing = false;
        //Bandera
        private bool enablePaint = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myBrush = new SolidBrush(panel2.BackColor);
            myGraphics = panel1.CreateGraphics();
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel2.BackColor = colorDialog1.Color;
                myBrush.Color = panel2.BackColor;

            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing == true)
            {
                myGraphics.FillEllipse(myBrush, e.X, e.Y, trackBar1.Value, trackBar1.Value);
            }
        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            //Backgroun canvas
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = colorDialog2.Color;
                panel3.BackColor = colorDialog2.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Incializa un componente SaveFileDialog.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //Cuando buscas archivos te muestra todos los .bmp.
            saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            //Titulo
            saveFileDialog.Title = "Guardar gráfico como imagen";
            // preguntamos si elegiste un nombre de archivo.
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Extención del archivo por defecto segun el filtro del saveFileDialog
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        saveFileDialog.DefaultExt = "jpg";
                        break;

                    case 2:
                        saveFileDialog.DefaultExt = "bmp";
                        break;

                    case 3:
                        saveFileDialog.DefaultExt = "gif";
                        break;
                }

                //Obtenemos alto y ancho del panel
                int width = panel1.Width;
                int height = panel1.Height;
                //Inicializamos un objeto BitMap con las dimensiones del Panel
                Bitmap bitMap = new Bitmap(width, height);
                //Inicializamos un objeto Rectangle en la posicion 0,0 y con dimensiones iguales a las del panel.
                //0,0 y las mismas dimensiones del panel porque queremos tomar todo el panel
                // o si solo queremos tomar una parte pues podemos dar un punto de inicio diferente y dimensiones distintas.
                     Rectangle rec = new Rectangle(0, 0, width, height);
                //Este metodo hace la magia de copiar las graficas a el objeto Bitmap
                panel1.DrawToBitmap(bitMap, rec);
                // Y por ultimo salvamos el archivo pasando como parametro el nombre que asignamos en el saveDialogFile
                bitMap.Save(saveFileDialog.FileName);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!enablePaint)
            {
                isDrawing = true;
            }
            return;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            enablePaint = true;
        }
    }
}
