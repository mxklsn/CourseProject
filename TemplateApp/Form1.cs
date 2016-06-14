using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenGlTemplateApp;

namespace TemplateApp
{
    public partial class Form1 : Form
    {
        public static double positionX;
        public static double positionY;
        public Form1()
        {
            InitializeComponent();
            var modelCreator = new ModelCreator(new ModelData("../TestCase/test.json"));
            modelCreator.Create();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dxControl1.Invalidate();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            dxControl1.EnablePaint = true;
            dxControl1.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dxControl1.Invalidate();
        }

        private void dxControl1_MouseMove(object sender, MouseEventArgs e)
        {
            positionX = e.X;
            positionY = e.Y;
            dxControl1.Refresh();
        }
    }
}
