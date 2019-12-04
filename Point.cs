using System;
using System.Drawing;

namespace GeometricModeling
{
    class Point :IObjectScene
    {
        public double this[int key]{
            get {
                    switch (key)
                    {
                        case 0: return X;
                        case 1: return Y;
                        case 2: return Z;
                        case 3: return H;
                        default: throw new ArgumentNullException(nameof(key));
                    }
                }
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double H { get; set; }

		public void Draw(MatrixTransform matrix, Graphics plane)
		{
			Point p = this * matrix;
			plane.FillEllipse(new SolidBrush(Color.DarkRed), (float)p.X - 1, (float)p.Y - 1, 2, 2);
		}

		public string GetDescription()
		{
			return $"Точка ({X}, {Y}, {Z}, {H})";
		}

		public static Point operator*(Point point, MatrixTransform matrix)
        {
            Point p = new Point();
            p.X = matrix.Matrix[0,0] * point[0] + matrix.Matrix[1,0] * point[1] + matrix.Matrix[2,0] * point[2] + matrix.Matrix[3,0] * point[3];
            p.Y = matrix.Matrix[0,1] * point[0] + matrix.Matrix[1,1] * point[1] + matrix.Matrix[2,1] * point[2] + matrix.Matrix[3,1] * point[3];
            p.Z = matrix.Matrix[0,2] * point[0] + matrix.Matrix[1,2] * point[1] + matrix.Matrix[2,2] * point[2] + matrix.Matrix[3,2] * point[3];
            p.H = matrix.Matrix[0,3] * point[0] + matrix.Matrix[1,3] * point[1] + matrix.Matrix[2,3] * point[2] + matrix.Matrix[3,3] * point[3];

			p.X /= p.H;
			p.Y /= p.H;
			p.Z /= p.H;
			p.H /= p.H;

			return p;
        }
    }
}