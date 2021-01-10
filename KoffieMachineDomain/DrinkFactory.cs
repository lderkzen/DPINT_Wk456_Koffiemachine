using KoffieMachineDomain.Drinks;
using KoffieMachineDomain.Strategy;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
	public class DrinkFactory
	{
		private Dictionary<string, Configurable> _configurables;

		private IConfigurationReader _configReader;

		public DrinkFactory()
		{
			_configReader = new CSVReader();
			_configurables = _configReader.ReadConfigurations();
		}

		public IDrink CreateDrink(string drinkName, IDictionary<string, Amount> options, Strength strength = 0)
		{
			// Create the Drink
			IDrink drink = null;

			switch (drinkName)
			{
				case "Coffee":
					drink = new Coffee() { DrinkStrength = strength };
					break;
				case "Espresso":
					drink = new Espresso();
					break;
				case "Capuccino":
					drink = new Capuccino();
					break;
				case "Wiener Melange":
					drink = new WienerMelange();
					break;
				case "Café au Lait":
					drink = new CafeAuLait();
					break;
				case "Chocolate":
					drink = new Chocolate();
					break;
				case "Chocolate Deluxe":
					drink = new ChocolateDeluxe();
					break;
				case "Irish Coffee":
					drink = new IrishCoffee(_configurables[drinkName]);
					break;
				case "Italian Coffee":
					drink = new ItalianCoffee(_configurables[drinkName]);
					break;
				case "Spanish Coffee":
					drink = new SpanishCoffee(_configurables[drinkName]);
					break;
			}
			
			if (drink != null)
			// Set price using decorators
				foreach (var option in options)
				{
					switch (option.Key)
					{
						case "Sugar":
							if (drink.CompatibleToppings.Contains("Sugar"))
								drink = new SugarDecorator(drink, option.Value);
							break;
						case "Milk":
							if (drink.CompatibleToppings.Contains("Milk"))
								drink = new MilkDecorator(drink, option.Value);
							break;
					}
				}

			return drink;
		}
	}
}
