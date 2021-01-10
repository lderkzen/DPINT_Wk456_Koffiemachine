using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain.Drinks
{
	public class ChocolateDeluxe : Drink
	{
		private HotChocolate _chocolate;

		public override string Name => _chocolate.GetNameOfDrink();
		public override List<string> CompatibleToppings { get; set; }

		public ChocolateDeluxe()
		{
			CompatibleToppings = new List<string>();

			_chocolate = new HotChocolate();
			_chocolate.MakeDeluxe();
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
