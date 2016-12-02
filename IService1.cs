using Register1.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Register1
{
    [ServiceContract(Name = "Reg1", Namespace = "sua.feri.um/Register1/v2")]
    public interface IService1
    {
        [OperationContract(Name = "Register1_CreateVehicle")]
        List<cVehicle> Register1_CreateVehicle(string pKey, string pTypeID, string pTRR, string pIP = "");

        [OperationContract(Name = "Register1_RequestRegisterVehicle")]
        bool Register1_RequestRegisterVehicle(cVehicle pVozilo, string pIP = "");

        [OperationContract(Name = "Register1_DajTipeVozila")]
        List<cType> Register1_ReturnTypes();
        [OperationContract(Name = "Register1_IFVoziloNaSumo")]
        bool Register1_IsVehicleInSumo(string pVehicleID, string pIP = "");

        [OperationContract(Name = "Register1_RouteExists")]
        bool Register1_RouteExists(cRoute pRoute, string pIP);
        [OperationContract(Name = "Register1_ShortestRoute")]
        cRoute Register1_ShortestRoute(cRoute pXmlRoutes, string pStart, string pEnd);
        [OperationContract(Name = "Register1_DeleteVehicle")]
        bool Register1_DeleteVehicle(string pSerijskaSt, string pIpAddressP = "");

        [OperationContract(Name = "Register1_GetVehicleType")]
        cType Register1_GetVehicleType(string pSerijskaSt);

        [OperationContract(Name = "Register1_CreatePerson")]
        List<cPerson> Register1_CreatePerson(string pTRR, string pIme = "", string pPriimek = "");

        [OperationContract(Name = "Register1_VehiclePerson")]
        bool Register1_VehiclePerson(string pTRR, string pSerijskaST);

    }
}
