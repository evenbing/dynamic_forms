using Orchard.DynamicForms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.DynamicFields
{
    public class DynamicCheckBoxField : IDynamicField
    {
        public Localizer T { get; set; }
        public string Name
        {
            get { return "CheckBoxField"; }
        }

        public string Form
        {
            get { return "SetCheckBoxProperties"; }
        }

        public LocalizedString Description
        {
            get { return T("A field to select a option"); }
        }
    }
}