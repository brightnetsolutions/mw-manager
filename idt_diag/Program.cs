using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace idt_diag
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("app_set.ini");

            Boolean start_first = Boolean.Parse(data["APP"]["RUN_FIRST"]);

            System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcessesByName("mw_mgr");

            if(proc.Length > 1)
            {
                MessageBox.Show("Only one instance of MW Manager can start!", "Error");
                return;
            }

            if (start_first == false)
            {
                Application.Run(new frm_clt());
            }
            else
            {
                Application.Run(new frm_slct());
            }
        }
    }
}
