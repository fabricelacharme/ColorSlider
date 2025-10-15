namespace ColorSliderTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorSlider8 = new ColorSlider.ColorSlider();
            this.colorSlider7 = new ColorSlider.ColorSlider();
            this.colorSlider6 = new ColorSlider.ColorSlider();
            this.colorSlider5 = new ColorSlider.ColorSlider();
            this.colorSlider4 = new ColorSlider.ColorSlider();
            this.colorSlider3 = new ColorSlider.ColorSlider();
            this.colorSlider2 = new ColorSlider.ColorSlider();
            this.colorSlider1 = new ColorSlider.ColorSlider();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.colorSlider8);
            this.panel1.Controls.Add(this.colorSlider7);
            this.panel1.Controls.Add(this.colorSlider6);
            this.panel1.Controls.Add(this.colorSlider5);
            this.panel1.Controls.Add(this.colorSlider4);
            this.panel1.Controls.Add(this.colorSlider3);
            this.panel1.Controls.Add(this.colorSlider2);
            this.panel1.Controls.Add(this.colorSlider1);
            this.panel1.Location = new System.Drawing.Point(27, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 300);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(516, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "50";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(454, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "30";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(409, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "40";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colorSlider8
            // 
            this.colorSlider8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(77)))), ((int)(((byte)(95)))));
            this.colorSlider8.BarThickness = 3F;
            this.colorSlider8.Font = new System.Drawing.Font("Verdana", 7.25F);
            this.colorSlider8.Location = new System.Drawing.Point(504, 81);
            this.colorSlider8.Name = "colorSlider8";
            this.colorSlider8.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorSlider8.ShowDivisionsText = false;
            this.colorSlider8.Size = new System.Drawing.Size(54, 200);
            this.colorSlider8.TabIndex = 6;
            this.colorSlider8.Text = "colorSlider8";
            this.colorSlider8.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.colorSlider8.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.colorSlider8.ValueChanged += new System.EventHandler(this.colorSlider8_ValueChanged);
            // 
            // colorSlider7
            // 
            this.colorSlider7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(77)))), ((int)(((byte)(95)))));
            this.colorSlider7.BarThickness = 2.5F;
            this.colorSlider7.ColorSchema = ColorSlider.ColorSlider.ColorSchemas.RedColors;
            this.colorSlider7.ElapsedInnerColor = System.Drawing.Color.Red;
            this.colorSlider7.ElapsedPenColorBottom = System.Drawing.Color.Salmon;
            this.colorSlider7.ElapsedPenColorTop = System.Drawing.Color.LightCoral;
            this.colorSlider7.Font = new System.Drawing.Font("Verdana", 7.25F);
            this.colorSlider7.Location = new System.Drawing.Point(409, 81);
            this.colorSlider7.Name = "colorSlider7";
            this.colorSlider7.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorSlider7.ShowDivisionsText = false;
            this.colorSlider7.Size = new System.Drawing.Size(30, 200);
            this.colorSlider7.TabIndex = 5;
            this.colorSlider7.Text = "colorSlider7";
            this.colorSlider7.ThumbInnerColor = System.Drawing.Color.Red;
            this.colorSlider7.ThumbPenColor = System.Drawing.Color.Red;
            this.colorSlider7.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.colorSlider7.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.colorSlider7.ValueChanged += new System.EventHandler(this.colorSlider7_ValueChanged);
            // 
            // colorSlider6
            // 
            this.colorSlider6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(77)))), ((int)(((byte)(95)))));
            this.colorSlider6.BarThickness = 3F;
            this.colorSlider6.Font = new System.Drawing.Font("Verdana", 7.25F);
            this.colorSlider6.Location = new System.Drawing.Point(409, 19);
            this.colorSlider6.Name = "colorSlider6";
            this.colorSlider6.ShowDivisionsText = false;
            this.colorSlider6.Size = new System.Drawing.Size(129, 42);
            this.colorSlider6.TabIndex = 4;
            this.colorSlider6.Text = "colorSlider6";
            this.colorSlider6.ThumbRoundRectSize = new System.Drawing.Size(4, 4);
            this.colorSlider6.ThumbSize = new System.Drawing.Size(8, 20);
            this.colorSlider6.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // colorSlider5
            // 
            this.colorSlider5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(77)))), ((int)(((byte)(95)))));
            this.colorSlider5.ColorSchema = ColorSlider.ColorSlider.ColorSchemas.GreenColors;
            this.colorSlider5.ElapsedInnerColor = System.Drawing.Color.Green;
            this.colorSlider5.ElapsedPenColorBottom = System.Drawing.Color.LightGreen;
            this.colorSlider5.ElapsedPenColorTop = System.Drawing.Color.SpringGreen;
            this.colorSlider5.Font = new System.Drawing.Font("Verdana", 7.25F);
            this.colorSlider5.Location = new System.Drawing.Point(454, 81);
            this.colorSlider5.Name = "colorSlider5";
            this.colorSlider5.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorSlider5.ShowDivisionsText = false;
            this.colorSlider5.Size = new System.Drawing.Size(30, 200);
            this.colorSlider5.TabIndex = 3;
            this.colorSlider5.Text = "colorSlider5";
            this.colorSlider5.ThumbInnerColor = System.Drawing.Color.Green;
            this.colorSlider5.ThumbPenColor = System.Drawing.Color.Green;
            this.colorSlider5.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.colorSlider5.ValueChanged += new System.EventHandler(this.colorSlider5_ValueChanged);
            // 
            // colorSlider4
            // 
            this.colorSlider4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(77)))), ((int)(((byte)(95)))));
            this.colorSlider4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorSlider4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.colorSlider4.Location = new System.Drawing.Point(150, 81);
            this.colorSlider4.Name = "colorSlider4";
            this.colorSlider4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorSlider4.Size = new System.Drawing.Size(79, 200);
            this.colorSlider4.TabIndex = 2;
            this.colorSlider4.Text = "colorSlider4";
            this.colorSlider4.ThumbImage = ((System.Drawing.Image)(resources.GetObject("colorSlider4.ThumbImage")));
            this.colorSlider4.TickColor = System.Drawing.Color.LightGreen;
            // 
            // colorSlider3
            // 
            this.colorSlider3.BackColor = System.Drawing.Color.Transparent;
            this.colorSlider3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorSlider3.Location = new System.Drawing.Point(13, 81);
            this.colorSlider3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.colorSlider3.Name = "colorSlider3";
            this.colorSlider3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorSlider3.ScaleSubDivisions = 2;
            this.colorSlider3.ShowSmallScale = true;
            this.colorSlider3.Size = new System.Drawing.Size(69, 200);
            this.colorSlider3.TabIndex = 2;
            this.colorSlider3.Text = "colorSlider3";
            this.colorSlider3.ThumbImage = global::ColorSliderTest.Properties.Resources.BTN_Thumb_Blue;
            this.colorSlider3.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.colorSlider3.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // colorSlider2
            // 
            this.colorSlider2.BackColor = System.Drawing.Color.Transparent;
            this.colorSlider2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorSlider2.Location = new System.Drawing.Point(13, 3);
            this.colorSlider2.Name = "colorSlider2";
            this.colorSlider2.ScaleSubDivisions = 2;
            this.colorSlider2.ShowSmallScale = true;
            this.colorSlider2.Size = new System.Drawing.Size(339, 58);
            this.colorSlider2.TabIndex = 1;
            this.colorSlider2.Text = "colorSlider2";
            this.colorSlider2.ThumbImage = global::ColorSliderTest.Properties.Resources.BTN_Thumb_Blue;
            this.colorSlider2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.colorSlider2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // colorSlider1
            // 
            this.colorSlider1.BackColor = System.Drawing.Color.Transparent;
            this.colorSlider1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorSlider1.Location = new System.Drawing.Point(297, 81);
            this.colorSlider1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.colorSlider1.Name = "colorSlider1";
            this.colorSlider1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorSlider1.ScaleDivisions = 5;
            this.colorSlider1.ScaleSubDivisions = 1;
            this.colorSlider1.ShowSmallScale = true;
            this.colorSlider1.Size = new System.Drawing.Size(55, 200);
            this.colorSlider1.TabIndex = 0;
            this.colorSlider1.Text = "colorSlider1";
            this.colorSlider1.ThumbImage = global::ColorSliderTest.Properties.Resources.BTN_Thumb_Blue;
            this.colorSlider1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.colorSlider1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(77)))), ((int)(((byte)(95)))));
            this.ClientSize = new System.Drawing.Size(629, 324);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Color Sider Control Demo";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private ColorSlider.ColorSlider colorSlider1;
        private ColorSlider.ColorSlider colorSlider2;
        private ColorSlider.ColorSlider colorSlider3;
        private ColorSlider.ColorSlider colorSlider5;
        private ColorSlider.ColorSlider colorSlider4;
        private ColorSlider.ColorSlider colorSlider6;
        private ColorSlider.ColorSlider colorSlider8;
        private ColorSlider.ColorSlider colorSlider7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}