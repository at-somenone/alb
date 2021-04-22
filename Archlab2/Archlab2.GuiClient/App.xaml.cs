using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Archlab2.GuiClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        [DllImport("Kernel32.dll")]
        private static extern bool AttachConsole(int processId);

        protected override void OnStartup(StartupEventArgs e) {
            AttachConsole(-1);
            Console.WriteLine("Start");
            base.OnStartup(e);
            //Console.WriteLine("Stop");
        }
    }
}
