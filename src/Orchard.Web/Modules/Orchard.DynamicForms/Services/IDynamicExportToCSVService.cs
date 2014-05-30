using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DM.Orchard.DynamicForms.Models;
using Orchard.Data;

namespace Orchard.DynamicForms.Services
{
    public interface IExportToCSVService : IDependency
    {
        IList<DynamicFormResponseRecord> GetFormData(int Id);
        IList<DynamicFormResponseRecord> GetFormData();
        
    }

    public class ExportToCSVService : IExportToCSVService
    {
        private IRepository<DynamicFormResponseRecord> _repository;
        public ExportToCSVService(IRepository<DynamicFormResponseRecord> repository)
        {
            _repository = repository;
        }
        public IList<DynamicFormResponseRecord> GetFormData(int Id)
        {
            return _repository.Table.Where(x => x.DynamicFormRecord_id == Id).ToList();
        }
        public IList<DynamicFormResponseRecord> GetFormData()
        {
            return _repository.Table.ToList();
        }
    }

}