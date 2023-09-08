using ALauncher;
using ModsManager;

Settings.Instance.ModAPIPath = "D:\\Games\\Spore ModAPI Launcher Kit";

Console.Write("Enter .sporemod path: ");
var mod = ModsInstaller.UnpackSporemod(Console.ReadLine() ?? string.Empty);
Console.WriteLine(mod);

for (int i = 0; i < mod.ComponentsCount; i++)
{
    Component component = mod.GetComponentAt(i);
    Console.Write(component.DisplayName + "\n" +
        component.Description + "\nInstall? (y/n) ");
    var choice = Console.ReadLine();
    component.Selected = choice?.ToLower() == "y";
}

ModsInstaller.InstallMod(mod);
