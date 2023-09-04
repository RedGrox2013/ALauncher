using ALauncher;

namespace ModsManager
{
    public static class ModsInstaller
    {
        private readonly static Settings _settings;

        public const string MODAPI_MODS_DIR = "/mLibs";

        static ModsInstaller()
        {
            _settings = Settings.Instance;
        }

        public static void InstallMod(Mod mod, string modConfigPath)
        {
            for (int i = 0; i < mod.PrerequisitesCount; i++)
            {
                Prerequisite prerequisite = mod.GetPrerequisiteAt(i);
                if (prerequisite.Files == null)
                    continue;
                for (int j = 0; j < prerequisite.Files.Length; j++)
                {
                    string path;
                    if (prerequisite.Game == null || j > prerequisite.Game.Length)
                        path = _settings.ModAPIPath + MODAPI_MODS_DIR;
                    else
                    {
                        path = prerequisite.Game[j].ToLower() switch
                        {
                            "spore" => _settings.SporePath,
                            "galacticadventures" => _settings.SporeEP1Path,
                            _ => _settings.ModAPIPath + MODAPI_MODS_DIR,
                        };
                    }

                    File.Copy(modConfigPath + "/" + prerequisite.Files[j],
                        path + "/" + prerequisite.Files[j]);
                }
            }
        }
    }
}
