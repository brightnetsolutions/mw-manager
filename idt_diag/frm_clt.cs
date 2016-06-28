using System;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using mw_mgr;

namespace idt_diag
{
    public partial class frm_clt : Form
    {
        FileIniDataParser parser = new FileIniDataParser();
        string appMode;
        init init_cls = new init();
        svr_uc server_control;
        clt_uc client_control;
        Boolean closing = false;

        private static int WM_QUERYENDSESSION = 0x11;
        private static bool systemShutdown = false;

        public frm_clt()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            IniData data = parser.ReadFile("app_set.ini");
            appMode = data["APP"]["MANAGE_MODE"];

            if (appMode == "svr")
            {
                this.Text = "SERVER MODE";
            }
            else
            {
                this.Text = "CLIENT MODE";
            }

            menuStrip1.Visible = false;
            Application.ApplicationExit += Application_ApplicationExit;
            this.FormClosing += Frm_clt_FormClosing;
            this.KeyDown += Frm_clt_KeyDown;
        }


        private void Frm_clt_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F10 && e.Modifiers == Keys.Shift)
            {
                if(menuStrip1.Visible == false)
                {
                    menuStrip1.Visible = true;
                    return;
                }
            }
            if(e.KeyCode == Keys.F10)
            {
                if (menuStrip1.Visible)
                {
                    menuStrip1.Visible = false;
                    return;
                }
            }
        }

        private void Frm_clt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            if (systemShutdown)
            {
                this.Close();
            }
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {

            if (appMode == "svr")
            {
                server_control.CloseServer();
            }
            else if(appMode == "clt")
            {
                client_control.sendMsg("DISCONNECTED");
            }
            else
            {
                return;
            }
        }

        private void frm_clt_Load(object sender, EventArgs e)
        {
            if (appMode == "svr")
            {
                server_control = new svr_uc();
                updateToolStripMenuItem.Visible = false;
                reconnectToolStripMenuItem.Visible = false;
                rollbackToolStripMenuItem.Visible = false;
                usr_cont.Controls.Add(server_control);
            }
            else
            {
                taskToolStripMenuItem.Visible = false;
                client_control = new clt_uc();

                usr_cont.Controls.Add(client_control);
            }
        }

        private void alertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server_control.sendMsgstoSome("TEST_MSG");
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notify_01.Visible = true;
            }
        }

        private void notify_01_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notify_01.Visible = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appMode == "svr")
            {
                svr_settings svr_setting = new svr_settings();
                svr_setting.Show();
            }
            else
            {
                settings clt_setting = new settings();
                clt_setting.Show();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about_win = new about();
            about_win.ShowDialog();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closing = true;
            this.Close();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client_control.update_MW();
        }

        private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client_control.reconn();
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_QUERYENDSESSION)
            {
                systemShutdown = true;
            }
            base.WndProc(ref m);

        }

        private void transferHDDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void rollbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void restartClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void rollbackToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (appMode == "clt")
            {
                client_control.roolback_MW();
            }
        }

        private void retrieveRDImagesFromClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                server_control.sendMsgstoSome("RD_COPY");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void restartClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appMode == "svr")
            {
                server_control.sendMsgstoSome("RESTART");
            }
        }

        private void retrieveCSVFromClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server_control.sendMsgstoSome("CSV_COPY");
        }

        private void moveMWLogsFromClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server_control.sendMsgstoSome("LOG_COPY");
        }

        private void updateClientsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            server_control.sendMsgstoSome("UPDATE_AVAILABLE");
        }

        private void transferHitdataDBOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appMode == "svr")
            {
                server_control.transferHit();
            }
        }

        private void transferHitdataDBAndFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appMode == "svr")
            {
                server_control.transferZipData();
            }
        }

        private void checkDBLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appMode == "svr")
            {
                server_control.locationCheck();
            }
        }

        private void removeDBDuplicatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appMode == "svr")
            {
                server_control.deleteDuplicates();
            }
        }

        private void purgeDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server_control.purgeData();
        }

        private void transferZipDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server_control.transferZipHQ();
        }

        private void verifyHitdataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server_control.verifyHitdata();
        }
    }
}
