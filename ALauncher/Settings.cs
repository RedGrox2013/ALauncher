using Microsoft.Win32;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ALauncher
{
    [Serializable]
    public class Settings
    {
        private static Settings? _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        XmlSerializer serializer = new(typeof(Settings));
                        using var stream = File.OpenRead(FILE_NAME);
                        _instance = serializer.Deserialize(stream) as Settings;
                    }
                    catch
                    {
                        if (File.Exists(FILE_NAME))
                            File.Delete(FILE_NAME);
                    }
                    finally { _instance ??= new Settings(); }
                }
                return _instance;
            }
        }

        public const string FILE_NAME = "Settings.xml";
        public const string MODAPI_NAME = "Spore ModAPI Launcher.exe";

        private string? _sporePath, _sporeEP1Path,
            _mySporeCreationsPath, _modAPIPath,
            _mainDirectory;

        public string SporePath
        {
            get => _sporePath ?? string.Empty;
            set
            {
                _sporePath = value;
                MainSporePath = value;
                FindApps();
            }
        }
        public string SporeEP1Path
        {
            get => _sporeEP1Path ?? string.Empty;
            set
            {
                _sporeEP1Path = value;
                MainSporePath = value;
                FindApps();
            }
        }
        public string MySporeCreationsPath
        {
            get => _mySporeCreationsPath ?? string.Empty;
            set => _mySporeCreationsPath = value;
        }
        public string ModAPIPath
        {
            get => _modAPIPath ?? string.Empty;
            set => _modAPIPath = value;
        }
        [XmlIgnore]
        public string MainSporePath
        {
            get => _mainDirectory ?? string.Empty;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                DirectoryInfo dir = new(value);
                _mainDirectory = dir.Parent?.ToString();
            }
        }
        [XmlIgnore]
        public string SporeAppPath { get; private set; } = string.Empty;
        [XmlIgnore]
        public string SporeEP1AppPath { get; private set; } = string.Empty;
        public string? SteamPath { get; set; }

        public string LineArguments { get; set; } = string.Empty;
        public bool IsSteamVersion { get; set; }
        public SporeLanguages Language { get; set; }
        public bool IsFirstStart { get; set; } = true;

        private int _selectedGameIndex = 0;
        public int SelectedGameIndex
        {
            get => _selectedGameIndex;
            set
            {
                _selectedGameIndex = value;
                Serialize();
            }
        }

        private Settings()
        {
            const string KEY_NAME = "datadir";

            SporePath = GetRegistryValue("spore", KEY_NAME)?.ToString() ?? string.Empty;
            _sporeEP1Path = GetRegistryValue("SPORE_EP1", KEY_NAME)?.ToString();

            MySporeCreationsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                "\\" + GetRegistryValue("spore", "playerdir");
            SteamPath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Valve\\Steam",
                "InstallPath", null)?.ToString();
            if (SteamPath != null)
            {
                if (SteamPath[^1] != '\\')
                    SteamPath += "\\";
                SteamPath += "steam.exe";
            }

            FindApps();
        }

        private void FindApps()
        {
            string? installLoc = GetRegistryValue("spore", nameof(installLoc))?.ToString();
            if (installLoc == null)
                SporeAppPath = _mainDirectory ?? string.Empty;
            else
                SporeAppPath = installLoc;
            if (SporeAppPath.Length != 0 && SporeAppPath[^1] != '\\')
                SporeAppPath += "\\";
            SporeAppPath += "SporeBin\\SporeApp.exe";

            installLoc = GetRegistryValue("SPORE_EP1", nameof(installLoc))?.ToString();
            if (installLoc == null)
                SporeEP1AppPath = _mainDirectory ?? string.Empty;
            else
                SporeEP1AppPath = installLoc;
            if (SporeEP1AppPath.Length != 0 && SporeEP1AppPath[^1] != '\\')
                SporeEP1AppPath += "\\";
            SporeEP1AppPath += "SporebinEP1\\SporeApp.exe";
        }

        private static object? GetRegistryValue(string keyName, string valueName) =>
            Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\electronic arts\\" + keyName, valueName, null);

        public static void Serialize()
        {
            if (_instance == null)
                return;

            XmlSerializer serializer = new(typeof(Settings));
            using var stream = File.Create(FILE_NAME);
            serializer.Serialize(stream, _instance);
        }
    }

    public enum SporeLanguages
    {
        Russian,
        EnglishUSA,
        EnglishUK,
        Czech,
        Danish,
        German,
        Spanish,
        Finnish,
        French,
        Italian,
        Hungarian,
        Dutch,
        Norwegian,
        Polish,
        Swedish,
        Portuguese
    }
}
