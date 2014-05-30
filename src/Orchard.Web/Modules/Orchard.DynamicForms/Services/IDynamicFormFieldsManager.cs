using Orchard.DynamicForms.Models;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.Services
{
    public interface IDynamicFormFieldsManager : IDependency
    {
        IList<DynamicFormFieldsRecord> GetFormFieldsDetails(int ContentID);
        bool SetFormFieldsDetails(DynamicFormFieldsRecord objDynamicFormRecord);

    }
}