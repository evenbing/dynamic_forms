using Orchard.DynamicForms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.DynamicFields
{
    public class DynamicDateTimeField : IDynamicField
    {
        public Localizer T { get; set; }
        public string Name
        {
            get { return "DateTimeField"; }
        }

        public string Form
        {
            get { return "SetDateTimeProperties"; }
        }

        public LocalizedString Description
        {
            get { return T("A Date-Time field with name and validations."); }
        }
    }
  
}