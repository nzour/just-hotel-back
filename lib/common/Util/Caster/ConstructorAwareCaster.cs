using System;
using System.Linq;
using System.Reflection;

namespace Common.Util.Caster
{
    /// <summary>
    /// Умеет кастить объект в тип T. Тип T обязательно должен содержать конструкор с одним аргументом,
    /// в котором описана реализация каста из переданного объекта.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ConstructorAwareCaster<T>
    {
        public static T Cast(object @object)
        {
            if (!HasNeededConstructor(@object.GetType()))
            {
                throw CasterException.ConstructorWasNotFound<T>(@object.GetType());
            }

            return (T) Activator.CreateInstance(typeof(T), @object);
        }

        private static bool HasNeededConstructor(Type objectType)
        {
            return null != objectType.GetConstructors()
                       .FirstOrDefault(c => IsNeededConstructor(c, objectType));
        }

        private static bool IsNeededConstructor(ConstructorInfo constructor, Type objectType)
        {
            if (constructor.GetParameters().Length != 1)
            {
                return false;
            }

            ParameterInfo parameter = constructor.GetParameters().First()!;

            return parameter.ParameterType.IsEquivalentTo(objectType);
        }
    }
}