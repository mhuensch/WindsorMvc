using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace Run00.WindsorMvc
{
	public static class ExtensionsForIWindsorContainer 
	{
		public static void InstallFrom(this IWindsorContainer container, IEnumerable<string> directories)
		{
			foreach (var dir in directories)
				container.Install(FromAssembly.InDirectory(new AssemblyFilter(dir)));
		}

		public static void InstallFromBin(this IWindsorContainer container)
		{
			container.Install(FromAssembly.InDirectory(new AssemblyFilter(HttpRuntime.BinDirectory)));
		}

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
