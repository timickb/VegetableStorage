namespace VegetableStorage.Entities
{
    public class Operation
    {
        public string Name { get;  }
        public string Argument { get; }
        
        public Operation(string name, string arg)
        {
            Name = name;
            Argument = arg;
        }

        public Operation(string name)
        {
            Name = name;
            Argument = string.Empty;
        }
    }
}