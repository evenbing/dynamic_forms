using Orchard.DynamicForms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.DynamicFields
{
    public class MultilineTextField : IDynamicField
    {
        public Localizer T { get; set; }
        public string Name
        {
            get { return "MultiLineTextField"; }
        }

        public string Form
        {
            get { return "SetMultiLineTextProperties"; }
        }

        public Orchard.Localization.LocalizedString Description
        {
            get { return T("A Multiline text field with name and validations."); }
        }
    }
}