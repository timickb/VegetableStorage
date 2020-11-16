using Newtonsoft.Json;

namespace VegetableStorage.Entities
{
    public class Operation
    {
        public string Name { get; }
        public string Argument { get; }
        
        [JsonConstructor]
        public Operation(string name, string argument)
        {
            Name = name;
            Argument = argument;
        }

        public override string ToString()
        {
            return $"{Name} {Argument}";
        }
    }
}