using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using IniParser;
using IniParser.Model;
using System.Windows.Forms;

namespace idt_diag
{
    class db_conn
    {
        SqlConnection conn = new SqlConnection();
        global global_class = new global();
        init init_class = new init();
        FileIniDataParser parser = new FileIniDataParser();
        SqlDataAdapter sql_adp = new SqlDataAdapter();
        IniData data;

        //Connect To Database
        public bool openConn(string datasource, string database, string user, string password)
        {
            try
            {
                conn.ConnectionString = connString(datasource, database, user, password);
                return true;
            }
            catch
            {
                return false;
            }
            //conn.Open();
        }

        //Terminate Connection
        public void closeConn()
        {
            conn.Close();
            conn.Dispose();
        }

        //Start Query, return query result in dataset

        public String connString(string datasource, string database, string user, string password)
        {
            String dataSource = "Data Source=" + datasource + "; ";
            String dataBase = "Initial Catalog=" + database + "; ";
            String userName = "User=" + user + "; ";
            String passWord = "PASSWORD=" + global_class.DycryptPass(password) + "; ";
            String strConn;

            strConn = dataSource + dataBase + userName + passWord + "MultipleActiveResultSets=True";

            return strConn;
        }


        public DataSet server_Conn()
        {
            DataSet queryResults = new DataSet();
            SqlDataAdapter adapt = new SqlDataAdapter(server_conn_select(), conn.ConnectionString);
            adapt.Fill(queryResults);

            return queryResults;
        }

        public Boolean update_server(String pc_name, String ipaddress, String mw_ver, String msg)
        {
            try
            {
                DataSet queryResults = new DataSet();
                SqlDataAdapter adapt = new SqlDataAdapter(server_conn_select(), conn.ConnectionString);
                adapt.Fill(queryResults);

                DataRow new_row = queryResults.Tables[0].NewRow();

                new_row[0] = pc_name;
                new_row[1] = ipaddress;
                new_row[2] = mw_ver;
                new_row[3] = DateTime.Now;
                new_row[4] = msg;

                queryResults.Tables[0].Rows.Add(new_row);
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(adapt);
                adapt.Update(queryResults);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean client_Conn()
        {
            DataSet queryResults = new DataSet();
            SqlDataAdapter adapt = new SqlDataAdapter(client_conn_select(), conn.ConnectionString);
            adapt.Fill(queryResults);
            int row_count =  queryResults.Tables[0].Rows.Count;

            if (row_count > 0)
            {
                queryResults.Tables[0].Rows[0].BeginEdit();
                queryResults.Tables[0].Rows[0][2] = DateTime.Now;
                queryResults.Tables[0].Rows[0][5] = "Connected";
                queryResults.Tables[0].Rows[0].EndEdit();
                
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(adapt);
                adapt.Update(queryResults);

                return true;
            }
            else
            {
                DataTable live_table = queryResults.Tables[0];
                DataRow new_row = live_table.NewRow();

                new_row[0] = init_class.PCname();
                new_row[1] = init_class.GetLocalIPAddress();
                new_row[2] = DateTime.Now;
                new_row[3] = init_class.GetMWVer();
                new_row[4] = "1.02.05";
                new_row[5] = "Connected";

                live_table.Rows.Add(new_row);
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(adapt);
                adapt.Update(queryResults);

                return true;
            }
        }

        public Boolean transferData()
        {
            DataSet queryResults = new DataSet();
            SqlDataAdapter adapt = new SqlDataAdapter(client_conn_select(), conn.ConnectionString);
            adapt.Fill(queryResults);
            int row_count = queryResults.Tables[0].Rows.Count;

            if (row_count > 0)
            {
                queryResults.Tables[0].Rows[0].BeginEdit();
                queryResults.Tables[0].Rows[0][2] = DateTime.Now;
                queryResults.Tables[0].Rows[0][5] = "Connected";
                queryResults.Tables[0].Rows[0].EndEdit();

                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(adapt);
                adapt.Update(queryResults);

                return true;
            }
            else
            {   
                DataTable live_table = queryResults.Tables[0];
                DataRow new_row = live_table.NewRow();

                new_row[0] = init_class.PCname();
                new_row[1] = init_class.GetLocalIPAddress();
                new_row[2] = DateTime.Now;
                new_row[3] = init_class.GetMWVer();
                new_row[4] = "1.02.05";
                new_row[5] = "Connected";

                live_table.Rows.Add(new_row);
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(adapt);
                adapt.Update(queryResults);

                return true;
            }
        }

        public String client_conn_select()
        {
            return "SELECT * FROM jimUdtLive AS j WHERE j.PC_NAME = '"+ init_class.PCname() +"' AND j.IP_ADDRESS = '"+ init_class.GetLocalIPAddress() + "';";
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public String server_conn_select()
        {
            return "SELECT * FROM jimUdtHist";
        }

        public String getAllNotTRansferred()
        {
            return "select * from jimScnTrx J where J.scnDbCopy IS NULL and J.scnDTCopy IS NOT NULL";
        }

        public String getAllData()
        {
            return "select * from jimScnTrx";
        }

        public String getLoc()
        {
            var parser = new FileIniDataParser();
            data = parser.ReadFile("app_set.ini");

            return "select * from jimScnTrx where scnIDLoca <> '"+data["HITDATA"]["AIR_LOC"]+"'";
        }

        public String getAllDataNotTransferred()
        {
            return "select * from jimScnTrx where scnDTCopy IS NULL";
        }

        public String getAllDataTransferred()
        {
            return "select TOP 30000 * from jimScnTrx where scnDTCopy IS NOT NULL AND scnDTSync IS NULL";
        }

        public string getDuplicate()
        {
            string sqry;

            sqry = "SELECT * FROM jim_Svr.dbo.jimScnTrx WHERE scnRnID NOT IN (SELECT MIN(scnRnID) FROM jim_Svr.dbo.jimScnTrx GROUP BY scnChNme, scnChDoN, scnIdCIOB, scnJiUsrID)";
            return (sqry);
        }

        public DataSet duplicateData()
        {
            DataSet pset;
            try
            {
                var parser = new FileIniDataParser();
                data = parser.ReadFile("app_set.ini");

                SqlConnection Reconn = new SqlConnection();
                Reconn.ConnectionString = connString(data["DATABASE"]["DATA SOURCE"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);
                pset = new DataSet();
                sql_adp = new SqlDataAdapter(getDuplicate(), Reconn.ConnectionString);
                sql_adp.SelectCommand.CommandTimeout = 3600;
                sql_adp.Fill(pset);

                return pset;
            }catch(Exception e)
            {
                pset = null;
                return pset;
            }
        }

        public DataSet verifyLoc()
        {
            DataSet pset;
            try
            {
                var parser = new FileIniDataParser();
                data = parser.ReadFile("app_set.ini");

                SqlConnection Reconn = new SqlConnection();
                Reconn.ConnectionString = connString(data["DATABASE"]["DATA SOURCE"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);
                pset = new DataSet();
                sql_adp = new SqlDataAdapter(getLoc(), Reconn.ConnectionString);
                sql_adp.SelectCommand.CommandTimeout = 3600;
                sql_adp.Fill(pset);

                return pset;
            }
            catch (Exception e)
            {
                pset = null;
                return pset;
            }
        }

        public DataSet verifyData(string mode)
        {
            DataSet pset;
            try
            {
                var parser = new FileIniDataParser();
                data = parser.ReadFile("app_set.ini");

                SqlConnection Reconn = new SqlConnection();
                if(mode == "regional")
                {
                    Reconn.ConnectionString = connString(data["DATABASE"]["DATA SOURCE"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);
                }
                else
                {
                    Reconn.ConnectionString = connString(data["NETWORK"]["HQ1"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);
                }
                pset = new DataSet();
                sql_adp = new SqlDataAdapter(getAllDataNotTransferred(), Reconn.ConnectionString);
                sql_adp.SelectCommand.CommandTimeout = 3600;
                sql_adp.Fill(pset);

                return pset;
            }
            catch (Exception e)
            {
                pset = null;
                return pset;
            }
        }

        public DataSet serverdata()
        {
            DataSet pset;
            try
            {
                var parser = new FileIniDataParser();
                data = parser.ReadFile("app_set.ini");

                SqlConnection Reconn = new SqlConnection();
                Reconn.ConnectionString = connString(data["NETWORK"]["HQ1"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);
                pset = new DataSet();
                sql_adp = new SqlDataAdapter(getAllData(), Reconn.ConnectionString);
                sql_adp.SelectCommand.CommandTimeout = 3600;
                sql_adp.Fill(pset);

                return pset;
            }
            catch (Exception e)
            {
                pset = null;
                return pset;
            }
        }

        public Boolean transferHitdata()
        {
            try
            {
                var parser = new FileIniDataParser();
                data = parser.ReadFile("app_set.ini");

                SqlConnection HQconn = new SqlConnection();
                SqlConnection Reconn = new SqlConnection();
                HQconn.ConnectionString = connString(data["NETWORK"]["HQ1"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);
                Reconn.ConnectionString = connString(data["DATABASE"]["DATA SOURCE"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);

                DataSet HQResults = new DataSet();
                SqlDataAdapter HQadapt = new SqlDataAdapter(getAllData(), HQconn.ConnectionString);
                HQadapt.Fill(HQResults);

                DataSet REResults = new DataSet();
                SqlDataAdapter REadapt = new SqlDataAdapter(getAllNotTRansferred(), Reconn.ConnectionString);
                REadapt.Fill(REResults);

                int count = 1;
                int total = 0;

                
                if (HQResults.Tables[0].Rows.Count != 0)
                {
                    DataRow tempRow = HQResults.Tables[0].Rows[HQResults.Tables[0].Rows.Count - 1];
                    total = int.Parse(tempRow[0].ToString());
                }

                foreach (DataRow row in REResults.Tables[0].Rows)
                {
                    DataRow newrow = HQResults.Tables[0].NewRow();

                    newrow.ItemArray = row.ItemArray;
                    newrow[0] = count + total;
                    row[46] = 'Y';

                    HQResults.Tables[0].Rows.Add(newrow);

                    count++;
                }
                SqlCommandBuilder hqComm = new SqlCommandBuilder(HQadapt);
                SqlCommandBuilder reComm = new SqlCommandBuilder(REadapt);

                HQadapt.Update(HQResults);
                REadapt.Update(REResults);

                HQadapt.Dispose();
                REadapt.Dispose();

                HQadapt = null;
                REadapt = null;

                return true;
            }catch(Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return false;
            }
        }

        public DataSet transferHitdataZip(String transState)
        {
            try
            {
                var parser = new FileIniDataParser();
                data = parser.ReadFile("app_set.ini");

                SqlConnection Reconn = new SqlConnection();
                Reconn.ConnectionString = connString(data["DATABASE"]["DATA SOURCE"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);

                DataSet REResults = new DataSet();

                if (transState == "purge")
                {
                    sql_adp = new SqlDataAdapter(getAllDataTransferred(), Reconn.ConnectionString);
                }
                else if(transState == "manual")
                {
                    sql_adp = new SqlDataAdapter(getAllDataNotTransferred(), Reconn.ConnectionString);
                }
                sql_adp.Fill(REResults);

                return REResults;
            }
            catch (Exception e)
            {
                DataSet REResults = null;
                
                return REResults;
            }
        }

        public void updateData(DataSet pset)
        {
            SqlCommandBuilder build = new SqlCommandBuilder(sql_adp);
            sql_adp.UpdateCommand = build.GetUpdateCommand();
            sql_adp.UpdateCommand.CommandTimeout = 3600;
            sql_adp.Update(pset);
        }

        public Boolean transferToHq(DataSet pset)
        {
            try
            {
                var parser = new FileIniDataParser();
                data = parser.ReadFile("app_set.ini");

                SqlConnection HQconn = new SqlConnection();
                HQconn.ConnectionString = connString(data["NETWORK"]["HQ1"], data["MW"]["CATALOG"], data["DATABASE"]["USER"], data["DATABASE"]["PASSWORD"]);

                DataSet HQResults = new DataSet();
                SqlDataAdapter HQadapt = new SqlDataAdapter(getAllData(), HQconn.ConnectionString);
                HQadapt.Fill(HQResults);

                int count = 1;
                int total = 0;


                if (HQResults.Tables[0].Rows.Count != 0)
                {
                    DataRow tempRow = HQResults.Tables[0].Rows[HQResults.Tables[0].Rows.Count - 1];
                    total = int.Parse(tempRow[0].ToString());
                }

                foreach (DataRow row in pset.Tables[0].Rows)
                {
                    DataRow newrow = HQResults.Tables[0].NewRow();

                    newrow.ItemArray = row.ItemArray;
                    newrow[0] = count + total;

                    HQResults.Tables[0].Rows.Add(newrow);

                    count++;
                }
                SqlCommandBuilder hqComm = new SqlCommandBuilder(HQadapt);

                HQadapt.Update(HQResults);

                HQadapt.Dispose();

                HQadapt = null;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void disposeAdapter()
        {
            sql_adp.Dispose();
            sql_adp = null;
        }
    }
}
