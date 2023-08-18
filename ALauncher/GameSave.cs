using System;
using System.IO;

namespace ALauncher
{
    partial class GameSave
    {
        private readonly DirectoryInfo _directory;

        private static string? _launcherSavesPath;
        public static string LauncherSavesPath => _launcherSavesPath ??=
            SavesPath + ALAUNCHER_DIRECTORY_NAME;
        private static string? _savesPath;
        public static string SavesPath => _savesPath ??=
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\Spore\\Games\\";

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _directory.MoveTo(LauncherSavesPath + "\\" + value);
            }
        }
        public string Path => _directory.FullName;

        public const string CURRENT_SAVE_FILE_NAME = "CurrentSave";
        public const string ALAUNCHER_DIRECTORY_NAME = "ALauncher";

        public static string GetCurrentSaveName(string defaultSaveName)
        {
            CreateSavesDirectory();
            if (!File.Exists(LauncherSavesPath + "\\" + CURRENT_SAVE_FILE_NAME))
            {
                RenameCurrentSave(defaultSaveName);
                return defaultSaveName;
            }

            return File.ReadAllText(LauncherSavesPath + "\\" + CURRENT_SAVE_FILE_NAME);
        }
        public static void RenameCurrentSave(string saveName)
        {
            CreateSavesDirectory();
            using var writer = File.CreateText(LauncherSavesPath + "\\" + CURRENT_SAVE_FILE_NAME);
            writer.Write(saveName);
        }
        /// <summary>
        /// Заменяет одно сохранение на другое
        /// </summary>
        /// <param name="save">Новое сохранение</param>
        /// <returns>Старое сохранение</returns>
        public static GameSave ReplaceCurrentSave(GameSave save)
        {
            GameSave oldSave = new(GetCurrentSaveName(string.Empty));
            var saveDirs = Directory.GetDirectories(SavesPath);
            foreach (var saveDir in saveDirs)
            {
                if (saveDir != LauncherSavesPath)
                {
                    var dir = new DirectoryInfo(saveDir);
                    dir.MoveTo(oldSave.Path + "\\" + dir.Name);
                }
            }

            RenameCurrentSave(save.Name);
            saveDirs = Directory.GetDirectories(save.Path);
            foreach (var saveDir in saveDirs)
            {
                var dir = new DirectoryInfo(saveDir);
                dir.MoveTo(SavesPath + dir.Name);
            }
            save.Delete();

            return oldSave;
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
        public GameSave(string name)
        {
            _name = name;
            _directory = Directory.CreateDirectory(LauncherSavesPath + "\\" + name);
        }
        public GameSave(DirectoryInfo directory)
        {
            _directory = directory;
            _name = directory.Name;

            if (!directory.Exists)
                directory.Create();
        }

        //[GeneratedRegex("My Save*")]
        //private static partial Regex SaveRegex();

        public override string ToString() => _name;

        public static DirectoryInfo CreateSavesDirectory()
        {
            var dir = new DirectoryInfo(LauncherSavesPath);
            if (!dir.Exists)
                dir.Create();

            return dir;
        }

        public void Delete()
        {
            if (_directory.Exists)
                _directory.Delete(true);
        }
    }
}
