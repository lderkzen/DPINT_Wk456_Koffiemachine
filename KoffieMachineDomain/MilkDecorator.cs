namespace KoffieMachineDomain
{
	public class MilkDecorator : BaseDrinkDecorator
	{
		private static readonly double _fee = 0.15;

		private Amount _amount;

		public MilkDecorator(IDrink drink, Amount amount = 0) : base(drink)
		{
			_amount = amount;
		}

		public override double GetPrice()
		{
			return base.GetPrice() + _fee;
		}
	}
}
