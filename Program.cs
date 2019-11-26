using System.Collections;
using System;

namespace Example_06.ChainOfResponsibility
{
    public enum CurrencyType
    {
        Eur,
        Dollar,
        Ruble
    }

    public interface IBanknote
    {
        CurrencyType Currency { get; }
        string Value { get; }
    }

    public class Bancomat
    {
        private readonly BanknoteHandler _handler;

        public Bancomat()
        {
            _handler = new TenRubleHandler(_handler);
            _handler = new FiftyRubleHandler(_handler);
            _handler = new HundredRubleHandler(_handler);
            _handler = new FiveHundredRubleHandler(_handler);
            _handler = new ThousandRubleHandler(_handler);

            _handler = new OneDollarHandler(_handler);
            _handler = new TwoDollarHandler(_handler);
            _handler = new FiveDollarHandler(_handler);
            _handler = new TenDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
        }
        public bool Validate(string banknote)
        {
            return _handler.Validate(banknote);
        }
        public bool CashOut(int cash , CurrencyType currency)
        {
            return _handler.CashOut(cash , currency);
        }
    }

    public abstract class BanknoteHandler
    {
        private readonly BanknoteHandler _nextHandler;

        protected BanknoteHandler(BanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }
        public virtual bool Validate(string banknote)
        {
            return _nextHandler != null && _nextHandler.Validate(banknote);
        }
        public virtual bool CashOut(int cash , CurrencyType currency)
        {
            return _nextHandler != null && _nextHandler.CashOut(cash, currency);
        }

    }

    public abstract class DollarHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.Equals($"{Value}$"))
            {
                return true;
            }
            return base.Validate(banknote);
        }

        protected abstract int Value { get; }

        protected DollarHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        { }
        public override bool CashOut(int cash, CurrencyType currency)
        {
            if ((currency == CurrencyType.Dollar && cash == Value) || cash == 0)
                return true;
            return base.CashOut(cash - ((int)Math.Floor((double)cash/Value)*Value) , currency);
        }
    }
    public abstract class RubleHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.Equals($"{Value}$"))
            {
                return true;
            }
            return base.Validate(banknote);
        }

        protected abstract int Value { get; }

        protected RubleHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        { }
        public override bool CashOut(int cash, CurrencyType currency)
        {
            if ((currency == CurrencyType.Ruble && cash == Value) || cash == 0)
                return true;
            return base.CashOut(cash - ((int)Math.Floor((double)cash / Value) * Value), currency);
        }
    }

    public class TenRubleHandler : RubleHandlerBase
    {
        protected override int Value => 10;

        public TenRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class FiftyRubleHandler : RubleHandlerBase
    {
        protected override int Value => 50;

        public FiftyRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class HundredRubleHandler : RubleHandlerBase
    {
        protected override int Value => 100;

        public HundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class FiveHundredRubleHandler : RubleHandlerBase
    {
        protected override int Value => 500;

        public FiveHundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class ThousandRubleHandler : RubleHandlerBase
    {
        protected override int Value => 1000;

        public ThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class OneDollarHandler : DollarHandlerBase
    {
        protected override int Value => 1;

        public OneDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class TwoDollarHandler : DollarHandlerBase
    {
        protected override int Value => 2;

        public TwoDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class FiveDollarHandler : DollarHandlerBase
    {
        protected override int Value => 5;

        public FiveDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class TenDollarHandler : DollarHandlerBase
    {
        protected override int Value => 10;

        public TenDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class FiftyDollarHandler : DollarHandlerBase
    {
        protected override int Value => 50;

        public FiftyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class HundredDollarHandler : DollarHandlerBase
    {
        protected override int Value => 100;

        public HundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    public class main 
    {
        public static void Main(String[] arg)
        {
            var bankomate = new Bancomat();
            Console.WriteLine(bankomate.CashOut(6507, CurrencyType.Ruble));
            Console.WriteLine(bankomate.CashOut(6507, CurrencyType.Dollar));
        }
    }
    

}