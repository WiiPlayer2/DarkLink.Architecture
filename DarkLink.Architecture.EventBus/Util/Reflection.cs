namespace DarkLink.Architecture.EventBus.Util;

internal static class Reflection
{
    public static bool Implements<TInterface>(this Type type)
        => type.Implements(typeof(TInterface));

    public static bool Implements(this Type type, Type interfaceType)
        => type.GetInterfaces().Contains(interfaceType);

    public static bool IsConcrete(this Type type)
        => !type.IsAbstract && (!type.IsGenericType || type.IsConstructedGenericType) && !type.IsInterface;
}
