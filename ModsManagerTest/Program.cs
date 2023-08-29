using ModsManager;

var mod = Mod.ParseXML("test.xml");
Console.WriteLine(mod);

Console.WriteLine("Prerequisites:");
for (int i = 0; i < mod.PrerequisitesCount; i++)
{
    Console.Write("(" + mod.GetPrerequisiteAt(i).Game + ") ");
    var files = mod.GetPrerequisiteAt(i).Files;
    if (files != null)
        foreach (var file in files)
            Console.Write(file + " ");
    Console.WriteLine();
}
