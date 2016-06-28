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
    public partial class settings : Form
    {
        FileIniDataParser parser = new FileIniDataParser();
        IniData data;
        global global_class;

        public settings()
        {
            InitializeComponent();
            global_class = new global();
            var parser = new FileIniDataParser();
            data = parser.ReadFile("app_set.ini");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data["NETWORK"]["MULTI_IP"] = txt_add.Text;
            data["MW"]["CSV_LOC"] = txt_csvDir.Text;
            data["NETWORK"]["PASSWORD"] = global_class.EncryptPass(txt_netPass.Text);
            data["NETWORK"]["USER"] = txt_netUser.Text;
            data["NETWORK"]["MULTI_PORT"] = txt_port.Text;
            data["MW"]["RD_LOC"] = txt_rdDir.Text;
            data["MW"]["ROLLBACK"] = txt_rollBackDir.Text;
            data["MW"]["UPDATE"] = txt_updateDir.Text;

            parser.WriteFile("app_set.ini", data);

            this.Close();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            txt_add.Text = data["NETWORK"]["MULTI_IP"];
            txt_csvDir.Text = data["MW"]["CSV_LOC"];
            txt_netPass.Text = global_class.DycryptPass(data["NETWORK"]["PASSWORD"]);
            txt_netUser.Text = data["NETWORK"]["USER"];
            txt_port.Text = data["NETWORK"]["MULTI_PORT"];
            txt_rdDir.Text = data["MW"]["RD_LOC"];
            txt_rollBackDir.Text = data["MW"]["ROLLBACK"];
            txt_updateDir.Text = data["MW"]["UPDATE"];
        }
    }
}
