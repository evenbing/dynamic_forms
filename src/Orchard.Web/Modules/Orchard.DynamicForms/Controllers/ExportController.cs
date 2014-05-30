using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.DynamicForms.Services;
using DM.Orchard.DynamicForms.Models;
using System.Web.Mvc;
using Orchard.Data;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orchard.Forms.Services;
using System.Xml;
using System.IO;

namespace Orchard.DynamicForms.Controllers
{
    public class ExportController : Controller
    {
        private readonly IExportToCSVService _exportToCSVService;

        public ExportController(IExportToCSVService exportToCSVService)
        {
            _exportToCSVService = exportToCSVService;
        }


        public ActionResult ExportData(int formID)
        {
            IList<DynamicFormResponseRecord> formData = _exportToCSVService.GetFormData(formID);
            dynamic _JsonData = new JObject();

            var eachResponse = "";
            for (var i = 0; i < formData.Count; i++)
            {
                eachResponse += formData[i].Response;
            }


            return Json(eachResponse, JsonRequestBehavior.AllowGet);
        }

       
        
        
    }
}