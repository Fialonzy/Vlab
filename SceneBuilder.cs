using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricModeling
{
	class SceneBuilder
	{
		private readonly SceneReader _reader;
		private SceneController _sceneController;

		public SceneBuilder(SceneReader reader)
		{
			_reader = reader;
			//_sceneController = new SceneController();
		}

		public void ReadSceneFromFile(string path)
		{
			List<Point> points;
			List<Connection> connections;
			_reader.ReadFile(path, out points, out connections);
			_sceneController = new SceneController(points.ToArray(), connections.ToArray());
			//_sceneController.Points = points;
			//_sceneController.Connections = connections;
		}

		public void SetSizeScene(Size size)
		{
			_sceneController.Size = size;
		}

		public IScene Build() => _sceneController;
	}
}
