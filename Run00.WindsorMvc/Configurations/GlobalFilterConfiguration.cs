using System.Collections.Generic;
using System.Web.Mvc;

namespace Run00.WindsorMvc.Configurations
{
	public class GlobalFilterConfiguration : IComponentConfiguration
	{
		public GlobalFilterConfiguration(GlobalFilterCollection filterCollection, IEnumerable<IMvcFilter> mvcFilters)
		{
			_filterCollection = filterCollection;
			_mvcFilters = mvcFilters;
		}

		void IComponentConfiguration.Configure()
		{
			foreach (var filter in _mvcFilters)
				_filterCollection.Add(filter);
		}

		private readonly GlobalFilterCollection _filterCollection;
		private readonly IEnumerable<IMvcFilter> _mvcFilters;
	}
}