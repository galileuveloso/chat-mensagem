using System.Linq.Expressions;
using System.Reflection;

namespace ChatMensagem.Api.Helpers
{
    public static class MessageHelper
    {
        public static string NotFoundFor<T>(T o, Expression<Func<T, object>> property)
        {
            return $"{typeof(T).Name}.{GetName(property)}: '{GetValue(o, property)}' não encontrado.";
        }

        public static string ServiceErrorFor<T>(T o, Expression<Func<T, object>> property)
        {
            return $"{typeof(T).Name}.{GetName(property)}: '{GetValue(o, property)}' inválido.";
        }

        public static string InvalidFor<T>(T o, Expression<Func<T, object>> property)
        {
            return $"{typeof(T).Name}.{GetName(property)}: '{GetValue(o, property)}' inválido.";
        }

        public static string DuplicateKeyFor<T>(T o, Expression<Func<T, object>> property)
        {
            return $"{typeof(T).Name}.{GetName(property)}: '{GetValue(o, property)}' já cadastrado.";
        }

        public static string EmptyFor<T>(Expression<Func<T, object>> property)
        {
            return $"Campo '{typeof(T).Name}.{GetName(property)}' está vazio.";
        }

        public static string NullFor<T>(Expression<Func<T, object>> property)
        {
            return $"Campo '{typeof(T).Name}.{GetName(property)}' é nulo.";
        }

        public static string NullFor<T>()
        {
            return $"'{nameof(T)}' é nulo.";
        }

        private static string GetName<T>(Expression<Func<T, object>> property)
        {
            return GetMemberExpression(property).Member.Name;
        }

        private static string GetValue<T>(T o, Expression<Func<T, object>> property)
        {
            var expr = GetMemberExpression(property);
            var prop = (PropertyInfo)expr.Member;
            var value = prop.GetValue(o);
            return value == null ? "null" : value.ToString();
        }

        public static string OutOfRange<T>(Expression<Func<T, object>> property, int minimum, int maximum)
        {
            return $"Campo '{typeof(T).Name}.{GetName(property)}' está fora do intervalo [{minimum}, {maximum}].";
        }

        public static string ForeignKeyFor<T>(T o, Expression<Func<T, object>> property, string reference)
        {
            return $"{typeof(T).Name}.{GetName(property)}: '{GetValue(o, property)}' é referenciado por {reference}";
        }

        public static string NotFoundFor<T>(Expression<Func<T, object>> property, object value)
        {
            return $"{typeof(T).Name}.{GetName(property)}: '{value}' não encontrado.";
        }

        private static MemberExpression GetMemberExpression<T>(Expression<Func<T, object>> property)
        {
            if (property is null)
                return null;
            MemberExpression member = property.Body as MemberExpression;
            return member ?? (property.Body is UnaryExpression unary ? unary.Operand as MemberExpression : null);
        }
    }
}
