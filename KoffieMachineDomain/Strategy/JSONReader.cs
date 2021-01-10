using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Strategy
{
	public class JSONReader : IConfigurationReader
	{
		public Dictionary<string, Configurable> ReadConfigurations()
		{
			// Just here for demonstration purposes
			throw new NotImplementedException();
		}
	}
}
