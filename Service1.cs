using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Register1.Class;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using Register1.TraasWS;

namespace Register1
{

    public class Service1 : IService1
    {
        List<cPerson> mPersons;

        static public List<cType> mTypes;
        List<cVehicle> mVehicles;
        List<cRoute> mRoutesXML;

        List<cVehicle> mVehicles_sumo;
        string mIP = "";
      

        public List<cVehicle> Register1_CreateVehicle(string pKey, string pTypeID, string pTRR, string pAddress = "")
        {

            //cConnect lc = new cConnect();
            //lc.Connect();

            cNonServiceLogic.Init(ref mTypes,ref mRoutesXML, ref mVehicles_sumo);       
            try
            {
                int mNumberOfExistsingCars = cNonServiceLogic.returnCarsNumber(mVehicles);
                if (mVehicles == null)
                {
                    mVehicles = new List<cVehicle>();
                }
                cVehicle lVozilo = new cVehicle("", pTypeID, pTRR, mTypes, mNumberOfExistsingCars);
                mVehicles.Add(lVozilo);
                return mVehicles;
            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return null;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return null;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
                return null;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return null;
            }
        }
        public cRoute Register1_ShortestRoute(cRoute pXmlRoutes, string pStart, string pEnd)
        {
            return cLogic.ShortestRoute(mRoutesXML, pStart, pEnd);
        }
        public bool Register1_RequestRegisterVehicle(cVehicle pVozilo, string pAddress = "")
        {
            try 
            {

                if (mVehicles == null)
                {
                    mVehicles = new List<cVehicle>();
                }
              
                cNonServiceLogic.Init(ref mTypes,ref  mRoutesXML,ref mVehicles_sumo);
                pVozilo = new cVehicle("blabla0",  mTypes[0].Id, "000-111", mTypes, mVehicles.Count());
                pVozilo.Route = mRoutesXML[10];
                mVehicles.Add(pVozilo);
                cLogic.RequestRegisterVehicle(pVozilo, mRoutesXML, ref mIP, pAddress);

                //cNonServiceLogic.Init(ref mTypes, ref mRoutesXML, ref mVehicles_sumo);
                //pVozilo = new cVehicle("blabla1", mTypes[1].Id, "000-111", mTypes,  mVehicles.Count());
                //pVozilo.Route = mRoutesXML[10];
                //cLogic.RequestRegisterVehicle(pVozilo, mRoutesXML, ref mIP, pAddress);
                //mVehicles.Add(pVozilo);
                //cNonServiceLogic.Init(ref mTypes, ref mRoutesXML, ref mVehicles_sumo);
                //pVozilo = new cVehicle("blabla2", mTypes[2].Id, "000-111", mTypes, mVehicles.Count());
                //pVozilo.Route = mRoutesXML[10];
                //cLogic.RequestRegisterVehicle(pVozilo, mRoutesXML, ref mIP, pAddress);
                //mVehicles.Add(pVozilo);
                //cNonServiceLogic.Init(ref mTypes, ref mRoutesXML, ref mVehicles_sumo);
                //pVozilo = new cVehicle("blabla3", mTypes[3].Id, "000-111", mTypes, mVehicles.Count());
                //pVozilo.Route = mRoutesXML[10];
                //cLogic.RequestRegisterVehicle(pVozilo, mRoutesXML, ref mIP, pAddress);
                //mVehicles.Add(pVozilo);
                //cNonServiceLogic.Init(ref mTypes, ref mRoutesXML, ref mVehicles_sumo);
                //pVozilo = new cVehicle("blabla4", mTypes[4].Id, "000-111", mTypes, mVehicles.Count());
                //pVozilo.Route = mRoutesXML[10];
                //cLogic.RequestRegisterVehicle(pVozilo, mRoutesXML, ref mIP, pAddress);
                //mVehicles.Add(pVozilo);
                //cNonServiceLogic.Init(ref mTypes, ref mRoutesXML, ref mVehicles_sumo);
                //pVozilo = new cVehicle("blabla5", mTypes[5].Id, "000-111", mTypes, mVehicles.Count());
                //pVozilo.Route = mRoutesXML[10];
                //cLogic.RequestRegisterVehicle(pVozilo, mRoutesXML, ref mIP, pAddress);
                //mVehicles.Add(pVozilo);
                //cNonServiceLogic.Init(ref mTypes, ref mRoutesXML, ref mVehicles_sumo);
                //pVozilo = new cVehicle("blabla6", mTypes[6].Id, "000-111", mTypes, mVehicles.Count());
                //pVozilo.Route = mRoutesXML[10];
                //cLogic.RequestRegisterVehicle(pVozilo, mRoutesXML, ref mIP, pAddress);
                //mVehicles.Add(pVozilo);


                return true;
            
            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return false;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return false;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
                return false;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return false;
            }
        }

        public List<cType> Register1_ReturnTypes()
        {
            try
            {
                if (mTypes != null)
                {
                    return mTypes;
                }
                else
                {
                    cNonServiceLogic.LoadTypesFromXML(ref mTypes);
                    return mTypes;
                }

            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return null;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return null;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
                return null;
            }
        }
        public bool Register1_IsVehicleInSumo(string pVehicleID, string pAddress = "")
        {
            try
            {
                return cLogic.IsVehicleInSumo(pVehicleID, ref mIP, pAddress);
            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return false;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return false;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
                return false;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return false;
            }
        }
        public bool Register1_RouteExists(cRoute pRoute, string pAddress)
        {
            try
            {
                return cLogic.RouteExists(pRoute, ref mIP,ref mRoutesXML, ref mVehicles_sumo, pAddress);

            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return false;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return false;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
                return false;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return false;
            }
        }

        public bool Register1_DeleteVehicle(string pSerijskaSt,string pAddress)
        {
            try
            {
                if (mVehicles != null)
                {
                    if (mVehicles.Count > 0)
                    {
                        if(cLogic.IsVehicleInSumo(pSerijskaSt,ref mIP, pAddress))
                        {
                            cLogic.DeleteVehicle(pSerijskaSt, ref mIP, pAddress);
                        }
                        mVehicles = mVehicles.Where(x => x.SerijskaStevilka != pSerijskaSt).ToList();
                        return true;
                    }
                }
                return false;
            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return false;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return false;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
                return false;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return false;
            }

        }
        public cType Register1_GetVehicleType(string pSerijskaSt)
        {
            try
            {
                cType lType = cNonServiceLogic.GetVehicleType(mVehicles, pSerijskaSt);
                if (lType != null)
                {
                    return lType;
                }
                cNonServiceLogic.ThrowNapaka(new StackTrace().GetFrame(1).GetMethod().Name, "Vozilo ne obstaja!");
                return null;
            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return null;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return null;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
                return null;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return null;
            }
        }
        public List<cPerson> Register1_CreatePerson(string pTRR, string pIme = "", string pPriimek = "")
        {
            try
            {
                return cLogic.CreatePerson(pTRR,ref mPersons, pIme, pPriimek);
            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return null;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return null;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
                return null;
            }
            catch (Exception lEx)
            {
                cNonServiceLogic.ThrowNapaka(lEx.StackTrace, lEx.Message);
                return null;
            }
        }
        public bool Register1_VehiclePerson(string pTRR, string pSerijskaST)
        {
            try
            {
                return cLogic.VehiclePerson(ref mVehicles, pTRR, pSerijskaST);
            }
            catch (TimeoutException lTimeNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lTimeNapaka.StackTrace, lTimeNapaka.Message);
                return false;
            }
            catch (FaultException<FaultNapaka> lFENapaka)
            {
                cNonServiceLogic.ThrowNapaka(lFENapaka.StackTrace, lFENapaka.Message);
                return false;
            }
            catch (CommunicationException lComNapaka)
            {
                cNonServiceLogic.ThrowNapaka(lComNapaka.StackTrace, lComNapaka.Message);
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
