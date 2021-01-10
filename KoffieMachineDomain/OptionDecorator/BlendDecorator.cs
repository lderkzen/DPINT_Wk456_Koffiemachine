using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain.OptionDecorator
{
	public class BlendDecorator : BaseDrinkDecorator
	{
		private TeaBlend _blend;

		public override string Name => _blend.Name + " tea";

		public BlendDecorator(IDrink drink, TeaBlend blend) : base(drink)
		{
			_blend = blend;
		}

		public override double GetPrice()
		{
			return base.GetPrice();
		}

		public override void LogDrinkMaking(ICollection<string> log)
		{
			base.LogDrinkMaking(log);
			log.Add($"Adding blend {_blend.Name}.");
		}
	}
}
