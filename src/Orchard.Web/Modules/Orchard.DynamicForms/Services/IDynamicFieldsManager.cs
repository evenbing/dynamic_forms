using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.Services
{
    public interface IDynamicFieldsManager : IDependency
    {
        IEnumerable<IDynamicField> GetDynamicFields();
        IDynamicField GetDynamicFieldByName(string name);
    }
}