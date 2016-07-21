using System;
using System.IO;
using System.Management;
using NetFwTypeLib;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using IniParser;
using IniParser.Model;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace idt_diag
{
    class init
    {
        private const string CLSID_FIREWALL_MANAGER = "{304CE942-6E39-40D8-943A-B913C40C9CD4}";

        [DllImport("User32")]
        extern public static int GetGuiResources(IntPtr hProcess, int uiFlags);

        [DllImport("user32")]
        private static extern int IsWindowEnabled(int hWnd);

        DriveInfo[] allDrives = DriveInfo.GetDrives();

        private static INetFwMgr GetFirewallManager()
        {
            Type objectType = Type.GetTypeFromCLSID(
                  new Guid(CLSID_FIREWALL_MANAGER));
            return Activator.CreateInstance(objectType)
                  as INetFwMgr;
        }

        public String OSName()
        {
            //var OS = Environment.OSVersion.VersionString;

            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName;
        }

        public int OSBit()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                return 64;
            }
            else
            {
                return 32;
            }
        }

        public String PCname()
        {
            return Environment.MachineName.ToString();
        }

        public DriveInfo[] HDspace()
        {
            DriveInfo[] hdd = DriveInfo.GetDrives();
            return hdd;
        }

        public Boolean FRcheck()
        {
            INetFwMgr manager = GetFirewallManager();
            bool isFirewallEnabled =  manager.LocalPolicy.CurrentProfile.FirewallEnabled;
            return isFirewallEnabled;
        }

        public Boolean COPYFile(string origin, string target)
        {
            try
            {
                File.Copy(origin, target);

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public Boolean UPDATEFile(string origin, string target)
        {
            try
            {
                File.Copy(origin, target, true);
                File.Delete(origin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean MOVEFile(String origin, string target)
        {
            if (Directory.Exists(origin))
            {
                String new_dir = target+@"\"+PCname();

                if (Directory.Exists(new_dir)== false)
                {
                    Directory.CreateDirectory(new_dir);
                }

                String[] all_file = Directory.GetFiles(origin, "*.*", SearchOption.AllDirectories);
                String[] all_dir = Directory.GetDirectories(origin, "*.*", SearchOption.AllDirectories);

                
                foreach (string newPath in all_file)
                {
                    String net_path = Path.GetDirectoryName(newPath).Replace(origin,"");
                    String copy_to_path = new_dir + newPath.Replace(origin, "");
                    if (Directory.Exists(new_dir+net_path)==false)
                    {
                        Directory.CreateDirectory(new_dir + @"\" + net_path);
                    }
                    File.Copy(newPath, copy_to_path, true);

                    File.Delete(newPath);
                } 
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean COPYUpdate(String origin, String destination)
        {

            try
            {
                File.Copy(origin, destination);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public void RESTARTMachine()
        {
            Process.Start("shutdown.exe", "-r -t 0");
        }

        public Boolean ENDProcess(String processname)
        {
            Process[] myProcess = Process.GetProcessesByName(processname);

            if (myProcess.Length > 0)
            {
                try
                {
                    myProcess[0].Close();
                    return true;
                }
                catch
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public string GetMWVer()
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("app_set.ini");
            FileVersionInfo file_info = FileVersionInfo.GetVersionInfo(data["MW"]["LOC"]+ @"\MW\BNID_MW_SA.exe");
            return file_info.FileVersion.ToString();
        }

        public int GETProcHandles(Process proc)
        {
            return proc.Threads.Count;
        }
        
        public int GETProcThreads(Process proc)
        {
            return proc.HandleCount;
        }

        public double GETRemainDriveSpace()
        {
            DriveInfo drive = new DriveInfo("C:\\");

            return Math.Round(drive.TotalFreeSpace / Math.Pow(1024, 3), 2);
        }

        public double GETDriveSpace()
        {
            DriveInfo drive = new DriveInfo("C:\\");

            return Math.Round(drive.TotalSize / Math.Pow(1024, 3), 2);
        }

        public TimeSpan GETUpTime
        {
            get
            {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();       
                    return TimeSpan.FromSeconds(uptime.NextValue());
                }
            }
        }

        public String pingHQ(String ip)
        {
            // Ping's the local machine.
            try
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("app_set.ini");
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(ip);

                if (reply.Status == IPStatus.Success)
                {
                    return "PASS";
                }
                else
                {
                    return "FAIL";
                }
            }
            catch
            {
                return "FAIL";
            } 
        }

        public String pingGoogle()
        {
            // Ping's the local machine.
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send("www.google.com");

                if (reply.Status == IPStatus.Success)
                {
                    return "PASS";
                }
                else
                {
                    return "FAIL";
                }
            }
            catch
            {
                return "FAIL";
            }
        }

        public String pingEight()
        {
            // Ping's the local machine.
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send("8.8.8.8");

                if (reply.Status == IPStatus.Success)
                {
                    return "PASS";
                }
                else
                {
                    return "FAIL";
                }
            }
            catch
            {
                return "FAIL";
            }
            
        }

        public String GETgraphicDriver()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");

            string graphicsCard = string.Empty;
            foreach (ManagementObject mo in searcher.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    if (property.Name == "Description")
                    {
                        graphicsCard = property.Value.ToString();
                    }
                }
            }

            return graphicsCard;
        }

        public String BiosSerial()
        {
            string serialNumber = string.Empty;
             
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(" Select * From Win32_BIOS ");
                foreach (ManagementObject getserial in MOS.Get())
                {
                    serialNumber = getserial["SerialNumber"].ToString();
                }
            return serialNumber;
        }

        public double GetDirSize(String dir)
        {
            string[] a = Directory.GetFiles(dir, "*.*",SearchOption.AllDirectories);

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            
            return Math.Round(b / Math.Pow(1024, 3), 2);

        }

        public String GETAntivirus()
        {
            String antivirus = "";
            try
            {
                ManagementObjectSearcher mos = null;
                if (Environment.OSVersion.Version.Major > 5)
                {
                    mos = new ManagementObjectSearcher(@"\" +
                        Environment.MachineName + @"rootSecurityCenter2",
                                     "SELECT * FROM AntivirusProduct");
                }
                else
                {
                    mos = new ManagementObjectSearcher(@"\" +
                        Environment.MachineName + @"rootSecurityCenter",
                                     "SELECT * FROM AntivirusProduct");
                }

                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    antivirus += mo["displayName"].ToString();
                }
            }
            catch(Exception e)
            {
                antivirus = e.Message;
            }
            

            return antivirus;

        }

        public String GETReaderSDK()
        {
            String regLoc;

            if (OSBit() == 64)
            {
                regLoc = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Regula\Document Reader SDK", "Version", null);
            }
            else
            {
                regLoc = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Regula\Document Reader SDK", "Version", null);
            }
            
            return regLoc;
        }

        public String GETReaderDB()
        {
            String regLoc;

            if (OSBit() == 64)
            {
                regLoc = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Regula\Reader Documents Database", "Version", null);
            }
            else
            {
                regLoc = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Regula\Reader Documents Database", "Version", null);
            }

            return regLoc;
        }

        public String GETReaderDriver()
        {
            String regLoc;

            if (OSBit() == 64)
            {
                regLoc = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Regula\Drivers\Reader", "Version", null);
            }
            else
            {
                regLoc = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Regula\Drivers\Reader", "Version", null);
            }

            return regLoc;
        }

        public String GetCameraConnected()
        {
            return "";
        }

        public Boolean NeuroLicense()
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("app_set.ini");
            Boolean existingDir = Directory.Exists(data["MW"]["LOC"]);

            if (existingDir)
            {
                String[] licenseFile =  Directory.GetFiles(data["MW"]["LOC"],"*.lic");

                if(licenseFile.Length == 0)
                {
                    existingDir = Directory.Exists(@"C:\Program Files\Neurotechnology");

                    if (existingDir)
                    {
                        licenseFile = Directory.GetFiles(@"C:\Program Files\Neurotechnology", "*.lic", SearchOption.AllDirectories);
                        if (licenseFile.Length == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public String getCamera()
        {
            string camera = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%cam%'");

            foreach (ManagementObject device in searcher.Get())
            {
                // To make the example more simple,
                string name = device.GetPropertyValue("Name").ToString();

                camera = camera + name + " ";
            }

            return camera;
        }

        public Boolean getScanner()
        {
            Boolean scanner = false;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Manufacturer LIKE '%regula%'");

            if(searcher.Get().Count > 0)
            {
                scanner = true;
            }

            return scanner;
        }

        public void clearDir(String dir)
        {
            foreach (var directory in Directory.GetDirectories(dir))
            {
                clearDir(directory);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        public Boolean deleteAllFiles(String dir)
        {
            try
            {
                Array.ForEach(Directory.GetFiles(dir), File.Delete);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean vipCounter()
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(@"C:\R7024BFM\MW\Cfg_Def.ini");
            String dat = data["GBXCFGS"]["CHXVVIP"];
            return Boolean.Parse(data["GBXCFGS"]["CHXVVIP"].Remove(dat.IndexOf(' ')));
        }

        public int GetTotalMemoryInGBytes()
        {
            ulong ram = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            double gb = Math.Pow(1024, 3);
            int tot_ram = Convert.ToInt32(ram/gb);

            return tot_ram;
        }

        public string GetNetFramework()
        {
            string productName = "";
            int productVer;

            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full");

            if (reg != null)
            {
                productVer = (int)reg.GetValue("Release");

                if (productVer == 378389)
                {
                    productName = "4.5";
                }
                else if (productVer == 378675)
                {
                    productName = "4.5.1";
                }
                else if (productVer == 378758)
                {
                    productName = "4.5.1";
                }
                else if (productVer == 379893)
                {
                    productName = "4.5.2";
                }
                else if (productVer == 393295 || productVer == 393297)
                {
                    productName = "4.6";
                }
                else if (productVer == 394254 || productVer == 394271)
                {
                    productName = "4.6.1";
                }
                else
                {
                    productName = "4.6.2";
                }

            }
            else
            {
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");

                productName = (string)reg.GetValue("Version");
            }

            return productName;
        }
        
    }
}
