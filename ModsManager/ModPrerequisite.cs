using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModsManager
{
    [Serializable]
    public struct ModPrerequisite
    {
        [XmlAttribute("game")]
        public string Game { get; set; }
        [XmlText]
        public string File { get; set; }
    }
}
