using Orchard.DynamicForms.Models;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Orchard.DynamicForms.Services
{
    public class DynamicFormFieldsManager : IDynamicFormFieldsManager
    {

        private readonly IRepository<DynamicFormFieldsRecord> _formRepo;

        public DynamicFormFieldsManager(IRepository<DynamicFormFieldsRecord> formRepo)
        {
            _formRepo = formRepo;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }


        public IList<DynamicFormFieldsRecord> GetFormFieldsDetails(int ContentID)
        {
            IList<DynamicFormFieldsRecord> DynamicFormFieldsRecord = _formRepo.Fetch(x => x.ContentID == ContentID).ToList();
            return DynamicFormFieldsRecord;
        }

        public bool SetFormFieldsDetails(DynamicFormFieldsRecord objDynamicFormFieldsRecord)
        {
            _formRepo.Create(objDynamicFormFieldsRecord);
            _formRepo.Flush();
            return true;
        }

        public bool CleareFormFields(int ContentID)
        {
            IEnumerable DynamicFormFieldsRecord = _formRepo.Fetch(x => x.ContentID == ContentID);


            foreach (DynamicFormFieldsRecord dynamicFormFieldRecord in DynamicFormFieldsRecord)
            {
                _formRepo.Delete(dynamicFormFieldRecord);
            }
            _formRepo.Flush();
            return true;
        }


    }
}