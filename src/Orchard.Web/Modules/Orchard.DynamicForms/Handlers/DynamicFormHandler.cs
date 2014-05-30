using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.DynamicForms.Models;
//using Orchard.DynamicForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Orchard.DynamicForms.Handlers
{
    public class DynamicFormPartHandler : ContentHandler
    {
        public void DynamicFormHandler(IRepository<DynamicFormPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }

        
    }
}