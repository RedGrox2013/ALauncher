using System.Xml.Serialization;

namespace ModsManager
{
    [Serializable, XmlRoot("mod")]
    public class Mod
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("displayName")]
        public string DisplayName { get; set; }
        [XmlAttribute("unique")]
        public string Unique { get; set; }
        [XmlAttribute("description")]
        public string? Description { get; set; }
        [XmlAttribute("installerSystemVersion")]
        public string InstallerSystemVersion { get; set; }
        [XmlAttribute("hasCustomInstaller")]
        public bool HasCustomInstaller { get; set; }
        [XmlAttribute("dllsBuild")]
        public string DllsBuild { get; set; }
        [XmlAttribute("isExperimental")]
        public bool IsExperimental { get; set; }
        [XmlAttribute("requiresGalaxyReset")]
        public bool RequiresGalaxyReset { get; set; }
        [XmlAttribute("causesSaveDataDependency")]
        public bool CausesSaveDataDependency { get; set; }

        [XmlElement("prerequisite")]
        public ModPrerequisite Prerequisite { get; set; }

        public Mod() : this("unknown", "unknown", "1.0.1.0", "2.5.20") { }
        public Mod(string name, string unique, string installerSystemVersion,
            string dllsBuild, string? description = null, bool hasCustomInstaller = false,
            bool isExperimental = false, bool requiresGalaxyReset = false,
            bool causesSaveDataDependency = false)
        {
            Name = DisplayName = name;
            Unique = unique;
            Description = description;
            InstallerSystemVersion = installerSystemVersion;
            HasCustomInstaller = hasCustomInstaller;
            DllsBuild = dllsBuild;
            IsExperimental = isExperimental;
            RequiresGalaxyReset = requiresGalaxyReset;
            CausesSaveDataDependency = causesSaveDataDependency;
        }

        public override string ToString()
            => $@"{nameof(Name)}: {Name}
{nameof(DisplayName)}: {DisplayName}
{nameof(Unique)}: {Unique}
{nameof(Description)}: {Description}
{nameof(InstallerSystemVersion)}: {InstallerSystemVersion}
{nameof(HasCustomInstaller)}: {HasCustomInstaller}
{nameof(DllsBuild)}: {DllsBuild}
{nameof(IsExperimental)}: {IsExperimental}
{nameof(RequiresGalaxyReset)}: {RequiresGalaxyReset}
{nameof(CausesSaveDataDependency)}: {CausesSaveDataDependency}";
    }
}