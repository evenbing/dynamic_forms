using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.DynamicForms.Models;
using Orchard.DynamicForms.ViewModels;
using Orchard.Security;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Orchard.DynamicForms.Services;
using Orchard.Data;
using Orchard.Forms.Services;
using Orchard.DisplayManagement;
using Orchard.Settings;
using Orchard.Localization;
using Orchard.Core.Title.Models;
using Orchard;

namespace Orchard.DynamicForms.Drivers
{
    public class DynamicFormFieldsDriver : ContentPartDriver<DynamicFormPart>
    {
        private readonly ISiteService _siteService;
        private readonly IRepository<DynamicFormPartRecord> _dynamicFormRecord;
        private readonly IRepository<DynamicFormFieldsRecord> _dynamicFormFieldsRecord;
        private readonly IFormManager _formManager;
        private readonly IDynamicFieldsManager _dynamicFieldsManager;
        private readonly IContentManager _contentManager;
        private readonly IDynamicFormFieldsManager _dynamicFormFieldsManager;
        public DynamicFormFieldsDriver(
            IOrchardServices services,
            IShapeFactory shapeFactory,
            ISiteService siteService,
            IRepository<DynamicFormPartRecord> dynamicFormRecord,
            IRepository<DynamicFormFieldsRecord> dynamicFormFieldsRecord,
            IDynamicFieldsManager dynamicFieldsManager,
            IFormManager formManager,
            IContentManager contentManager,
            IDynamicFormFieldsManager dynamicFormFieldsManager
           )
        {
            _siteService = siteService;
            _dynamicFormRecord = dynamicFormRecord;
            _dynamicFormFieldsRecord = dynamicFormFieldsRecord;
            _dynamicFieldsManager = dynamicFieldsManager;
            _formManager = formManager;
            _contentManager = contentManager;
            _dynamicFormFieldsManager = dynamicFormFieldsManager;
            Services = services;

            T = NullLocalizer.Instance;
            New = shapeFactory;
        }

        dynamic New { get; set; }
        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }


        protected override DriverResult Display(DynamicFormPart part, string displayType, dynamic shapeHelper)
        {
            var dynamicFormDefinitionViewModel = CreateDynamicFormDefinitionViewModel(part.ContentItem.Id);
            return ContentShape("Parts_DynamicForm", () => shapeHelper.Parts_DynamicForm(FormDefinition: dynamicFormDefinitionViewModel));
        }

        //GET
        protected override DriverResult Editor(DynamicFormPart part, dynamic shapeHelper)
        {
            DynamicFormDefinitionViewModel dynamicFormDefinitionViewModel = new DynamicFormDefinitionViewModel();
            dynamicFormDefinitionViewModel = CreateDynamicFormDefinitionViewModel(part.Id);

            var viewModel = new AdminEditViewModel
            {
                DynamicFormDefinition = dynamicFormDefinitionViewModel,
                FormId = part.ContentItem.Id,
                AllDynamicFields = _dynamicFieldsManager.GetDynamicFields(),
            };
            return ContentShape("Parts_DynamicForm_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/DynamicForm",
                    Model: viewModel,
                    Prefix: Prefix));

        }
        //POST
        protected override DriverResult Editor(DynamicFormPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (part.Id != 0)
            {
                var data = HttpContext.Current.Request.Form["data"];
                var state = FormParametersHelper.FromJsonString(data);

                DynamicFormFieldsManager objDynamicFormFieldsManager = new DynamicFormFieldsManager(_dynamicFormFieldsRecord);
                objDynamicFormFieldsManager.CleareFormFields(part.ContentItem.Id);

                foreach (var field in state.DynamicFields)
                {
                    DynamicFormFieldsRecord dynamicFormFieldRecord = new DynamicFormFieldsRecord
                    {
                        ContentID = part.ContentItem.Id,
                        Name = field.Name,
                        DropZoneID = field.DropZoneID,
                        State = FormParametersHelper.ToJsonString(field.State),
                    };
                    objDynamicFormFieldsManager = new DynamicFormFieldsManager(_dynamicFormFieldsRecord);
                    objDynamicFormFieldsManager.SetFormFieldsDetails(dynamicFormFieldRecord);
                }
            }
            else
            {
                DynamicFormPartRecord dynamicFormRecord = new DynamicFormPartRecord
                {
                    //FormId = part.ContentItem.Id,
                    //Name = HttpContext.Current.Request.Form["Title.Title"],
                    //Enabled = HttpContext.Current.Request.Form["DynamicForm.Active.Value"] != null ? true : false,
                };
                DynamicFormManager objDynamicFormManager = new DynamicFormManager(_dynamicFormRecord);
                objDynamicFormManager.SetFormDetails(dynamicFormRecord);
            }

            return Editor(part, shapeHelper);
        }

        //Use DI here
        private DynamicFormDefinitionViewModel CreateDynamicFormDefinitionViewModel(int ContentID)
        {
            var _FieldsRecord = _dynamicFormFieldsManager.GetFormFieldsDetails(ContentID);

            //View Model has ID but not the content part which is ok
            var dynamicFormDefinitionModel = new DynamicFormDefinitionViewModel
            {
                Id = ContentID,
            };

            dynamic form = new JObject();

            form.DynamicFields = new JArray(_FieldsRecord.Select(x =>
            {
                dynamic dynamicfield = new JObject();
                dynamicfield.Name = x.Name;
                dynamicfield.Id = x.Id;
                dynamicfield.DropZoneID = x.DropZoneID;
                dynamicfield.State = FormParametersHelper.FromJsonString(x.State);

                return dynamicfield;
            }));


            dynamicFormDefinitionModel.JsonData = FormParametersHelper.ToJsonString(form);

            return dynamicFormDefinitionModel;
        }

    }
}