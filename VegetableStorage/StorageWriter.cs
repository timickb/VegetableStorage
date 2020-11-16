using System;
using System.IO;
using Newtonsoft.Json;
using VegetableStorage.Entities;

namespace VegetableStorage
{
    public class StorageWriter
    {
        private Storage _storage;
        
        public StorageWriter(Storage s)
        {
            _storage = s;
        }

        public void WriteToConsole()
        {
            Console.WriteLine(_storage.ToString());
            
        }

        public void WriteToFile(string path)
        {
            try
            {
                var sw = new StreamWriter(path, false, System.Text.Encoding.UTF8);
                sw.WriteLine(JsonConvert.SerializeObject(_storage));
                sw.Flush();
                sw.Close();
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException();
            }
            catch (IOException)
            {
               throw new IOException();
            }
        }
    }
}