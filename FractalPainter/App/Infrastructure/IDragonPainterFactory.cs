using FractalPainting.App.Fractals;

namespace FractalPainting.Infrastructure
{
    public interface IDragonPainterFactory
    {
        DragonPainter CreateDragonPainter(DragonSettings settings);
    }
}
