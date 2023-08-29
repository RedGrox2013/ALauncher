using System.Xml;

namespace ModsManager
{
    public class Mod
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        private string? _unique;
        public string Unique
        {
            get => _unique ??= "unknown";
            set => _unique = value;
        }
        public string? Description { get; set; }
        private string? _installerSystemVersion;
        public string InstallerSystemVersion
        {
            get => _installerSystemVersion ??= "1.0.1.0";
            set => _installerSystemVersion = value;
        }
        private string? _dllsBuild;
        public string DllsBuild
        {
            get => _dllsBuild ??= "2.5.20";
            set => _dllsBuild = value;
        }
        public bool HasCustomInstaller { get; set; }
        public bool IsExperimental { get; set; }
        public bool RequiresGalaxyReset { get; set; }
        public bool CausesSaveDataDependency { get; set; }

        private List<Prerequisite>? _prerequisites;
        public int PrerequisitesCount => _prerequisites == null ? 0 : _prerequisites.Count;
        public void AddPrerequisite(Prerequisite prerequisite)
        {
            _prerequisites ??= new List<Prerequisite>();
            _prerequisites.Add(prerequisite);
        }
        public Prerequisite GetPrerequisiteAt(int index)
        {
            if (_prerequisites == null || index < 0 || index >= PrerequisitesCount)
                throw new ArgumentOutOfRangeException(nameof(index));
            return _prerequisites[index];
        }

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
                        mod.Unique = reader.GetAttribute("unique") ?? mod.Unique;
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
                    else if (reader.Name == "prerequisite")
                    {
                        mod.AddPrerequisite(new Prerequisite
                        {
                            Game = reader.GetAttribute("game"),
                            Files = reader.ReadElementContentAsString().Split('?')
                        });
                        continue;
                    }
                    // доделать compatFile
                }
                reader.Read();
            }

            return mod;
        }

        private static bool GetAttributeValue(XmlReader reader, string attributeName, bool defaultValue = default)
        {
            var value = reader.GetAttribute(attributeName);
            if (bool.TryParse(value, out bool result))
                return result;
            return defaultValue;
        }

        public override string ToString()
        {
            if (Name == null && DisplayName == null)
                return Unique;

            return $"{Unique} ({DisplayName ?? Name})";
        }
    }
}