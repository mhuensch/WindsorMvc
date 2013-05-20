using System.Web.Mvc;
using System.Web.Routing;

namespace Run00.WindsorMvc.Configurations
{
	public class DefaultRouteConfiguration : IComponentConfiguration
	{
		public DefaultRouteConfiguration(RouteCollection routes)
		{
			_routes = routes;
		}

		void IComponentConfiguration.Configure()
		{
			_routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
		}

		private readonly RouteCollection _routes;
	}
}