using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using IniParser;
using IniParser.Model;


namespace idt_diag
{
    class multi
    {
        FileIniDataParser parser = new FileIniDataParser();

        UdpClient client = new UdpClient(6000);
        IPAddress multicastAdd;
        IniData data;
        IPEndPoint remoteep;

        public Boolean connMX()
        {
            data = parser.ReadFile("app_set.ini");
            multicastAdd = IPAddress.Parse(data["NETWORK"]["MULTI_IP"]);
            remoteep = new IPEndPoint(multicastAdd, int.Parse(data["NETWORK"]["MULTI_PORT"]));

            try
            {
                client.JoinMulticastGroup(multicastAdd);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean sendMsg(string msg)
        {
            IPAddress ip;
            try
            {
                Console.WriteLine("MCAST Send on Group: {0} Port: {1} TTL: {2}", data["NETWORK"]["MULTI_IP"], int.Parse(data["NETWORK"]["MULTI_PORT"]), "2");
                ip = IPAddress.Parse(data["NETWORK"]["MULTI_IP"]);

                Socket s = new Socket(AddressFamily.InterNetwork,
                                SocketType.Dgram, ProtocolType.Udp);

                s.SetSocketOption(SocketOptionLevel.IP,
                    SocketOptionName.AddMembership, new MulticastOption(ip));

                s.SetSocketOption(SocketOptionLevel.IP,
                    SocketOptionName.MulticastTimeToLive, 1);

                byte[] b = new byte[10];
                for (int x = 0; x < b.Length; x++) b[x] = (byte)(x + 65);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(data["NETWORK"]["MULTI_IP"]), int.Parse(data["NETWORK"]["MULTI_PORT"]));

                Console.WriteLine("Connecting...");

                s.Connect(ipep);

                for (int x = 0; x < 2; x++) {
                    Console.WriteLine("Sending ABCDEFGHIJ...");
                    s.Send(b, b.Length, SocketFlags.None);
                }

                Console.WriteLine("Closing Connection...");
                s.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void sendCfmConn()
        {
            try
            {
                String msg = "conn";
                Byte[] msgToSend = Encoding.ASCII.GetBytes("");

                client.Send(msgToSend, msgToSend.Length, remoteep);
            }
            catch
            {
                return;
            }
        }

        public string getMsgs()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, int.Parse(data["NETWORK"]["MULTI_PORT"]));
            s.Bind(ipep);

            IPAddress ip = IPAddress.Parse(data["NETWORK"]["MULTI_IP"]);

            s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));

            while (true)
            {
                byte[] b = new byte[10];
                Console.WriteLine("Waiting for data..");
                s.Receive(b);
                string str = System.Text.Encoding.ASCII.GetString(b, 0, b.Length);
                return str;
            }
        }
    }
}
