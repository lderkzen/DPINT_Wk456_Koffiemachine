using KoffieMachineDomain.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Drinks
{
	public class ItalianCoffee : Drink
	{
		public override string Name => "Italian Coffee";

		private Configurable _configurable;

		public override List<string> CompatibleToppings { get; set; }

		public ItalianCoffee(Configurable configurable)
		{
			CompatibleToppings = new List<string>();

			_configurable = configurable;
		}

		public override double GetPrice()
		{
			return _configurable.Price;
		}

		public override void LogDrinkMaking(ICollection<string> log)
		{
			base.LogDrinkMaking(log);
			// All the toppings are added here because the options aren't for the user to pick but come pre-configured

			log.Add($"Filling with coffee...");
			log.Add($"Setting liquor strength to {_configurable.LiquorStrength}.");
			log.Add($"Adding amaretto...");
			log.Add($"Setting sugar amount to {_configurable.SugarAmount}.");
			log.Add($"Adding sugar...");
			log.Add($"Setting whipped cream amount to {_configurable.WhippedCreamAmount}.");
			log.Add($"Adding whipped cream...");
		}
	}
}
