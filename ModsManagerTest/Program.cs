using ModsManager;
using System.Xml.Serialization;

XmlSerializer xmlSerializer = new(typeof(Mod));
Console.Write("Serialize or deserialize (s/d)? ");
var input = Console.ReadLine();
string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/test.xml";
if (input == "s")
{
    Mod mod = new()
    {
        Prerequisite = new ModPrerequisite()
        {
            File = "test",
            Game = "Spore"
        }
    };
    using var stream = File.Create(path);
    xmlSerializer.Serialize(stream, mod);
}
else if (input == "d")
{
    using var stream = File.OpenRead(path);
    if (xmlSerializer.Deserialize(stream) is Mod mod)
        Console.WriteLine(mod);
}