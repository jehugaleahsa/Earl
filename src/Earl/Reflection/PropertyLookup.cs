using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Earl.Reflection
{
    internal static class PropertyLookup
    {
        public static IPropertyLookup CreatePropertyLookup(object instance)
        {
            if (instance != null && isDictionaryType(instance.GetType()))
            {
                return DictionaryPropertyLookup.Create((dynamic)instance);
            }
            else
            {
                return new ObjectPropertyLookup(instance);
            }
        }

        private static bool isDictionaryType(Type type)
        {
            if (!type.GetTypeInfo().IsGenericType)
            {
                return false;
            }
            Type[] typeArguments = type.GetTypeInfo().GetGenericArguments();
            if (typeArguments.Length != 2)
            {
                return false;
            }
            Type dictionaryType = typeof(IDictionary<,>).MakeGenericType(typeArguments);
            if (!dictionaryType.GetTypeInfo().IsAssignableFrom(type))
            {
                return false;
            }
            Type keyArgumentType = typeArguments.First();
            return keyArgumentType == typeof(String);
        }

        public static IEnumerable<object> GetValues(IProperty property, bool isExploded = false)
        {
            if (isSimpleType(property.Type))
            {
                object result = property.Value;
                return new object[] { result };
            }
            else if (isListType(property.Type))
            {
                var result = (IEnumerable)property.Value;
                return result.Cast<object>().ToArray();
            }
            else
            {
                object instance = property.Value;
                var subProperties = PropertyLookup.CreatePropertyLookup(instance);
                List<object> values = new List<object>();
                foreach (IProperty subProperty in subProperties.GetProperties())
                {
                    object value = subProperty.Value;
                    if (isExploded)
                    {
                        var pair = new Tuple<string, object>(subProperty.Name, value);
                        values.Add(pair);
                    }
                    else
                    {
                        values.Add(subProperty.Name);
                        values.Add(value);
                    }
                }
                return values.ToArray();
            }
        }

        private static bool isListType(Type type)
        {
            if (!typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type))
            {
                return false;
            }
            if (type.GetTypeInfo().IsGenericType)
            {
                Type[] typeArguments = type.GetTypeInfo().GetGenericArguments();
                if (typeArguments.Length != 1)
                {
                    return false;
                }
                Type typeArgument = typeArguments.Single();
                if (!isSimpleType(typeArgument))
                {
                    return false;
                }
                return true;
            }
            else if (type.IsArray)
            {
                return isSimpleType(type.GetElementType());
            }
            else
            {
                return false;
            }
        }

        private static bool isSimpleType(Type type)
        {
            Type[] simpleTypes = new Type[]
            { 
                typeof(string), 
                typeof(byte), typeof(byte?),
                typeof(sbyte), typeof(sbyte?),
                typeof(short), typeof(short?),
                typeof(ushort), typeof(ushort?),
                typeof(int), typeof(int?),
                typeof(uint), typeof(uint?),
                typeof(long), typeof(long?),
                typeof(ulong), typeof(ulong?),
                typeof(decimal), typeof(decimal?),
                typeof(double), typeof(double?),
                typeof(float), typeof(float?),
                typeof(Guid), typeof(Guid?),
                typeof(DateTime), typeof(DateTime?),
                typeof(DateTimeOffset), typeof(DateTimeOffset?), 
            };
            return simpleTypes.Contains(type);
        }
    }
}
