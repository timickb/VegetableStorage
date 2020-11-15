using System;

namespace VegetableStorage.Exceptions
{
    public class BoxAddException : Exception
    {
        public override string Message => "Ящик не помещается в контейнер.";
    }
}