namespace KoffieMachineDomain
{
	public class SugarDecorator : BaseDrinkDecorator
	{
		private static readonly double _fee = 0.1;

		private Amount _amount;

		public SugarDecorator(IDrink drink, Amount amount = 0) : base(drink)
		{
			_amount = amount;
		}

		public override double GetPrice()
		{
			return base.GetPrice() + _fee;
		}
	}
}
