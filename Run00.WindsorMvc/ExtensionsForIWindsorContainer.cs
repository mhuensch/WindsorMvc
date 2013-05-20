using Castle.Windsor;
using System;

namespace Run00.WindsorMvc
{
	public static class ExtensionsForIWindsorContainer 
	{
		public static void ConfigureComponents(this IWindsorContainer container)
		{
			var configurations = container.ResolveAll<IComponentConfiguration>();
			foreach (var config in configurations)
				config.Configure();

			foreach (var config in configurations)
				container.Release(config);
		}
	}
}
