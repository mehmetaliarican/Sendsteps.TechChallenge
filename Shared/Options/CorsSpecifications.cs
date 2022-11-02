using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Options
{

    public class CorsSpecifications
    {
        public string Name { get; set; }
        public string[] Methods { get; set; }
        public string[] Origins { get; set; }
        public string[] Headers { get; set; }
        public bool AllowAnyHeader { get; set; }
        public bool AllowAnyMethod { get; set; }
        public bool AllowAnyOrigin { get; set; }
    }


}
