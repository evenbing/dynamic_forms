using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Localization;
using Orchard.DisplayManagement;
using Orchard.Forms.Services;

namespace Orchard.DynamicForms.Forms
{
    public class DateTimeFieldForm : IFormProvider
    {

        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public DateTimeFieldForm(IShapeFactory shapeFactory)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, dynamic> form =
                           shape => Shape.Form(
                           Id: "DateTimeField",
                           _Type: Shape.FieldSet(
                               Title: T("Date-Time Fields"),
                               _FieldName: Shape.Textbox(
                                   Id: "Title",
                                   Name: "Title",
                                   Title: T("Field Title"),
                                   Description: T("Specify the Field Title.")                               
                                   )
                               ,
                         _IsRequired: Shape.Checkbox(
                               Id: "IsRequired", Name: "IsRequired", Value:"Yes",
                               Title: T("Required"),
                               Description: T("Mark the Checkbox if field is required.")
                               )
                           
                              )
                           );

            context.Form("SetDateTimeProperties", form);

        }

    }
}