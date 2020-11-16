namespace VegetableStorage.Entities
{
    public class Action
    {
        public string Name { get;  }
        public string Argument { get; }
        
        public Action(string name, string arg)
        {
            Name = name;
            Argument = arg;
        }

        public Action(string name)
        {
            Name = name;
            Argument = string.Empty;
        }
    }
}