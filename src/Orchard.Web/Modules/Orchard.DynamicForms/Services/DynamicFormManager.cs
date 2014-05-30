using Orchard.DynamicForms.Models;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.Services
{
    public class DynamicFormManager : IDynamicFormManager
    {

        private readonly IRepository<DynamicFormPartRecord> _formRepo;

        public DynamicFormManager(IRepository<DynamicFormPartRecord> formRepo)
        {
            _formRepo = formRepo;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }


        public DynamicFormPartRecord GetFormDetails(int FormID)
        {
            DynamicFormPartRecord formRecord = _formRepo.Get(FormID);
            return formRecord;
        }

        public bool SetFormDetails(DynamicFormPartRecord objDynamicFormRecord)
        {
            _formRepo.Create(objDynamicFormRecord);
            _formRepo.Flush();
            return true;
        }


    }
}