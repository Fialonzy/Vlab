namespace GeometricModeling
{
	internal interface IShear
	{

		void ShearXToY(double value);

		void ShearXToZ(double value);

		void ShearYToX(double value);

		void ShearYToZ(double value);

		void ShearZToX(double value);

		void ShearZToY(double value);
	}
}