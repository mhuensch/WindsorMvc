using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Run00.WindsorMvc
{
	/// <summary>
	/// Factory class responsible for delivering to MVC constructed instances of <see cref="IController"/>, given a 
	/// type. A pre-registered Windsor IOC container is used for the resolution.
	/// </summary>
	public class WindsorControllerFactory : DefaultControllerFactory
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WindsorControllerFactory"/> class. Takes in a Windsor IOC 
		/// container to be used for resolving the controllers.
		/// </summary>
		/// <param name="container">The Windsor IOC container used to resolve the controllers.</param>
		public WindsorControllerFactory(IKernel kernel)
		{
			Contract.Requires(kernel != null);

			this._kernel = kernel;
		}

		/// <summary>
		/// Retrieves the controller instance for the specified request requestContext and controller type.
		/// </summary>
		/// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route 
		/// data.</param>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns>The controller instance.</returns>
		/// <exception cref="System.Web.HttpException"><see cref="controllerType"/> is null.</exception>
		/// <exception cref="System.ArgumentException"><see cref="controllerType"/> cannot be assigned.</exception>
		/// <exception cref="System.InvalidOperationException">An instance of <see cref="controllerType"/> cannot be 
		/// created.</exception>
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			Contract.Assume(_kernel != null);

			var area = requestContext.RouteData.Values["area"];

			if (controllerType == null)
			{
				var type = (controllerType != null) ? controllerType.ToString() : "null";
				var url = "null";
				if (requestContext != null
					&& requestContext.HttpContext != null
					&& requestContext.HttpContext.Request != null
					&& requestContext.HttpContext.Request.Url != null)
				{
					url = requestContext.HttpContext.Request.Url.ToString();
				}

				throw new HttpException(404, string.Format("No Controller found, url:{0}, type:{1}", url, type));
			}

			if (!_kernel.HasComponent(controllerType))
				throw new InvalidOperationException("Windsor container does not have controller type registered:" + controllerType.FullName);

			return (IController)_kernel.Resolve(controllerType);
		}

		/// <summary>
		/// Releases the specified controller.
		/// </summary>
		/// <param name="controller">The controller to release.</param>
		public override void ReleaseController(IController controller)
		{
			_kernel.ReleaseComponent(controller);
			base.ReleaseController(controller);
		}

		private readonly IKernel _kernel;
	}
}
