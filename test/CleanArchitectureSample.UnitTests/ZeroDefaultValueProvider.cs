namespace CleanArchitectureSample.UnitTests;

public class ZeroDefaultValueProvider : DefaultValueProvider
{
    protected override object GetDefaultValue(Type type, Mock mock)
    {
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
        return type.IsValueType ? Activator.CreateInstance(type) : null;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
    }
}
