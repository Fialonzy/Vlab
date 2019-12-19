using System;
using System.Drawing;

namespace GeometricModeling
{
    class Point
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

        public Point(double x=0, double y=0, double z=0, double h=1)
        {
            X = x;
            Y = y;
            Z = z;
            H = h;
        }

		public static bool operator ==(Point a, Point b)
		{
			if (a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.H != b.H) return false;
			return true;
		}

		public static bool operator !=(Point a, Point b)
		{
			return !(a==b);
		}

		public static Point operator * (Point point, double coefficient){
            Point p = new Point();
            p.X = point.X * coefficient;
            p.Y = point.Y * coefficient;
            p.Z = point.Z * coefficient;
			p.H = point.H * coefficient;
			return Normalize(p);
			return p;
		}

		public static Point operator * (double coefficient,Point point){
            Point p = new Point();
            p.X = point.X * coefficient;
            p.Y = point.Y * coefficient;
            p.Z = point.Z * coefficient;
			p.H = point.H * coefficient;
			return Normalize(p);
			return p;
		}

		public static Point operator*(Point point, MatrixTransform matrix)
        {
            Point p = new Point();
            p.X = matrix.Matrix[0,0] * point[0] + matrix.Matrix[1,0] * point[1] + matrix.Matrix[2,0] * point[2] + matrix.Matrix[3,0] * point[3];
            p.Y = matrix.Matrix[0,1] * point[0] + matrix.Matrix[1,1] * point[1] + matrix.Matrix[2,1] * point[2] + matrix.Matrix[3,1] * point[3];
            p.Z = matrix.Matrix[0,2] * point[0] + matrix.Matrix[1,2] * point[1] + matrix.Matrix[2,2] * point[2] + matrix.Matrix[3,2] * point[3];
            p.H = matrix.Matrix[0,3] * point[0] + matrix.Matrix[1,3] * point[1] + matrix.Matrix[2,3] * point[2] + matrix.Matrix[3,3] * point[3];


            return Normalize(p);
		}

		public static Point Normalize(Point p)
		{
			return new Point(p.X /= p.H, p.Y /= p.H, p.Z /= p.H);
		}

        public static Point operator+(Point a, Point b){
            Point result = new Point();
            result.X = a.X + b.X;
            result.Y = a.Y + b.Y;
            result.Z = a.Z + b.Z;
			result.H = a.H + b.H;
			return Normalize(result);
			return result;
        }

        public override string  ToString(){
            return $"({X}, {Y}, {Z}, {H})";
        } 
    }
}