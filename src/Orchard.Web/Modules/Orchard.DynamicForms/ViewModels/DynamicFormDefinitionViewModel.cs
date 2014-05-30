using System.Collections.Generic;

namespace Orchard.DynamicForms.ViewModels
{
    public class DynamicFormDefinitionViewModel {
        public int Id { get; set; }
        public string JsonData { get; set; }
    }

    public class FieldViewModel
    {
        /// <summary>
        /// The local id used for connections
        /// </summary>
        //public string ClientId { get; set; }

        /// <summary>
        /// The name of the activity
        /// </summary>
        public string Name { get; set; }

        public IDictionary<string, string> State { get; set; }
    }

    

}