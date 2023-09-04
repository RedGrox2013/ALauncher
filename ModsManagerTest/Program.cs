using ModsManager;

string path = "TestMod";
var mod = Mod.ParseXML(path + "/ModInfo.xml");
Console.WriteLine(mod);

ModsInstaller.InstallMod(mod, path);
