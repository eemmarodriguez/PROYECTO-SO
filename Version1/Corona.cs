using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Version1
{
    public partial class Corona : Form
    {
        public Corona()
        {
            InitializeComponent();
        }

        int categoria = 0;

        bool deportes;
        bool historia;
        bool ciencia;
        bool arte;
        bool entretenimiento;
        bool geografia;
        public void SetCorrectas(bool d, bool h, bool c, bool a, bool e, bool g)
        {
            deportes = d;
            historia = h;
            ciencia = c;
            arte = a;
            entretenimiento = e;
            geografia = g;
        }
        private void DeportesPicture_Click(object sender, EventArgs e)
        {
            if (!deportes)
            {
                categoria = 1;
                //MessageBox.Show("Has escogio Deportes");
                this.Close();
            }
        }

        private void HistoriaBox_Click(object sender, EventArgs e)
        {
            if (!historia)
            {
                categoria = 2;
                //MessageBox.Show("Has escogio Historia");
                this.Close();
            }
        }

        private void CienciaBox_Click(object sender, EventArgs e)
        {
            if (!ciencia)
            {
                categoria = 3;
                this.Close();
            }
        }

        private void ArteBox_Click(object sender, EventArgs e)
        {
            if (!arte)
            {
                categoria = 4;
                this.Close();
            }
        }

        private void EntretenimientoBox_Click(object sender, EventArgs e)
        {
            if (!entretenimiento)
            {
                categoria = 5;
                this.Close();
            }
        }

        private void GeografiaBox_Click(object sender, EventArgs e)
        {
            if (!geografia)
            {
                categoria = 6;
                this.Close();
            }
        }
        public int GetCat ()
        {
            return categoria;
        }

        private void Corona_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            if (deportes) { DeportesPicture.Image = Image.FromFile("Imagenes/DeportesTick.png"); DeportesPicture.Enabled = false; DeportesPicture.Refresh(); }
            if (historia) { HistoriaBox.Image = Image.FromFile("Imagenes/HistoriaTick.png"); HistoriaBox.Enabled = false; HistoriaBox.Refresh(); }
            if (ciencia) { CienciaBox.Image = Image.FromFile("Imagenes/CienciaTick.png"); CienciaBox.Enabled = false; CienciaBox.Refresh(); }
            if (arte) { ArteBox.Image = Image.FromFile("Imagenes/ArteTick.png"); ArteBox.Enabled = false; ArteBox.Refresh(); }
            if (entretenimiento) { EntretenimientoBox.Image = Image.FromFile("Imagenes/EntretenimientoTick.png"); EntretenimientoBox.Enabled = false; EntretenimientoBox.Refresh(); }
            if (geografia) { GeografiaBox.Image = Image.FromFile("Imagenes/GeografiaTick.png"); GeografiaBox.Enabled = false; GeografiaBox.Refresh(); }
                   

        }
    }
}
