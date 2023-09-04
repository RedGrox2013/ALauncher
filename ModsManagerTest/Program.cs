using ModsManager;

const string path = "TestMod";
var mod = Mod.ParseXML(path + "/ModInfo.xml");
Console.WriteLine(mod);

for (int i = 0; i < mod.ComponentsCount; i++)
{
    Component component = mod.GetComponentAt(i);
    Console.Write(component.DisplayName + "\n" +
        component.Description + "\nInstall? (y/n) ");
    var choice = Console.ReadLine();
    component.Selected = choice?.ToLower() == "y";
}

ModsInstaller.InstallMod(mod, path);
