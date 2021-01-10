using KoffieMachineDomain.Strategy;
using System.Collections.Generic;

namespace KoffieMachineDomain.Drinks
{
	public class SpanishCoffee: Drink
	{
		public override string Name => "Irish Coffee";
		private Configurable _configurable;

		public override List<string> CompatibleToppings { get; set; }

		public SpanishCoffee(Configurable configurable)
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
			log.Add($"Adding cointreau...");
			log.Add($"Adding cognac...");
			log.Add($"Setting sugar amount to {_configurable.SugarAmount}.");
			log.Add($"Adding sugar...");
			log.Add($"Setting whipped cream amount to {_configurable.WhippedCreamAmount}.");
			log.Add($"Adding whipped cream...");
		}
	}
}
