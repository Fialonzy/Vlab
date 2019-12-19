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
		private readonly Point[] points;
		private List<Point> debugPoints;
		private readonly Connection[] connections;

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


		public SceneController(Point[] points, Connection[] connections)
		{
			_sceneAdjustment = new MatrixTransform();
			_sceneAdjustment.Matrix[1, 1] = -1;
			_sceneAdjustment.Matrix[2, 2] = -1;
			_approximation = 4;
			this.points = points;
			debugPoints = new List<Point>(points);
			this.connections = connections;
			RecalculateScene();
		}

		public void Move(Point vector)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetTranslation(vector);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetTranslation(vector);
			}
			//_movingMatrix *= MatrixTransform.GetTranslation(vector);
		}

		public void MoveX(double distantion)
		{

			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetTranslation(new Point() { X = distantion, H = 1.0 });
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetTranslation(new Point() { X = distantion, H = 1.0 });
			}
			//_movingMatrix *= MatrixTransform.GetTranslation(new Point() { X = distantion, H = 1.0 });

		}

		public void MoveY(double distantion)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetTranslation(new Point() { Y = distantion, H = 1.0 });
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetTranslation(new Point() { Y = distantion, H = 1.0 });
			}
			//_movingMatrix *= MatrixTransform.GetTranslation(new Point() { Y = distantion, H = 1.0 });  
		}

		public void MoveZ(double distantion)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetTranslation(new Point() { Z = distantion, H = 1.0 });
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetTranslation(new Point() { Z = distantion, H = 1.0 });
			}
			//_movingMatrix *= MatrixTransform.GetTranslation(new Point() { Z = distantion, H = 1.0 });
		}

		public void OppX(double focus)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetOppX(focus);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetOppX(focus);
			}
			//_oppMatrix *= MatrixTransform.GetOppX(focus);
		}

		public void OppY(double focus)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetOppY(focus);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetOppY(focus);
			}
			//_oppMatrix *= MatrixTransform.GetOppY(focus);
		}

		public void OppZ(double focus)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetOppZ(focus);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetOppZ(focus);
			}
			//_oppMatrix *= MatrixTransform.GetOppZ(focus);
		}

        /// <summary>
        /// Сбрасывает все преобразования до единичных матриц.
        /// </summary>
		public void Default()
		{
			RecalculateScene();
			debugPoints = new List<Point>(points);
		}

		private static double g0(double t) => (1 - t) * (1 - t);
		private static double g1(double t) => (1 - t) * (1 - t) * 2 * t;
		private static double g2(double t) => (1 - t) * 2 * t * t;
		private static double g3(double t) => t * t;

		public void RecalculateScene()
		{
			List<Point> points = new List<Point>();
			List<Connection> lines = new List<Connection>();
			// Исключительный случай и это поверхность образованная кривыми
			//if (connections.Length == 4 && connections[0].Connections.Count == 4 && connections[1].Connections.Count == 4 && connections[2].Connections.Count == 4 && connections[3].Connections.Count == 4)
			//{
				 
			//}
			//else
				foreach (var connect in connections)
				{
					int count = connect.Connections.Count;

					if(count == 16)
					{
						if (_approximation > 0)
						{
							Point[,] localpoints =
							{
						{ this.points[connect.Connections[0]], this.points[connect.Connections[1]], this.points[connect.Connections[2]], this.points[connect.Connections[3]] },
						{ this.points[connect.Connections[4]], this.points[connect.Connections[5]], this.points[connect.Connections[6]], this.points[connect.Connections[7]] },
						{ this.points[connect.Connections[8]], this.points[connect.Connections[9]], this.points[connect.Connections[10]], this.points[connect.Connections[11]] },
						{ this.points[connect.Connections[12]], this.points[connect.Connections[13]], this.points[connect.Connections[14]], this.points[connect.Connections[15]] },

						//{ this.points[connections[0].Connections[0]], this.points[connections[1].Connections[0]], this.points[connections[2].Connections[0]], this.points[connections[3].Connections[0]] },
						//{ this.points[connections[0].Connections[1]], this.points[connections[1].Connections[1]], this.points[connections[2].Connections[1]], this.points[connections[3].Connections[1]] },
						//{ this.points[connections[0].Connections[2]], this.points[connections[1].Connections[2]], this.points[connections[2].Connections[2]], this.points[connections[3].Connections[2]] },
						//{ this.points[connections[0].Connections[3]], this.points[connections[1].Connections[3]], this.points[connections[2].Connections[3]], this.points[connections[3].Connections[3]] },
					};
							List<Point> resultLine = new List<Point>();
							for (int i = 0; i <= _approximation; i++)
							{
								double t1 = (double)i / _approximation;
								for (int j = 0; j <= _approximation; j++)
								{
									double x = 0, y = 0, z = 0, h = 1;
									double t2 = (double)j / _approximation;


									x = localpoints[0, 0].X * g0(t2) * g0(t1)
										+ localpoints[0, 1].X * g0(t2) * g1(t1)
										+ localpoints[0, 2].X * g0(t2) * g2(t1)
										+ localpoints[0, 3].X * g0(t2) * g3(t1)
										+ localpoints[1, 0].X * g1(t2) * g0(t1)
										+ localpoints[1, 1].X * g1(t2) * g1(t1)
										+ localpoints[1, 2].X * g1(t2) * g2(t1)
										+ localpoints[1, 3].X * g1(t2) * g3(t1)
										+ localpoints[2, 0].X * g2(t2) * g0(t1)
										+ localpoints[2, 1].X * g2(t2) * g1(t1)
										+ localpoints[2, 2].X * g2(t2) * g2(t1)
										+ localpoints[2, 3].X * g2(t2) * g3(t1)
										+ localpoints[3, 0].X * g3(t2) * g0(t1)
										+ localpoints[3, 1].X * g3(t2) * g1(t1)
										+ localpoints[3, 2].X * g3(t2) * g2(t1)
										+ localpoints[3, 3].X * g3(t2) * g3(t1);

									y = localpoints[0, 0].Y * g0(t2) * g0(t1)
										+ localpoints[0, 1].Y * g0(t2) * g1(t1)
										+ localpoints[0, 2].Y * g0(t2) * g2(t1)
										+ localpoints[0, 3].Y * g0(t2) * g3(t1)
										+ localpoints[1, 0].Y * g1(t2) * g0(t1)
										+ localpoints[1, 1].Y * g1(t2) * g1(t1)
										+ localpoints[1, 2].Y * g1(t2) * g2(t1)
										+ localpoints[1, 3].Y * g1(t2) * g3(t1)
										+ localpoints[2, 0].Y * g2(t2) * g0(t1)
										+ localpoints[2, 1].Y * g2(t2) * g1(t1)
										+ localpoints[2, 2].Y * g2(t2) * g2(t1)
										+ localpoints[2, 3].Y * g2(t2) * g3(t1)
										+ localpoints[3, 0].Y * g3(t2) * g0(t1)
										+ localpoints[3, 1].Y * g3(t2) * g1(t1)
										+ localpoints[3, 2].Y * g3(t2) * g2(t1)
										+ localpoints[3, 3].Y * g3(t2) * g3(t1);

									z = localpoints[0, 0].Z * g0(t2) * g0(t1)
										+ localpoints[0, 1].Z * g0(t2) * g1(t1)
										+ localpoints[0, 2].Z * g0(t2) * g2(t1)
										+ localpoints[0, 3].Z * g0(t2) * g3(t1)
										+ localpoints[1, 0].Z * g1(t2) * g0(t1)
										+ localpoints[1, 1].Z * g1(t2) * g1(t1)
										+ localpoints[1, 2].Z * g1(t2) * g2(t1)
										+ localpoints[1, 3].Z * g1(t2) * g3(t1)
										+ localpoints[2, 0].Z * g2(t2) * g0(t1)
										+ localpoints[2, 1].Z * g2(t2) * g1(t1)
										+ localpoints[2, 2].Z * g2(t2) * g2(t1)
										+ localpoints[2, 3].Z * g2(t2) * g3(t1)
										+ localpoints[3, 0].Z * g3(t2) * g0(t1)
										+ localpoints[3, 1].Z * g3(t2) * g1(t1)
										+ localpoints[3, 2].Z * g3(t2) * g2(t1)
										+ localpoints[3, 3].Z * g3(t2) * g3(t1);

									h = localpoints[0, 0].H * g0(t2) * g0(t1)
										+ localpoints[0, 1].H * g0(t2) * g1(t1)
										+ localpoints[0, 2].H * g0(t2) * g2(t1)
										+ localpoints[0, 3].H * g0(t2) * g3(t1)
										+ localpoints[1, 0].H * g1(t2) * g0(t1)
										+ localpoints[1, 1].H * g1(t2) * g1(t1)
										+ localpoints[1, 2].H * g1(t2) * g2(t1)
										+ localpoints[1, 3].H * g1(t2) * g3(t1)
										+ localpoints[2, 0].H * g2(t2) * g0(t1)
										+ localpoints[2, 1].H * g2(t2) * g1(t1)
										+ localpoints[2, 2].H * g2(t2) * g2(t1)
										+ localpoints[2, 3].H * g2(t2) * g3(t1)
										+ localpoints[3, 0].H * g3(t2) * g0(t1)
										+ localpoints[3, 1].H * g3(t2) * g1(t1)
										+ localpoints[3, 2].H * g3(t2) * g2(t1)
										+ localpoints[3, 3].H * g3(t2) * g3(t1);

									resultLine.Add(new Point(x, y, z, h));
								}
							}
						//for (int i = 0; i < resultLine.Count /( _approximation + 1); i++)
						//{
						//	for (int j = 0; j < _approximation; j++)
						//	{
						//		points.Add(resultLine[i * (_approximation + 1) + j]);
						//	}

						//	//if (i + 1 + _approximation < resultLine.Count)
						//	//{
						//	//	lines.Add(new Connection(i, i + 1 + _approximation));
						//	//}
						//}
						int countAllready = points.Count;
							for (int i = 0; i < resultLine.Count; i++)
							{
								points.Add(resultLine[i]);
								if (i + 1 < resultLine.Count && (i + 1) % (_approximation + 1) != 0)
								{
									lines.Add(new Connection(i + countAllready, i + 1 + countAllready));
								}
								if (i + (1 + _approximation) < resultLine.Count)
								{
									lines.Add(new Connection(i + countAllready, i + 1 + _approximation + countAllready));
								}
							}
						}
					}
					else
					// Если мы получили 4 точки соединения, то будем считать, что это кривая
					if (count == 4)
					{
						// Если количество отрезков задано больше ноля, то считаем. 
						// При 0 мы получаем исключение, деление на ноль.
						if (_approximation > 0)
						{
							// Находим точки, на которых основывается кривая
							Point[] localpoints = { this.points[connect.Connections[0]], this.points[connect.Connections[1]], this.points[connect.Connections[2]], this.points[connect.Connections[3]] };
							List<Point> resultLine = new List<Point>();
							for (int i = 0; i <= _approximation; i++)
							{
								// Считаем координаты точки по уравнению из методички

								double x = 0, y = 0, z = 0, h = 1;
								double t = (double)i / _approximation;

								x = localpoints[0].X *   g0(t)
									+ localpoints[1].X * g1(t)
									+ localpoints[2].X * g2(t)
									+ localpoints[3].X * g3(t);

								y =   localpoints[0].Y * g0(t)
									+ localpoints[1].Y * g1(t)
									+ localpoints[2].Y * g2(t)
									+ localpoints[3].Y * g3(t);

								z =   localpoints[0].Z * g0(t)
									+ localpoints[1].Z * g1(t)
									+ localpoints[2].Z * g2(t)
									+ localpoints[3].Z * g3(t);

								h =   localpoints[0].H * g0(t)
									+ localpoints[1].H * g1(t)
									+ localpoints[2].H * g2(t)
									+ localpoints[3].H * g3(t);

								resultLine.Add(new Point(x, y, z, h));
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
							points.Add(this.points[startPoint]);
							points.Add(this.points[endPoint]);
							lines.Add(new Connection(points.Count - 2, points.Count - 1));
						}
				}
			// Наконец получив все точки мы применяем к ним преобразования сцены и возвращаем
			//for (int i = 0; i < points.Count; i++)
			//{
			//	points[i] = points[i] * _scaleMatrix * _rotateMatrix * _movingMatrix * _shearMatrix * _oppMatrix * _sceneAdjustment;
			//}
			Points = points;
			Connections = lines;
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
			// Исключительный случай и это поверхность образованная кривыми
			if(Connections.Count == 4 && Connections[0].Connections.Count == 4 && Connections[1].Connections.Count == 4 && Connections[2].Connections.Count == 4 && Connections[3].Connections.Count == 4)
			{

			}
			else
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

							double x = 0, y = 0, z = 0, h = 1;
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

			//var scene = CalculateScene();

			// Нарисовать все точки
			foreach (var point in Points)
			{
				Point p = point * _sceneAdjustment;
				plane.FillEllipse(new SolidBrush(Color.DarkRed), (float)(p.X - 2), (float)p.Y - 2, 4, 4);
			}

			// Нарисовать все линии
			foreach (var line in Connections)
			{
				Point p1 = Points[line.Connections.First()] * _sceneAdjustment, p2 = Points[line.Connections.Last()] * _sceneAdjustment ;
				plane.DrawLine(new Pen(new SolidBrush(Color.Black), 1), (float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y);
			}

#if DEBUG
			foreach (var point in debugPoints)
			{
				Point p = point * _sceneAdjustment;
				plane.FillEllipse(new SolidBrush(Color.Blue), (float)(p.X - 2), (float)p.Y - 2, 4, 4);
			}
#endif



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
			RecalculateScene();
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
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetRotateX(angle);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetRotateX(angle);
			}
			//_rotateMatrix *= MatrixTransform.GetRotateX(angle);
		}

		public void RotateY(double angle)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetRotateY(angle);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetRotateY(angle);
			}
			//_rotateMatrix *= MatrixTransform.GetRotateY(angle);
		}

		public void RotateZ(double angle)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetRotateZ(angle);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetRotateZ(angle);
			}
			//_rotateMatrix *= MatrixTransform.GetRotateZ(angle);
		}

		public void Scale(double scale)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetScaleX(scale) * MatrixTransform.GetScaleY(scale) * MatrixTransform.GetScaleZ(scale);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetScaleX(scale) * MatrixTransform.GetScaleY(scale) * MatrixTransform.GetScaleZ(scale);
			}
			//_scaleMatrix *= MatrixTransform.GetScaleX(scale) * MatrixTransform.GetScaleY(scale) * MatrixTransform.GetScaleZ(scale);
		}

		public void ScaleX(double scale)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetScaleX(scale);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetScaleX(scale);
			}
			//_scaleMatrix *= MatrixTransform.GetScaleX(scale);
		}

		public void ScaleY(double scale)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetScaleY(scale);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetScaleY(scale);
			}
			//_scaleMatrix *= MatrixTransform.GetScaleY(scale);
		}

		public void ScaleZ(double scale)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetScaleZ(scale);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetScaleZ(scale);
			}
			//_scaleMatrix *= MatrixTransform.GetScaleZ(scale);
		}

		public void ShearXToY(double value)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetShearXToY(value);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetShearXToY(value);
			}
			//_shearMatrix *= MatrixTransform.GetShearXToY(value);
		}

		public void ShearXToZ(double value)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetShearXToZ(value);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetShearXToZ(value);
			}
			//_shearMatrix *= MatrixTransform.GetShearXToZ(value);
		}

		public void ShearYToX(double value)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetShearYToX(value);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetShearYToX(value);
			}
			//_shearMatrix *= MatrixTransform.GetShearYToX(value);
		}

		public void ShearYToZ(double value)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetShearYToZ(value);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetShearYToZ(value);
			}
			//_shearMatrix *= MatrixTransform.GetShearYToZ(value);
		}

		public void ShearZToX(double value)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetShearZToX(value);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetShearZToX(value);
			}
			//_shearMatrix *= MatrixTransform.GetShearZToX(value);
		}

		public void ShearZToY(double value)
		{
			for (int i = 0; i < Points.Count; i++)
			{
				Points[i] = Points[i] * MatrixTransform.GetShearZToY(value);
			}
			for (int i = 0; i < debugPoints.Count; i++)
			{
				debugPoints[i] = debugPoints[i] * MatrixTransform.GetShearZToY(value);
			}
			//_shearMatrix *= MatrixTransform.GetShearZToY(value);
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
			Moving(scene);
			Scaling(scene);
			// Просчитать новые размеры сцены
			//scene = GetScene();
			// Передвинуть сцену в начало координат
			//RecalculateScene();
		}

		private void Moving(RectangleF scene)
		{
			// Находим центр сцены
			Point center = new Point((scene.Width / 2 + scene.X), (-scene.Height / 2 + scene.Y));
			// Перемещаем на растояние Дельта от центра сцены до центра окна
			Move(new Point() { X = - center.X, Y = -center.Y, H = 1.0 });
			//_movingMatrix *= MatrixTransform.GetTranslation(new Point() { X = Size.Width/2-center.X, Y = -Size.Height / 2+center.Y, H = 1.0 });
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
			Scale(scale);
			//_scaleMatrix *= MatrixTransform.GetScaleX(scale) * MatrixTransform.GetScaleY(scale) * MatrixTransform.GetScaleZ(scale);
		}

		private RectangleF GetScene()
		{
			// Посчитать точки сцены
			//var scene = CalculateScene();
			//List<Point> points = new List<Point>(scene.Item1);

			// Найти максимумы и минимумы по осям
			double maxX = double.MinValue, maxY = double.MinValue,
				minX = double.MaxValue, minY = double.MaxValue;
			foreach (var point in Points)
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
