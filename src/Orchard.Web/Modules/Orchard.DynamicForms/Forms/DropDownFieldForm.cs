using Orchard.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Localization;
using Orchard.DisplayManagement;

namespace Orchard.DynamicForms.Forms
{
    public class DropDownFieldForm : IFormProvider
    {

        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public DropDownFieldForm(IShapeFactory shapeFactory)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, dynamic> form =
                           shape => Shape.Form(
                           Id: "DropDownField",
                           _Type: Shape.FieldSet(
                               Title: T("DropDown Fields"),
                               _FieldName: Shape.Textbox(
                                   Id: "Title",
                                   Name: "Title",
                                   Title: T("Field Title"),
                                   Description: T("Specify the DropDown Title.")                            
                                   )
                               ,
                                _IsRequired: Shape.Checkbox(
                               Id: "IsRequired", Name: "IsRequired",
                               Title: T("Required"),
                               Description: T("Mark the Checkbox if field is required.")
                               ),
                               _DropDownFieldValues: Shape.Textbox(
                                   Id: "DropDownFieldValues",
                                   Name: "DropDownFieldValues",
                                   Title: T("Field Values"),
                                   Description: T("Specify a comma-separated list of all DropDown Values.")                       
                                                
                               )
                               )
                           );

            context.Form("SetDropDownProperties", form);

        }

    }
}