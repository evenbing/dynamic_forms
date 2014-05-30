using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.Orchard.DynamicForms.Services
{
    public interface IDynamicFormResultService : IDependency
    {
        void SubmitForm(string DATA, int formID);
    }
}