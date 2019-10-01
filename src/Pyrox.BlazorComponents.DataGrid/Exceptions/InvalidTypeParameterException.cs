using System;

namespace Pyrox.BlazorComponents.DataGrid.Exceptions
{
    public class InvalidTypeParameterException : Exception
    {
        public InvalidTypeParameterException(string type, string genericClass) :
            base($"Cannot use {type} as a type parameter for class {genericClass}.")
        {
        }
    }
}
