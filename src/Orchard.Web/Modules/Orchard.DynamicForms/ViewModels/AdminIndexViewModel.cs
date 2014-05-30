using System.Collections.Generic;
using Orchard.DynamicForms.Models;

namespace Orchard.DynamicForms.ViewModels
{

    public class AdminIndexViewModel {
        public IList<DynamicFormDefinitionEntry> WorkflowDefinitions { get; set; }
        public AdminIndexOptions Options { get; set; }
        public dynamic Pager { get; set; }
    }

    public class DynamicFormDefinitionEntry
    {
        public DynamicFormPartRecord DynamicFormRecord { get; set; }
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public int DynamicFormDefinitionId { get; set; }
        public string Name { get; set; }
    }

    public class AdminIndexOptions {
        public string Search { get; set; }
        public DynamicFormDefinitionOrder Order { get; set; }
        public DynamicFormDefinitionFilter Filter { get; set; }
        public DynamicFormDefinitionBulk BulkAction { get; set; }
    }

    public enum DynamicFormDefinitionOrder
    {
        Name,
        Creation
    }

    public enum DynamicFormDefinitionFilter
    {
        All
    }

    public enum DynamicFormDefinitionBulk
    {
        None,
        Delete
    }
}
