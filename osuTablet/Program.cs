using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace osuTablet
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var resolution = Screen.PrimaryScreen.Bounds;
            InputAssistant.SetBoundingBox(resolution.Location, resolution.Size);

            var cu = new CommunicationUdp();
            cu.Open(5000);

            Application.Run(new MainForm());

            cu.Close();
        }

    }
}
