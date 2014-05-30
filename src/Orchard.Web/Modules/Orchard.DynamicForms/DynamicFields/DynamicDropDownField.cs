using Orchard.DynamicForms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.DynamicFields
{
    public class DynamicDropDownField : IDynamicField
    {
        public Localizer T { get; set; }
        public string Name
        {
            get { return "DropDownField"; }
        }

        public string Form
        {
            get { return "SetDropDownProperties"; }
        }
        
        public LocalizedString Description
        {
            get { return T("DropDown field and its values"); }
        }
    }
   
}