using AutoMapper;

namespace Simple.Common.Helpers;

public static class MapperHelper
{
    private static IMapper? _mapper;

    public static IMapper Mapper
    {
        get
        {
            if (_mapper == null) throw new NullReferenceException(nameof(Mapper));
            return _mapper;
        }
    }

    public static void Configure(IMapper? mapper)
    {
        if(_mapper != null)
        {
            throw new Exception($"{nameof(Mapper)}不可修改！");
        }
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public static TDestination Map<TDestination>(object source)
    {
        return Mapper.Map<TDestination>(source);
    }

    public static TDestination Map<TSource, TDestination>(TSource source)
    {
        return Mapper.Map<TSource, TDestination>(source);
    }

    public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return Mapper.Map<TSource, TDestination>(source, destination);
    }

    public static object Map(object source, Type sourceType, Type destinationType)
    {
        return Mapper.Map(source, sourceType, destinationType);
    }

    public static object Map(object source, object destination, Type sourceType, Type destinationType)
    {
        return Mapper.Map(source, destination, sourceType, destinationType);
    }
}
