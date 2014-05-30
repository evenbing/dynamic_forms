using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.DynamicForms.Forms
{
    public class MultiLineTextFieldForm : IFormProvider
    {
        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public MultiLineTextFieldForm(IShapeFactory shapeFactory)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }
        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, dynamic> form =
                            shape => Shape.Form(
                           Id: "MultiLineTextField",
                                _Type: Shape.FieldSet(
                                    Title: T("MultiLIne Text Field"),
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
                                ),
                            _Dimension:Shape.Textbox(
                                   Id: "Dimension",
                                   Name: "Dimension",
                                   Title: T("Dimension"),
                                   Description: T("Specify a comma-separated value for rows and columns.")
                                   )
                                );

            context.Form("SetMultiLineTextProperties", form);
        }
    }
}