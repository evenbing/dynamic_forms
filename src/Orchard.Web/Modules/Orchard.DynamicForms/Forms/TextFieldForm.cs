using System;
using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;

namespace Orchard.DynamicForms.Forms
{
    public class TextFieldForm : IFormProvider
    {

        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public TextFieldForm(IShapeFactory shapeFactory)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, dynamic> form =
                           shape => Shape.Form(
                          Id: "TextField",
                               _Type: Shape.FieldSet(
                                   Title: T("Text Field"),
                               _FieldName: Shape.Textbox(
                                   Id: "Title",
                                   Name: "Title",
                                   Title: T("Field Title"),
                                   Description: T("Specify the field title.")
                               //)
                               ),
                         _IsRequired: Shape.Checkbox(
                               Id: "IsRequired", Name: "IsRequired",
                               Title: T("Required"),
                               Description: T("Mark the Checkbox if field is required.")
                               ),
                           _Regex: Shape.Textbox(
                               Id: "Regex", Name: "Regex",
                               Title: T("Regular Expression"),
                               Description: T("Regular expression for the field.")
                               )
                               )
                               );


            //var form = Shape.Form(
            //           Id: "ActionDelay",
            //           _Amount: Shape.Textbox(
            //               Id: "Amount", Name: "Amount",
            //               Title: T("Amount"),
            //               Classes: new[] { "text-small" }),
            //           _Type: Shape.SelectList(
            //               Id: "Unity", Name: "Unity",
            //               Title: T("Amount type"))
            //               .Add(new SelectListItem { Value = "Minute", Text = T("Minutes").Text, Selected = true })
            //               .Add(new SelectListItem { Value = "Hour", Text = T("Hours").Text })
            //               .Add(new SelectListItem { Value = "Day", Text = T("Days").Text })
            //               .Add(new SelectListItem { Value = "Week", Text = T("Weeks").Text })
            //               .Add(new SelectListItem { Value = "Month", Text = T("Months").Text })
            //           );



            context.Form("SetTextProperties", form);

        }
    }
}