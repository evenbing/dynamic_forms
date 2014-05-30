using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data.Conventions;


namespace DM.Orchard.DynamicForms.Models
{
    public class DynamicFormResponseRecord
    {
        public virtual int Id { get; set; }

        /// <summary>
        /// The type of the activity.
        /// </summary>

        //    public virtual int DynamicFormDefinitionRecord_id { get; set; }

        public virtual int DynamicFormRecord_id { get; set; }
        //public virtual DynamicFormPartRecord DynamicFormRecord { get; set; }
        //public virtual int ContentID { get; set; }
        /// <summary>
        /// The top coordinate of the activity.
        /// </summary>

        [StringLengthMax]
        public virtual string Response { get; set; }


    }
}