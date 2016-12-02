using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Register1.Class
{
    [DataContract(Name = "cType", Namespace = "sua.feri.um/Register1/v2")]
    public class cType
    {
        string id;
        string name;
        string color;
        string accel;
        string decel;
        string sigma;
        string length;
        string minGap;
        string maxSpeed;
        string guiShape;
        string emissionClass;
        [DataMember]
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        [DataMember]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        [DataMember]
        public string Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }
        [DataMember]
        public string Accel
        {
            get
            {
                return accel;
            }

            set
            {
                accel = value;
            }
        }
        [DataMember]
        public string Decel
        {
            get
            {
                return decel;
            }

            set
            {
                decel = value;
            }
        }
        [DataMember]
        public string Sigma
        {
            get
            {
                return sigma;
            }

            set
            {
                sigma = value;
            }
        }
        [DataMember]
        public string Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }
        [DataMember]
        public string MinGap
        {
            get
            {
                return minGap;
            }

            set
            {
                minGap = value;
            }
        }
        [DataMember]
        public string MaxSpeed
        {
            get
            {
                return maxSpeed;
            }

            set
            {
                maxSpeed = value;
            }
        }
        [DataMember]
        public string GuiShape
        {
            get
            {
                return guiShape;
            }

            set
            {
                guiShape = value;
            }
        }
        [DataMember]
        public string EmissionClass
        {
            get
            {
                return emissionClass;
            }

            set
            {
                emissionClass = value;
            }
        }

        public cType(string id, string name, string color, string accel, string decel, string sigma, string length, string minGap, string maxSpeed, string guiShape, string emissionClass)
        {
            this.Id = id;
            this.Name = name;
            this.Color = color;
            this.Accel = accel;
            this.Decel = decel;
            this.Sigma = sigma;
            this.Length = length;
            this.MinGap = minGap;
            this.MaxSpeed = maxSpeed;
            this.GuiShape = guiShape;
            this.EmissionClass = emissionClass;
        }

        public cType(string pID, string pName)
        {
            this.id = pID;
            this.name = pName;
        }
        public cType()
        {
        }
    }
    [DataContract(Name = "ImeModula", Namespace = "sua.feri.um/Register1/v2")]
    public static class ModuleName
    {
        public static KeyValuePair<int, string> OBU = new KeyValuePair<int, string>(1, "OBU");
        public static KeyValuePair<int, string> Register_OBU = new KeyValuePair<int, string>(2, "Register_OBU");
        public static KeyValuePair<int, string> Pametno_parkirisce = new KeyValuePair<int, string>(3, "Pametno_parkirisce");
        public static KeyValuePair<int, string> DARS = new KeyValuePair<int, string>(4, "DARS");
        public static KeyValuePair<int, string> Prometni_center = new KeyValuePair<int, string>(5, "Prometni_center");
        public static KeyValuePair<int, string> Kontrola_mestnega_prometa = new KeyValuePair<int, string>(6, "Kontrola_mestnega_prometa");
        public static KeyValuePair<int, string> Avtoservis = new KeyValuePair<int, string>(7, "Avtoservis");
        public static KeyValuePair<int, string> Taksi_sluzba = new KeyValuePair<int, string>(8, "Taksi_sluzba");
        public static KeyValuePair<int, string> Share_a_car = new KeyValuePair<int, string>(9, "Share_a_car");
    }
}
