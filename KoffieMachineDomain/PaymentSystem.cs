using KoffieMachineDomain.Repositories;
using System;

namespace KoffieMachineDomain
{
	public class PaymentSystem
	{
		public event EventHandler<PriceChanged> RemainingPriceChanged;
		public double RemainingPriceToPay { get; set; }

		private UserRepository _users;

		public PaymentSystem(UserRepository users)
		{
			_users = users;
		}

		public void PayWithCash(double insertedMoney)
		{
			Math.Max(Math.Round(RemainingPriceToPay - insertedMoney, 2), 0);

			RemainingPriceChanged.Invoke(this, new PriceChanged
			{
				RemainingPrice = RemainingPriceToPay,
				InsertedMoney = insertedMoney,
				UserRepository = _users
			});
		}

		public void PayWithCard(string selectedPaymentCard)
		{

			double insertedMoney = _users.Cards[selectedPaymentCard];
			if (RemainingPriceToPay <= insertedMoney)
			{
				_users.Cards[selectedPaymentCard] = insertedMoney - RemainingPriceToPay;
				RemainingPriceToPay = 0;
			} else
			{
				_users.Cards[selectedPaymentCard] = 0;

				RemainingPriceToPay -= insertedMoney;
			}

			RemainingPriceChanged.Invoke(this, new PriceChanged
			{
				RemainingPrice = RemainingPriceToPay,
				InsertedMoney = insertedMoney,
				UserRepository = _users
			});
		}

		public class PriceChanged : EventArgs
		{
			public double RemainingPrice { get; set; }
			public double InsertedMoney { get; set; }
			public UserRepository UserRepository { get; set; }
		}
	}
}
