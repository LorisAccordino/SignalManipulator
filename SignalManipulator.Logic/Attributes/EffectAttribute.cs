namespace SignalManipulator.Logic.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EffectAttribute : Attribute
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public EffectAttribute(string name, string category = "Misc", string description = "")
        {
            Category = category;
            Description = description;
            Name = name;
        }
    }
}