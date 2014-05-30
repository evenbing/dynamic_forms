using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json.Linq;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Mvc.Extensions;
using Orchard.Security;
using Orchard.Themes;
using System;
using Orchard.Settings;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;
using Orchard.DynamicForms.Models;
using Orchard;
using Orchard.DynamicForms.ViewModels;
using Orchard.DynamicForms.Services;
//using Orchard.Core.Contents.Controllers;



namespace Orchard.DynamicForms.Controllers
{
    [ValidateInput(false)]
    public class AdminController : Controller, IUpdateModel
    {
        private readonly ISiteService _siteService;
        private readonly IRepository<DynamicFormPartRecord> _DynamicFormFieldsRecord;
        private readonly IFormManager _formManager;
        private readonly IDynamicFieldsManager _dynamicFieldsManager;
        private readonly IDynamicFormResponseRecordService _dynamicFormRecord;

        public AdminController(
           IOrchardServices services,
           IShapeFactory shapeFactory,
           ISiteService siteService,
           IRepository<DynamicFormPartRecord> dynamicFormFieldsRecord,
           IDynamicFieldsManager dynamicFieldsManager,
           IFormManager formManager,
            IDynamicFormResponseRecordService dynamicFormRecord
           )
        {
            _siteService = siteService;
            _DynamicFormFieldsRecord = dynamicFormFieldsRecord;
            _dynamicFieldsManager = dynamicFieldsManager;
            _formManager = formManager;
            _dynamicFormRecord = dynamicFormRecord;
            Services = services;

            T = NullLocalizer.Instance;
            New = shapeFactory;
        }

        dynamic New { get; set; }
        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }


        [HttpPost, ActionName("Edit")]
        [FormValueRequired("submit.Save")]
        public ActionResult EditPost(int formID, string data)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to edit dynamic forms")))
                return new HttpUnauthorizedResult();

            var dynamicFormDefinitionRecord = _DynamicFormFieldsRecord.Get(formID);

            if (dynamicFormDefinitionRecord == null)
            {
                return HttpNotFound();
            }

            
            var state = FormParametersHelper.FromJsonString(data);
          

            Services.Notifier.Information(T("DynamicForm saved successfully"));

            return RedirectToAction("Edit", new { formID });
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("submit.Cancel")]
        public ActionResult EditPostCancel()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to edit dynamicforms")))
                return new HttpUnauthorizedResult();

            return View();
        }


        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        public void AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }

        [Themed(false)]
        [HttpPost]
        public ActionResult RenderField(FieldViewModel model)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to edit workflows")))
                return new HttpUnauthorizedResult();

            var dfield = _dynamicFieldsManager.GetDynamicFieldByName(model.Name);

            if (dfield == null)
            {
                return HttpNotFound();
            }

            dynamic shape = New.DynamicField(dfield);

            if (model.State != null)
            {
                var state = FormParametersHelper.ToDynamic(FormParametersHelper.ToString(model.State));
                shape.State(state);
            }
            else
            {
                shape.State(FormParametersHelper.FromJsonString("{}"));
            }

            shape.Metadata.Alternates.Add("Field__" + dfield.Name);

            return new ShapeResult(this, shape);
        }


        public ActionResult EditField(string FieldName, string ClientId, int FormId)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to edit fields")))
                return new HttpUnauthorizedResult();

            var field = _dynamicFieldsManager.GetDynamicFieldByName(FieldName);

            if (field == null)
            {
                return HttpNotFound();
            }

            // build the form, and let external components alter it
            var form = field.Form == null ? null : _formManager.Build(field.Form);

            // form is bound on client side
            var viewModel = New.ViewModel(fieldname: FieldName, Form: form, clientid: ClientId, formid: FormId);

            return View(viewModel);
        }

        [HttpPost, ActionName("EditField")]
        [FormValueRequired("_submit.Save")]
        public ActionResult EditFieldPost(string FieldName, int formID, FormCollection formValues, string ClientID)
        {
            var returnUrl = Request.QueryString["returnUrl"];
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to edit fields")))
                return new HttpUnauthorizedResult();

            var field = _dynamicFieldsManager.GetDynamicFieldByName(FieldName);

            if (field == null)
            {
                return HttpNotFound();
            }

            // validating form values
            _formManager.Validate(new ValidatingContext { FormName = field.Form, ModelState = ModelState, ValueProvider = ValueProvider });

            // stay on the page if there are validation errors
            if (!ModelState.IsValid)
            {

                // build the form, and let external components alter it
                var form = field.Form == null ? null : _formManager.Build(field.Form);

                // bind form with existing values.
                _formManager.Bind(form, ValueProvider);

                var viewModel = New.ViewModel(formid: formID, Form: form);

                return View(viewModel);
            }

            var model = new UpdatedFieldModel
            {
              
                dynamicFieldClientId = ClientID,
                Data = formValues
            };

            TempData["UpdatedViewModel"] = model;

            return Redirect(returnUrl);
        }


        [HttpPost, ActionName("EditField")]
        [FormValueRequired("_submit.Cancel")]
        public ActionResult EditFieldPostCancel(string FieldName, int formID, FormCollection formValues, string ClientID)
        {
            
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to edit dynamicforms")))
                return new HttpUnauthorizedResult();

            var returnUrl = Request.QueryString["returnUrl"];
            return Redirect(returnUrl);
        }
        /// <summary>
        /// Check if particular form is active/editable or not
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public ActionResult IsFormEditable(int FormId)
        {
            var flag = _dynamicFormRecord.IsEditable(FormId);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }
    }
}