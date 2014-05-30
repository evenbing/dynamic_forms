using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Orchard.DynamicForms.Models
{
    public class DynamicFormPartRecord : ContentPartRecord
    {
        //public virtual int Id { get; set; }
        //public virtual int FormId { get; set; }
        //public virtual bool Enabled { get; set; }

        //[Required, StringLength(1024)]
        //public virtual string Name { get; set; }
    }

    public class DynamicFormPart : ContentPart<DynamicFormPartRecord>
    {
        //public int Id { get { return Record.Id; } set { Record.Id = value; } }
        //public int FormId { get { return Record.FormId; } set { Record.FormId = value; } }
        //public bool Enabled { get { return Record.Enabled; } set { Record.Enabled = value; } }

        //[Required, StringLength(1024)]
        //public string Name { get { return Record.Name; } set { Record.Name = value; } }
    }
}