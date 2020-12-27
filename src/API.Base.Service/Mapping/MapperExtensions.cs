using System;
using System.Linq.Expressions;
using System.Reflection;
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

        public static TDestination MapTo<TDestination>(this object source, params (Expression<Func<TDestination, object>> expression, object value)[] expressionValuePairs)
            where TDestination : class
        {
            var mappedObject = _mapper.Map<TDestination>(source);
            foreach (var expressionValuePair in expressionValuePairs)
            {
                var expression = (MemberExpression) expressionValuePair.expression.Body;
                var property = expression.Member as PropertyInfo;
                property!.SetValue(mappedObject, expressionValuePair.value);
            }

            return mappedObject;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreUnmapped<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mapper)
        {
            mapper.ForAllOtherMembers(p => p.Ignore());
            return mapper;
        }
    }
}