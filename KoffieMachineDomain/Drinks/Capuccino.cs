using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class Capuccino : Drink
    {
        public override string Name => "Capuccino";
		public override List<string> CompatibleToppings { get; set; }
		protected virtual Strength DrinkStrength { get; set; }

        public Capuccino()
		{
			CompatibleToppings = new List<string>();
			CompatibleToppings.Add("Sugar");

			DrinkStrength = Strength.Normal;
        }

        public override double GetPrice()
        {
            return BaseDrinkPrice + 0.8;
        }
        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with coffee...");
            log.Add("Creaming milk...");
            log.Add("Adding milk to coffee...");
        }
    }
}
