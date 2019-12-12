using System;
using System.Collections.Generic;
using Stock.Model.Base;
using Stock.Model.Entities;

namespace Stock.Repository.LiteDb.Configuration
{
    public class DocumentCollectionMapping
    {
        private static IReadOnlyDictionary<Type, string> TypeCollectionMapping;

        static DocumentCollectionMapping()
        {
            TypeCollectionMapping = new Dictionary<Type, string>
            {
                { typeof(Product), "product" },
                { typeof(ProductType), "producttype" },
                { typeof(Provider), "provider" },
                { typeof(Store), "store" },

            };
        }

        public static string GetCollectionName<T>() where T : IEntity
        {
            return GetCollectionName(typeof(T));
        }

        public static string GetCollectionName(Type type)
        {
            string nameMapped = null;

            if (!TypeCollectionMapping.TryGetValue(type, out nameMapped))
            {
                throw new ArgumentOutOfRangeException($"The document {type.FullName} is not mapped to any collection in the configuration");
            }

            return nameMapped.ToLower();
        }
    }
}