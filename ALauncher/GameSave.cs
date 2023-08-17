using System;
using System.IO;

namespace ALauncher
{
    partial class GameSave
    {
        private readonly DirectoryInfo _directory;

        private static string? _savesPath;
        public static string SavesPath => _savesPath ??=
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\Spore\\Games\\ALauncher\\";

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _directory.MoveTo(SavesPath + value);
            }
        }

        public const string CURRENT_SAVE_FILE_NAME = "CurrentSave";

        public static string GetCurrentSave(string defaultSaveName)
        {
            CreateSavesDirectory();
            if (!File.Exists(SavesPath + CURRENT_SAVE_FILE_NAME))
            {
                using var writer = File.CreateText(SavesPath + CURRENT_SAVE_FILE_NAME);
                writer.Write(defaultSaveName);
                return defaultSaveName;
            }

            return File.ReadAllText(SavesPath + CURRENT_SAVE_FILE_NAME);
        }

        //public GameSave(string? name = null)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        string[] dirs = Directory.GetDirectories(SavesPath);
        //        int count = 0;
        //        foreach (string dir in dirs)
        //        {
        //            Regex regex = SaveRegex();
        //            if (regex.IsMatch(dir))
        //                ++count;
        //        }
        //        _name = "My Save";
        //        if (count > 0)
        //            _name += $" ({count})";
        //    }
        //    else
        //        _name = name;

        //    _directory = new DirectoryInfo(SavesPath + _name);
        //}
        public GameSave(DirectoryInfo directory)
        {
            _directory = directory;
            _name = directory.Name;
        }

        //[GeneratedRegex("My Save*")]
        //private static partial Regex SaveRegex();

        public override string ToString() => _name;

        public static DirectoryInfo CreateSavesDirectory()
        {
            var dir = new DirectoryInfo(SavesPath);
            if (!dir.Exists)
                dir.Create();

            return dir;
        }
    }
}
