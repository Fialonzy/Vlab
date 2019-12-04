using System.Drawing;

namespace GeometricModeling{
    class Line:IObjectScene
    {
        public Point Point1 { get; }
        public Point Point2 { get; }

		public Line(Point firstPoint, Point secondPoint)
		{
			Point1 = firstPoint ?? throw new System.ArgumentNullException(nameof(firstPoint));
			Point2 = secondPoint ?? throw new System.ArgumentNullException(nameof(secondPoint));
		}

		public void Draw(MatrixTransform matrix, Graphics plane)
		{
			var p1 = Point1 * matrix;
			var p2 = Point2 * matrix;
			plane.DrawLine(new Pen(new SolidBrush(Color.Black), 1), (float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y);
			plane.FillEllipse(new SolidBrush(Color.DarkRed), (float)p1.X - 1, (float)p1.Y - 1, 2, 2);
			plane.FillEllipse(new SolidBrush(Color.DarkRed), (float)p2.X - 1, (float)p2.Y - 1, 2, 2);
		}

		public string GetDescription()
		{
			return $"Линия ({Point1.X}, {Point1.Y}, {Point1.Z}, {Point1.H}) -> ({Point2.X}, {Point2.Y}, {Point2.Z}, {Point2.H})";
		}
	}
}
