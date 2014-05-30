using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.DynamicForms.Services;
using Orchard.Data;
using DM.Orchard.DynamicForms.Models;

namespace Orchard.DynamicForms.Services
{
    public class DynamicFormResponseRecordService : IDynamicFormResponseRecordService
    {
        private IRepository<DynamicFormResponseRecord> _dynamicFormRepository;
        public DynamicFormResponseRecordService(IRepository<DynamicFormResponseRecord> dynamicFormRepository)
        {
            _dynamicFormRepository = dynamicFormRepository;
        }
        public bool IsEditable(int FormID)
        {
            bool flag = false;
            var formData =_dynamicFormRepository.Table.Where(x => x.DynamicFormRecord_id == FormID);
            
            return formData.Count() != 0 ? true : flag;
        }
    }
}