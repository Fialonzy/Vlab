namespace GeometricModeling
{
	internal interface IMove
	{		 
		void Move(Point vector);

		void MoveX(double distantion);

		void MoveY(double distantion);

		void MoveZ(double distantion);
	}
}