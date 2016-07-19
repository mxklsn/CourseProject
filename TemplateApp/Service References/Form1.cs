﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenGlTemplateApp;

namespace TemplateApp
{
    public partial class Form1 : Form
    {
        public static double PositionX;
        public static double PositionY;

        public static bool DxRefresh;

        public static int ChangerX;
        public static int ChangerY;
        public static int ChangerZ;
        public static double StepChanger;
        public static double CoefDepth;

        public static int[] CountFe;
        public static int CountArray;
        public static double[] Points;

        public Form1()
        {
            InitializeComponent();
            var modelCreator = new ModelCreator(new ModelData("../TestCase/test.json"));
            modelCreator.Create();

            Points = modelCreator.Points;
            CountFe = modelCreator.CountFe;
            CountArray = modelCreator.CountArray;

            CoefDepth = 0.7;
            StepChanger = Convert.ToDouble(stepChange.Value);
            DxRefresh = false;
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
            PositionX = e.X;
            PositionY = e.Y;
            dxControl1.Refresh();
        }
        

        /* Коэф одного шага */
        private void stepChange_ValueChanged(object sender, EventArgs e)
        {
            StepChanger = Convert.ToDouble(stepChange.Value);
            dxControl1.Refresh();
        }

        private void minusX_Click(object sender, EventArgs e)
        {
            ChangerX--;
            dxControl1.Refresh();
        }

        private void plusX_Click(object sender, EventArgs e)
        {
            ChangerX++;
            dxControl1.Refresh();
        }

        private void minusY_Click(object sender, EventArgs e)
        {
            ChangerY--;
            dxControl1.Refresh();
        }

        private void plusY_Click(object sender, EventArgs e)
        {
            ChangerY++;
            dxControl1.Refresh();
        }

        private void minusZ_Click(object sender, EventArgs e)
        {
            ChangerZ--;
            dxControl1.Refresh();
        }

        private void plusZ_Click(object sender, EventArgs e)
        {
            ChangerZ++;
            dxControl1.Refresh();
        }

        private void minusScale_Click(object sender, EventArgs e)
        {
            CoefDepth -= 0.1;
            dxControl1.Refresh();
        }

        private void plusScale_Click(object sender, EventArgs e)
        {
            CoefDepth += 0.1;
            dxControl1.Refresh();
        }
    }
}
