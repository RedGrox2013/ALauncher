namespace ModsManager
{
    public class Component
    {
        public string DisplayName { get; set; }
        public string Unique { get; set; }
        public string[]? Game { get; set; }
        public string? Description { get; set; }
        public ImagePlacement ImagePlacement { get; set; }

        public string[]? Files { get; set; }

        public Component(string displayName, string unique,
            string[]? game = null, string? description = null,
            ImagePlacement imagePlacement = ImagePlacement.None)
        {
            DisplayName = displayName;
            Unique = unique;
            Game = game;
            Description = description;
            ImagePlacement = imagePlacement;
        }
    }
}
