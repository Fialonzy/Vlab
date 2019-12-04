using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricModeling
{
	class ObjectScene
	{
		private List<Point> _points;
		private List<Connection> _connections;

		public List<Point> Points {
			get
			{
				List<Point> list = new List<Point>();
				foreach (var point in _points)
				{
					list.Add(point * LocaleTransform);
				}
				return list;
			}
			set => _points = value; }
		public List<Connection> Connections
		{
			get => _connections;
			set => _connections = value;
		}

		public MatrixTransform LocaleTransform { get; set; }

		public ObjectScene()
		{
			Points = new List<Point>();
			Connections = new List<Connection>();
			LocaleTransform = new MatrixTransform();
		}
	}
}
