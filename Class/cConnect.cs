using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Register1.Class
{
    public class cConnect
    {
        private string connection_String = "Data Source=registerobu1.database.windows.net;Initial Catalog=Register1;Integrated Security=False;User ID=register1;Password=ii32134560II;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //ConfigurationManager.ConnectionStrings["Register1Connect"].ConnectionString;
        private SqlConnection Connection;

        public void Connect()
        {
            try
            {
                string aba = ConfigurationManager.ConnectionStrings["Register1Connect"].ConnectionString;
                SqlConnection Connection = new SqlConnection(connection_String);
            }
            catch (SqlException lSqlEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lSqlEx.Message);
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
            }
        }
        public bool Open()
        {
            try
            {
                if (Connection == null)
                {
                    Connect();
                }
                Connection.Open();
                return true;
            }
            catch (SqlException lSqlEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lSqlEx.Message);
                return false;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
                return false;
            }
        }
        public bool Close()
        {
            try
            {
                if (Connection == null)
                {
                    return true;
                }
                Connection.Close();
                return true;
            }
            catch (SqlException lSqlEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lSqlEx.Message);
                return false;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
                return false;
            }
        }
        public bool ExecuteNonQuery(string pSql)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Open();
                }
                SqlCommand lCommand = new SqlCommand(pSql, Connection);
                lCommand.Connection.Open();
                lCommand.ExecuteNonQuery();
                Close();
                return true;
            }
            catch (Exception lEx)
            {
                try
                {
                    ResetConnection();
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Open();
                    }
                    SqlCommand lCommand = new SqlCommand(pSql, Connection);
                    lCommand.Connection.Open();
                    lCommand.ExecuteNonQuery();
                    Close();
                    return true;
                }
                catch (SqlException lSqlEx)
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
                    return false;
                }
                catch (Exception llEx)
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, llEx.Message);
                    return false;
                }
            }
        }
        public DataTable ReadDataTable(string pSql)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Open();
                }
                SqlCommand lCommand = new SqlCommand(pSql, Connection);

                DataTable lDT = new DataTable();
                using (SqlDataAdapter lSqlAdapter = new SqlDataAdapter(lCommand))
                {
                    lSqlAdapter.Fill(lDT);
                }
                Close();
                return lDT;
            }
            catch (Exception lEx)
            {
                try
                {
                    ResetConnection();
                    SqlCommand lCommand = new SqlCommand(pSql, Connection);

                    DataTable lDT = new DataTable();
                    using (SqlDataAdapter lSqlAdapter = new SqlDataAdapter(lCommand))
                    {
                        lSqlAdapter.Fill(lDT);
                    }
                    Close();
                    return lDT;
                }
                catch (SqlException lSqlEx)
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lSqlEx.Message);
                    return null;
                }
                catch (Exception llEx)
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, llEx.Message);
                    return null;
                }
            }
        }
        //not implemented
        public void ExecuteReader(string pSql)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Open();
                }
                SqlCommand lCommand = new SqlCommand(pSql, Connection);
                SqlDataReader reader = lCommand.ExecuteReader();

                while (reader.Read())
                {

                }

            }
            catch (Exception lEx)
            {

            }
        }

        //nit implemented
        public object ExecuteScalar(string pSql)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Open();
                }
                SqlCommand lCommand = new SqlCommand(pSql, Connection);
                return lCommand.ExecuteScalar();

            }
            catch (Exception lEx)
            {
                try
                {
                    ResetConnection();
                    if (Connection.State == ConnectionState.Closed)
                    {
                        Open();
                    }
                    SqlCommand lCommand = new SqlCommand(pSql, Connection);
                    return lCommand.ExecuteScalar();

                }
                catch (SqlException lSqlEx)
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lSqlEx.Message);
                    return null;
                }
                catch (Exception llEx)
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, llEx.Message);
                    return null;
                }
            }
        }

        private void ResetConnection()
        {
            Connection.Close();

            if (Connection == null)
            {
                Connect();
            }

            Connection.Open();
        }
    }
    public static class cQueries
    {
        public static DataTable GetVehiclesDB(string pType_ID = "", string pRouteID = "", string pOsebaID = "", string pSerijskaStevilka = "")
        {
            try
            {
                cConnect lConnect = new cConnect();
                string lSql = "Select * From Vozilo";
                if (pType_ID != "" || pRouteID != "" || pOsebaID != "" || pSerijskaStevilka != "")
                {
                    lSql += " Where ";
                }
                if (pType_ID != "")
                {
                    lSql += " TypeID=" + pType_ID;
                }

                if (pRouteID != "" && pType_ID == "")
                {
                    lSql += " RouteID=" + pRouteID;
                }
                else if (pRouteID != "" && pType_ID != "")
                {
                    lSql += " And RouteID=" + pRouteID;
                }

                if (pOsebaID != "" && (pType_ID == "" && pRouteID == ""))
                {
                    lSql += " OsebaID=" + pOsebaID;

                }
                else if (pOsebaID != "" && (pRouteID != "" || pType_ID != ""))
                {
                    lSql += " And OsebaID=" + pOsebaID;
                }

                if (pSerijskaStevilka != "" && (pOsebaID == "" && pRouteID == "" && pType_ID == ""))
                {
                    lSql += " SerijskaStevilka=" + pSerijskaStevilka;
                }
                else if (pSerijskaStevilka != "" && (pOsebaID != "" || pRouteID != "" || pType_ID != ""))
                {
                    lSql += " AND SerijskaStevilka=" + pSerijskaStevilka;
                }
                return lConnect.ReadDataTable(lSql);
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
                return null;
            }
        }
        public static DataTable GetTypes(string pType_ID = "", string pType_Name = "")
        {
            try
            {
                cConnect lConnect = new cConnect();
                string lSql = "Select * From Type";
                if (pType_ID != "" || pType_Name != "")
                {
                    lSql += " Where ";
                }
                if (pType_Name != "")
                {
                    lSql += " Name like '%" + pType_Name + "%'";
                }
                else if (pType_Name != "" && pType_ID != "")
                {
                    lSql += " And ID_Sumo=" + pType_ID;
                }
                else if (pType_Name == "" && pType_ID != "")
                {
                    lSql += " ID_Sumo=" + pType_ID;
                }
                return lConnect.ReadDataTable(lSql);
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
                return null;
            }
        }
        public static DataTable GetPersons(string pImePriimek = "", string pTRR = "")
        {
            try
            {
                cConnect lConnect = new cConnect();
                string lSql = "Select * From Oseba";
                if (pImePriimek != "" || pTRR != "")
                {
                    lSql += " Where ";
                }
                if (pImePriimek != "")
                {
                    string lIme = "";
                    string lPriimek = "";
                    if (pImePriimek.Split(' ').Count() > 0)
                    {
                        lIme = lPriimek.Split(' ')[0];
                        if (pImePriimek.Split(' ').Count() > 1)
                        {
                            lPriimek = pImePriimek.Split(' ')[1];
                        }
                    }
                    lSql += " Ime like '%" + lIme + "%'";
                    if (lPriimek != "")
                    {
                        lSql += " Priimek like '%" + lPriimek + "%'";
                    }
                }
                else if (pImePriimek != "" && pTRR != "")
                {
                    lSql += " And ID_Sumo=" + pTRR;
                }
                else if (pImePriimek == "" && pTRR != "")
                {
                    lSql += " ID_Sumo=" + pTRR;
                }
                return lConnect.ReadDataTable(lSql);
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
                return null;
            }
        }
        //not implemented
        public static DataTable GetRoutes(string pID = "", string pStart = "", string pStop = "", string pVia = "")
        {
            try
            {
                cConnect lConnect = new cConnect();
                string lSql = "Select * From Route";
                if (pID != "" || pStart != "" || pStop != "" || pVia != "")
                {
                    lSql += " Where ";
                }
                return null;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
                return null;
            }
        }
    }
}
