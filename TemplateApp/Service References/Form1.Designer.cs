﻿namespace TemplateApp
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dxControl1 = new TemplateApp.DxControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stepChange = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.plusScale = new System.Windows.Forms.Button();
            this.minusScale = new System.Windows.Forms.Button();
            this.plusZ = new System.Windows.Forms.Button();
            this.minusZ = new System.Windows.Forms.Button();
            this.plusY = new System.Windows.Forms.Button();
            this.minusY = new System.Windows.Forms.Button();
            this.plusX = new System.Windows.Forms.Button();
            this.minusX = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepChange)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dxControl1
            // 
            this.dxControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dxControl1.EnablePaint = false;
            this.dxControl1.Location = new System.Drawing.Point(0, 0);
            this.dxControl1.Name = "dxControl1";
            this.dxControl1.Size = new System.Drawing.Size(819, 689);
            this.dxControl1.TabIndex = 3;
            this.dxControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dxControl1_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.stepChange);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.plusScale);
            this.panel1.Controls.Add(this.minusScale);
            this.panel1.Controls.Add(this.plusZ);
            this.panel1.Controls.Add(this.minusZ);
            this.panel1.Controls.Add(this.plusY);
            this.panel1.Controls.Add(this.minusY);
            this.panel1.Controls.Add(this.plusX);
            this.panel1.Controls.Add(this.minusX);
            this.panel1.Location = new System.Drawing.Point(816, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 689);
            this.panel1.TabIndex = 4;
            // 
            // stepChange
            // 
            this.stepChange.BackColor = System.Drawing.SystemColors.MenuBar;
            this.stepChange.DecimalPlaces = 2;
            this.stepChange.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.stepChange.Location = new System.Drawing.Point(124, 7);
            this.stepChange.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.stepChange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.stepChange.Name = "stepChange";
            this.stepChange.ReadOnly = true;
            this.stepChange.Size = new System.Drawing.Size(55, 20);
            this.stepChange.TabIndex = 27;
            this.stepChange.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.stepChange.ValueChanged += new System.EventHandler(this.stepChange_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Маштаб:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Коэффициент:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(60, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "умен. / увел.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "поворот по OZ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "поворот по OY";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "поворот по OX";
            // 
            // plusScale
            // 
            this.plusScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plusScale.Location = new System.Drawing.Point(147, 189);
            this.plusScale.Name = "plusScale";
            this.plusScale.Size = new System.Drawing.Size(32, 23);
            this.plusScale.TabIndex = 13;
            this.plusScale.Text = "+";
            this.plusScale.UseVisualStyleBackColor = true;
            this.plusScale.Click += new System.EventHandler(this.plusScale_Click);
            // 
            // minusScale
            // 
            this.minusScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minusScale.Location = new System.Drawing.Point(22, 189);
            this.minusScale.Name = "minusScale";
            this.minusScale.Size = new System.Drawing.Size(32, 23);
            this.minusScale.TabIndex = 14;
            this.minusScale.Text = "−";
            this.minusScale.UseVisualStyleBackColor = true;
            this.minusScale.Click += new System.EventHandler(this.minusScale_Click);
            // 
            // plusZ
            // 
            this.plusZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plusZ.Location = new System.Drawing.Point(147, 91);
            this.plusZ.Name = "plusZ";
            this.plusZ.Size = new System.Drawing.Size(32, 23);
            this.plusZ.TabIndex = 15;
            this.plusZ.Text = "+";
            this.plusZ.UseVisualStyleBackColor = true;
            this.plusZ.Click += new System.EventHandler(this.plusZ_Click);
            // 
            // minusZ
            // 
            this.minusZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minusZ.Location = new System.Drawing.Point(22, 91);
            this.minusZ.Name = "minusZ";
            this.minusZ.Size = new System.Drawing.Size(32, 23);
            this.minusZ.TabIndex = 16;
            this.minusZ.Text = "−";
            this.minusZ.UseVisualStyleBackColor = true;
            this.minusZ.Click += new System.EventHandler(this.minusZ_Click);
            // 
            // plusY
            // 
            this.plusY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plusY.Location = new System.Drawing.Point(147, 62);
            this.plusY.Name = "plusY";
            this.plusY.Size = new System.Drawing.Size(32, 23);
            this.plusY.TabIndex = 17;
            this.plusY.Text = "+";
            this.plusY.UseVisualStyleBackColor = true;
            this.plusY.Click += new System.EventHandler(this.plusY_Click);
            // 
            // minusY
            // 
            this.minusY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minusY.Location = new System.Drawing.Point(22, 62);
            this.minusY.Name = "minusY";
            this.minusY.Size = new System.Drawing.Size(32, 23);
            this.minusY.TabIndex = 18;
            this.minusY.Text = "−";
            this.minusY.UseVisualStyleBackColor = true;
            this.minusY.Click += new System.EventHandler(this.minusY_Click);
            // 
            // plusX
            // 
            this.plusX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plusX.Location = new System.Drawing.Point(147, 33);
            this.plusX.Name = "plusX";
            this.plusX.Size = new System.Drawing.Size(32, 23);
            this.plusX.TabIndex = 19;
            this.plusX.Text = "+";
            this.plusX.UseVisualStyleBackColor = true;
            this.plusX.Click += new System.EventHandler(this.plusX_Click);
            // 
            // minusX
            // 
            this.minusX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minusX.Location = new System.Drawing.Point(22, 33);
            this.minusX.Name = "minusX";
            this.minusX.Size = new System.Drawing.Size(32, 23);
            this.minusX.TabIndex = 20;
            this.minusX.Text = "−";
            this.minusX.UseVisualStyleBackColor = true;
            this.minusX.Click += new System.EventHandler(this.minusX_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 689);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dxControl1);
            this.Name = "Form1";
            this.Text = "Тестовое приложение";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepChange)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DxControl dxControl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown stepChange;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button plusScale;
        private System.Windows.Forms.Button minusScale;
        private System.Windows.Forms.Button plusZ;
        private System.Windows.Forms.Button minusZ;
        private System.Windows.Forms.Button plusY;
        private System.Windows.Forms.Button minusY;
        private System.Windows.Forms.Button plusX;
        private System.Windows.Forms.Button minusX;
    }
}
