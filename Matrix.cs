using System;

namespace GeometricModeling{
    class MatrixTransform{
        public double[,] Matrix { get; }

        public MatrixTransform()
        {
            Matrix = new double[4,4]{
                { 1.0, 0.0, 0.0, 0.0 },
                { 0.0, 1.0, 0.0, 0.0 },
                { 0.0, 0.0, 1.0, 0.0 },
                { 0.0, 0.0, 0.0, 1.0 }
            };
        }

        public static MatrixTransform GetTranslation(Point vector){
            var matrix = new MatrixTransform();
            matrix.Matrix[3,0] = vector.X;
            matrix.Matrix[3,1] = vector.Y;
            matrix.Matrix[3,2] = vector.Z;
            return matrix;
        }

        public static MatrixTransform GetScaleX(double scale){
            var matrix = new MatrixTransform();
            matrix.Matrix[0,0] = scale;
            return matrix;
        }

        public static MatrixTransform GetScaleY(double scale){
            var matrix = new MatrixTransform();
            matrix.Matrix[1,1] = scale;
            return matrix;
        }

        public static MatrixTransform GetScaleZ(double scale){
            var matrix = new MatrixTransform();
            matrix.Matrix[2,2] = scale;
            return matrix;
        }

        public static MatrixTransform GetRotateX(double alpha){
            var matrix = new MatrixTransform();
            matrix.Matrix[1,1] = Math.Cos(alpha);
            matrix.Matrix[1,2] = Math.Sin(alpha);
            matrix.Matrix[2,1] = -Math.Sin(alpha);
            matrix.Matrix[2,2] = Math.Cos(alpha);
            return matrix;
        }

        public static MatrixTransform GetRotateY(double alpha){
            var matrix = new MatrixTransform();
            matrix.Matrix[0,0] = Math.Cos(alpha);
            matrix.Matrix[0,2] = -Math.Sin(alpha);
            matrix.Matrix[2,0] = Math.Sin(alpha);
            matrix.Matrix[2,2] = Math.Cos(alpha);
            return matrix;
        }

        public static MatrixTransform GetRotateZ(double alpha){
            var matrix = new MatrixTransform();
            matrix.Matrix[0,0] = Math.Cos(alpha);
            matrix.Matrix[0,1] = Math.Sin(alpha);
            matrix.Matrix[1,0] = -Math.Sin(alpha);
            matrix.Matrix[1,1] = Math.Cos(alpha);
            return matrix;
        }

        public static MatrixTransform GetShearXToY(double value){
            var matrix = new MatrixTransform();
            matrix.Matrix[1,0] = value;
            return matrix;
        }

        public static MatrixTransform GetShearXToZ(double value){
            var matrix = new MatrixTransform();
            matrix.Matrix[2,0] = value;
            return matrix;
        }

        public static MatrixTransform GetShearYToX(double value){
            var matrix = new MatrixTransform();
            matrix.Matrix[0,1] = value;
            return matrix;
        }

        public static MatrixTransform GetShearYToZ(double value){
            var matrix = new MatrixTransform();
            matrix.Matrix[2,1] = value;
            return matrix;
        }

        public static MatrixTransform GetShearZToX(double value){
            var matrix = new MatrixTransform();
            matrix.Matrix[0,2] = value;
            return matrix;
        }

        public static MatrixTransform GetShearZToY(double value){
            var matrix = new MatrixTransform();
            matrix.Matrix[1,2] = value;
            return matrix;
        }

        public static MatrixTransform GetOppX(double focal){
            var matrix = new MatrixTransform();
            matrix.Matrix[0,3] = 1/focal;
            return matrix;
        }

        public static MatrixTransform GetOppY(double focal){
            var matrix = new MatrixTransform();
            matrix.Matrix[1,3] = 1/focal;
            return matrix;
        }

        public static MatrixTransform GetOppZ(double focal){
            var matrix = new MatrixTransform();
            matrix.Matrix[2,3] = 1/focal;
            return matrix;
        }

        public static Point operator*(MatrixTransform matrix, Point point)
        {
			// ��������� ������� ���� (x, y, z, h) �� ������� ����	| a11, a12, a13, a14 |
			//														| a21, a22, a23, a24 |
			//														| a31, a32, a33, a34 |
			//														| a41, a42, a43, a44 |
			// ��������� => ����� ������, ( x*a11+y*a12+z*a13+h*a14, x*a21+y*a22+z*a23+h*a24, x*a31+y*a32+z*a33+h*a34, x*a41+y*a42+z*a43+h*a44)
			Point p = new Point();
            p.X = matrix.Matrix[0,0] * point.X + matrix.Matrix[0,1] * point.Y + matrix.Matrix[0,2] * point.Z + matrix.Matrix[0,3] * point.H;
            p.Y = matrix.Matrix[1,0] * point.X + matrix.Matrix[1,1] * point.Y + matrix.Matrix[1,2] * point.Z + matrix.Matrix[1,3] * point.H;
            p.Z = matrix.Matrix[2,0] * point.X + matrix.Matrix[2,1] * point.Y + matrix.Matrix[2,2] * point.Z + matrix.Matrix[2,3] * point.H;
            p.H = matrix.Matrix[3,0] * point.X + matrix.Matrix[3,1] * point.Y + matrix.Matrix[3,2] * point.Z + matrix.Matrix[3,3] * point.H;
            return p;
        }

        public static MatrixTransform operator *(MatrixTransform a, MatrixTransform b){
			MatrixTransform result = new MatrixTransform();

			// ��������� ������. �� ����� ����� ��������� ������ �� �������.
			// ��� ���������� ����� ������ ���������� ������ ������� ������ ������ ������� �������� �� ������ ������� ������� ������ �������.
			// ��� ������� � ������ ���������� ��������� ������������ ��������. ��. ������.
            result.Matrix[0,0] = a.Matrix[0,0] * b.Matrix[0,0] + a.Matrix[0,1] * b.Matrix[1,0] + a.Matrix[0,2] * b.Matrix[2,0] + a.Matrix[0,3] * b.Matrix[3,0];
            result.Matrix[0,1] = a.Matrix[0,0] * b.Matrix[0,1] + a.Matrix[0,1] * b.Matrix[1,1] + a.Matrix[0,2] * b.Matrix[2,1] + a.Matrix[0,3] * b.Matrix[3,1];
            result.Matrix[0,2] = a.Matrix[0,0] * b.Matrix[0,2] + a.Matrix[0,1] * b.Matrix[1,2] + a.Matrix[0,2] * b.Matrix[2,2] + a.Matrix[0,3] * b.Matrix[3,2];
            result.Matrix[0,3] = a.Matrix[0,0] * b.Matrix[0,3] + a.Matrix[0,1] * b.Matrix[1,3] + a.Matrix[0,2] * b.Matrix[2,3] + a.Matrix[0,3] * b.Matrix[3,3];

            result.Matrix[1,0] = a.Matrix[1,0] * b.Matrix[0,0] + a.Matrix[1,1] * b.Matrix[1,0] + a.Matrix[1,2] * b.Matrix[2,0] + a.Matrix[1,3] * b.Matrix[3,0];
            result.Matrix[1,1] = a.Matrix[1,0] * b.Matrix[0,1] + a.Matrix[1,1] * b.Matrix[1,1] + a.Matrix[1,2] * b.Matrix[2,1] + a.Matrix[1,3] * b.Matrix[3,1];
            result.Matrix[1,2] = a.Matrix[1,0] * b.Matrix[0,2] + a.Matrix[1,1] * b.Matrix[1,2] + a.Matrix[1,2] * b.Matrix[2,2] + a.Matrix[1,3] * b.Matrix[3,2];
            result.Matrix[1,3] = a.Matrix[1,0] * b.Matrix[0,3] + a.Matrix[1,1] * b.Matrix[1,3] + a.Matrix[1,2] * b.Matrix[2,3] + a.Matrix[1,3] * b.Matrix[3,3];

            result.Matrix[2,0] = a.Matrix[2,0] * b.Matrix[0,0] + a.Matrix[2,1] * b.Matrix[1,0] + a.Matrix[2,2] * b.Matrix[2,0] + a.Matrix[2,3] * b.Matrix[3,0];
            result.Matrix[2,1] = a.Matrix[2,0] * b.Matrix[0,1] + a.Matrix[2,1] * b.Matrix[1,1] + a.Matrix[2,2] * b.Matrix[2,1] + a.Matrix[2,3] * b.Matrix[3,1];
            result.Matrix[2,2] = a.Matrix[2,0] * b.Matrix[0,2] + a.Matrix[2,1] * b.Matrix[1,2] + a.Matrix[2,2] * b.Matrix[2,2] + a.Matrix[2,3] * b.Matrix[3,2];
            result.Matrix[2,3] = a.Matrix[2,0] * b.Matrix[0,3] + a.Matrix[2,1] * b.Matrix[1,3] + a.Matrix[2,2] * b.Matrix[2,3] + a.Matrix[2,3] * b.Matrix[3,3];

            result.Matrix[3,0] = a.Matrix[3,0] * b.Matrix[0,0] + a.Matrix[3,1] * b.Matrix[1,0] + a.Matrix[3,2] * b.Matrix[2,0] + a.Matrix[3,3] * b.Matrix[3,0];
            result.Matrix[3,1] = a.Matrix[3,0] * b.Matrix[0,1] + a.Matrix[3,1] * b.Matrix[1,1] + a.Matrix[3,2] * b.Matrix[2,1] + a.Matrix[3,3] * b.Matrix[3,1];
            result.Matrix[3,2] = a.Matrix[3,0] * b.Matrix[0,2] + a.Matrix[3,1] * b.Matrix[1,2] + a.Matrix[3,2] * b.Matrix[2,2] + a.Matrix[3,3] * b.Matrix[3,2];
            result.Matrix[3,3] = a.Matrix[3,0] * b.Matrix[0,3] + a.Matrix[3,1] * b.Matrix[1,3] + a.Matrix[3,2] * b.Matrix[2,3] + a.Matrix[3,3] * b.Matrix[3,3];

            return result;
        }
    }
}
