using ALauncher;
using System.IO.Compression;

namespace ModsManager
{
    public static class ModsInstaller
    {
        private readonly static Settings _settings;

        public const string MODAPI_MODS_DIR = "/mLibs";
        public const string MODAPI_MODS_CONFIG_DIR = "/ModConfigs";

        static ModsInstaller()
        {
            _settings = Settings.Instance;
        }

        public static void InstallMod(Mod mod)
        {
            string modConfigPath = _settings.ModAPIPath + MODAPI_MODS_CONFIG_DIR + "/" + mod.Unique;

            for (int i = 0; i < mod.PrerequisitesCount; i++)
            {
                Prerequisite prerequisite = mod.GetPrerequisiteAt(i);
                CopyFiles(prerequisite.Files, prerequisite.Game, modConfigPath);
            }

            for (int i = 0; i < mod.ComponentsCount; i++)
            {
                Component component = mod.GetComponentAt(i);
                if (!component.Selected)
                    continue;

                CopyFiles(component.Files, component.Game, modConfigPath);
            }
        }

        private static void CopyFiles(string[]? files, string[]? game, string modConfigPath)
        {
            if (files == null) return;

            for (int i = 0; i < files.Length; i++)
            {
                string path;
                if (game == null || i >= game.Length)
                    path = _settings.ModAPIPath + MODAPI_MODS_DIR;
                else
                {
                    path = game[i].ToLower() switch
                    {
                        "spore" => _settings.SporePath,
                        "galacticadventures" => _settings.SporeEP1Path,
                        _ => _settings.ModAPIPath + MODAPI_MODS_DIR,
                    };
                }
                path += "/" + files[i];

                if (!File.Exists(path))
                    File.Copy(modConfigPath + "/" + files[i], path);
            }
        }

        public static Mod UnpackSporemod(string path)
        {
            string modConfigPath = _settings.ModAPIPath + MODAPI_MODS_CONFIG_DIR + "/temp";
            ZipFile.ExtractToDirectory(path, modConfigPath, true);
            Mod mod = Mod.ParseXML(modConfigPath + "/ModInfo.xml");
            Directory.Move(modConfigPath, _settings.ModAPIPath + MODAPI_MODS_CONFIG_DIR + "/" + mod.Unique);

            return mod;
        }
    }
}
