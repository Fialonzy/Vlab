using System.Drawing;

namespace GeometricModeling{
    interface IObjectScene
    {
        void Draw(MatrixTransform matrix, Graphics plane);
		string GetDescription();
    }
}
