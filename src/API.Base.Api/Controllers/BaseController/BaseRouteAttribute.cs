using System;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.Base.Api.Controllers.BaseController
{
    public abstract class BaseRouteAttribute : Attribute, IRouteTemplateProvider
    {
        public abstract string Name { get; }
        public int? Order => 1;

        public virtual string Template
        {
            get
            {
                if (!string.IsNullOrEmpty(routeTemplate))
                    return $"{apiVersionTemplate}/{routeTemplate}/{controllerTemplate}";
                return $"{apiVersionTemplate}/{controllerTemplate}";
            }
        }

        protected string apiVersionTemplate => "v{version:apiVersion}";
        protected string controllerTemplate => "[controller]";
        protected abstract string routeTemplate { get; }
    }

    public class ApiRouteAttribute : BaseRouteAttribute
    {
        private readonly string _name;
        private readonly string _route;

        public ApiRouteAttribute(string name, string route = "")
        {
            _name = name;
            _route = route;
        }

        public override string Name => _name;
        protected override string routeTemplate => _route;
    }
}