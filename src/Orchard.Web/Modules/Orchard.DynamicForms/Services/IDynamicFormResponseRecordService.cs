using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.Services
{
    public interface IDynamicFormResponseRecordService : IDependency
    {
        bool IsEditable(int FormID);
    }
}