using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopMostApp.Model
{
    public class LangInfo
    {
        
        public string Id { get; set; }

       
        public string NameNative { get; set; }

        
        public string NameEnglish { get; set; }

        
        public string TargetVersion { get; set; }

        public override string ToString()
        {
            return $@"{NameNative} / {NameEnglish} ({Id})";
        }
    }
}
