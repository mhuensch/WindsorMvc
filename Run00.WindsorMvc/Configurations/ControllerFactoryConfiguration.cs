using System.Web.Mvc;

namespace Run00.WindsorMvc.Configurations
{
	public class ControllerFactoryConfiguration : IComponentConfiguration
	{
		public ControllerFactoryConfiguration(IControllerFactory factory)
		{
			_factory = factory;
		}

		void IComponentConfiguration.Configure()
		{
			ControllerBuilder.Current.SetControllerFactory(_factory);
		}

		private readonly IControllerFactory _factory;
	}
}
