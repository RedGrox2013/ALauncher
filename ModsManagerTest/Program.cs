using ModsManager;

var mod = Mod.ParseXML("http://davoonline.com/sporemodder/rob55rod/ModAPI/Public/Samples/1-0-1-0/ModInfo.xml");
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
