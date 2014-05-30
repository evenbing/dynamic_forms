using Orchard.DynamicForms.Models;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.Services
{
    public interface IDynamicFormManager : IDependency
    {
        DynamicFormPartRecord GetFormDetails(int FormID);
        bool SetFormDetails(DynamicFormPartRecord objDynamicFormRecord);

    }
}