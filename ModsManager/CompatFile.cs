using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsManager
{
    public struct CompatFile
    {
        public string[]? CompatTargetFileName { get; set; }
        public string? CompatTargetGame { get; set; }
        public string? Game { get; set; }
        public bool RemoveTargets { get; set; }
    }
}
