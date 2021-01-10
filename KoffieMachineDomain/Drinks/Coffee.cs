using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class Coffee : Drink
    {
        public virtual Strength DrinkStrength { get; set; }

		public override string Name => "Koffie";
		public override List<string> CompatibleToppings { get; set; }

		public Coffee()
		{
			CompatibleToppings = new List<string>();
			CompatibleToppings.Add("Sugar");
			CompatibleToppings.Add("Milk");
		}

        public override double GetPrice()
        {
            return BaseDrinkPrice;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with coffee...");
        }
    }
}
