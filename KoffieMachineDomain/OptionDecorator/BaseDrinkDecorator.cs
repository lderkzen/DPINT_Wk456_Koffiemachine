using System;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
	public abstract class BaseDrinkDecorator : IDrink
	{
		public IDrink Drink;

		public virtual string Name { get => Drink.Name; }

		public List<string> CompatibleToppings { get => Drink.CompatibleToppings; set => Drink.CompatibleToppings = value; }

		public BaseDrinkDecorator(IDrink drink)
		{
			Drink = drink;
		}

		public virtual double GetPrice()
		{
			if (Drink != null)
				return Drink.GetPrice();
			throw new NullReferenceException();
		}

		public virtual void LogDrinkMaking(ICollection<string> log)
		{
			if (Drink != null)
				Drink.LogDrinkMaking(log);

			return;
		}
	}
}
