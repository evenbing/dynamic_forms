﻿using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Roles.Models;
using Orchard.Roles.Services;
using Orchard.DynamicForms.Models;
using Orchard.DynamicForms.Services;

namespace Orchard.DynamicForms.DynamicFields
{
    public class DynamicTextField : IDynamicField
    {
        public Localizer T { get; set; }
        public string Name
        {
            get { return "TextField"; }
        }

        public string Form
        {
            get { return "SetTextProperties"; }
        }


        public LocalizedString Description
        {
            get { return T("A text field with name and validations."); }
        }
    }
}