using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLogic.TraasWS;
using System.ServiceModel;

namespace Register1.Class
{
    public static class cLogic
    {
        public static bool RequestRegisterVehicle(cVehicle pVozilo, List<cRoute> pRoutesXML, ref string pAddresObj, string pAddress = "")
        {
            ServiceLogic.TraasWS.ServiceImplClient lClient = new ServiceLogic.TraasWS.ServiceImplClient();
            SetEndpoit(ref lClient, pAddress, ref pAddresObj);
            if (pVozilo != null)
            {
                cRoute lRoute_obj = pVozilo.Route;
                if (lRoute_obj != null)
                {
                    if (pVozilo.Type != null)
                    {
                        if(lRoute_obj.Edges == null)
                        {
                            return false;
                        }
                        else if (lRoute_obj.Edges.Count() == 0)
                        {
                            return false;
                        }
                        string[] lRoute = new string[lRoute_obj.Edges.Count()];
                        if (cNonServiceLogic.CheckIfRouteIsValid(pRoutesXML, lRoute_obj))
                        {
                            for(int i = 0; i < lRoute_obj.Edges.Count(); i++)
                            {
                                lRoute[i] = lRoute_obj.Edges[i];
                            }
                        }
                        if (cNonServiceLogic.CheckIfID_ExistsOnSumo(lRoute_obj.Id, lClient.Route_getIDList()))
                        {
                            lRoute_obj.Id += "_1";
                        }
                        if (!cNonServiceLogic.CheckIfID_ExistsOnSumo(lRoute_obj.Id, lClient.Route_getIDList()) 
                            && !cNonServiceLogic.CheckIfID_ExistsOnSumo(pVozilo.SerijskaStevilka,lClient.Vehicle_getIDList()))
                        {
                            double lSpeed = 0;
                            if (pVozilo.Type.MaxSpeed != "0")
                            {
                                if (!double.TryParse(pVozilo.Type.MaxSpeed,out lSpeed))
                                {
                                    lSpeed = 10;
                                }
                            }
                            lClient.Route_add(lRoute_obj.Id,lRoute);
                            lClient.Vehicle_add(pVozilo.SerijskaStevilka, pVozilo.Type.Id, lRoute_obj.Id, 0, 0.0,
                                lSpeed, 0);
                            return true;
                        }else
                        {
                            cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Vozilo je še na SUMO");
                        }
                    }
                    else
                    {
                        cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Tip vozila ne obstaja");
                    }
                }
                else
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Ruta vozila ne obstaja");
                }
            }
            else
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Vozilo ne obstaja");
            }
            return false;
        }
        public static bool IsVehicleInSumo(string pVehicleID, ref string pAdressObj, string pAddress = "")
        {
            try
            {
                ServiceLogic.TraasWS.ServiceImplClient lClient = new ServiceLogic.TraasWS.ServiceImplClient();
                cLogic.SetEndpoit(ref lClient, pAddress, ref pAdressObj);

                string[] lIDs = lClient.Vehicle_getIDList();
                foreach (string Str in lIDs)
                {
                    if (Str == pVehicleID)
                    {
                        return true;
                    }
                }
                //string b = "1";
                //object obj = lClient.Vehicle_getPosition(pVehicleID);
                return false;
            }
            catch(Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
                return false;
            }
           
        }
        public static bool RouteExists(cRoute pRoute,ref string pAddressObj, ref List<cRoute> pRoutes,
            ref List<cVehicle> pVehicles_sumo, string pAddress="")
        { 
            ServiceLogic.TraasWS.ServiceImplClient lClient = new ServiceLogic.TraasWS.ServiceImplClient();
            SetEndpoit(ref lClient, pAddress, ref pAddressObj);

            string[] lEdges = lClient.Route_getIDList();
            cNonServiceLogic.LoadRoutesFromXML(ref pRoutes,ref pVehicles_sumo);
            foreach (string s in lEdges)
            {
                if (pRoutes.Exists(y => y.Id == pRoute.Id))
                {
                    return true;
                }
            }
            return false;
        }
        public static cRoute ShortestRoute(List<cRoute> pXMLRoutes, string pStart, string pEnd)
        {
            List<cRoute> tempXMLwithEdges = pXMLRoutes.FindAll(x => x.Edges[0].Contains(pStart) && x.Edges[x.Edges.Count() - 1].Contains(pEnd));
            //tempXMLwithEdges = pXMLRoutes.FindAll(x => x.Edges[0].Contains(pStart));
            //tempXMLwithEdges = pXMLRoutes.FindAll(x => x.Edges[x.Edges.Count() - 1].Contains(pEnd));
            if(tempXMLwithEdges == null)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Ne obstaja ruta!");
                return null;
            }
            else
            {
                if(tempXMLwithEdges.Count == 0)
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Ne obstaja ruta!");
                    return null;
                }
            }
            double lShortest = Double.MaxValue;
            foreach (cRoute lRoute in tempXMLwithEdges)
            {
                if (lRoute.Edges.Count() <= lShortest)
                {
                    lShortest = lRoute.Edges.Count();
                }
            }
            List<cRoute> temp = new List<cRoute>();
            foreach (cRoute lRoute in tempXMLwithEdges)
            {
                if (lRoute.Edges.Count == lShortest)
                {
                    temp.Add(lRoute);
                }
            }

            return temp[0];
        }
        public static void SetEndpoit(ref ServiceImplClient pClient, string pAddress, ref string pAddressObj)
        {
            try
            {
                if (pClient != null)
                {
                    if (pAddress != null)
                    {
                        pClient.Endpoint.Address = new EndpointAddress(pAddress);
                        pAddressObj = pAddress;
                    }
                    else
                    {
                        pClient.Endpoint.Address = new EndpointAddress("http://127.0.0.1:8080/TRAAS_WS");
                    }
                }
            }
            catch(Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, lEx.Message);
            }        
        }

        public static bool DeleteVehicle(string pSerijskaSt,ref string pAddressObj, string pAddress)
        {
            try
            {
                ServiceLogic.TraasWS.ServiceImplClient lClient = new ServiceLogic.TraasWS.ServiceImplClient();
                cLogic.SetEndpoit(ref lClient, pAddress, ref pAddressObj);
                lClient.Vehicle_remove(pSerijskaSt, 0);
                return true;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Napaka-brisanje vozila z sumota");
                return false;
            }
        }
        public static List<cPerson> CreatePerson(string pTRR, ref List<cPerson> pPersons, string pIme = "", string pPriimek = "")
        {
            try
            {
                if (pTRR == "")
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Ni mogoče storiti osebo; nedostaten TRR!");
                    return null;
                }
                if (pPersons == null)
                {
                    pPersons = new List<cPerson>();
                }
                pPersons.Add(new cPerson(pTRR, pIme, pPriimek));
                return pPersons;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return null;
            }
        }
        public static bool VehiclePerson(ref List<cVehicle> pVehicles, string pTrr, string pSerijskaST)
        {
            try
            {
                if (pTrr == "" || pSerijskaST == "")
                {
                    cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "TRR ali seriska stevila ne obstajajo!");
                    return false;
                }
                foreach(cVehicle lVehicle in pVehicles)
                {
                    if(lVehicle.SerijskaStevilka==pSerijskaST)
                    {
                        lVehicle.PersonID = pTrr;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return false;
            }
        }
    }
}
