using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mw_mgr
{
    public partial class comp_obj : UserControl
    {
        public int client_no;
        public string comp_name;
        public string ipaddess;
        public string status;

        public comp_obj()
        {
            InitializeComponent();
        }

        public void init_obj(int clt_no, String comp, String ip)
        {
            lbl_ipAdd.Text = ip;
            lbl_compName.Text = comp;
            client_no = clt_no;
        }

        private void btn_info_Click(object sender, EventArgs e)
        {

        }
    }
}
