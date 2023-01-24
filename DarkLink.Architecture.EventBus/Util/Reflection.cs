namespace DarkLink.Architecture.EventBus.Util;

public static class Reflection
{
    public static Type GetImplementedType(this Type type, Type openGenericInterfaceType)
        => type.GetInterfaces().Single(t => t.IsGenericType && t.GetGenericTypeDefinition() == openGenericInterfaceType);

    public static bool Implements<TInterface>(this Type type)
        => type.Implements(typeof(TInterface));

    public static bool Implements(this Type type, Type interfaceType)
        => interfaceType.IsOpenGeneric()
            ? type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == interfaceType)
            : type.GetInterfaces().Contains(interfaceType);

    public static bool IsConcrete(this Type type)
        => !type.IsAbstract && (!type.IsGenericType || type.IsConstructedGenericType) && !type.IsInterface;

    public static bool IsOpenGeneric(this Type type)
        => type.IsGenericType && !type.IsConstructedGenericType;
}
