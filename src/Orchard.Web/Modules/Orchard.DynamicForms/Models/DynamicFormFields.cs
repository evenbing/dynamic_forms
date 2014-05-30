using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data.Conventions;

namespace Orchard.DynamicForms.Models
{
    public class DynamicFormFieldsRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }


        public virtual string DropZoneID { get; set; }

        [StringLengthMax]
        public virtual string State { get; set; }

        public virtual int ContentID { get; set; }

    }
}