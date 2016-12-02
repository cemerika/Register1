using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Register1.Class
{
    [DataContract(Name = "cRoute", Namespace = "sua.feri.um/Register1/v2")]
    public class cRoute
    {
        private string id;
        private List<string> edges;
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
        public List<string> Edges
        {
            get
            {
                return edges;
            }

            set
            {
                edges = value;
            }
        }
    }
}
