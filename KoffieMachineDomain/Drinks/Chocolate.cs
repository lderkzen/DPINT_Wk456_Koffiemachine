using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain.Drinks
{
	public class Chocolate : Drink
	{
		private HotChocolate _chocolate;

		public override string Name => _chocolate.GetNameOfDrink();
		public override List<string> CompatibleToppings { get; set; }

		public Chocolate()
		{
			CompatibleToppings = new List<string>();

			_chocolate = new HotChocolate();
		}

		public override double GetPrice()
		{
			return _chocolate.Cost();
		}

		public override void LogDrinkMaking(ICollection<string> log)
		{
			base.LogDrinkMaking(log);

			foreach (string step in _chocolate.GetBuildSteps())
			{
				log.Add(step);
			}
		}
	}
}
