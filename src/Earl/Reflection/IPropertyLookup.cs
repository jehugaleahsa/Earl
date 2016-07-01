using System;
using System.Collections.Generic;

namespace Earl.Reflection
{
    internal interface IPropertyLookup
    {
        IProperty GetProperty(string name);

        IEnumerable<IProperty> GetProperties();
    }
}
