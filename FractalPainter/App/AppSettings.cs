using FractalPainting.Infrastructure;

namespace FractalPainting.App
{
	public class AppSettings: IImageDirectoryProvider
	{
		public string ImagesDirectory => ".";
	    public ImageSettings ImageSettings { get; set; }
	}
}

