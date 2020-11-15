using System;

namespace VegetableStorage.Exceptions
{
    public class ContainerNotFoundException : Exception
    {
        public override string Message => "Контейнера с таким идентификатором не существует.";
    }
}