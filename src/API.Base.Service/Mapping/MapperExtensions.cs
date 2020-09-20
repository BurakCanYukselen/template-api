using AutoMapper;

namespace API.Base.Service.Mapping
{
    public static class MapperExtensions
    {
        private static IMapper _mapper;

        public static void SetMapper(IMapper mapper) => _mapper = mapper;

        public static TDestination MapTo<TDestination>(this object source)
            where TDestination : class
        {
            var mappedObject = _mapper.Map<TDestination>(source);
            return mappedObject;
        }
    }
}