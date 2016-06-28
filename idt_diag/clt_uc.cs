using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using IniParser;
using IniParser.Model;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using System.Timers;
using System.IO;

namespace idt_diag
{
    public partial class clt_uc : UserControl
    {
        init init_cls = new init();
        db_conn conn_class = new db_conn();
        Boolean get_msg = false;
        FileIniDataParser parser = new FileIniDataParser();
        IniData data;
        static IScsClient client;
        global global_class;
        TimeSpan uptime = new TimeSpan();

        private static System.Timers.Timer refreshUptime = new System.Timers.Timer(1000);
        private static System.Timers.Timer refreshProcess = new System.Timers.Timer(10000);
        private static System.Timers.Timer refreshHD = new System.Timers.Timer(3600000);
        private static System.Timers.Timer update_time = new System.Timers.Timer(3600000);
        private static System.Timers.Timer refreshConn = new System.Timers.Timer(30000);

        Process[] mw_process = Process.GetProcessesByName("BNID_MW_SA");
        Process[] read_process = Process.GetProcessesByName("READERDEMO");

        public clt_uc()
        {
            InitializeComponent();

            global_class = new global();
            var parser = new FileIniDataParser();
            data = parser.ReadFile("app_set.ini");

            uptime = init_cls.GETUpTime;

            client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(data["NETWORK"]["MULTI_IP"], int.Parse(data["NETWORK"]["MULTI_PORT"])));
            client.MessageReceived += Client_MessageReceived;
            client.Disconnected += Client_Disconnected;

            refreshUptime.Elapsed += new ElapsedEventHandler(refreshTime);
            refreshProcess.Elapsed += new ElapsedEventHandler(refreshProcs);
            refreshConn.Elapsed += RefreshConn_Elapsed;
            refreshHD.Elapsed += RefreshHD_Elapsed;

            //refreshProcess.Enabled = true;
            refreshHD.Enabled = true;
            refreshHD.Start();
            //refreshProcess.Start();

            ConnectToServer();
            checkUpdate();
        }

        private void RefreshHD_Elapsed(object sender, ElapsedEventArgs e)
        {
            refreshInfo();
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {
            lbl_serv.BeginInvoke(new Action(() => lbl_serv.Text = "Diconnected"));
            lbl_serv.BeginInvoke(new Action(() => lbl_serv.ForeColor = Color.Red));

            refreshConn.Enabled = true;
        }

        private void RefreshConn_Elapsed(object sender, ElapsedEventArgs e)
        {
            lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Reconnecting to server"));
            if (client.CommunicationState.ToString() == "Disconnected")
            {
                try
                {
                    string msgtosend = init_cls.PCname() + " " + init_cls.GetLocalIPAddress() + " " + init_cls.GetMWVer() + " CONNECTED ";
                    ScsTextMessage clientMsg = new ScsTextMessage(msgtosend);
                    client.Connect();
                    client.SendMessage(clientMsg);
                    lbl_serv.BeginInvoke(new Action(() => lbl_serv.ForeColor = Color.Green));
                    lbl_serv.BeginInvoke(new Action(() => lbl_serv.Text = "Connected"));
                    refreshConn.Enabled = false;
                }
                catch (Exception exp)
                {
                    lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = ""));
                    return;
                    //MessageBox.Show(e.Message);
                }
            }
            lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = ""));
        }

        public void reconn()
        {
            if (client.CommunicationState.ToString() == "Disconnected")
            {
                try
                {
                    string msgtosend = init_cls.PCname() + " " + init_cls.GetLocalIPAddress() + " " + init_cls.GetMWVer() + " CONNECTED ";
                    ScsTextMessage clientMsg = new ScsTextMessage(msgtosend);
                    client.Connect();
                    client.SendMessage(clientMsg);
                    lbl_serv.ForeColor = Color.Green;
                    lbl_serv.Text = "Connected";
                }
                catch (Exception exp)
                {
                    return;
                    //MessageBox.Show(e.Message);
                }
            }
        }

        public void refreshInfo()
        {
            //if (init_cls.FRcheck())
            //{
            //    lbl_firewall.BeginInvoke(new Action(()=> lbl_firewall.ForeColor = Color.Red));
            //    lbl_firewall.BeginInvoke(new Action(() => lbl_firewall.Text = "ON"));
            //}
            //else
            //{
            //    lbl_firewall.BeginInvoke(new Action(() => lbl_firewall.ForeColor = Color.Green));
            //    lbl_firewall.BeginInvoke(new Action(() => lbl_firewall.Text = "OFF"));
            //}

            lbl_hq_ping.BeginInvoke(new Action(() => lbl_hq_ping.Text = init_cls.pingHQ(data["NETWORK"]["HQ1"])));
            lbl_hq2_ping.BeginInvoke(new Action(() => lbl_hq2_ping.Text = init_cls.pingHQ(data["NETWORK"]["HQ2"])));
            lbl_hq_back.BeginInvoke(new Action(() => lbl_hq_back.Text = init_cls.pingHQ(data["NETWORK"]["HQBACK"])));

            if (lbl_hq_ping.Text == "PASS")
            {
                lbl_hq_ping.BeginInvoke(new Action(()=>lbl_hq_ping.ForeColor = Color.Green));
            }
            else
            {
                lbl_hq_ping.BeginInvoke(new Action(() => lbl_hq_ping.ForeColor = Color.Red));
            }

            if (lbl_hq2_ping.Text == "PASS")
            {
                lbl_hq2_ping.BeginInvoke(new Action(() => lbl_hq2_ping.ForeColor = Color.Green));
            }
            else
            {
                lbl_hq2_ping.BeginInvoke(new Action(() => lbl_hq2_ping.ForeColor = Color.Red));
            }

            if (lbl_hq_back.Text == "PASS")
            {
                lbl_hq_back.BeginInvoke(new Action(() => lbl_hq_back.ForeColor = Color.Green));
            }
            else
            {
                lbl_hq_back.BeginInvoke(new Action(() => lbl_hq_back.ForeColor = Color.Red));
            }

            if (init_cls.getScanner())
            {
                lbl_scanner.BeginInvoke(new Action(() => lbl_scanner.Text = "Connected"));
            }
            else
            {
                lbl_scanner.BeginInvoke(new Action(() => lbl_scanner.Text = "Not Connected"));
            }

            lbl_mw_ver.BeginInvoke(new Action(() => lbl_mw_ver.Text = init_cls.GetMWVer()));
            lbl_hd.BeginInvoke(new Action (()=> lbl_hd.Text = (init_cls.GETDriveSpace() - init_cls.GETRemainDriveSpace()).ToString() + " GB (" + (Math.Round((100 - (init_cls.GETRemainDriveSpace() / init_cls.GETDriveSpace()) * 100), 2)).ToString() + "%)"));
            get_msg = true;
            lbl_regulaver.BeginInvoke(new Action(()=>lbl_regulaver.Text = init_cls.GETReaderSDK()));
            lbl_reguladb.BeginInvoke(new Action(()=>lbl_reguladb.Text = init_cls.GETReaderDB()));
            lbl_driver_ver.BeginInvoke(new Action(()=>lbl_driver_ver.Text = init_cls.GETReaderDriver()));
            lbl_cam.BeginInvoke(new Action(()=>lbl_cam.Text = init_cls.getCamera()));
            lbl_rd_size.BeginInvoke(new Action (()=>lbl_rd_size.Text = init_cls.GetDirSize(data["MW"]["RD_LOC"]) + " GB"));
            lbl_csv_size.BeginInvoke(new Action(()=>lbl_csv_size.Text = init_cls.GetDirSize(data["MW"]["CSV_LOC"]) * 1000 + " MB"));
        }

        private void svr_dgv_Load(object sender, EventArgs e)
        {
            
            lbl_pc_name.Text = init_cls.PCname();
            lbl_os.Text = init_cls.OSBit().ToString();

            if (lbl_os.Text == "64")
            {
                lbl_os.ForeColor = Color.Red;
            }

            lbl_os_name.Text = init_cls.OSName();

            if (lbl_os_name.Text.Contains("Windows 7"))
            {
                lbl_os_name.ForeColor = Color.Red;
            }

           

            double time = double.Parse(uptime.TotalHours.ToString("#.##"));
            var minutes = time - Math.Truncate(time);
            lbl_uptime.Text = Math.Floor(uptime.TotalHours).ToString("#.##") + " Hours " + (minutes * 60).ToString("#") + " Minutes";

            if (uptime.TotalHours > 24)
            {
                lbl_uptime.ForeColor = Color.Red;
                MessageBox.Show("This PC has been on more than 24 hours! Please restart or shutdown");
            }

            try
            {
                lbl_mwthread.Text = init_cls.GETProcThreads(mw_process[0]).ToString();
            }
            catch
            {
                lbl_mwthread.Text = "Not Started";
            }

            try
            {
                lbl_rthread.Text = init_cls.GETProcThreads(read_process[0]).ToString();
            }
            catch
            {
                lbl_rthread.Text = "Not Started";
            }

            try
            {
                if (init_cls.FRcheck())
                {
                    lbl_firewall.ForeColor = Color.Red;
                    lbl_firewall.Text = "ON";
                }
                else
                {
                    lbl_firewall.ForeColor = Color.Green;
                    lbl_firewall.Text = "OFF";
                }
            }catch
            {
                lbl_mgr_stat.Text = "Firewall unable to detect";
                lbl_firewall.ForeColor = Color.Red;
                lbl_firewall.Text = "Error";
            }
            

            lbl_google.Text = init_cls.pingGoogle();

            if (lbl_google.Text == "PASS")
            {
                lbl_google.ForeColor = Color.Red;
            }
            else
            {
                lbl_google.ForeColor = Color.Green;
            }

            lbl_hq_ping.Text = init_cls.pingHQ(data["NETWORK"]["HQ1"]);
            lbl_hq2_ping.Text = init_cls.pingHQ(data["NETWORK"]["HQ2"]);
            lbl_hq_back.Text = init_cls.pingHQ(data["NETWORK"]["HQBACK"]);

            if (lbl_hq_ping.Text == "PASS")
            {
                lbl_hq_ping.ForeColor = Color.Green;
            }
            else
            {
                lbl_hq_ping.ForeColor = Color.Red;
            }

            if (lbl_hq2_ping.Text == "PASS")
            {
                lbl_hq2_ping.ForeColor = Color.Green;
            }
            else
            {
                lbl_hq2_ping.ForeColor = Color.Red;
            }

            if (lbl_hq_back.Text == "PASS")
            {
                lbl_hq_back.ForeColor = Color.Green;
            }
            else
            {
                lbl_hq_back.ForeColor = Color.Red;
            }

            if (init_cls.getScanner())
            {
                lbl_scanner.Text = "Connected";
            }
            else
            {
                lbl_scanner.Text = "Not Connected";
            }

            lbl_lcl_ip.Text = init_cls.GetLocalIPAddress();
            lbl_mw_ver.Text = init_cls.GetMWVer();
            update_time.Elapsed += Update_time_Elapsed;
            lbl_graph.Text = init_cls.GETgraphicDriver();
            lbl_bios.Text = init_cls.BiosSerial();
            lbl_8txt.Text = init_cls.pingEight();
            lbl_hd.Text = (init_cls.GETDriveSpace() - init_cls.GETRemainDriveSpace()).ToString() + " GB (" + (Math.Round((100 - (init_cls.GETRemainDriveSpace() / init_cls.GETDriveSpace()) * 100), 2)).ToString() + "%)";
            get_msg = true;
            lbl_regulaver.Text = init_cls.GETReaderSDK();
            lbl_reguladb.Text = init_cls.GETReaderDB();
            lbl_driver_ver.Text = init_cls.GETReaderDriver();
            lbl_cam.Text = init_cls.getCamera();
            lbl_rd_size.Text = init_cls.GetDirSize(data["MW"]["RD_LOC"]) + " GB";
            lbl_csv_size.Text = init_cls.GetDirSize(data["MW"]["CSV_LOC"]) * 1000 + " MB";
            lbl_vvip.Text = init_cls.vipCounter().ToString();
            refreshUptime.Enabled = true;

            Boolean neurotechLicense = init_cls.NeuroLicense();

            if (neurotechLicense)
            {
                lbl_neuro.Text = "Activated";
            }
            else
            {
                lbl_neuro.Text = "Not Activated";
            }

        }

        private void Update_time_Elapsed(object sender, ElapsedEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Update available! Update now?", "Update", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Process[] MW = Process.GetProcessesByName("BNID_MW_SA");
                Process[] RD = Process.GetProcessesByName("READERDEMO");

                if (MW.Length > 0)
                {
                    MW[0].Close();

                    if (RD.Length > 0)
                    {
                        RD[0].Close();
                    }
                    update_MW();

                    update_time.Enabled = false;
                }
                else
                {
                    if (RD.Length > 0)
                    {
                        RD[0].Close();
                    }
                    update_MW();
                }
            }
            else
            {
                return;
            }
        }

        private void ConnectToServer()
        {
            Boolean db_connected = conn_class.openConn(data["DATABASE"]["DATA SOURCE"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);

            if (db_connected)
            {
                lbl_db_conn.ForeColor = Color.Green;
                lbl_db_conn.Text = "Connected";
            }
            else
            {
                lbl_db_conn.ForeColor = Color.Red;
                lbl_db_conn.Text = "Not Connected";
            }
            connectmsgsvr();
        }

        private void connectmsgsvr()
        {
            try
            {
                refreshConn.Enabled = false;
                client.Connect();
                sendMsg("CONNECTED");
            }
            catch (Exception e)
            {
                refreshConn.Enabled = true;
                lbl_serv.Text = "Disconnected";
                lbl_serv.ForeColor = Color.Red;
                return;
                //MessageBox.Show(e.Message);
            }
        }

        void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;
            processMsg(message.Text);
            if (message == null)
            {
                return;
            }
        }

        private void processMsg(String ser_msg)
        {
            if (ser_msg == "RD_COPY")
            {
                transferRD();
            }
            if (ser_msg == "UPDATE_AVAILABLE")
            {
                copyUpdate();
            }
            if (ser_msg == "RESTART")
            {
                sendMsg("RESTART MACHINE");
                init_cls.RESTARTMachine();
            }
            if (ser_msg == "TEST_MSG")
            {
                MessageBox.Show("THIS IS A TEST FROM SERVER");
            }
            if(ser_msg == "CSV_COPY")
            {
                try
                {
                    transferCSV();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                
            }
            if (ser_msg == "LOG_COPY")
            {
                transferLogs();
            }
        }

        private void copyUpdate()
        {
            using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
            {
                if (unc.NetUseWithCredentials(data["NETWORK"]["RD_COPY"], data["NETWORK"]["USER"], data["NETWORK"]["MULTI_IP"], global_class.DycryptPass(data["NETWORK"]["PASSWORD"])))
                {

                    Boolean copied = false;
                    try
                    {
                        sendMsg("COPYING_UPDATE");
                        String[] updateFiles = Directory.GetFiles(data["NETWORK"]["UPDATE_FOLDER"], "*.*", SearchOption.AllDirectories);
                        String[] updatedDirectories = Directory.GetDirectories(data["NETWORK"]["UPDATE_FOLDER"]);
                        int count_files = updateFiles.Length;

                        int copying_files = 1;

                        lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Copying updates found"));

                        if (Directory.Exists(data["NETWORK"]["UPDATE_FOLDER"]))
                        {
                            foreach (string file in updateFiles)
                            {
                                lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Copying update (" + copying_files.ToString() + " of " + count_files.ToString() + ")"));
                                copied = init_cls.COPYUpdate(file, data["MW"]["UPDATE"] + @"\" + Path.GetFileName(file));
                                copying_files++;
                            }
                        }
                        copying_files = 0;

                        if (copied)
                        {
                            sendMsg("UPDATE_COPIED");
                            data["MW"]["UPDATE_AVAILABLE"] = "true";

                            parser.WriteFile("app_set.ini", data);

                            update_MW();
                        }
                        else
                        {
                            MessageBox.Show("Unable to copy update!");
                        }
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        sendMsg("COPY_UPDATE_ERROR");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to connect to " + data["NETWORK"]["MULTI_IP"] +
                                    "\r\nLastError = " + unc.LastError.ToString(),
                                    "Failed to connect",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        public void sendMsg(string msg)
        {
            string msgtosend = init_cls.PCname() + " " + init_cls.GetLocalIPAddress() + " " + init_cls.GetMWVer() + " " + msg + " ";
            ScsTextMessage clientMsg = new ScsTextMessage(msgtosend);
            try
            {
                client.SendMessage(clientMsg);
                lbl_serv.ForeColor = Color.Green;
                lbl_serv.Text = "Connected";
            }
            catch
            {
                //MessageBox.Show("Client not connected to server! Please check the server status", "Error connection");
                lbl_serv.ForeColor = Color.Red;
                lbl_serv.Text = "Not Connected";
            }

        }

        private void refreshTime(object source, ElapsedEventArgs e)
        {
            uptime = init_cls.GETUpTime;

            double time = double.Parse(uptime.TotalHours.ToString("#.##"));
            var minutes = time - Math.Truncate(time);
            lbl_uptime.BeginInvoke(new Action(() => lbl_uptime.Text = Math.Floor(uptime.TotalHours).ToString("#.##") + " Hours " + (minutes * 60).ToString("#") + " Minutes"));

            if (uptime.TotalHours > 24)
            {
                lbl_uptime.BeginInvoke(new Action(() => lbl_uptime.ForeColor = Color.Red));
            }
        }

        private void refreshProcs(object source, ElapsedEventArgs e)
        {
            refreshUptime.Enabled = false;
            Process[] mw_proc = Process.GetProcessesByName("BNID_MW_SA");
            Process[] read_proc = Process.GetProcessesByName("READERDEMO");

            if (mw_proc.Length == 0)
            {
                lbl_mwthread.BeginInvoke(new Action(() => lbl_mwthread.Text = init_cls.GETProcThreads(mw_proc[0]).ToString()));
            }
            else
            {
                lbl_mwthread.BeginInvoke(new Action(() => lbl_mwthread.Text = "Not Started"));
            }

            if (read_proc.Length == 0)
            {
                lbl_rthread.BeginInvoke(new Action(() => lbl_rthread.Text = init_cls.GETProcThreads(read_proc[0]).ToString()));

            }
            else
            {
                lbl_rthread.BeginInvoke(new Action(() => lbl_rthread.Text = "Not Started"));
            }

        }

        public void update_MW()
        {  
            String[] updateFiles = Directory.GetFiles(data["MW"]["UPDATE"]);

            String[] ldifFiles = Directory.GetFiles(data["MW"]["UPDATE"], "*.ldif");

            String[] rollBackFiles = Directory.GetFiles(data["MW"]["ROLLBACK"]);

            try
            {
                if (updateFiles.Length > 0)
                {
                    //Kill MW and readerdemo process
                    Process[] MW = Process.GetProcessesByName("BNID_MW_SA");

                    DialogResult dialogResult = MessageBox.Show("Update available! Update now?", "Update", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        sendMsg("UPDATING");
                        //Delete all rollback file
                        if (rollBackFiles.Length > 0)
                        {
                            foreach (String file in rollBackFiles)
                            {
                                File.Delete(file);
                            }
                        }

                        //If update contain ldif, clear all ldif
                        if (ldifFiles.Length > 0)
                        {
                            String[] delLdif = Directory.GetFiles(data["MW"]["LOC"] + @"\LDIF");

                            foreach (String file in delLdif)
                            {
                                File.Copy(file, data["MW"]["ROLLBACK"] + @"\" + Path.GetFileName(file));
                                File.Delete(file);
                            }
                        }

                        foreach (string file in updateFiles)
                        {
                            if (Path.GetExtension(file) == ".bat")
                            {
                                try
                                {
                                    Process newproc = new Process();
                                    newproc.StartInfo.UseShellExecute = true;
                                    newproc.StartInfo.WorkingDirectory = file;
                                    newproc.StartInfo.FileName = file;
                                    newproc.StartInfo.WorkingDirectory = file.Replace(@"\" + Path.GetFileName(file), "");
                                    newproc.StartInfo.CreateNoWindow = false;
                                    newproc.Start();
                                    newproc.WaitForExit();
                                    return;
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message);
                                }
                            }
                            else
                            {
                                if (Path.GetFileName(file) == "data.dat")
                                {
                                    string[] myfile;
                                    String rollback_file;

                                    if (init_cls.OSBit() == 64)
                                    {
                                        myfile = Directory.GetFiles(data["MW"]["SDK64"], Path.GetFileName(file));
                                        rollback_file = data["MW"]["ROLLBACK"] + @"\" + myfile[0].Replace(data["MW"]["SDK64"], "");
                                    }
                                    else
                                    {
                                        myfile = Directory.GetFiles(data["MW"]["SDK32"], Path.GetFileName(file));
                                        rollback_file = data["MW"]["ROLLBACK"] + @"\" + myfile[0].Replace(data["MW"]["SDK32"], "");
                                    }


                                    File.Copy(myfile[0], rollback_file, true);

                                    File.Copy(file, myfile[0], true);
                                    File.Delete(file);
                                }
                                else if (Path.GetExtension(file) == ".ldif")
                                {
                                    File.Copy(file, data["MW"]["LOC"] + @"\LDIF\" + Path.GetFileName(file), true);
                                    File.Delete(file);
                                }
                                else
                                {
                                    string[] myfile = Directory.GetFiles(data["MW"]["LOC"], Path.GetFileName(file), SearchOption.AllDirectories);

                                    if (myfile.Length > 0)
                                    {
                                        String rollback_file = data["MW"]["ROLLBACK"] + @"\" + myfile[0].Replace(data["MW"]["LOC"], "");
                                        Directory.CreateDirectory(rollback_file.Replace(Path.GetFileName(file), ""));
                                        File.Copy(myfile[0], rollback_file, true);

                                        File.Copy(file, myfile[0], true);
                                        File.Delete(file);
                                    }
                                }
                            }
                        }

                        MessageBox.Show("Update Complete!");
                        sendMsg("UPDATE_COMPLETE");
                        data["MW"]["UPDATE_AVAILABLE"] = "false";

                        parser.WriteFile("app_set.ini", data);

                        DialogResult diagRes = MessageBox.Show("Restart the computer to keep changes?", "Update", MessageBoxButtons.YesNo);

                        if (diagRes == DialogResult.Yes)
                        {
                            init_cls.RESTARTMachine();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        update_time.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("No update files are found!");
                }
            }
            catch(Exception e)
            {
                sendMsg("UPDATE_ERROR");
                MessageBox.Show("Update failed! Please contact the system admin.");
            }
        }

      
        public void roolback_MW()
        {
            String[] rollBackFiles = Directory.GetFiles(data["MW"]["ROLLBACK"]);

            String[] ldifFiles = Directory.GetFiles(data["MW"]["ROLLBACK"], "*.ldif");

            //String[] rollBackFiles = Directory.GetFiles(data["MW"]["ROLLBACK"]);

            try
            {
                if (rollBackFiles.Length > 0)
                {
                    //Kill MW and readerdemo process
                    Process[] MW = Process.GetProcessesByName("BNID_MW_SA");

                    DialogResult dialogResult = MessageBox.Show("Rollback changes?", "Rollback", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        //Delete all rollback file
                        //if (rollBackFiles.Length > 0)
                        //{
                        //    foreach (String file in rollBackFiles)
                        //    {
                        //        File.Delete(file);
                        //    }
                        //}

                        //If update contain ldif, clear all ldif
                        if (ldifFiles.Length > 0)
                        {
                            String[] delLdif = Directory.GetFiles(data["MW"]["LOC"] + @"\LDIF");

                            foreach (String file in delLdif)
                            {
                                File.Copy(file, data["MW"]["ROLLBACK"] + @"\" + Path.GetFileName(file));
                                File.Delete(file);
                            }
                        }

                        foreach (string file in rollBackFiles)
                        {
                            if (Path.GetExtension(file) == ".bat")
                            {
                                try
                                {
                                    Process newproc = new Process();
                                    newproc.StartInfo.WorkingDirectory = file;
                                    newproc.StartInfo.FileName = Path.GetFileName(file);
                                    newproc.StartInfo.CreateNoWindow = false;
                                    newproc.Start();
                                    newproc.WaitForExit();
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message);
                                }
                            }
                            else
                            {
                                if (Path.GetFileName(file) == "data.dat")
                                {
                                    string[] myfile;
                                    String rollback_file;

                                    if (init_cls.OSBit() == 64)
                                    {
                                        myfile = Directory.GetFiles(data["MW"]["SDK64"], Path.GetFileName(file));
                                        rollback_file = data["MW"]["ROLLBACK"] + @"\" + myfile[0].Replace(data["MW"]["SDK64"], "");
                                    }
                                    else
                                    {
                                        myfile = Directory.GetFiles(data["MW"]["SDK32"], Path.GetFileName(file));
                                        rollback_file = data["MW"]["ROLLBACK"] + @"\" + myfile[0].Replace(data["MW"]["SDK32"], "");
                                    }


                                    File.Copy(myfile[0], rollback_file, true);

                                    File.Copy(file, myfile[0], true);
                                    File.Delete(file);
                                }
                                else if (Path.GetExtension(file) == ".ldif")
                                {
                                    File.Copy(file, data["MW"]["LOC"] + @"\LDIF\" + Path.GetFileName(file), true);
                                    File.Delete(file);
                                }
                                else
                                {
                                    string[] myfile = Directory.GetFiles(data["MW"]["LOC"], Path.GetFileName(file), SearchOption.AllDirectories);

                                    if (myfile.Length > 0)
                                    {
                                        String rollback_file = data["MW"]["ROLLBACK"] + @"\" + myfile[0].Replace(data["MW"]["LOC"], "");
                                        Directory.CreateDirectory(rollback_file.Replace(Path.GetFileName(file), ""));
                                        File.Copy(myfile[0], rollback_file, true);

                                        File.Copy(file, myfile[0], true);
                                        File.Delete(file);
                                    }
                                }
                            }
                        }

                        MessageBox.Show("Update Complete!");

                        DialogResult diagRes = MessageBox.Show("Restart the computer to keep changes?", "Update", MessageBoxButtons.YesNo);

                        if (diagRes == DialogResult.Yes)
                        {
                            init_cls.RESTARTMachine();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        update_time.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("No rollback files are found!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Rollback failed! Please contact the system admin.");
            }
        }

        private void transferCSV()
        {
            try
            {
                using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
                {
                    if (unc.NetUseWithCredentials(data["NETWORK"]["CSV_COPY"], data["NETWORK"]["USER"], data["NETWORK"]["MULTI_IP"], global_class.DycryptPass(data["NETWORK"]["PASSWORD"])))
                    {
                        String[] csv_files = Directory.GetFiles(data["MW"]["CSV_LOC"], "*.*", SearchOption.AllDirectories);
                        sendMsg("COPYING_CSV");
                        if (csv_files.Length > 0)
                        {
                            Boolean csv_copy;

                            lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Transferring CSV Files"));
                            Boolean csv_task = init_cls.MOVEFile(data["MW"]["CSV_LOC"], data["NETWORK"]["CSV_COPY"]);
                            csv_copy = csv_task;

                            if (csv_copy)
                            {
                                sendMsg("CSV_COPIED");
                                lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Transferring CSV Files Successfull"));
                            }
                            else
                            {
                                sendMsg("CSV_COPY_ERROR");
                            }
                        }
                        else
                        {
                            lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "No CSV files to transfer"));
                            sendMsg("CSV_COPIED");
                        }
                    }
                    else
                    {
                        MessageBox.Show(data["NETWORK"]["CSV_COPY"] + " " + data["NETWORK"]["USER"] + " " + data["NETWORK"]["MULTI_IP"] + " " + global_class.DycryptPass(data["NETWORK"]["PASSWORD"]));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                sendMsg("CSV_COPY_ERROR");
            }
        }

        private void transferLogs()
        {
            using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
            {
                if (unc.NetUseWithCredentials(data["NETWORK"]["LOG_COPY"], data["NETWORK"]["USER"], data["NETWORK"]["MULTI_IP"], global_class.DycryptPass(data["NETWORK"]["PASSWORD"])))
                {
                    String[] log_files = Directory.GetFiles(data["MW"]["LOG_LOC"], "*.*", SearchOption.AllDirectories);

                    if (log_files.Length > 0)
                    {
                        Boolean csv_copy;

                        lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Transferring Log Files"));
                        Boolean csv_task = init_cls.MOVEFile(data["MW"]["LOG_LOC"], data["NETWORK"]["LOG_COPY"]);

                        csv_copy = csv_task;

                        if (csv_copy)
                        {
                            sendMsg("LOG_COPIED");
                            lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Transferring Log Files Successfull"));
                        }
                        else
                        {
                            sendMsg("LOG_COPY_ERROR");
                        }
                    }
                    else
                    {
                        lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "No log files to transfer"));
                    }
                }
            }
        }
        
        private void transferRD()
        {
            String[] all_file = Directory.GetFiles(data["MW"]["RD_LOC"], "*.*", SearchOption.AllDirectories);

            if (all_file.Length>0)
            {
                refreshHD.Enabled = false;
                refreshHD.Stop();
                using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
                {
                    if (unc.NetUseWithCredentials(data["NETWORK"]["RD_COPY"], data["NETWORK"]["USER"], data["NETWORK"]["MULTI_IP"], global_class.DycryptPass(data["NETWORK"]["PASSWORD"])))
                    {
                        String rd_loc = data["MW"]["RD_LOC"] + @"\" + DateTime.Today.Year.ToString() + @"\" + DateTime.Today.Month.ToString() + @"\" + (DateTime.Today.Day - 1).ToString();

                        Boolean copied;
                        lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Transferring RD Images"));


                        if (Directory.Exists(data["MW"]["RD_LOC"]))
                        {
                            String new_dir = data["NETWORK"]["RD_COPY"] + @"\" + init_cls.PCname();

                            if (Directory.Exists(new_dir) == false)
                            {
                                Directory.CreateDirectory(new_dir);
                            }


                            String[] all_dir = Directory.GetDirectories(data["MW"]["RD_LOC"], "*.*", SearchOption.AllDirectories);

                            prog_stat.BeginInvoke(new Action(() => prog_stat.Visible = true));
                            prog_stat.BeginInvoke(new Action(() => prog_stat.Maximum = all_file.Length));
                            prog_stat.BeginInvoke(new Action(() => prog_stat.Minimum = 0));
                            prog_stat.BeginInvoke(new Action(() => prog_stat.Value = 1));
                            prog_stat.BeginInvoke(new Action(() => prog_stat.Step = 1));

                            foreach (string newPath in all_file)
                            {
                                String net_path = Path.GetDirectoryName(newPath).Replace(data["MW"]["RD_LOC"], "");
                                String copy_to_path = new_dir + newPath.Replace(data["MW"]["RD_LOC"], "");
                                if (Directory.Exists(new_dir + net_path) == false)
                                {
                                    Directory.CreateDirectory(new_dir + @"\" + net_path);
                                }
                                File.Copy(newPath, copy_to_path, true);

                                File.Delete(newPath);

                                prog_stat.BeginInvoke(new Action(() => prog_stat.PerformStep()));
                            }

                            prog_stat.BeginInvoke(new Action(() => prog_stat.Visible = false));
                            prog_stat.BeginInvoke(new Action(() => prog_stat.Value = 0));
                        }
                        else
                        {

                        }
                        Boolean task = init_cls.MOVEFile(data["MW"]["RD_LOC"], data["NETWORK"]["RD_COPY"]);

                        copied = task;

                        if (copied)
                        {
                            sendMsg("RD_COPIED");
                        }
                        else
                        {
                            sendMsg("RD_COPY_ERROR");
                        }

                        lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = ""));
                    }
                    else
                    {
                        MessageBox.Show("Failed to connect to " + data["NETWORK"]["MULTI_IP"] +
                                        "\r\nLastError = " + unc.LastError.ToString(),
                                        "Failed to connect",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }

                refreshHD.Enabled = true;
                refreshHD.Start();
            }
            else
            {
                lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "No RD Images found"));

                try
                {
                    transferCSV();
                }
                catch (Exception e)
                {
                    lbl_mgr_stat.BeginInvoke(new Action(() => lbl_mgr_stat.Text = "Exception Occured!, Unable to transfer CSV Files!"));
                }
            }    
        }
         
        private void checkUpdate()
        {
            if (Boolean.Parse(data["MW"]["UPDATE_AVAILABLE"]))
            {
                update_MW();
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reconn();
        }
    }
}
