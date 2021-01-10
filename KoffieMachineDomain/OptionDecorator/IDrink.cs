using System.Collections.Generic;

namespace KoffieMachineDomain
{
	public interface IDrink
	{
		string Name { get; }
		List<string> CompatibleToppings { get; set; }

		double GetPrice();
		void LogDrinkMaking(ICollection<string> log);
	}
}
