using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ALauncher
{
    partial class GameSave
    {
        private DirectoryInfo _directory;

        private static string? _savesPath;
        public static string SavesPath => _savesPath ??=
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\Roaming\\Spore\\Games\\ALauncher\\";

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

        public GameSave()
        {
            string[] dirs = Directory.GetDirectories(SavesPath);
            int count = 0;
            foreach (string dir in dirs)
            {
                Regex regex = SaveRegex();
                if (regex.IsMatch(dir))
                    ++count;
            }
            _name = "My Save";
            if (count > 0)
                _name += $" ({count})";
            _directory = Directory.CreateDirectory(SavesPath + _name);
        }

        [GeneratedRegex("My Save*")]
        private static partial Regex SaveRegex();
    }
}
