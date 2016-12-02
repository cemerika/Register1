using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Register1.Class
{
    [DataContract(Name ="cVehicle", Namespace = "sua.feri.um/Register1/v2")]
    public class cVehicle
    {
        private cType type;
        private string serijskaStevilka;
        private string personID;
        private cRoute route;
        [DataMember]
        public cType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
        [DataMember]
        public string SerijskaStevilka
        {
            get
            {
                return serijskaStevilka;
            }

            set
            {
                serijskaStevilka = value;
            }
        }
        [DataMember]
        public string PersonID
        {
            get
            {
                return personID;
            }

            set
            {
                personID = value;
            }
        }
        [DataMember]
        public cRoute Route
        {
            get
            {
                return route;
            }

            set
            {
                route = value;
            }
        }
        public cVehicle()
        {

        }
        public cVehicle(string pNazivSkupine, string pTypeID, string pTRR, List<cType> pTypes, int pNumberOfExistsingCars = 0)
        {
            //pNazivSKupine ne treba kak paramtere-> prek key-a za prijavo id==serijskastevilka
            string lNule = "";
            switch (pNumberOfExistsingCars.ToString().Length)
            {
                case 1:
                    lNule = "0000";
                    break;
                case 2:
                    lNule = "000";
                    break;
                case 3:
                    lNule = "00";
                    break;
                case 4:
                    lNule = "0";
                    break;
                default:
                    break;
            }
            
            cType lType = pTypes.Find(x => x.Id == pTypeID);
            if (lType != null)
            {
               serijskaStevilka = pNazivSkupine + "_" + lType.Name + "_" + "_" + lNule + (pNumberOfExistsingCars + 1).ToString();
            }
            else
            {
                throw new Exception("Tip vozila ne obstaja pri dodajanju");
            }
            type = lType;
            personID = pTRR;
        }
    }
}
