using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricModeling
{
	public partial class Form1 : Form
	{
		private IScene _scene;
		private SceneBuilder _sceneBuilder;
		Graphics _plane;

		public Form1()
		{
			InitializeComponent();

			_sceneBuilder = new SceneBuilder(new SceneReader());

			openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
			openFileDialog1.FileName = null;

			var bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
			_plane = Graphics.FromImage(bitmap);
			pictureBox1.Image = bitmap;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
				return;
			string path = openFileDialog1.FileName;
			_sceneBuilder.ReadSceneFromFile(path);
			_sceneBuilder.SetSizeScene(pictureBox1.Size);
			_scene = _sceneBuilder.Build();
			_scene.Scale(trackBar1.Value);
			_scene.Render(ref pictureBox1);
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.Size = pictureBox1.Size;
			_scene.Render(ref pictureBox1);
			//Bitmap bmp = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height, _plane);
			//pictureBox1.Image = bmp;
		}

		private void trackBar1_ValueChanged(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.Scale(((TrackBar)sender).Value);
			_scene.Render(ref pictureBox1);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.MoveX(-10);
			_scene.Render(ref pictureBox1);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.MoveX(10);
			_scene.Render(ref pictureBox1);
		}

		private void button6_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.MoveY(-10);
			_scene.Render(ref pictureBox1);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.MoveY(10);
			_scene.Render(ref pictureBox1);
		}

		private void button7_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.RotateZ(10.0 * Math.PI / 180.0);
			_scene.Render(ref pictureBox1);
		}

		private void button8_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.RotateZ(-10.0 * Math.PI / 180.0);
			_scene.Render(ref pictureBox1);
		}

		private void button9_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.RotateY(10.0 * Math.PI / 180.0);
			_scene.Render(ref pictureBox1);
		}

		private void button10_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.RotateY(-10.0 * Math.PI / 180.0);
			_scene.Render(ref pictureBox1);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (_scene == null) return;
			switch ((Keys)e.KeyValue)
			{
				case Keys.A:
					if(e.Modifiers == Keys.Shift)
						_scene.MoveX(-100);
					else if(e.Modifiers == Keys.Control)
						_scene.MoveX(-1);
					else
						_scene.MoveX(-10);
					break;
				case Keys.S:
					if (e.Modifiers == Keys.Shift)
						_scene.MoveY(-100);
					else if (e.Modifiers == Keys.Control)
						_scene.MoveY(-1);
					else
						_scene.MoveY(-10);
					break;
				case Keys.D:
					if (e.Modifiers == Keys.Shift)
						_scene.MoveX(100);
					else if (e.Modifiers == Keys.Control)
						_scene.MoveX(1);
					else
						_scene.MoveX(10);
					break;
				case Keys.W:
					if (e.Modifiers == Keys.Shift)
						_scene.MoveY(100);
					else if (e.Modifiers == Keys.Control)
						_scene.MoveY(1);
					else
						_scene.MoveY(10);
					break;
				case Keys.Q:
					if (e.Modifiers == Keys.Shift)
						_scene.RotateZ(100.0 * Math.PI / 180.0);
					else if (e.Modifiers == Keys.Control)
						_scene.RotateZ(1.0 * Math.PI / 180.0);
					else
						_scene.RotateZ(10.0 * Math.PI / 180.0);
					break;
				case Keys.E:
					if (e.Modifiers == Keys.Shift)
						_scene.RotateZ(-100.0 * Math.PI / 180.0);
					else if (e.Modifiers == Keys.Control)
						_scene.RotateZ(-1.0 * Math.PI / 180.0);
					else
						_scene.RotateZ(-10.0 * Math.PI / 180.0);
					break;
				case Keys.Z:
					if (e.Modifiers == Keys.Shift)
						_scene.RotateY(-100.0 * Math.PI / 180.0);
					else if (e.Modifiers == Keys.Control)
						_scene.RotateY(-1.0 * Math.PI / 180.0);
					else
						_scene.RotateY(-10.0 * Math.PI / 180.0);
					break;
				case Keys.X:
					if (e.Modifiers == Keys.Shift)
						_scene.RotateY(100.0 * Math.PI / 180.0);
					else if (e.Modifiers == Keys.Control)
						_scene.RotateY(1.0 * Math.PI / 180.0);
					else
						_scene.RotateY(10.0 * Math.PI / 180.0);
					break;
				case Keys.R:
					if (e.Modifiers == Keys.Shift)
						_scene.RotateX(-100.0 * Math.PI / 180.0);
					else if (e.Modifiers == Keys.Control)
						_scene.RotateX(-1.0 * Math.PI / 180.0);
					else
						_scene.RotateX(-10.0 * Math.PI / 180.0);
					break;
				case Keys.F:
					if (e.Modifiers == Keys.Shift)
						_scene.RotateX(100.0 * Math.PI / 180.0);
					else if (e.Modifiers == Keys.Control)
						_scene.RotateX(1.0 * Math.PI / 180.0);
					else
						_scene.RotateX(10.0 * Math.PI / 180.0);
					break;
			}
			_scene.Render(ref pictureBox1);
		}

		private void button11_Click(object sender, EventArgs e)
		{
			_scene.Enter();
			_scene.Render(ref pictureBox1);
		}

		private void button12_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			try
			{
				if (!string.IsNullOrEmpty(textBoxShearXY.Text)) _scene.ShearXToY(Convert.ToDouble(textBoxShearXY.Text));
				if (!string.IsNullOrEmpty(textBoxShearXZ.Text)) _scene.ShearXToZ(Convert.ToDouble(textBoxShearXZ.Text));
				if (!string.IsNullOrEmpty(textBoxShearYX.Text)) _scene.ShearYToX(Convert.ToDouble(textBoxShearYX.Text));
				if (!string.IsNullOrEmpty(textBoxShearYZ.Text)) _scene.ShearYToZ(Convert.ToDouble(textBoxShearYZ.Text));
				if (!string.IsNullOrEmpty(textBoxShearZX.Text)) _scene.ShearZToX(Convert.ToDouble(textBoxShearZX.Text));
				if (!string.IsNullOrEmpty(textBoxShearZY.Text)) _scene.ShearZToY(Convert.ToDouble(textBoxShearZY.Text));
			}
			catch
			{
				MessageBox.Show("Не удалос преобразовать данные");
				return;
			}
			 textBoxShearXY.Text = "";
			 textBoxShearXZ.Text = "";
			 textBoxShearYX.Text = "";
			 textBoxShearYZ.Text = "";
			 textBoxShearZX.Text = "";
			textBoxShearZY.Text = "";
			_scene.Render(ref pictureBox1);
		}

		private void button13_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			try
			{
				if (!string.IsNullOrEmpty(textBoxOppX.Text)) _scene.OppX(Convert.ToDouble(textBoxOppX.Text));
				if (!string.IsNullOrEmpty(textBoxOppY.Text)) _scene.OppY(Convert.ToDouble(textBoxOppY.Text));
				if (!string.IsNullOrEmpty(textBoxOppZ.Text)) _scene.OppZ(Convert.ToDouble(textBoxOppZ.Text));
			}
			catch
			{
				MessageBox.Show("Не удалос преобразовать данные");
				return;
			}
			textBoxOppX.Text = "";
			textBoxOppY.Text = "";
			textBoxOppZ.Text = "";
			_scene.Render(ref pictureBox1);
		}

		private void button14_Click(object sender, EventArgs e)
		{
			if (_scene == null) return;
			_scene.Default();
			_scene.Render(ref pictureBox1);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			button14_Click(null, null);
		}
	}
}
