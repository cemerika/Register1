using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Register1.Class
{
    [DataContract(Name = "CustomNapaka", Namespace = "sua.feri.um/Register1/v2")]
    public class FaultNapaka
    {
        string operation = "";
        string message = "";
        [DataMember(Name = "OperacijaNapaka")]
        public string Operation
        {
            get
            {
                return operation;
            }

            set
            {
                operation = value;
            }
        }
        [DataMember(Name = "Poruka")]
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }
    }
}
