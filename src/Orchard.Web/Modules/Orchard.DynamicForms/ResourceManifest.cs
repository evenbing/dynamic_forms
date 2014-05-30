using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            builder.Add().DefineStyle("DynamicFormsAdmin").SetUrl("Orchard-DynamicForms-Admin.css").SetDependencies("~/Themes/TheAdmin/Styles/Site.css");

            builder.Add().DefineScript("jsPlumb").SetUrl("jquery.jsPlumb-1.4.1-all-min.js").SetDependencies("jQueryUI");
            builder.Add().DefineScript("jsKnockout").SetUrl("knockout-2.2.1.js");
        }
    }
}