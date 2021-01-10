using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class CafeAuLait : Drink
    {
        public override string Name => "Café au Lait";
		public override List<string> CompatibleToppings { get; set; }

		public CafeAuLait()
		{
			CompatibleToppings = new List<string>();
		}

		public override double GetPrice()
		{
			return BaseDrinkPrice + 0.5;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Filling half with coffee...");
            log.Add("Filling other half with milk...");
        }
    }
}
