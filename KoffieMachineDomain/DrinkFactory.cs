using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KoffieMachineDomain
{
	public class DrinkFactory
	{
		public IDrink CreateDrink(string drinkName, ref ObservableCollection<string> logText, IDictionary<string, Amount> options, Strength strength = 0)
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
				default:
					logText.Add($"Could not make {drinkName}, recipe not found.");
					break;
			}
			
			if (drink != null)
			// Set price using decorators
				foreach (var option in options)
				{
					switch (option.Key)
					{
						case "Sugar":
							drink = new SugarDecorator(drink, option.Value);
							break;
						case "Milk":
							drink = new MilkDecorator(drink, option.Value);
							break;
					}
				}

			return drink;
		}
	}
}
