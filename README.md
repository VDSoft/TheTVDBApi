TheTVDBApi
==========

An api written in .NET C# to communicate with the TheTVDB database project.

See the database at http://thetvdb.com/

A documentation for the api can be found at http://vdsoft.at/Projects/TheTvDbApi/Docu/index.html

See a sample how to use the api in this project: https://seriesmanager.codeplex.com/ 

Visit new Github Page under: http://vdsoft.github.io/TheTVDBApi/

##Use TheTVDBApi with Ninject
With the new added interface definition of the webinterface the api can be easily used with ninject. The following sample shows how.

```csharp
using Ninject;
using TVDB.Web;

namespace Docunamespace
{
    /// <summary>
    /// Class for the docu.
    /// </summary>
    class DocuClass
    {
		/// <summary>
		/// Api key for the application.
		/// </summary>
		private readonly string apiKey = "ABCD12345";
	
		/// <summary>
		/// Setup for ninject.
		/// </summary>
		private void SetupKernel()
		{
			IKernel kernel = new StandardKernel();
	
			kernel.Bind<ITvDb>()
				.To<WebInterface>()
				.InSingletonScope()
				.WithConstructorArgument("apiKey", this.apiKey);
		}
    }
}
```
