using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Register1.Class
{
    [DataContract(Name = "cPerson", Namespace = "sua.feri.um/Register1/v2")]
    public class cPerson
    {
        private string ime;
        private string priimek;
        private string trr;
        [DataMember]
        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                ime = value;
            }
        }
        [DataMember]
        public string Priimek
        {
            get
            {
                return priimek;
            }

            set
            {
                priimek = value;
            }
        }
        [DataMember]
        public string Trr
        {
            get
            {
                return trr;
            }

            set
            {
                trr = value;
            }
        }
        public cPerson(string pIme, string pPriimek, string pTrr)
        {
            ime = pIme;
            priimek = pPriimek;
            trr = pTrr;
        }
        public cPerson()
        {

        }
    }
}
