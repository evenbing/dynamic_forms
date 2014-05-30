using Orchard.DynamicForms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.DynamicFields
{
    public class DynamicNumericField : IDynamicField
    {
        public Localizer T { get; set; }
        public string Name
        {
            get { return "NumericField"; }
        }

        public string Form
        {
            get { return "SetNumericProperties"; }
        }

        public LocalizedString Description
        {
            get { return T("A Numeric field with name and validations."); }
        }
    }
   
}