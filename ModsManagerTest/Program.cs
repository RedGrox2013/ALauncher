using ModsManager;

string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/test.xml";
var mod = Mod.ParseXML(path);
Console.WriteLine(mod.Name + " " + mod.DisplayName);
