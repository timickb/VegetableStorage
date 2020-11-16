using Newtonsoft.Json;

namespace VegetableStorage.Entities
{
    public class Operation
    {
        
        [JsonProperty("Name")]
        public string Name { get; }
        
        [JsonProperty("Argument")]
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