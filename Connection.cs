using System;
using System.Collections.Generic;

namespace GeometricModeling
{
	class Connection
	{
		List<int> _connections;

		public Connection()
		{
			_connections = new List<int>();
		}

		public Connection(params int[] connect)
		{
			if (connect.Length < 2) throw new Exception("Для связи необходимо указать как минимум 2 точки");
			_connections = new List<int>(connect);
		}

		public IReadOnlyList<int> Connections { get => _connections; }
	}
}