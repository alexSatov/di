using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction
	{
	    private readonly IDragonPainterFactory dragonPainterFactory;
	    private static DragonSettingsGenerator dragonSettingsGenerator;
	    public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

	    public DragonFractalAction(IDragonPainterFactory painterFactory, DragonSettingsGenerator settingsGenerator)
	    {
            dragonPainterFactory = painterFactory;
	        dragonSettingsGenerator = settingsGenerator;
	    }

		public void Perform()
		{
			var dragonSettings = CreateRandomSettings();
			// редактируем настройки:
			SettingsForm.For(dragonSettings).ShowDialog();
			// создаём painter с такими настройками
		    var dragonPainter = dragonPainterFactory.CreateDragonPainter(dragonSettings);
            dragonPainter.Paint();
		}

		private static DragonSettings CreateRandomSettings()
		{
			return dragonSettingsGenerator.Generate();
		}
	}
}