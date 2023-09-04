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

        public const char FILES_SEPARATOR = '?';

        private List<Prerequisite>? _prerequisites;
        public int PrerequisitesCount => _prerequisites?.Count ?? 0;
        public void AddPrerequisite(Prerequisite prerequisite)
            => AddElement(prerequisite, ref _prerequisites);
        public Prerequisite GetPrerequisiteAt(int index)
            => GetElementAt(index, _prerequisites);

        private List<CompatFile>? _compatFiles;
        public int CompatFilesCount => _compatFiles?.Count ?? 0;
        public CompatFile GetCompatFileAt(int index)
            => GetElementAt(index, _compatFiles);
        public void AddCompatFile(CompatFile compatFile)
            => AddElement(compatFile, ref _compatFiles);

        private List<Component>? _components;
        public int ComponentsCount => _components?.Count ?? 0;
        public Component GetComponentAt(int index)
            => GetElementAt(index, _components);
        public void AddComponent(Component component)
            => AddElement(component, ref _components);

        private static T GetElementAt<T>(int index, List<T>? list)
        {
            if (list == null)
                throw new ArgumentOutOfRangeException(nameof(index));
            return list[index];
        }
        private static void AddElement<T>(T element, ref List<T>? list)
        {
            list ??= new List<T>();
            list.Add(element);
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
                            Game = reader.GetAttribute("game")?.Split(FILES_SEPARATOR),
                            Files = reader.ReadElementContentAsString().Split(FILES_SEPARATOR)
                        });
                        continue;
                    }
                    else if (reader.Name == "compatFile")
                    {
                        mod.AddCompatFile(new CompatFile
                        {
                            CompatTargetFileName = reader.GetAttribute("compatTargetFileName")
                                ?.Split(FILES_SEPARATOR),
                            CompatTargetGame = reader.GetAttribute("compatTargetGame")
                                ?.Split(FILES_SEPARATOR),
                            Game = reader.GetAttribute("game")?.Split(FILES_SEPARATOR),
                            RemoveTargets = GetAttributeValue(reader, "removeTargets"),
                            Files = reader.ReadElementContentAsString().Split(FILES_SEPARATOR)
                        });
                        continue;
                    }
                    else if (reader.Name == "component")
                    {
                        string? displayName = reader.GetAttribute("displayName"),
                            unique = reader.GetAttribute("unique");
                        if (displayName == null || unique == null)
                            throw new NullReferenceException(
                                "The component does not have a \"displayName\" or \"unique\" attribute.");
                        Component component = new
                            (
                                displayName, unique,
                                reader.GetAttribute("game")?.Split(FILES_SEPARATOR),
                                reader.GetAttribute("description")
                            );
                        if (Enum.TryParse(reader.GetAttribute("imagePlacement"), true,
                                out ImagePlacement imagePlacement))
                            component.ImagePlacement = imagePlacement;
                        component.Files = reader.ReadElementContentAsString().Split(FILES_SEPARATOR);
                        mod.AddComponent(component);
                        continue;
                    }
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