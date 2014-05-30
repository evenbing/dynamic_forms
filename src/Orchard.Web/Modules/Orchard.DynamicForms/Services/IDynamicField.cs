using System.Collections.Generic;
using Orchard.Localization;
using Orchard.DynamicForms.Models;
using Orchard;


namespace Orchard.DynamicForms.Services
{
    public interface IDynamicField : IDependency {
        string Name { get; }      
        string Form { get; }
        LocalizedString Description { get; }

    }
}