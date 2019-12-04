using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricModeling
{
	interface IScene:IRotate, IMove,IScale,IShear, IOpp
	{
		Size Size { get; set; }
		void Render(ref Graphics plane);
		void Render(ref PictureBox picture);

		void Enter();

		void Default();
	}
}
