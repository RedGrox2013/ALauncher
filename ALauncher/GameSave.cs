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
                _directory.MoveTo(LauncherSavesPath + "/" + value);
            }
        }
        public string Path => _directory.FullName;

        public const string CURRENT_SAVE_FILE_NAME = "CurrentSave";
        public const string ALAUNCHER_DIRECTORY_NAME = "ALauncher";

        /// <summary>
        /// Читает файл CurrentSave и возвращает имя текущего сохранения.
        /// Если файл отсутсвует, создат новый
        /// </summary>
        /// <param name="defaultSaveName">Имя сохранения, если файл отсутствует</param>
        /// <returns>Имя текущего сохранения</returns>
        public static string GetCurrentSaveName(string defaultSaveName)
        {
            CreateSavesDirectory();
            if (!File.Exists(LauncherSavesPath + "/" + CURRENT_SAVE_FILE_NAME))
            {
                RenameCurrentSave(defaultSaveName);
                return defaultSaveName;
            }

            return File.ReadAllText(LauncherSavesPath + "/" + CURRENT_SAVE_FILE_NAME);
        }
        /// <summary>
        /// Переименовывает текущее сохранение, изменяя файл CurrentSave
        /// </summary>
        /// <param name="saveName">Имя сохранения</param>
        public static void RenameCurrentSave(string saveName)
        {
            CreateSavesDirectory();
            using var writer = File.CreateText(LauncherSavesPath + "/" + CURRENT_SAVE_FILE_NAME);
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
                    dir.MoveTo(oldSave.Path + "/" + dir.Name);
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

        public GameSave(string name)
        {
            _name = name;
            _directory = Directory.CreateDirectory(LauncherSavesPath + "/" + name);
        }
        public GameSave(DirectoryInfo directory)
        {
            _directory = directory;
            _name = directory.Name;

            if (!directory.Exists)
                directory.Create();
        }

        public override string ToString() => _name;

        /// <summary>
        /// Создаёт папку "ALauncher" в %appdata%/spore/games
        /// </summary>
        /// <returns></returns>
        public static DirectoryInfo CreateSavesDirectory()
        {
            var dir = new DirectoryInfo(LauncherSavesPath);
            if (!dir.Exists)
                dir.Create();

            return dir;
        }

        /// <summary>
        /// Удаляет директорию сохранения в папке "ALauncher"
        /// </summary>
        public void Delete()
        {
            if (_directory.Exists)
                _directory.Delete(true);
        }
    }
}
