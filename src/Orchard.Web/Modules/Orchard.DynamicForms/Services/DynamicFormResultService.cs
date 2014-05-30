using Orchard.DynamicForms.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.DynamicForms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DM.Orchard.DynamicForms.Models;

namespace DM.Orchard.DynamicForms.Services
{
    public class DynamicFormResultService : IDynamicFormResultService
    {
        private readonly IContentManager _contentManager;
        private readonly IOrchardServices _orchardServices;
        private readonly IRepository<DynamicFormResponseRecord> _responseRepo;
        private readonly IDynamicFormManager _dynamicFormManager;

        public DynamicFormResultService(IContentManager contentManager, IOrchardServices orchardServices,
                           IRepository<DynamicFormResponseRecord> responseRepo, IDynamicFormManager dynamicFormManager)
        {
            this._responseRepo = responseRepo;
            this._orchardServices = orchardServices;
            this._contentManager = contentManager;
            this._dynamicFormManager = dynamicFormManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void SubmitForm(string DATA, int formID)
        {
            var formResult = SaveDynamicFormToDB(DATA, formID);
        }
        private DynamicFormResponseRecord SaveDynamicFormToDB(string DATA, int formID)
        {
            var resultRecord = new DynamicFormResponseRecord
            {
               Response = DATA,
               //DynamicFormDefinitionRecord_id = formID
               //DynamicFormRecord = _dynamicFormManager.GetFormDetails(formID)
               //ContentID = formID
               DynamicFormRecord_id = formID
            };
            this._responseRepo.Create(resultRecord);


            return resultRecord;
        }
    }
}
