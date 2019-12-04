namespace GeometricModeling
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
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
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button11 = new System.Windows.Forms.Button();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.button10 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxShearXY = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxShearXZ = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxShearYX = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxShearYZ = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxShearZX = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxShearZY = new System.Windows.Forms.TextBox();
			this.button12 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxOppX = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxOppY = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxOppZ = new System.Windows.Forms.TextBox();
			this.button13 = new System.Windows.Forms.Button();
			this.button14 = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(33, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(117, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Load...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.button14);
			this.panel1.Controls.Add(this.button13);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.textBoxOppZ);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.textBoxOppY);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.textBoxOppX);
			this.panel1.Controls.Add(this.button12);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.textBoxShearZY);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.textBoxShearZX);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textBoxShearYZ);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.textBoxShearYX);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.textBoxShearXZ);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.textBoxShearXY);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.button11);
			this.panel1.Controls.Add(this.trackBar1);
			this.panel1.Controls.Add(this.button10);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.button9);
			this.panel1.Controls.Add(this.button8);
			this.panel1.Controls.Add(this.button7);
			this.panel1.Controls.Add(this.button6);
			this.panel1.Controls.Add(this.button5);
			this.panel1.Controls.Add(this.button4);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Location = new System.Drawing.Point(742, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(189, 640);
			this.panel1.TabIndex = 2;
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(33, 32);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(117, 23);
			this.button11.TabIndex = 10;
			this.button11.Text = "Enter";
			this.button11.UseVisualStyleBackColor = true;
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(67, 153);
			this.trackBar1.Maximum = 35;
			this.trackBar1.Minimum = 1;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(117, 45);
			this.trackBar1.TabIndex = 9;
			this.trackBar1.TickFrequency = 5;
			this.trackBar1.Value = 1;
			this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
			// 
			// button10
			// 
			this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button10.Location = new System.Drawing.Point(118, 585);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(35, 35);
			this.button10.TabIndex = 8;
			this.button10.Text = "↩";
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button9
			// 
			this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button9.Location = new System.Drawing.Point(36, 585);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(35, 35);
			this.button9.TabIndex = 7;
			this.button9.Text = "↪";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button8
			// 
			this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button8.Location = new System.Drawing.Point(118, 503);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(35, 35);
			this.button8.TabIndex = 6;
			this.button8.Text = "↷";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button7
			// 
			this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button7.Location = new System.Drawing.Point(36, 503);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(35, 35);
			this.button7.TabIndex = 5;
			this.button7.Text = "↶";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button6
			// 
			this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button6.Location = new System.Drawing.Point(77, 585);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(35, 35);
			this.button6.TabIndex = 4;
			this.button6.Text = "↓";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.Location = new System.Drawing.Point(77, 503);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(35, 35);
			this.button5.TabIndex = 3;
			this.button5.Text = "↑";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(118, 544);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(35, 35);
			this.button4.TabIndex = 2;
			this.button4.Text = "→";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(77, 544);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(35, 35);
			this.button3.TabIndex = 1;
			this.button3.Text = "↹";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(36, 544);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(35, 35);
			this.button2.TabIndex = 0;
			this.button2.Text = "←";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(724, 640);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(109, 135);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Scale";
			// 
			// textBoxShearXY
			// 
			this.textBoxShearXY.Location = new System.Drawing.Point(12, 153);
			this.textBoxShearXY.Name = "textBoxShearXY";
			this.textBoxShearXY.Size = new System.Drawing.Size(42, 20);
			this.textBoxShearXY.TabIndex = 12;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 135);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Shear XY";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 176);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "Shear XZ";
			// 
			// textBoxShearXZ
			// 
			this.textBoxShearXZ.Location = new System.Drawing.Point(12, 194);
			this.textBoxShearXZ.Name = "textBoxShearXZ";
			this.textBoxShearXZ.Size = new System.Drawing.Size(42, 20);
			this.textBoxShearXZ.TabIndex = 14;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 217);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(52, 13);
			this.label4.TabIndex = 17;
			this.label4.Text = "Shear YX";
			// 
			// textBoxShearYX
			// 
			this.textBoxShearYX.Location = new System.Drawing.Point(12, 235);
			this.textBoxShearYX.Name = "textBoxShearYX";
			this.textBoxShearYX.Size = new System.Drawing.Size(42, 20);
			this.textBoxShearYX.TabIndex = 16;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 258);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 19;
			this.label5.Text = "Shear YZ";
			// 
			// textBoxShearYZ
			// 
			this.textBoxShearYZ.Location = new System.Drawing.Point(12, 276);
			this.textBoxShearYZ.Name = "textBoxShearYZ";
			this.textBoxShearYZ.Size = new System.Drawing.Size(42, 20);
			this.textBoxShearYZ.TabIndex = 18;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(9, 299);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 13);
			this.label6.TabIndex = 21;
			this.label6.Text = "Shear ZX";
			// 
			// textBoxShearZX
			// 
			this.textBoxShearZX.Location = new System.Drawing.Point(12, 317);
			this.textBoxShearZX.Name = "textBoxShearZX";
			this.textBoxShearZX.Size = new System.Drawing.Size(42, 20);
			this.textBoxShearZX.TabIndex = 20;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 340);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(52, 13);
			this.label7.TabIndex = 23;
			this.label7.Text = "Shear ZY";
			// 
			// textBoxShearZY
			// 
			this.textBoxShearZY.Location = new System.Drawing.Point(12, 358);
			this.textBoxShearZY.Name = "textBoxShearZY";
			this.textBoxShearZY.Size = new System.Drawing.Size(42, 20);
			this.textBoxShearZY.TabIndex = 22;
			// 
			// button12
			// 
			this.button12.Location = new System.Drawing.Point(12, 384);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(55, 23);
			this.button12.TabIndex = 24;
			this.button12.Text = "Apply";
			this.button12.UseVisualStyleBackColor = true;
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(115, 258);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(39, 13);
			this.label8.TabIndex = 26;
			this.label8.Text = "OPP X";
			// 
			// textBoxOppX
			// 
			this.textBoxOppX.Location = new System.Drawing.Point(118, 276);
			this.textBoxOppX.Name = "textBoxOppX";
			this.textBoxOppX.Size = new System.Drawing.Size(42, 20);
			this.textBoxOppX.TabIndex = 25;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(115, 299);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(39, 13);
			this.label9.TabIndex = 28;
			this.label9.Text = "OPP Y";
			// 
			// textBoxOppY
			// 
			this.textBoxOppY.Location = new System.Drawing.Point(118, 317);
			this.textBoxOppY.Name = "textBoxOppY";
			this.textBoxOppY.Size = new System.Drawing.Size(42, 20);
			this.textBoxOppY.TabIndex = 27;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(115, 340);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(39, 13);
			this.label10.TabIndex = 30;
			this.label10.Text = "OPP Z";
			// 
			// textBoxOppZ
			// 
			this.textBoxOppZ.Location = new System.Drawing.Point(118, 358);
			this.textBoxOppZ.Name = "textBoxOppZ";
			this.textBoxOppZ.Size = new System.Drawing.Size(42, 20);
			this.textBoxOppZ.TabIndex = 29;
			// 
			// button13
			// 
			this.button13.Location = new System.Drawing.Point(112, 384);
			this.button13.Name = "button13";
			this.button13.Size = new System.Drawing.Size(55, 23);
			this.button13.TabIndex = 31;
			this.button13.Text = "Apply";
			this.button13.UseVisualStyleBackColor = true;
			this.button13.Click += new System.EventHandler(this.button13_Click);
			// 
			// button14
			// 
			this.button14.Location = new System.Drawing.Point(33, 61);
			this.button14.Name = "button14";
			this.button14.Size = new System.Drawing.Size(117, 23);
			this.button14.TabIndex = 32;
			this.button14.Text = "Default";
			this.button14.UseVisualStyleBackColor = true;
			this.button14.Click += new System.EventHandler(this.button14_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(943, 664);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.panel1);
			this.KeyPreview = true;
			this.Name = "Form1";
			this.Text = "Form1";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxOppZ;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxOppY;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxOppX;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxShearZY;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxShearZX;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxShearYZ;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxShearYX;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxShearXZ;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxShearXY;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button14;
	}
}

