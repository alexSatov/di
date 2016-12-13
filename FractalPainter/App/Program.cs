using System;
using System.Windows.Forms;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(GetForm());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private static Form GetForm()
        {
            var container = new StandardKernel();

            container.Bind<Palette>().ToSelf().InSingletonScope();
            container.Bind<ImageSettings>().ToSelf().InSingletonScope();
            container.Bind<DragonSettingsGenerator>().ToSelf();
            container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();

            container.Bind<IDragonPainterFactory>().ToFactory();
            container.Bind<IImageDirectoryProvider>().To<AppSettings>();

            container.Bind(c => c.FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IUiAction>()
                    .BindAllInterfaces()
            );

//            container.Bind<IUiAction>().To<DragonFractalAction>();
//            container.Bind<IUiAction>().To<ImageSettingsAction>();
//            container.Bind<IUiAction>().To<KochFractalAction>();
//            container.Bind<IUiAction>().To<PaletteSettingsAction>();
//            container.Bind<IUiAction>().To<SaveImageAction>();

            return container.Get<MainForm>();
        }
    }
}