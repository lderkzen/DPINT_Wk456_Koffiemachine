using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain.Drinks
{
	public class Tea : Drink
	{
		private TeaAndChocoLibrary.Tea _tea;

		public override string Name => "Tea";
		public override List<string> CompatibleToppings { get; set; }

		public Tea(Amount sugarAmount, TeaBlend blend)
		{
			CompatibleToppings = new List<string>();
			CompatibleToppings.Add("Sugar");

			_tea = new TeaAndChocoLibrary.Tea();
			_tea.Blend = blend;

			_tea.RemoveSugar();
			switch (sugarAmount)
			{
				case Amount.Few:
					_tea.AddSugar();
					break;
				case Amount.Normal:
					_tea.AddSugar();
					_tea.AddSugar();
					break;
				case Amount.Extra:
					_tea.AddSugar();
					_tea.AddSugar();
					_tea.AddSugar();
					break;
			}
		}

		public override double GetPrice()
		{
			return BaseDrinkPrice;
		}

		public override void LogDrinkMaking(ICollection<string> log)
		{
			base.LogDrinkMaking(log);
			log.Add("Adding hot water...");
		}
	}
}
