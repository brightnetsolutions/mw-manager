using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace idt_diag
{
    public partial class frm_slct : Form
    {
        public frm_slct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string select = cmb_mode.SelectedItem.ToString();

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("app_set.ini");

            
            
            if (select == "Server Mode")
            {
                data["APP"]["MANAGE_MODE"] = "svr";
            }
            else
            {
                data["APP"]["MANAGE_MODE"] = "clt";
            }

            data["APP"]["RUN_FIRST"] = "false";
            parser.WriteFile("app_set.ini", data);

            frm_clt main_form = new frm_clt();
            main_form.Show();
            this.Hide();
        }
    }
}
