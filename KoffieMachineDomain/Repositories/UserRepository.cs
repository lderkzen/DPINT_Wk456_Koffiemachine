using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Repositories
{
	public class UserRepository
	{
		public Dictionary<string, double> Cards { get; set; }

		public UserRepository()
		{
			Cards = new Dictionary<string, double>();
			Cards["Arjen"] = 5.0;
			Cards["Bert"] = 3.5;
			Cards["Chris"] = 7.0;
			Cards["Daan"] = 6.0;
		}

		public void AddUser(string name, double money)
		{
			Cards.Add(name, money);
		}

		public void RemoveUser(string key)
		{
			if (Cards.ContainsKey(key))
				Cards.Remove(key);
		}
	}
}
