using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Robonom.Web.Core.wwwroot.apps.rb_editor
{
    public class RbEditorController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  RedirectResult Save(RbEditorModel m)
        {

            
            return Redirect("/");
        }
    }
}
