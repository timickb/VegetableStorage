using System.Collections.Generic;

namespace VegetableStorage
{
    public static class Storage
    {
        private static List<Container> containers = new List<Container>();

        public static void AddContainer(Container c)
        {
            containers.Add(c);
        }
        
        
    }
}