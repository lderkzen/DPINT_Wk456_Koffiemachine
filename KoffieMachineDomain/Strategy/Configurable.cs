using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Strategy
{
	public struct Configurable
	{
		public double Price;
		public Strength LiquorStrength;
		public Amount SugarAmount;
		public Amount WhippedCreamAmount;

		public Configurable(double price, Strength liquorStrength, Amount sugarAmount, Amount whippedCreamAmount)
		{
			Price = price;
			LiquorStrength = liquorStrength;
			SugarAmount = sugarAmount;
			WhippedCreamAmount = whippedCreamAmount;
		}
	}
} 
