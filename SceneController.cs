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
				// Переносим все координаты на растояния половины представления
				_sceneAdjustment *= MatrixTransform.GetTranslation(new Point() { X = value.Width / 2, Y = value.Height / 2, H = 1.0 });
			}
		}


		public SceneController()
		{
			_sceneAdjustment = new MatrixTransform();
			_sceneAdjustment.Matrix[1, 1] = -1;
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

		public void Render(ref Graphics plane)
		{

			// Закрасить фон

			plane.Clear(Color.White);
			plane.DrawRectangle(new Pen(new SolidBrush(Color.Red), 1), 0F, 0F, _size.Width - 1, _size.Height - 1);

			// Отрисовать все точки и линии в соответствии с изменениями.

			foreach (var point in Points)
			{
				Point p = point  * _scaleMatrix;
				p *= _rotateMatrix;
				p *= _movingMatrix;
				p *= _shearMatrix;
				p *= _oppMatrix;
				p *= _sceneAdjustment;
				plane.FillEllipse(new SolidBrush(Color.DarkRed), (float)(p.X - 1), (float)p.Y - 1, 2, 2);
			}

			foreach (var connect in Connections)
			{
				int count = connect.Connections.Count;
				for (int i = 0; i < count - 1; i++)
				{
					int startPoint = connect.Connections[i];
					int endPoint = connect.Connections[i + 1];
					Point p1 = Points[startPoint]  * _scaleMatrix * _rotateMatrix * _movingMatrix * _shearMatrix * _oppMatrix * _sceneAdjustment ,
						  p2 = Points[endPoint]  * _scaleMatrix * _rotateMatrix * _movingMatrix * _shearMatrix * _oppMatrix * _sceneAdjustment;
					plane.DrawLine(new Pen(new SolidBrush(Color.Black), 1), (float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y);
				}
			}

            // Нарисовать осевые линии

			Point o = new Point() { H = 1 };
			Point x = new Point() { H = 1, X = 20 };
			Point y = new Point() { H = 1, Y = 20 };
			Point z = new Point() { H = 1, Z = 20 };

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
			_scaleMatrix = MatrixTransform.GetScaleX(scale) * MatrixTransform.GetScaleY(scale) * MatrixTransform.GetScaleZ(scale);
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
			bool isFull = false;
			// Пока сцена не вписана повторяем перенос и масштабирование
			// Такой алгоритм нужен для того, чтобы при ОПП корректно вписывать. 
			// Так как при перемещении и маштабировании по отдельности размеры сцены могут меняться.
			do
			{
				RectangleF scene = GetScene();
				Scaling(scene);
				scene = GetScene();
				Moving(scene);
				scene = GetScene();
                /* Достаточно сложное условие получилось
				 * Тут главное что ширина и высота получившейся сцены должны быть меньше размеров представления на 10 пикселей
				 * Но чтобы как минимум одна из размерностей была больше размерности представления при прибавлении 20 пикселей
				 * Иными словами: недостаточно уменьшить изображение чтобы оно вписалось, нужно ещё его приблизить, если оно маленькое.
				 * Затем смотрю центр сцены, если он находится в начале координат, то ок. Иначе продолжаем преобразовывать
				 */
				if (scene.Width + 10 <= Size.Width && scene.Height + 10 <= Size.Height && 
					(scene.Width + 20 >= Size.Width || scene.Height + 20 >= Size.Height) && 
					Math.Round(scene.Width + scene.X) == Math.Round(scene.Width /2) && Math.Round(scene.Height - scene.Y) == Math.Round(scene.Height / 2))
					isFull = true;
			}
			while (!isFull);


			
		}

		private void Moving(RectangleF scene)
		{
			PointF center = new PointF((float)(scene.Width / 2 + scene.X), (float)(-scene.Height / 2 + scene.Y));
			_movingMatrix *= MatrixTransform.GetTranslation(new Point() { X = -center.X, Y = -center.Y, H = 1.0 });
		}

		private void Scaling(RectangleF scene)
		{
			double scaleX = (Size.Width - 10) / scene.Width;
			double scaleY = (Size.Height - 10) / scene.Height;
			double scale = scaleX < scaleY ? scaleX : scaleY;
			_scaleMatrix *= MatrixTransform.GetScaleX(scale) * MatrixTransform.GetScaleY(scale) * MatrixTransform.GetScaleZ(scale);
		}

		private RectangleF GetScene()
		{
			List<Point> points = new List<Point>();

			foreach (var point in Points)
			{
				Point p = point * _scaleMatrix;
				p *= _rotateMatrix;
				p *= _movingMatrix;
				p *= _shearMatrix;
				p *= _oppMatrix;
				points.Add(p);
			}

			double maxX = double.MinValue, maxY = double.MinValue,
				minX = double.MaxValue, minY = double.MaxValue;
			foreach (var point in points)
			{
				maxX = point.X >= maxX ? point.X : maxX;
				maxY = point.Y >= maxY ? point.Y : maxY;

				minX = point.X <= minX ? point.X : minX;
				minY = point.Y <= minY ? point.Y : minY;
			}
			return new RectangleF((float)minX, (float)maxY, (float)(maxX - minX), (float)(maxY - minY));
		}
	}
}
