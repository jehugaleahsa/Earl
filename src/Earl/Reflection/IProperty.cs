using System;

namespace Earl.Reflection
{
    internal interface IProperty
    {
        string Name { get; }

        Type Type { get; }

        object Value { get; }
    }
}
