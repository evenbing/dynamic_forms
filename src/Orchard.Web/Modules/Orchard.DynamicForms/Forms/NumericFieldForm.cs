using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.Forms
{
    public class NumericFieldForm  : IFormProvider
    {

        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public NumericFieldForm(IShapeFactory shapeFactory)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, dynamic> form =
                           shape => Shape.Form(
                           Id: "NumericField",
                           _Type: Shape.FieldSet(
                               Title: T("Numeric Field"),
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
                           _Regex: Shape.Textbox(
                               Id: "Regex", Name: "Regex",
                               Title: T("Regular Expression"),
                               Description: T("Regular expression for the field,example:[0-9]{min,max}")                               
                               )
                               )
                           );

            context.Form("SetNumericProperties", form);

        }

    }
  
}