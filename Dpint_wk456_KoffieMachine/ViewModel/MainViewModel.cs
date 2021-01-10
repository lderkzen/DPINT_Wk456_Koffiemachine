using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KoffieMachineDomain;
using KoffieMachineDomain.Repositories;
using KoffieMachineDomain.OptionDecorator;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TeaAndChocoLibrary;

namespace Dpint_wk456_KoffieMachine.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<string> LogText { get; private set; }
		public ObservableCollection<string> Teablends { get; set; }
		private Dictionary<string, double> _cashOnCards;
		private DrinkFactory _factory;
		private UserRepository _userRepository;
		private TeaBlendRepository _blendRepository;
		private PaymentSystem _paymentSystem;

		public MainViewModel()
        {
			_factory = new DrinkFactory();
			_userRepository = new UserRepository();
			_blendRepository = new TeaBlendRepository();
			_paymentSystem = new PaymentSystem(_userRepository);
			_paymentSystem.RemainingPriceChanged += HandlePayment;
            _coffeeStrength = Strength.Normal;
            _sugarAmount = Amount.None;
            _milkAmount = Amount.None;

            LogText = new ObservableCollection<string>();
            LogText.Add("Starting up...");
            LogText.Add("Done, what would you like to drink?");

			_cashOnCards = _userRepository.Cards;
            PaymentCardUsernames = new ObservableCollection<string>(_cashOnCards.Keys);
            SelectedPaymentCardUsername = PaymentCardUsernames[0];

			Teablends = new ObservableCollection<string>(_blendRepository.BlendNames);
			_selectedBlend = Teablends[0];
        }

		#region Drink properties to bind to
		private IDrink _selectedDrink;
        public string SelectedDrinkName
        {
            get { return _selectedDrink?.Name; }
        }

        public double? SelectedDrinkPrice
        {
            get { return _selectedDrink?.GetPrice(); }
        }

		private string _selectedBlend;
		public string SelectedTeablend {
			get { return _selectedBlend; }
			set {
				_selectedBlend = value;
				RaisePropertyChanged(() => SelectedTeablend);
			}
		}

        #endregion Drink properties to bind to

        #region Payment
        public RelayCommand PayByCardCommand => new RelayCommand(() =>
		{
			_paymentSystem.RemainingPriceToPay = RemainingPriceToPay;
			_paymentSystem.PayWithCard(SelectedPaymentCardUsername);
		});

        public ICommand PayByCoinCommand => new RelayCommand<double>(coinValue =>
        {
			_paymentSystem.RemainingPriceToPay = RemainingPriceToPay;
			_paymentSystem.PayWithCash(coinValue);
		});

		private void HandlePayment(object sender, PaymentSystem.PriceChanged e)
		{
			RemainingPriceToPay = e.RemainingPrice;
			_userRepository = e.UserRepository;
			_cashOnCards = _userRepository.Cards;

			RaisePropertyChanged(() => PaymentCardRemainingAmount);

			LogText.Add($"Inserted {e.InsertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");
			if (_selectedDrink != null && RemainingPriceToPay <= 0)
				MakeDrink();
		}

		private void MakeDrink()
		{
			_selectedDrink.LogDrinkMaking(LogText);
			LogText.Add($"Finished making {_selectedDrink.Name}");
			LogText.Add("------------------");
			_selectedDrink = null;
		}
        

        public double PaymentCardRemainingAmount => _cashOnCards.ContainsKey(SelectedPaymentCardUsername ?? "") ? _cashOnCards[SelectedPaymentCardUsername] : 0;

        public ObservableCollection<string> PaymentCardUsernames { get; set; }
        private string _selectedPaymentCardUsername;
        public string SelectedPaymentCardUsername
        {
            get { return _selectedPaymentCardUsername; }
            set
            {
                _selectedPaymentCardUsername = value;
                RaisePropertyChanged(() => SelectedPaymentCardUsername);
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }
        }

        private double _remainingPriceToPay;
        public double RemainingPriceToPay
        {
            get { return _remainingPriceToPay; }
            set { _remainingPriceToPay = value; RaisePropertyChanged(() => RemainingPriceToPay); }
        }
        #endregion Payment

        #region Coffee buttons
        private Strength _coffeeStrength;
        public Strength CoffeeStrength
        {
            get { return _coffeeStrength; }
            set { _coffeeStrength = value; RaisePropertyChanged(() => CoffeeStrength); }
        }

        private Amount _sugarAmount;
        public Amount SugarAmount
        {
            get { return _sugarAmount; }
            set { _sugarAmount = value; RaisePropertyChanged(() => SugarAmount); }
        }

        private Amount _milkAmount;
        public Amount MilkAmount
        {
            get { return _milkAmount; }
            set { _milkAmount = value; RaisePropertyChanged(() => MilkAmount); }
        }

        public ICommand DrinkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
			RemainingPriceToPay = 0;

			Dictionary<string, Amount> options = new Dictionary<string, Amount>();
			if (SugarAmount != Amount.None)
				options.Add("Sugar", SugarAmount);
			if (MilkAmount != Amount.None)
				options.Add("Milk", MilkAmount);

			_selectedDrink = _factory.CreateDrink(drinkName, options, _coffeeStrength);

			if (_selectedDrink != null)
				PrintSelectionAndPrice();
			else
				LogText.Add($"Could not make {drinkName}, recipe not found.");
		});

		public ICommand TeaCommand => new RelayCommand<string>((drinkName) =>
		{
			_selectedDrink = null;
			RemainingPriceToPay = 0;

			if (SugarAmount != Amount.None)
				_selectedDrink = new BlendDecorator(new SugarDecorator(new KoffieMachineDomain.Drinks.Tea(SugarAmount, _blendRepository.GetTeaBlend(SelectedTeablend)), SugarAmount), _blendRepository.GetTeaBlend(SelectedTeablend));
			else
				_selectedDrink = new BlendDecorator(new KoffieMachineDomain.Drinks.Tea(SugarAmount, _blendRepository.GetTeaBlend(SelectedTeablend)), _blendRepository.GetTeaBlend(SelectedTeablend));

			PrintSelectionAndPrice();
		});

		private void PrintSelectionAndPrice()
		{
			bool hasSugar = false;
			bool hasMilk = false;
			if (SugarAmount != Amount.None && _selectedDrink.CompatibleToppings.Contains("Sugar"))
				hasSugar = true;
			if (MilkAmount != Amount.None && !_selectedDrink.CompatibleToppings.Contains("Milk"))
				hasMilk = true;
			RemainingPriceToPay = _selectedDrink.GetPrice();
			LogText.Add($"Selected {_selectedDrink.Name}{(hasSugar ? hasMilk ? " with sugar and milk" : " with sugar" : hasMilk ? " with Milk" : "")}, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");
			RaisePropertyChanged(() => RemainingPriceToPay);
			RaisePropertyChanged(() => SelectedDrinkName);
			RaisePropertyChanged(() => SelectedDrinkPrice);
		}

        #endregion Coffee buttons
    }
}