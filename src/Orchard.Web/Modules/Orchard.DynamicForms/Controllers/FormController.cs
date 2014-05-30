using DM.Orchard.DynamicForms.Services;
using Orchard.DynamicForms.Services;
using Newtonsoft.Json.Linq;
using Orchard.Data;
using Orchard.DynamicForms.ViewModels;
using Orchard.Forms.Services;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orchard.DynamicForms.Controllers
{
    public class FormController : Controller
    {
        private readonly IDynamicFormResultService _dynamicformresultservice;

        public FormController(IDynamicFormResultService dynamicformresultservice)
        {
            _dynamicformresultservice = dynamicformresultservice;
        }

       
        [HttpPost, Themed]
        public ActionResult FormResponse(int formID, FormCollection controlCollection, string formName)
        {
           
            string allD = "";
            var s = "";
            foreach (var key in controlCollection.Keys)
            {
                if (!key.Equals("__RequestVerificationToken") && !key.Equals("formID") && !key.Equals("formName"))
                {
                    string eachD = "\"" + key + "\"" + ":" + "\"" + controlCollection[key.ToString()] + "\"" + ",";

                    allD = eachD + allD;

                    s = "[{" + allD + "}]";

                }
            }
            string D = s.Replace(",}]", "}]");
            string DATA = D.Replace(":\",", ":\"");


            _dynamicformresultservice.SubmitForm(DATA, formID);



           
            return View("Thankyou");       
        }      
    }
}