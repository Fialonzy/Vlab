using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricModeling
{
	/// <summary>
	/// Контроллер сцены.null Он хранит в себе неизменные точки и матрицы афинных преобразований. 
	/// По вызову метода <see cref="Render(ref PictureBox)"/> отрисовывает все точки и линии по связям.
	/// Реализует интерфейс <see cref="IScene"/>, который в свою очередь предоставляет интерфейсы афинных преобразований.
	/// </summary>
	class SceneController :IScene
	{
		MatrixTransform _movingMatrix = new MatrixTransform();
		MatrixTransform _scaleMatrix = new MatrixTransform();
		MatrixTransform _oppMatrix = new MatrixTransform();
		MatrixTransform _shearMatrix = new MatrixTransform();
		MatrixTransform _rotateMatrix = new MatrixTransform();
		MatrixTransform _sceneAdjustment = new MatrixTransform();
		Size _size;
        int _approximation;

        public List<Point> Points { get; set; }
		public List<Connection> Connections { get; set; }

		/// <summary>
		/// Размер отрисовываемой сцены. Необходим для переноса абсолютных координат к координатам окна представления.
		/// Т.е. центрирует начало координат не в левом верхнем углу, а по центру представления.
		/// </summary>
		public Size Size
		{
			get => _size; set
			{
				_size = value;
				// Делаем матрицу отражения относительно оси ОХ
				_sceneAdjustment = new MatrixTransform();
				_sceneAdjustment.Matrix[1, 1] = -1;
				_sceneAdjustment.Matrix[2, 2] = -1;
				// Переносим все координаты на растояния половины представления
				_sceneAdjustment *= MatrixTransform.GetTranslation(new Point() { X = value.Width / 2, Y = value.Height / 2, H = 1.0 });
			}
		}


		public SceneController()
		{
			_sceneAdjustment = new MatrixTransform();
			_sceneAdjustment.Matrix[1, 1] = -1;
			_sceneAdjustment.Matrix[2, 2] = -1;
			_approximation = 4;
		}

		public void Move(Point vector)
		{
			_movingMatrix *= MatrixTransform.GetTranslation(vector);
		}

		public void MoveX(double distantion)
		{
			_movingMatrix *= MatrixTransform.GetTranslation(new Point() { X = distantion, H = 1.0 });

		}

		public void MoveY(double distantion)
		{
			_movingMatrix *= MatrixTransform.GetTranslation(new Point() { Y = distantion, H = 1.0 });  
		}

		public void MoveZ(double distantion)
		{
			_movingMatrix *= MatrixTransform.GetTranslation(new Point() { Z = distantion, H = 1.0 });
		}

		public void OppX(double focus)
		{
			_oppMatrix *= MatrixTransform.GetOppX(focus);
		}

		public void OppY(double focus)
		{
			_oppMatrix *= MatrixTransform.GetOppY(focus);
		}

		public void OppZ(double focus)
		{
			_oppMatrix *= MatrixTransform.GetOppZ(focus);
		}

        /// <summary>
        /// Сбрасывает все преобразования до единичных матриц.
        /// </summary>
		public void Default()
		{
			_movingMatrix = new MatrixTransform();
			_oppMatrix = new MatrixTransform();
			_rotateMatrix = new MatrixTransform();
			_scaleMatrix = new MatrixTransform();
			_shearMatrix = new MatrixTransform();
		}

		/// <summary>
		/// Метод для вычисления точек сцены. Он обрабатывает линии связи, если это кривая, то добавляет точек.
		/// Преобразует точки с учётом сцены. 
		/// </summary>
		/// <returns>Возвращает готовый к визуализации массив точек и их связей.</returns>
		private (List<Point>, List<Connection>) CalculateScene()
		{
			List<Point> points = new List<Point>();
			List<Connection> lines = new List<Connection>();

			foreach (var connect in Connections)
			{
				int count = connect.Connections.Count;
				// Если мы получили 4 точки соединения, то будем считать, что это кривая
				if (count == 4)
				{
					// Если количество отрезков задано больше ноля, то считаем. 
					// При 0 мы получаем исключение, деление на ноль.
					if (_approximation > 0)
					{
						// Находим точки, на которых основывается кривая
						Point[] localpoints = { Points[connect.Connections[0]], Points[connect.Connections[1]], Points[connect.Connections[2]], Points[connect.Connections[3]] };
						List<Point> resultLine = new List<Point>();
						for (int i = 0; i <= _approximation; i++)
						{
							// Считаем координаты точки по уравнению из методички

							double x = 0, y = 0, z = 0, h = 0;
							double t = (double)i / _approximation;

							x =   localpoints[0].X * ((1 - t) * (1 - t))
								+ localpoints[1].X * (2 * t * ((1 - t) * (1 - t)))
								+ localpoints[2].X * (2 * (t * t) * (1 - t))
								+ localpoints[3].X * (t * t);

							y =   localpoints[0].Y * ((1 - t) * (1 - t))
								+ localpoints[1].Y * (2 * t * ((1 - t) * (1 - t)))
								+ localpoints[2].Y * (2 * (t * t) * (1 - t))
								+ localpoints[3].Y * (t * t);	 

							z =   localpoints[0].Z * ((1 - t) * (1 - t))
								+ localpoints[1].Z * (2 * t * ((1 - t) * (1 - t)))
								+ localpoints[2].Z * (2 * (t * t) * (1 - t))
								+ localpoints[3].Z * (t * t);

							h =   localpoints[0].H * ((1 - t) * (1 - t))
								+ localpoints[1].H * (2 * t * ((1 - t) * (1 - t)))
								+ localpoints[2].H * (2 * (t * t) * (1 - t))
								+ localpoints[3].H * (t * t);

							resultLine.Add(new Point(x,y,z,h));
						}
						for (int i = 0; i < resultLine.Count - 1; i++)
						{
							// Добавляем получившиеся точки в результирующий массив точек
							points.Add(resultLine[i]);
							points.Add(resultLine[i + 1]);
							// Добавляем связь между точками
							lines.Add(new Connection(points.Count - 2, points.Count - 1));
						}
					}
				}
				else
					// В том случае, когда у нас не кривая, мы просто перебираем связи и строим линию
					for (int i = 0; i < count - 1; i++)
					{
						int startPoint = connect.Connections[i];
						int endPoint = connect.Connections[i + 1];
						points.Add(Points[startPoint]);
						points.Add(Points[endPoint]);
						lines.Add(new Connection(points.Count - 2, points.Count - 1));
					}
			}
			// Наконец получив все точки мы применяем к ним преобразования сцены и возвращаем
			for (int i = 0; i < points.Count; i++)
			{
				points[i] = points[i] * _scaleMatrix * _rotateMatrix * _movingMatrix * _shearMatrix * _oppMatrix * _sceneAdjustment;
			}
			return (points, lines);
		}

		public void Render(ref Graphics plane)
		{

			// Закрасить фон

			plane.Clear(Color.White);
			plane.DrawRectangle(new Pen(new SolidBrush(Color.Red), 1), 0F, 0F, _size.Width - 1, _size.Height - 1);

			// Вычислить все точки сцены

			var scene = CalculateScene();

			// Нарисовать все точки
			foreach (var point in scene.Item1)
			{
				plane.FillEllipse(new SolidBrush(Color.DarkRed), (float)(point.X - 1), (float)point.Y - 1, 2, 2);
			}

			// Нарисовать все линии
			foreach (var line in scene.Item2)
			{
				Point p1 = scene.Item1[line.Connections.First()], p2 = scene.Item1[line.Connections.Last()];
				plane.DrawLine(new Pen(new SolidBrush(Color.Black), 1), (float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y);
			}

			

            // Нарисовать осевые линии

			Point o = new Point();
			Point x = new Point(x:20);
			Point y = new Point(y:20);
			Point z = new Point(z:20);

			var tempPoint1 = o * _scaleMatrix * _rotateMatrix * _movingMatrix * _shearMatrix * _oppMatrix * _sceneAdjustment;
			var tempPoint2 = x * _scaleMatrix * _rotateMatrix * _movingMatrix * _shearMatrix * _oppMatrix * _sceneAdjustment;

			plane.DrawLine(new Pen(new SolidBrush(Color.Green), 1), (float)tempPoint1.X, (float)tempPoint1.Y,
				(float)tempPoint2.X, (float)tempPoint2.Y);

			tempPoint2 = y * _scaleMatrix * _rotateMatrix * _movingMatrix * _shearMatrix * _oppMatrix * _sceneAdjustment;

			plane.DrawLine(new Pen(new SolidBrush(Color.Red), 1), (float)tempPoint1.X, (float)tempPoint1.Y,
				(float)tempPoint2.X, (float)tempPoint2.Y);

			tempPoint2 = z * _scaleMatrix * _rotateMatrix * _movingMatrix * _shearMatrix * _oppMatrix * _sceneAdjustment;

			plane.DrawLine(new Pen(new SolidBrush(Color.Blue), 1), (float)tempPoint1.X, (float)tempPoint1.Y,
				(float)tempPoint2.X, (float)tempPoint2.Y);
		}

		/// <summary>
		/// Метод для принятия нового значения апроксимации кривых(1/N, где N -- количество отрезков кривой). 
		/// </summary>
		/// <param name="count">Количество отрезков кривой</param>
		public void Approximation(int count){
			_approximation = count;
		}

		public void Render(ref PictureBox picture)
		{
			if (picture.Size != Size) Size = picture.Size;

			Bitmap bmp = new Bitmap(Size.Width, Size.Height);
			Graphics plane = Graphics.FromImage(bmp);
			Render(ref plane);
			picture.Image = bmp;
		}

		public void RotateX(double angle)
		{
			_rotateMatrix *= MatrixTransform.GetRotateX(angle);
		}

		public void RotateY(double angle)
		{
			_rotateMatrix *= MatrixTransform.GetRotateY(angle);
		}

		public void RotateZ(double angle)
		{
			_rotateMatrix *= MatrixTransform.GetRotateZ(angle);
		}

		public void Scale(double scale)
		{
			_scaleMatrix *= MatrixTransform.GetScaleX(scale) * MatrixTransform.GetScaleY(scale) * MatrixTransform.GetScaleZ(scale);
		}

		public void ScaleX(double scale)
		{
			_scaleMatrix *= MatrixTransform.GetScaleX(scale);
		}

		public void ScaleY(double scale)
		{
			_scaleMatrix *= MatrixTransform.GetScaleY(scale);
		}

		public void ScaleZ(double scale)
		{
			_scaleMatrix *= MatrixTransform.GetScaleZ(scale);
		}

		public void ShearXToY(double value)
		{
			_shearMatrix *= MatrixTransform.GetShearXToY(value);
		}

		public void ShearXToZ(double value)
		{
			_shearMatrix *= MatrixTransform.GetShearXToZ(value);
		}

		public void ShearYToX(double value)
		{
			_shearMatrix *= MatrixTransform.GetShearYToX(value);
		}

		public void ShearYToZ(double value)
		{
			_shearMatrix *= MatrixTransform.GetShearYToZ(value);
		}

		public void ShearZToX(double value)
		{
			_shearMatrix *= MatrixTransform.GetShearZToX(value);
		}

		public void ShearZToY(double value)
		{
			_shearMatrix *= MatrixTransform.GetShearZToY(value);
		}

        /// <summary>
        /// Метод вписывания в представление
        /// </summary>
		public void Enter()
		{
			/*
			 * Можно реализовать это немного иначе:
			 * Сначала переместить сцену в центр, а потом масштабировать.
			 * Но данный вариант тоже пригодный.
			 */

			// Вычислить габариты сцены(ширину, высоту и положение верхней левой точки)
			RectangleF scene = GetScene();
			// Смасштабировать с учётом сцены
			Scaling(scene);
			// Просчитать новые размеры сцены
			scene = GetScene();
			// Передвинуть сцену в начало координат
			Moving(scene);
		}

		private void Moving(RectangleF scene)
		{
			// Находим центр сцены
			Point center = new Point((scene.Width / 2 + scene.X), (-scene.Height / 2 + scene.Y));
			// Перемещаем на растояние Дельта от центра сцены до центра окна
			_movingMatrix *= MatrixTransform.GetTranslation(new Point() { X = Size.Width/2-center.X, Y = -Size.Height / 2+center.Y, H = 1.0 });
		}

		private void Scaling(RectangleF scene)
		{
			// Узнаём во сколько раз ширина сцены больше или меньше, чем ширина окна
			double scaleX = (Size.Width - 10) / scene.Width;
			// Узнаём во сколько раз высота сцены больше или меньше, чем высота окна
			double scaleY = (Size.Height - 10) / scene.Height;
			// Берём за основу масштаба ту ось, которая требует меньшего масштаба для вписания
			double scale = scaleX < scaleY ? scaleX : scaleY;
			// Масштабируем
			_scaleMatrix *= MatrixTransform.GetScaleX(scale) * MatrixTransform.GetScaleY(scale) * MatrixTransform.GetScaleZ(scale);
		}

		private RectangleF GetScene()
		{
			// Посчитать точки сцены
			var scene = CalculateScene();
			List<Point> points = new List<Point>(scene.Item1);

			// Найти максимумы и минимумы по осям
			double maxX = double.MinValue, maxY = double.MinValue,
				minX = double.MaxValue, minY = double.MaxValue;
			foreach (var point in points)
			{
				maxX = point.X >= maxX ? point.X : maxX;
				maxY = point.Y >= maxY ? point.Y : maxY;

				minX = point.X <= minX ? point.X : minX;
				minY = point.Y <= minY ? point.Y : minY;
			}
			// Вернуть прямоугольник, который составлен из максимумов и минимумов
			return new RectangleF((float)minX, (float)maxY, (float)(maxX - minX), (float)(maxY - minY));
		}
	}
}
