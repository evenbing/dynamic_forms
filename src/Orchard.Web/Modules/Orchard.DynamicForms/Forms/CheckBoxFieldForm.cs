using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.Forms
{
    public class CheckBoxFieldForm : IFormProvider
    {

        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public CheckBoxFieldForm(IShapeFactory shapeFactory)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, dynamic> form =
                           shape => Shape.Form(
                           Id: "CheckBoxField",
                           _Type: Shape.FieldSet(
                               Title: T("CheckBox Field"),
                               _FieldName: Shape.Textbox(
                                   Id: "Title",
                                   Name: "Title",
                                   Title: T("Field Title")
                                   )
                               ,
                         _IsRequired: Shape.Checkbox(
                               Id: "IsRequired", Name: "IsRequired",
                               Title: T("Required"),
                               Description: T("Mark the Checkbox if field is required.")
                               ),
                       _CheckBoxFieldValues: Shape.Textbox(
                                   Id: "CheckBoxFieldValues",
                                   Name: "CheckBoxFieldValues",
                                   Title: T("Field Values"),
                                   Description: T("Specify a comma-separated list of all CheckBox Values.")

                               )
                               )
                           );

            context.Form("SetCheckBoxProperties", form);

        }

    }

}