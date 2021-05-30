using System.Collections.Generic;

namespace API.Base.Data.EF.Models
{
    public class PagedEntityList<TEntity>
        where TEntity : class
    {
        public List<TEntity> List { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}