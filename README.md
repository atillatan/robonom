# Robonom
Simple blog framework


## All request handled DefaultController


```csharp
app.UseMvc(routes =>
            {
                // route1 : if we have a Controller we use Spacific NamedController
                routes.MapRoute(
                    "default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Default", action = "Index" });

                //route2 : if we don't have a Controller we use DefaultController
                routes.MapRoute(
                    "DefaultController",
                    "{*catchall}",
                    new { controller = "Default", action = "Index" }
                );

                //route3 : for all api call 
                routes.MapRoute("defaultApi",
                    "api/{controller}/{id?}"
                    );
            });
```

## How is it works

- Request: http://localhost:5000/subfolder1/default

read page information from `/wwwroot/data/pages/subfolder1/default.json`

Example pageInfo:

```json
{
  "layout": "~/wwwroot/data/themes/filehoo/theme1.cshtml",
  "permalink": "/downloads/softwarename/",
  "name": "softwarename",
  "title": "softwarename",
  "type": "file/folder",
  "description": "The AbsoluteFTP client application is the easy, powerful way to transfer files using FTP, with a simple Explorer-like interface...",
  "tags": "[Others, tag2, tag3",
  "category": "Internet",
  "sort_order": "100",
  "rating": "100",
  "changefreq": "monthly",
  "priority": "0.5",
  "published": "true",
  "hidden": "false",
  "authorized": "false",
  "default_document": "",
  "cached": "true",
  "content_type": "markdown",
  "create_date": "2017-10-23",
  "modified_date": "2017-10-23",
  "created_by": "atilla",
  "modified_by": "atilla",
  "comments": "true",
  "redirect_url": "",
  "version": "2.2.7"
}
```

- Load layour: `~/wwwroot/data/themes/filehoo/theme1.cshtml`

Example layout content

```html
@using System.ComponentModel
@using Robonom.Common
@using Robonom.Apps
@using Microsoft.AspNetCore.Mvc.ViewComponents
@using System.Threading.Tasks
@addTagHelper "*, Microsoft.AspNetCore.Mvc.TagHelpers"
@model dynamic

<!DOCTYPE html>
<html lang="en">
     @await Html.PartialAsync("/wwwroot/App_Data/_includes/header.cshtml")

<body id="body">

     <header class="container">
             @await Html.PartialAsync("/wwwroot/App_Data/_includes/nav-top.cshtml")
    </header>

    <section class="container">
        <div class="row">
                
            <div class="col-md-3">
                @await Html.PartialAsync("/wwwroot/App_Data/_includes/nav-left.cshtml")
            </div>
                
            <div class="col-md-9 content">
               <article>
                @await Component.InvokeAsync("Content")
                        
              </article>
            </div>
                
     
                
        </div>
    </section>
            <br>
        <footer class="container">
              @await Html.PartialAsync("/wwwroot/App_Data/_includes/footer.cshtml")
        </footer>
</body>
</html>






```

- After that, every modules responsible their own task
   - Every module can acceess "pageInfo"
   - Every module run according to page info