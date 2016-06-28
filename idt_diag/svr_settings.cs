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
using idt_diag;

namespace mw_mgr
{
    public partial class svr_settings : Form
    {
        FileIniDataParser parser = new FileIniDataParser();
        IniData data;
        global global_class;

        public svr_settings()
        {
            InitializeComponent();
            global_class = new global();
            var parser = new FileIniDataParser();
            data = parser.ReadFile("app_set.ini");
        }

        private void svr_settings_Load(object sender, EventArgs e)
        {
            if (Boolean.Parse(data["COPY"]["COPY_RD"]))
            {
                cbx_transfer.Checked = true;
            }
            if (Boolean.Parse(data["PC"]["RESTART"]))
            {
                cbx_restart.Checked = true;
            }

            txt_transTime.Text = data["COPY"]["COPY_TIME"];
            txt_reTime.Text = data["PC"]["RESTART_TIME"];
            txt_pass.Text = global_class.DycryptPass(data["DATABASE"]["PASSWORD"]);
            txt_user.Text = data["DATABASE"]["USER"];
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            data["COPY"]["COPY_TIME"] = txt_transTime.Text;
            data["PC"]["RESTART_TIME"] = txt_reTime.Text;
            data["DATABASE"]["PASSWORD"] = global_class.EncryptPass(txt_pass.Text);
            data["DATABASE"]["USER"] = txt_user.Text;

            if (cbx_transfer.Checked)
            {
                data["COPY"]["COPY_RD"] = "true";
            }
            else
            {
                data["COPY"]["COPY_RD"] = "false";
            }
            if (cbx_restart.Checked)
            {
                data["PC"]["RESTART"] = "true";
            }
            else
            {
                data["PC"]["RESTART"] = "false";
            }

            parser.WriteFile("app_set.ini", data);

            this.Close();
        }
    }
}
