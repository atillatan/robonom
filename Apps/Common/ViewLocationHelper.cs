using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Robonom.Web.Core.Apps.Common
{
    public class ViewLocationHelper : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var value = new Random().Next(0, 1);
            var theme = value == 0 ? "Theme1" : "Theme2";
            context.Values["theme"] = theme;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //@@   /Views/{1}/{0}.cshtml -->/Apps/{1}/{0}.cshtml

            //if (context.IsPartial) return viewLocations;
            var descriptor = (context.ActionContext.ActionDescriptor as ControllerActionDescriptor);
            if (descriptor == null) { return viewLocations; }

            string controllerName = descriptor.ControllerName;
            string actionName = context.ActionContext.ActionDescriptor.DisplayName;


            viewLocations = viewLocations.Select(f => f.Replace("/Views/Shared/", "/wwwroot/data/rb-themes/"));
            viewLocations = viewLocations.Select(g => g.Replace("/Views/", "/wwwroot/apps/"));
            viewLocations = new[]
            {
            "/wwwroot/apps/common/{0}.cshtml"
            }
            .Concat(viewLocations)
            .ToArray();

            return viewLocations;
        }
    }
}
