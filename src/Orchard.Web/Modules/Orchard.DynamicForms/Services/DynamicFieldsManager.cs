using System.Collections.Generic;
using System.Linq;
using Orchard.Utility.Extensions;

namespace Orchard.DynamicForms.Services
{
    public class DynamicFieldsManager : IDynamicFieldsManager
    {
        private readonly IEnumerable<IDynamicField> _dynamicFields;

        public DynamicFieldsManager(IEnumerable<IDynamicField> dynamicFields)
        {
            _dynamicFields = dynamicFields;
        }

        public IEnumerable<IDynamicField> GetDynamicFields() {
            return _dynamicFields.OrderBy(x => x.Name).ToReadOnlyCollection();
        }
        public IDynamicField GetDynamicFieldByName(string name) {
            return _dynamicFields.FirstOrDefault(x => x.Name == name);
        }

    }
}