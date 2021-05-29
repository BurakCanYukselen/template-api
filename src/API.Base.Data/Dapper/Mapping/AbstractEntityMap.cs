using System;
using System.Linq.Expressions;
using API.Base.Data.Entities.Base;
using Dapper.FluentMap.Mapping;

namespace API.Base.Data.Dapper.Mapping
{
    public abstract class AbstractEntityMap<TEntity> : EntityMap<TEntity> where TEntity : AbstractEntity
    {
        public AbstractEntityMap()
        {
            Map(p => p.Id, "id");
            SetEntityMaps();
        }

        protected abstract void SetEntityMaps();

        protected void Map(params (Expression<Func<TEntity, object>> property, string columnName)[] mappingPairs)
        {
            foreach (var mappingPair in mappingPairs)
            {
                Map(mappingPair.property, mappingPair.columnName);
            }
        }

        protected void Map(Expression<Func<TEntity, object>> property, string columnName)
        {
            Map(property).ToColumn(columnName);
        }
    }
}