﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace PortofoliuDeActiuni
{
    public partial class FormGrafic : Form
    {
        double[] vect = new double[20];
        int nrElem = 0;
        bool vb = false;
        Graphics gr;
        const int marg = 10;
        Color culoare = Color.Azure;
        public FormGrafic()
        {
            InitializeComponent();
            gr=panel1.CreateGraphics();
            //panel1.Paint += new PaintEventHandler(panel1_Paint);

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            panel1.Paint += new PaintEventHandler(panel1_Paint);
        }
        private void incarcaDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("fisierDateGrafic.txt");
            string linie = null;
            while((linie = sr.ReadLine()) != null)
            {
                vect[nrElem]=Convert.ToDouble(linie);
                nrElem++;
                vb = true;
            }
            sr.Close();
            MessageBox.Show("Date incarcate!");
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(vb==true)
            {
                Rectangle rec = new Rectangle(panel1.ClientRectangle.X + marg,
                    panel1.ClientRectangle.Y + 2 * marg,
                    panel1.ClientRectangle.Width - 2 * marg,
                    panel1.ClientRectangle.Height - 3 * marg);
                Pen pen = new Pen(Color.Red, 3);
                gr.DrawRectangle(pen, rec);

                double latime = rec.Width / nrElem / 2;
                double distanta = (rec.Width - nrElem * latime) / (nrElem + 1);
                double vMax = vect.Max();

                Brush br = new SolidBrush(culoare);
                Rectangle[] recs = new Rectangle[nrElem];
                for (int i = 0; i < nrElem; i++)
                {
                    recs[i] = new Rectangle((int)(rec.Location.X + (i + 1) * distanta + i * latime),
                        (int)(rec.Location.Y + rec.Height - vect[i] / vMax * rec.Height),
                        (int)latime,
                        (int)(vect[i] / vMax * rec.Height));
                    gr.FillRectangle(br, recs[i]);
                    //gr.FillEllipse(br, recs[i]);
                    gr.DrawString(vect[i].ToString(), this.Font,
                        br, recs[i].Location.X, recs[i].Location.Y - this.Font.Height);
                }
                //gr.FillRectangles(br, recs);
                for (int i = 0; i < nrElem - 1; i++)
                    gr.DrawLine(pen, new Point((int)(recs[i].Location.X + latime / 2),
                        recs[i].Location.Y),
                        new Point((int)(recs[i + 1].Location.X + latime / 2),
                        recs[i + 1].Location.Y));

            }
        }
        private void schimbaCuloareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                culoare = dlg.Color;
            panel1.Invalidate();
        }

        private void pd_print(object sender, PrintPageEventArgs e)
        {
            if (vb == true)
            {
                gr = e.Graphics;
                Rectangle rec = new Rectangle(e.PageBounds.X + marg,
                    e.PageBounds.Y + 2 * marg,
                    e.PageBounds.Width - 2 * marg,
                    e.PageBounds.Height - 3 * marg);
                Pen pen = new Pen(Color.Red, 3);
                gr.DrawRectangle(pen, rec);


                double latime = rec.Width / nrElem / 2;
                double distanta = (rec.Width - nrElem * latime) / (nrElem + 1);
                double vMax = vect.Max();

                Brush br = new SolidBrush(culoare);
                Rectangle[] recs = new Rectangle[nrElem];
                for (int i = 0; i < nrElem; i++)
                {
                    recs[i] = new Rectangle((int)(rec.Location.X + (i + 1) * distanta + i * latime),
                        (int)(rec.Location.Y + rec.Height - vect[i] / vMax * rec.Height),
                        (int)latime,
                        (int)(vect[i] / vMax * rec.Height));
                    gr.FillRectangle(br, recs[i]);
                    //gr.FillEllipse(br, recs[i]);
                    gr.DrawString(vect[i].ToString(), this.Font,
                        br, recs[i].Location.X, recs[i].Location.Y - this.Font.Height);

                }
                //gr.FillRectangles(br, recs);
                for (int i = 0; i < nrElem - 1; i++)
                    gr.DrawLine(pen, new Point((int)(recs[i].Location.X + latime / 2),
                        recs[i].Location.Y),
                        new Point((int)(recs[i + 1].Location.X + latime / 2),
                        recs[i + 1].Location.Y));

                gr.DrawString("Evolutia pretului actiunii: ", this.Font, Brushes.Yellow, new Point((int)(rec.Location.X + latime / 2),
                        rec.Location.Y));
            }
        }

        private void previzualizeazaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_print);
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pd;
            dlg.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // panel1.Paint += new PaintEventHandler(panel1_Paint);
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void previzualizareToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
