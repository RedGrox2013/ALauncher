using System.Xml;

namespace ModsManager
{
    public class Mod
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Unique { get; set; }
        public string? Description { get; set; }
        public string InstallerSystemVersion { get; set; } = "1.0.1.0";
        public string DllsBuild { get; set; } = "2.5.20";
        public bool HasCustomInstaller { get; set; }
        public bool IsExperimental { get; set; }
        public bool RequiresGalaxyReset { get; set; }
        public bool CausesSaveDataDependency { get; set; }

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
                        mod.Unique = reader.GetAttribute("unique");
                        mod.Name = reader.GetAttribute("name") ?? mod.Unique;
                        mod.DisplayName = reader.GetAttribute("displayName") ?? mod.Name;
                        mod.Description = reader.GetAttribute("description");
                        var installerSystemVersion = reader.GetAttribute("installerSystemVersion");
                        if (installerSystemVersion != null) mod.InstallerSystemVersion = installerSystemVersion;
                        var dllsBuild = reader.GetAttribute("dllsBuild");
                        if (dllsBuild != null) mod.DllsBuild = dllsBuild;
                        mod.HasCustomInstaller = GetAttributeValue(reader, "hasCustomInstaller");
                        mod.IsExperimental = GetAttributeValue(reader, "isExperimental");
                        mod.RequiresGalaxyReset = GetAttributeValue(reader, "requiresGalaxyReset");
                        mod.CausesSaveDataDependency = GetAttributeValue(reader, "causesSaveDataDependency");
                    }
                    reader.Read();
                }
                else
                    reader.Read();
            }

            return mod;
        }

        private static bool GetAttributeValue(XmlReader reader, string attributeName, bool defaultValue = default)
        {
            var value = reader.GetAttribute(attributeName);
            if (value != null)
                return bool.Parse(value);
            return defaultValue;
        }
    }
}