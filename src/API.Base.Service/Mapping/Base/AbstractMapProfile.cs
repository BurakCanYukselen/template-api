using System;
using System.Linq.Expressions;
using AutoMapper;

namespace API.Base.Service.Mapping.Base
{
    public abstract class AbstractMapProfile : Profile
    {
        public AbstractMapProfile()
        {
            SetServiceToEntityMaps();
            SetEntityToServiceMaps();
        }

        protected abstract void SetServiceToEntityMaps();
        protected abstract void SetEntityToServiceMaps();

        protected void Map<TSource, TDestination>(params (Expression<Func<TSource, object>> from, Expression<Func<TDestination, object>> to)[] expressionPairs)
        {
            var mappingExpression = CreateMap<TSource, TDestination>().IgnoreUnmapped();
            foreach (var expressionPair in expressionPairs)
            {
                mappingExpression.ForMember(expressionPair.to, cfg => cfg.MapFrom(expressionPair.from));
            }
        }
    }
}