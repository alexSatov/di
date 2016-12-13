using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App.Actions
{
	public class KochFractalAction : IUiAction
	{
	    private readonly KochPainter kochPainter;

        public KochFractalAction(KochPainter painter)
	    {
	        kochPainter = painter;
	    }

	    public string Category => "Фракталы";
		public string Name => "Кривая Коха";
		public string Description => "Кривая Коха";

		public void Perform()
		{
			kochPainter.Paint();
		}
	}
}