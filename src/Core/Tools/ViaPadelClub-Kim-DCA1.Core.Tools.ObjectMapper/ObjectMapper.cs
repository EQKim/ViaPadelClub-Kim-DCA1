using System.Reflection;

namespace ViaPadelClub_Kim_DCA1.Core.Tools.ObjectMapper;

public sealed class ObjectMapper : IObjectMapper
{
    private readonly Dictionary<(Type Source, Type Destination), Func<object, object>> _mappings = new();

    public ObjectMapper Register<TSource, TDestination>(IMapping<TSource, TDestination> mapping)
    {
        _mappings[(typeof(TSource), typeof(TDestination))] = source => mapping.Map((TSource)source)!;
        return this;
    }

    public TDestination Map<TDestination>(object source)
    {
        ArgumentNullException.ThrowIfNull(source);

        Type sourceType = source.GetType();
        Type destinationType = typeof(TDestination);

        if (_mappings.TryGetValue((sourceType, destinationType), out Func<object, object>? mapper))
        {
            return (TDestination)mapper(source);
        }

        return (TDestination)MapByMatchingConstructor(source, sourceType, destinationType);
    }

    private static object MapByMatchingConstructor(object source, Type sourceType, Type destinationType)
    {
        Dictionary<string, PropertyInfo> sourceProperties = sourceType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(property => property.Name, StringComparer.OrdinalIgnoreCase);

        ConstructorInfo constructor = destinationType
            .GetConstructors()
            .OrderByDescending(candidate => candidate.GetParameters().Length)
            .FirstOrDefault()
            ?? throw new InvalidOperationException($"No public constructor was found for {destinationType.Name}.");

        object?[] arguments = constructor
            .GetParameters()
            .Select(parameter => ResolveArgument(source, sourceProperties, parameter))
            .ToArray();

        return constructor.Invoke(arguments);
    }

    private static object? ResolveArgument(
        object source,
        IReadOnlyDictionary<string, PropertyInfo> sourceProperties,
        ParameterInfo parameter)
    {
        if (!sourceProperties.TryGetValue(parameter.Name ?? string.Empty, out PropertyInfo? property))
        {
            if (parameter.HasDefaultValue)
            {
                return parameter.DefaultValue;
            }

            throw new InvalidOperationException($"No source property matched constructor parameter {parameter.Name}.");
        }

        object? value = property.GetValue(source);

        if (value is null || parameter.ParameterType.IsInstanceOfType(value))
        {
            return value;
        }

        throw new InvalidOperationException(
            $"Source property {property.Name} cannot be assigned to constructor parameter {parameter.Name}.");
    }
}
