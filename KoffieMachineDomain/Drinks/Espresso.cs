using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class Espresso : Drink
    {
        public override string Name => "Espresso";
		public override List<string> CompatibleToppings { get; set; }

		public Espresso()
		{
			CompatibleToppings = new List<string>();
			CompatibleToppings.Add("Sugar");
			CompatibleToppings.Add("Milk");
		}

		public override double GetPrice()
        {
            return BaseDrinkPrice + 0.7;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {Strength.Strong}.");
            log.Add($"Setting coffee amount to {Amount.Few}.");
            log.Add("Filling with coffee...");
            log.Add("Creaming milk...");
            log.Add("Adding milk to coffee...");
        }
    }
}
