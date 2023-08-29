using System.Xml;
using System.Xml.Serialization;

namespace ModsManager
{
    public class Mod
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }

        public static Mod ParseXML(string path)
        {
            Mod mod = new();
            using XmlReader reader = XmlReader.Create(path);
            while (!reader.EOF)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "mod")
                    {
                        mod.Name = reader.GetAttribute("name");
                        mod.DisplayName = reader.GetAttribute("displayName") ?? mod.Name;
                    }
                    reader.Read();
                }
                else
                    reader.Read();
            }

            return mod;
        }
    }
}