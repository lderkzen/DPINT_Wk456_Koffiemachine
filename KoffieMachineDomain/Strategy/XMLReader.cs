using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace KoffieMachineDomain.Strategy
{
	public class XMLReader : IConfigurationReader
	{
		public Dictionary<string, Configurable> ReadConfigurations()
		{
			// Just here for demonstration purposes
			throw new NotImplementedException();
		}
	}
}
