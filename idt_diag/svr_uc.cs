﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using IniParser;
using IniParser.Model;
using System.Timers;
using System.Drawing;
using System.Data;
using System.IO;

namespace idt_diag
{
    public partial class svr_uc : UserControl
    {
        global global_class = new global();
        init init_cls = new init();
        db_conn conn_class = new db_conn();
        Boolean get_msg = false;
        FileIniDataParser parser = new FileIniDataParser();
        IniData data;
        IScsServer server;
        String pcName, ipadd, mw_ver, status;
        int client_count = 0;
        private static System.Timers.Timer check_task = new System.Timers.Timer(60000);
        private static System.Timers.Timer restart_pc = new System.Timers.Timer(30000);
        private static System.Timers.Timer get_csv = new System.Timers.Timer(60000);
        private static System.Timers.Timer get_log = new System.Timers.Timer(60000);
        CheckBox checkboxHeader = new CheckBox();

        public svr_uc()
        {
            InitializeComponent();
        }

        private void svr_uc_Load(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            data = parser.ReadFile("app_set.ini");
            server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(int.Parse(data["NETWORK"]["MULTI_PORT"])));
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            restart_pc.Elapsed += Restart_pc_Elapsed;
            check_task.Elapsed += Check_task_Elapsed;

            get_csv.Elapsed += Get_csv_Elapsed;
            get_log.Elapsed += Get_log_Elapsed;

            get_csv.Enabled = true;
            get_log.Enabled = true; 

            Rectangle rect = dgv_svr.GetCellDisplayRectangle(0, -1, true);

            // set checkbox header to center of header cell. +1 pixel to position correctly.
            rect.X = rect.Location.X + 8;
            rect.Y = rect.Location.Y + 10;
            rect.Width = rect.Size.Width;
            rect.Height = rect.Size.Height;

            
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(15, 15);
            checkboxHeader.Location = rect.Location;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dgv_svr.Controls.Add(checkboxHeader);


            if (Boolean.Parse(data["COPY"]["COPY_RD"]))
            {
                check_task.Enabled = true;
            }
            if (Boolean.Parse(data["PC"]["RESTART"]))
            {
                restart_pc.Enabled = true;
            }
                       
            ConnectToServer();
        }

        private void Get_log_Elapsed(object sender, ElapsedEventArgs e)
        {
            String curr_time = DateTime.Now.ToString("07:00 PM");
            if (curr_time == " ")
            {
                sendMsgstoAll("LOG_COPY");
            }
        }

        private void Get_csv_Elapsed(object sender, ElapsedEventArgs e)
        {
            String curr_time = DateTime.Now.ToString("08:00 PM");
            if (curr_time == " ")
            {
                sendMsgstoAll("CSV_COPY");
            }
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            if(dgv_svr.RowCount > 0)
            {
                foreach(DataGridViewRow row in this.dgv_svr.Rows)
                {
                    CheckBox cbx;
                    cbx = new CheckBox();
                    DataGridViewCheckBoxCell my_cell = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (checkboxHeader.Checked)
                    {
                        my_cell.Value = true;
                    }
                    else
                    {
                        my_cell.Value = false;
                    }
                }
            }
        }

        private void Restart_pc_Elapsed(object sender, ElapsedEventArgs e)
        {
            String copy_time = data["PC"]["RESTART_TIME"];
            String cur_time = DateTime.Now.ToString("hh:mm tt");

            if (Boolean.Parse(data["COPY"]["COPY_RD"]))
            {
                if (cur_time == copy_time)
                {
                    sendMsgstoAll("RESTART");
                }
            }
        }

        private void Check_task_Elapsed(object sender, ElapsedEventArgs e)
        {
            String copy_time = data["COPY"]["COPY_TIME"];
            String cur_time = DateTime.Now.ToString("hh:mm tt");
            if (Boolean.Parse(data["PC"]["RESTART"]))
            {
                if (cur_time == copy_time)
                {
                    sendMsgstoAll("RD_COPY");
                }    
            }
        }

        private void ConnectToServer()
        {
            try
            {
                server.Start();
                conn_class.openConn(data["DATABASE"]["DATA SOURCE"], data["DATABASE"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);
                lbl_status.Text = "Server Online";
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Boolean CloseServer()
        {
            try
            {
                server.Stop();
                return true;
            }
            catch
            {
                return false;
            }
        }

        void Server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            lbl_count.BeginInvoke(new Action(() => { lbl_count.Text = server.Clients.Count.ToString(); }));
            e.Client.MessageReceived += Client_MessageReceived;
        }

        void Server_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            lbl_count.BeginInvoke(new Action(() => { lbl_count.Text = server.Clients.Count.ToString(); }));
            long clientId = e.Client.ClientId;

            foreach(DataGridViewRow curr_row in dgv_svr.Rows)
            {
                long id = long.Parse(curr_row.Cells[5].Value.ToString());

                if(id == clientId)
                {
                    dgv_svr.BeginInvoke(new Action(() => { dgv_svr.Rows.Remove(curr_row); }));
                }
            }
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;
            if (message == null)
            {
                return;
            }

            String[] curr_msg = message.Text.Split(' ');

            pcName = curr_msg[0];
            ipadd = curr_msg[1];
            mw_ver = curr_msg[2];
            status = curr_msg[3];

            var client = (IScsServerClient)sender;

            
            if (status == "CONNECTED")
            {
                dgv_svr.BeginInvoke(new Action(() => dgv_svr.Rows.Add(false,pcName,ipadd,mw_ver,status, client.ClientId, DateTime.Now)));
                dgv_svr.BeginInvoke(new Action(() => dgv_svr.CurrentCell = dgv_svr[1,0]));
            }
            else
            {
                foreach (DataGridViewRow curr_row in dgv_svr.Rows)
                {
                    long id = long.Parse(curr_row.Cells[5].Value.ToString());

                    if (id == client.ClientId)
                    {
                        dgv_svr.BeginInvoke(new Action(() => { curr_row.Cells[4].Value = status; }));
                    }
                }
            }

            Boolean query = conn_class.update_server(pcName, ipadd, mw_ver, status);
        }

        internal void locationCheck()
        {
            throw new NotImplementedException();
        }

        public void verifyHitdata()
        {
            throw new NotImplementedException();
        }

        public async void purgeData()
        {
            tabControl1.SelectedIndex = 1;

            DataSet dat = conn_class.transferHitdataZip("purge");
            
            if(dat.Tables[0].Rows.Count > 0)
            {
                Boolean repeat = true;
                int count = 0;
                do
                {
                    if (dat == null)
                    {
                        txt_log.AppendText("Error connecting to DB!" + Environment.NewLine);
                    }
                    else
                    {
                        txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Purging Data " + dat.Tables[0].Rows.Count + " data" + Environment.NewLine);

                        foreach (DataRow row in dat.Tables[0].Rows)
                        {

                            txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Purging data no: " + row[0].ToString() + Environment.NewLine);
                            Task<Boolean> task = purgeRowData(row);

                            Boolean results = await task;

                            if (results)
                            {
                                txt_log.AppendText("File Deleted!" + Environment.NewLine);
                                row[48] = DateTime.Now;
                            }
                            else
                            {
                                txt_log.AppendText("File Not Deleted!" + Environment.NewLine);
                                row[47] = DBNull.Value;
                            }

                            count++;

                            if (count % 10000 == 0)
                            {
                                conn_class.updateData(dat);
                                txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Commiting Changes" + Environment.NewLine + Environment.NewLine);
                            }

                            if (count % 5000 == 0)
                            {
                                txt_log.Text = "";
                            }

                            txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Purge No: " + count + Environment.NewLine);
                        }

                        txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Commiting Changes" + Environment.NewLine + Environment.NewLine);

                        conn_class.updateData(dat);
                        conn_class.disposeAdapter();

                        dat = null;
                        dat = conn_class.transferHitdataZip("purge");

                        if (dat.Tables[0].Rows.Count == 0)
                        {
                            repeat = false;
                        }
                    }
                } while (repeat == true);

                txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Purge Data Done!" + Environment.NewLine);
            }
        }

        private async Task<Boolean> purgeRowData(DataRow row)
        {
            try
            {
                String directory = data["NETWORK"]["HQSTORE"];
                String locDir = data["HITDATA"]["SVR_HITDATA"];

                String preciseDir = directory + @"\Zip\" + Convert.ToDateTime(row[2]).ToString("yyyy") + @"\" + Convert.ToDateTime(row[2]).ToString("MM") + @"\" + Convert.ToDateTime(row[2]).ToString("yyyyMMdd") + @"\" + Convert.ToDateTime(row[2]).ToString("HH");
                String preciseLocDir = locDir + @"\Zip\" + Convert.ToDateTime(row[2]).ToString("yyyy") + @"\" + Convert.ToDateTime(row[2]).ToString("MM") + @"\" + Convert.ToDateTime(row[2]).ToString("yyyyMMdd") + @"\" + Convert.ToDateTime(row[2]).ToString("HH");

                String m_ExtFNm = row[3] + "_" + row[33] + ".zip";

                String m_ZipFNm = preciseDir + @"\" + m_ExtFNm;
                String L_ZipFNm = preciseLocDir + @"\" + m_ExtFNm;

                using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
                {
                    if (unc.NetUseWithCredentials(data["NETWORK"]["HQSTORE"], data["NETWORK"]["USER"], data["NETWORK"]["HQ1"], global_class.DycryptPass(data["NETWORK"]["PASSWORD"])))
                    {
                        if (File.Exists(m_ZipFNm))
                        {
                            if (File.Exists(L_ZipFNm))
                            {
                                File.Delete(L_ZipFNm);
                                return true;
                            }
                            return true;
                        }else
                        {
                            return false;                        
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void transferZipHQ()
        {
            tabControl1.SelectedIndex = 1;
            FolderBrowserDialog diag = new FolderBrowserDialog();

            DialogResult diagResult = diag.ShowDialog();
            
            if (diagResult == DialogResult.OK)
            {
                int count = 0;

                String[] fileName = Directory.GetFiles(diag.SelectedPath,"*.zip");

                if(fileName.Length > 0)
                {

                }
            }
        }

        public async void deleteDuplicates()
        {
            tabControl1.SelectedIndex = 1;
            DataSet pset = conn_class.duplicateData();
            if (pset == null)
            {
                txt_log.AppendText("Unable To Connect DB!" + Environment.NewLine);
            }
            else
            {
                tabControl1.SelectedIndex = 1;
                txt_log.AppendText(pset.Tables[0].Rows.Count.ToString() + " total duplicate rows found" + Environment.NewLine);
                foreach (DataRow row in pset.Tables[0].Rows)
                {

                    txt_log.AppendText("Deleting Data ID: " + row[0].ToString() + Environment.NewLine);
                    Task<Boolean> task = deleteData(row);

                    Boolean result = await task;

                    if (result)
                    {
                        txt_log.AppendText("File Deleted" + Environment.NewLine);
                    }else
                    {
                        txt_log.AppendText("File Not Found!" + Environment.NewLine);
                    }
                    
                }

                txt_log.AppendText("Commiting Changes" + Environment.NewLine + Environment.NewLine);

                conn_class.updateData(pset);
                conn_class.disposeAdapter();

                txt_log.AppendText("Deleting Duplicates Done!" + Environment.NewLine);
            }
           
        }

        private async Task<Boolean> deleteData(DataRow row)
        {
            try
            {
                String directory = data["HITDATA"]["SVR_HITDATA"];
                String preciseDir = directory + @"\Zip\" + Convert.ToDateTime(row[2]).ToString("yyyy") + @"\" + Convert.ToDateTime(row[2]).ToString("MM") + @"\" + Convert.ToDateTime(row[2]).ToString("yyyyMMdd") + @"\" + Convert.ToDateTime(row[2]).ToString("HH");
                String m_ExtFNm = row[3] + "_" + row[33] + ".zip";
                String m_ZipFNm = preciseDir + @"\" + m_ExtFNm;

                File.Delete(m_ZipFNm);
                row.Delete();
                return true;
            }
            catch (Exception e)
            {
                row.Delete();
                return false;
            }
        }

        private void dgv_svr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                dgv_svr.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !Convert.ToBoolean(dgv_svr.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        public async void sendMsgstoAll(string msg)
        {
            foreach(DataGridViewRow row in dgv_svr.Rows)
            {
                var message = new ScsTextMessage(msg);
                

                Task bulkMsg = Task.Run(() => server.Clients[int.Parse(row.Cells[5].Value.ToString())].SendMessage(message));

                if (msg == "RD_COPY")
                {
                    await Task.Delay(int.Parse(data["MW"]["RD_TIME"]));
                }
                if (msg == "UPDATE_AVAILABLE")
                {
                    await Task.Delay(int.Parse(data["MW"]["UPDATE_TIME"]));
                }
                if (msg == "RESTART")
                {
                    await Task.Delay(30000);
                }
                if(msg == "TEST_MSG")
                {
                    await Task.Delay(120000);
                }
                if (msg == "CSV_COPY")
                {
                    await Task.Delay(180000);
                }
                if (msg == "LOG_COPY")
                {
                    await Task.Delay(180000);
                }
            }
        }

        public async void sendMsgstoSome(string msg)
        {
            foreach (DataGridViewRow row in dgv_svr.Rows)
            {

                Boolean check = Convert.ToBoolean(row.Cells[0].Value);
                if (check)
                {
                    var message = new ScsTextMessage(msg);


                    Task bulkMsg = Task.Run(() => server.Clients[int.Parse(row.Cells[5].Value.ToString())].SendMessage(message));

                    if (msg == "RD_COPY")
                    {
                        await Task.Delay(int.Parse(data["MW"]["RD_TIME"]));
                    }
                    if (msg == "CSV_COPY")
                    {
                        await Task.Delay(30000);
                    }
                    if (msg == "UPDATE_AVAILABLE")
                    {
                        await Task.Delay(int.Parse(data["MW"]["UPDATE_TIME"]));
                    }
                    if (msg == "RESTART")
                    {
                        await Task.Delay(30000);
                    }
                    if (msg == "TEST_MSG")
                    {
                        await Task.Delay(120000);
                    }
                    if (msg == "LOG_COPY")
                    {
                        await Task.Delay(30000);
                    }
                } 
            }
        }

        private void processBulkMsgs(IScsServerClient clients, ScsTextMessage msg)
        {
            clients.SendMessage(msg);
        }

        private void tabpage1_Click(object sender, EventArgs e)
        {

        }

        public void transferHit()
        {
            Boolean transferHit = conn_class.transferHitdata();

            if (transferHit)
            {
                lbl_date.Text = DateTime.Now.ToString();
            }
            else
            {
                lbl_date.Text = "Failed Trasfer Occured";
            }
        }

        public async void transferZipData()
        {
            tabControl1.SelectedIndex = 1;
            DataSet dat = conn_class.transferHitdataZip("manual");

            FolderBrowserDialog diag = new FolderBrowserDialog();

            DialogResult diagResult = diag.ShowDialog();
            

            if(diagResult == DialogResult.OK)
            {
                int count = 0;
                if (dat == null)
                {
                    txt_log.AppendText("Error connecting to DB!" + Environment.NewLine);
                }
                else
                {
                    txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Transfering " + dat.Tables[0].Rows.Count + " data to " +diag.SelectedPath + Environment.NewLine);

                    foreach (DataRow row in dat.Tables[0].Rows)
                    {

                        txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt")+": Transfering Data no: " + row[0].ToString() + Environment.NewLine);
                        Task<Boolean> task = transferRowFile(row, diag.SelectedPath);

                        Boolean results = await task;

                        if (results)
                        {
                            txt_log.AppendText("File Transferred Done!" + Environment.NewLine);
                        }
                        else
                        {
                            txt_log.AppendText("File Transferred Failed!" + Environment.NewLine);
                        }

                        count++;

                        if(count%10000 == 0)
                        {
                            conn_class.updateData(dat);
                            txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Commiting Changes" + Environment.NewLine + Environment.NewLine);
                        }

                        if (count % 100000 == 0)
                        {
                            txt_log.Text = "";
                        }

                        txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Transfer No: " + count + Environment.NewLine);
                    }

                    txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Commiting Changes" + Environment.NewLine + Environment.NewLine);

                    conn_class.updateData(dat);
                    conn_class.disposeAdapter();

                    txt_log.AppendText(DateTime.Now.ToString("hh:mm:ss tt") + ": Transfer Data Done!" + Environment.NewLine);
                }
            }  
        }

        public async Task<Boolean> transferRowFile(DataRow row, string path)
        {
            try
            {
                String directory = data["HITDATA"]["SVR_HITDATA"];
                String zipDir = @"\Zip\" + Convert.ToDateTime(row[2]).ToString("yyyy") + @"\" + Convert.ToDateTime(row[2]).ToString("MM") + @"\" + Convert.ToDateTime(row[2]).ToString("yyyyMMdd") + @"\" + Convert.ToDateTime(row[2]).ToString("HH");
                String preciseDir = directory + zipDir;
                String m_ExtFNm = row[3] + "_" + row[33] + ".zip";
                String m_ZipFNm = preciseDir + @"\" + m_ExtFNm;


                if (!Directory.Exists(path + zipDir))
                {
                    Directory.CreateDirectory(path + zipDir);
                }
                File.Copy(m_ZipFNm, path + zipDir + @"\" + m_ExtFNm);
                row[47] = DateTime.Now;
                return true;
            }
            catch (Exception e)
            {
                txt_log.BeginInvoke(new Action(()=> txt_log.AppendText("File Transferred Error Due: " + e.Message + Environment.NewLine)));
                return false; 
            }
        }
    }
}