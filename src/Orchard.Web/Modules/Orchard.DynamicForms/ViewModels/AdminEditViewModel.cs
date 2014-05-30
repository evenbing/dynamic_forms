using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.DynamicForms.Services;


namespace Orchard.DynamicForms.ViewModels
{
    public class AdminEditViewModel {
        public int FormId { get; set; }
        public IEnumerable<IDynamicField> AllDynamicFields { get; set; }
        public DynamicFormDefinitionViewModel DynamicFormDefinition { get; set; }
    }

    public class UpdatedFieldModel {
        public string dynamicFieldClientId { get; set; }
        public FormCollection Data { get; set; }

    }
}