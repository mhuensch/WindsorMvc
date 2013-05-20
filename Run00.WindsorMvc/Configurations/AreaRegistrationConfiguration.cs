using System.Web.Mvc;

namespace Run00.WindsorMvc.Configurations
{
	public class AreaRegistrationConfiguration : IComponentConfiguration
	{
		void IComponentConfiguration.Configure()
		{
			AreaRegistration.RegisterAllAreas();
		}
	}
}