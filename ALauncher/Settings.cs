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
                _instance ??= new Settings();
                return _instance;
            }
        }

        public const string FILE_NAME = "Settings.xml";

        private bool _hasChanges = false;
        private string? _sporePath, _sporeEP1Path, _mySporeCreationsPath, _modAPIPath;

        public string SporePath
        {
            get => _sporePath ?? string.Empty;
            set => SetValue(ref _sporePath, value);
        }
        public string SporeEP1Path
        {
            get => _sporeEP1Path ?? string.Empty;
            set => SetValue(ref _sporeEP1Path, value);
        }
        public string MySporeCreationsPath
        {
            get => _mySporeCreationsPath ?? string.Empty;
            set => SetValue(ref _mySporeCreationsPath, value);
        }
        public string ModAPIPath
        {
            get => _modAPIPath ?? string.Empty;
            set => SetValue(ref _modAPIPath, value);
        }

        public int SelectedGameIndex { get; set; } = 0;

        private Settings()
        {
            const string KEY_NAME = "datadir";

            SporePath = GetRegistryValue("spore", KEY_NAME) as string ?? string.Empty;
            SporeEP1Path = GetRegistryValue("SPORE_EP1", KEY_NAME) as string ?? string.Empty;

            MySporeCreationsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                "\\" + GetRegistryValue("spore", "playerdir");
        }

        public static void Deserialize()
        {
            XmlSerializer serializer = new(typeof(Settings));
            using var stream = File.OpenRead(FILE_NAME);
            _instance = serializer.Deserialize(stream) as Settings;
        }

        private static object? GetRegistryValue(string keyName, string valueName) =>
            Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\electronic arts\\" + keyName, valueName, null);

        public static void Serialize()
        {
            if (_instance == null || !_instance._hasChanges)
                return;

            XmlSerializer serializer = new(typeof(Settings));
            using var stream = File.Create(FILE_NAME);
            serializer.Serialize(stream, _instance);
        }

        private void SetValue<T>(ref T param, T value)
        {
            param = value;
            _hasChanges = true;
        }
    }
}
