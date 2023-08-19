using ChatMensagem.Contracts;
using ChatMensagem.Domain;
using System.Collections;
using System.Reflection;

namespace ChatMensagem.Api.Extensions.Features
{
    public static class FeatureExtensions
    {
        public static T Map<T>(this Entity entity) where T : class, new()
        {
            return Convert<Entity, T>(entity);
        }

        public static T Map<T>(this BaseContract contract) where T : class, new()
        {
            return Convert<BaseContract, T>(contract);
        }

        public static T ToContract<F, T>(F from) where F : class where T : BaseContract, new()
        {
            var contract = Convert<F, T>(from);
            contract.TraceId = Guid.NewGuid();
            return contract;
        }

        public static T ToDomain<F, T>(F from) where F : class where T : Entity, new()
        {
            var entity = Convert<F, T>(from);
            entity.DataCadastro = DateTime.Now;
            return entity;
        }

        public static T Convert<F, T>(F from) where F : class where T : class, new()
        {
            T target = new();

            foreach (PropertyInfo propertyTarget in target.GetType().GetProperties())
            {
                PropertyInfo propertyFrom = Get(from, MapName(propertyTarget.Name));
                if (IsValid(propertyFrom, propertyTarget))
                    propertyTarget.SetValue(target, propertyFrom.GetValue(from));
            }

            return target;
        }

        private static bool IsValid(PropertyInfo propertyFrom, PropertyInfo propertyTarget)
        {
            return propertyFrom != null &&
                !IsCollection(propertyFrom) &&
                !(propertyFrom.PropertyType.BaseType == typeof(Entity)) &&
                IsMapped(propertyFrom, propertyTarget);
        }

        private static bool IsMapped(PropertyInfo propertyFrom, PropertyInfo propertyTarget)
        {
            return propertyFrom.PropertyType == propertyTarget.PropertyType;
        }

        private static bool IsCollection(PropertyInfo property)
        {
            return property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType);
        }

        private static string MapName(string name)
        {
            return name switch
            {
                "Id" => "IdExterno",
                _ => name,
            };
        }
        private static PropertyInfo Get<T>(T o, string name)
        {
            return o?
                .GetType()
                .GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
