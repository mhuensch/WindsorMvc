using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web;
using System.Web.Mvc;

namespace Run00.WindsorMvc
{
	public sealed class Installer : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

			container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>().LifeStyle.Singleton);

			container.Register(
				Types.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory))
				.BasedOn<IController>()
				.LifestyleTransient()
			);
		}
	}
}