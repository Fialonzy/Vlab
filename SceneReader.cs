using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GeometricModeling{
    class SceneReader
    {
		private Point[] _points;
		private Connection[] _connections;

		public void ReadFile(string path, out List<Point> points, out List<Connection> connections)
		{
			CheckFile(path);
			List<string> lines = Read(path);
			ProcessReading(lines);

			points = _points.ToList();
			connections = _connections.ToList();
		}

		private void ProcessReading(List<string> lines)
		{
			int countPoint, countLine;

			if (!int.TryParse(lines[0], out countPoint)) throw new Exception($"Не удалось преобразовать в число данные о количестве точек ({lines[0]})");
			if (countPoint < 1) throw new Exception("Количество точек меньше одной.");
			if (lines.Count < countPoint + 1) throw new Exception($"Количество строк меньше количества точек. (Строк - {lines.Count}, Количество точек - {countPoint})");

			GetPoint(lines.GetRange(1, countPoint));

			//if (lines[countPoint + 1].Contains("1_"))
			//{
			//	GetConnections(lines.GetRange(1 + countPoint + 1, 4));
			//	return;
			//}

			if (!int.TryParse(lines[countPoint + 1], out countLine)) return;
			if (countLine < 1) return;
			if (lines.Count < countPoint + countLine + 2) throw new Exception("Количество строк меньше количества связей.");

			GetConnections(lines.GetRange(1 + countPoint + 1, countLine));
		}

		private void GetConnections(List<string> lines)
		{
			int lineCount = lines.Count;
			_connections = new Connection[lineCount];

			for (int i = 0; i < lineCount; i++)
			{
				_connections[i] = ReadConnection(lines[i]);
			}
		}

		private Connection ReadConnection(string str)
		{
			var indexs = str.Trim().Split(' ');
			List<int> connect = new List<int>();
			foreach (var item in indexs)
			{
				int i;
				if (!int.TryParse(item, out i)) throw new Exception($"Не удаётся преобразовать число связи. ({item})");
				if (i >= _points.Length) throw new Exception($"Принятый индекс точки находится вне диапазона массивов точек." +
					$"\nТочек = {_points.Length}; Индекс = {i}");
				connect.Add(i);
			}
			return new Connection(connect.ToArray());
		}

		private void GetPoint(List<string> lines)
		{
			int pointCount = lines.Count;
			_points = new Point[pointCount];

			for (int i = 0; i < pointCount; i++)
			{
				_points[i] = ReadPoint(lines[i]);
			}
		}

		private void CheckFile(string path)
		{
			string catalog = Path.GetDirectoryName(path);
			string file = Path.GetFileName(path);
			if (!Directory.Exists(catalog)) throw new Exception($"Указанный каталог не найден, {catalog}");
			if (!File.Exists(path)) throw new Exception($"Указанный файл не найден, {file}");
		}

		private List<string> Read(string path)
		{
			string body = string.Empty;
			using (var f = new FileStream(path, FileMode.Open))
			using (var s = new StreamReader(f))
			{
				body = s.ReadToEnd();
			}

			return body.Split('\n').ToList();
		}

		private Point ReadPoint(string str){
            var coords =  str.Trim().Split(' ');
			if (coords.Length != 4) throw new Exception("Неверные данные");
            Point p = new Point();
			try
			{
				p.X = Convert.ToDouble(coords[0], new CultureInfo("en"));
				p.Y = Convert.ToDouble(coords[1], new CultureInfo("en"));
				p.Z = Convert.ToDouble(coords[2], new CultureInfo("en"));
				p.H = Convert.ToDouble(coords[3], new CultureInfo("en"));
			}
			catch (Exception ex)
			{
				throw new Exception("Не удалось преобразовать данные, см. внутреннее исключение.", ex);
			}
			return p;
        }
	}
}
