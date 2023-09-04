namespace ModsManager
{
    public struct CompatFile
    {
        public string[]? CompatTargetFileName { get; set; }
        public string[]? CompatTargetGame { get; set; }
        public string[]? Game { get; set; }
        public bool RemoveTargets { get; set; }

        public string[]? Files { get; set; }
    }
}
