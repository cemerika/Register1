using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
namespace Register1.Class
{
    public static class cNonServiceLogic
    {
        public static void Init(ref List<cType> pTypes, ref List<cRoute> pRoutes, ref List<cVehicle> pVehicles_sumo)
        {
            cNonServiceLogic.LoadTypesFromXML(ref pTypes);
            cNonServiceLogic.LoadRoutesFromXML(ref pRoutes, ref pVehicles_sumo);
        }
        public static void ThrowNapaka(string pOperation, string pMessage)
        {
            FaultNapaka lFault = new FaultNapaka();
            lFault.Operation = pOperation;
            lFault.Message = pMessage;
            throw new FaultException<FaultNapaka>(lFault);
        }
        public static int returnCarsNumber(List<cVehicle> pVehicles)
        {
            if (pVehicles != null)
            {
                return pVehicles.Count();
            }
            else
            {
                return 0;
            }
        }
        public static void LoadTypesFromXML(ref List<cType> pTypes)
        {
            if(pTypes != null)
            {
                if (pTypes.Count > 0)
                {
                    return;
                }
            }
            if (pTypes == null)
            {
                pTypes = new List<cType>();
            }
            string lPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            lPath += "\\Files\\joined_vtypes.add.xml";
            if (File.Exists(lPath))
            {
                XmlDocument lDoc = new XmlDocument();
                lDoc.Load(lPath);
                int lID_Addon = 1;
                foreach (XmlNode lNode in lDoc.DocumentElement.ChildNodes)
                {
                    if (lNode.OuterXml.Contains("private"))
                    {
                        foreach (XmlElement lElement in lNode)
                        {
                            cType lType;
                            string lNameStrr = lElement.OuterXml;
                            lNameStrr = Regex.Split(Regex.Split(lNameStrr, "id=\"")[1], "\"")[0];
                            if (lNameStrr.Contains("passenger5"))
                            {
                                lType = new cType(lNameStrr, "Taxi");
                            }
                            else if (lNameStrr.Contains("passenger3"))
                            {
                                lType = new cType(lNameStrr, "Share-a-Car");
                            }
                            else
                            {
                                lType = new cType(lNameStrr, "OsebnoVozilo" + lID_Addon);
                                lID_Addon++;
                            }
                            pTypes.Add(lType);
                        }
                    }
                    else if (lNode.OuterXml.Contains("ignoring"))
                    {
                        continue;
                    }
                    else
                    {
                        string lNameStrr = lNode.OuterXml;
                        lNameStrr = Regex.Split(Regex.Split(lNameStrr, "id=\"")[1], "\"")[0];
                        pTypes.Add(new cType(lNameStrr, lNameStrr));
                    }
                }
            }
            else
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Ne obstaja datoteka!");
            }
        }
        public static void CheckDoubleRoutes(ref List<cRoute> pRoutes, ref List<cVehicle> pVehicles_sumo)
        {
            if (pRoutes != null)
            {
                LoadRoutesFromXML(ref pRoutes,ref pVehicles_sumo);
                ServiceLogic.TraasWS.ServiceImplClient lClient = new ServiceLogic.TraasWS.ServiceImplClient();
                string[] lEdges = lClient.Route_getIDList();
                List<cRoute> temp = new List<cRoute>();




            }
        }
        public static void LoadRoutesFromXML(ref List<cRoute> pRoutes, ref List<cVehicle> pVehicles_sumo)
        {
            if (pRoutes != null)
            {
                if (pRoutes.Count > 0)
                {
                    return;
                }
            }
            if (pRoutes == null)
            {
                pRoutes = new List<cRoute>();
                pVehicles_sumo = new List<cVehicle>();
            }
            string lPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            if (File.Exists(lPath += "\\Files\\joined.rou.xml"))
            {
                XmlDocument lDoc = new XmlDocument();
                lDoc.Load(lPath);

                // int lID = 0;
                foreach (XmlNode lNode in lDoc.DocumentElement.ChildNodes)
                {
                    string lRouteStr = lNode.InnerXml;
                    if (lRouteStr == "")
                    {
                        continue;
                    }

                    string lVehicleID = Regex.Split(Regex.Split(lNode.OuterXml, "id=\"")[1], "\"")[0];

                    cVehicle lVehicle = new cVehicle();
                    lVehicle.SerijskaStevilka = lVehicleID;

                    string lTemp = Regex.Split(lRouteStr, "=\"")[1];
                    lTemp = Regex.Split(lTemp, " \" />")[0];

                    cRoute lRoute = new cRoute();
                    //lRoute.Id = "ID_0" + lID.ToString();
                    lRoute.Id = "!" + lVehicleID;

                    lVehicle.Route = lRoute;

                    List<string> lList = new List<string>();
                    foreach (string lS in lTemp.Split(' '))
                    {
                        lList.Add(lS);
                    }

                    lRoute.Edges = lList;
                    pVehicles_sumo.Add(lVehicle);
                    //lID++;
                    pRoutes.Add(lRoute);
                }
            }
        }
        public static bool CheckIfID_ExistsOnSumo(string pID, string[] pIDs)
        {
            foreach (string str in pIDs)
            {
                if (str==pID)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckIfRouteIsValid(List<cRoute> pRoutesXML, cRoute pRoute)
        {
            if (pRoutesXML != null)
            {
                if (pRoute != null)
                {
                    if (pRoutesXML.Count > 0)
                    {                 
                        foreach (cRoute lRoute in pRoutesXML)
                        {
                            string lEdgeRouteXML = "";
                            lEdgeRouteXML = ConnectEdges(lRoute.Edges);
                            string lEdgeRoute = ConnectEdges(pRoute.Edges);
                            if (lEdgeRouteXML == lEdgeRoute)
                            {
                                return true;
                            }
                        }                     
                    }
                }
            }
            return false;
        }
        public static string ConnectEdges(List<string> pEdges)
        {
            string lEdge = "";
            if (pEdges != null)
            {
                foreach (string edge in pEdges)
                {
                    lEdge += edge + " ";
                }
            }
            return lEdge;
        }
        public static cType GetVehicleType(List<cVehicle> pVehicles, string pSerijskaSt)
        {
            foreach (cVehicle lVehicle in pVehicles)
            {
                if (lVehicle.SerijskaStevilka == pSerijskaSt)
                {
                    return lVehicle.Type;
                }
            }
            return null;
        }
    }
}
