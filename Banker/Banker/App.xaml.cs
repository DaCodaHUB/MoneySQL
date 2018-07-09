using System.Windows;
using ShowMeTheXAML;

namespace Banker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            XamlDisplay.Init();
            base.OnStartup(e);
        }
    }
}
