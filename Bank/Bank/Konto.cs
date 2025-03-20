namespace Bank
{
    public class Konto
    {
        private string klient;  //nazwa klienta
        public string Klient
        {
            get => klient;
            private set => klient = value.Trim() != "" ? value.Trim() : throw new ArgumentException("Nazwa konto nie może być pusta!");
        }

        private decimal bilans;  //aktualny stan środków na koncie
        public decimal Bilans
        {
            get => bilans;
            private set => bilans = value > 0 ? value : 0;
        }

        private bool zablokowane = false; //stan konta
        public bool Zablokowane 
        { 
            get => zablokowane;
            private set => zablokowane = false; 
        }

        //private Konto() { }

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            Klient = klient;
            Bilans = bilansNaStart;
        }

        public virtual void Wplata(decimal kwota)
        {
            if (Zablokowane) throw new ArgumentException("Konto jest zablokowane.");
            if (kwota <= 0) throw new ArgumentException("Wpłata musi być większa od zera.");

            Bilans += kwota;
        }

        public virtual void Wyplata(decimal kwota)
        {
            if (Zablokowane) throw new ArgumentException("Konto jest zablokowane.");
            if (kwota <= 0) throw new ArgumentException("Wypłata musi być większa od zera.");
            if (Bilans < kwota) throw new ArgumentException("Brak wystarczających środków na koncie.");

            Bilans -= kwota;
        }

        public void BlokujKonto() => zablokowane = true;
        public void OdblokujKonto() => zablokowane = false;

        public override string ToString()
        {
            string status = Zablokowane ? "Zablokowane" : "Odblokowane";
            return $"Nazwa: {Klient}\n Bilans: {Bilans}\n Status: {status}";
        }

        public void ZmodyfikujBilans(decimal money)
        {
            Bilans += money;
        }

        public KontoPlus ZmianaNaPlus()
        {
            KontoPlus temp = new(Klient, Bilans, 10000);
            return temp;
        }
        public Konto ZmianaKonta()
        {
            if (Bilans < 0) throw new ArgumentException("Spłać najpierw zadłużenia!");

            Konto temp = new(Klient, Bilans);
            return temp;
        }
    }

    public class KontoPlus : Konto
    {
        private decimal limit;
        public decimal Limit
        {
            get => limit;
            set => limit = value > 0 ? value : 0;
        }
        public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limit = 100) : base(klient, bilansNaStart)
        {
            Limit = limit;
        }

        public override void Wyplata(decimal kwota)
        {
            if (Zablokowane) throw new ArgumentException("Konto jest zablokowane.");
            if (kwota <= 0) throw new ArgumentException("Wypłata musi być większa od zera.");
            if ((Bilans + Limit) < kwota) throw new ArgumentException("Brak wystarczających środków na koncie.");

            base.ZmodyfikujBilans(-kwota);

            if (Bilans < 0) base.BlokujKonto();
        }

        public override void Wplata(decimal kwota)
        {
            if (kwota <= 0) throw new ArgumentException("Wpłata musi być większa od zera.");

            base.ZmodyfikujBilans(kwota);

            if (Bilans >= 0) base.OdblokujKonto();
        }

        public override string ToString()
        {
            string status = Zablokowane ? "Zablokowane" : "Odblokowane";
            return $"Nazwa: {Klient}\n Bilans: {Bilans} ({Limit})\n Status: {status}";
        }
    }

    public class KontoLimit
    {
        public static explicit operator KontoPlus(KontoLimit konto)
        {
            return new KontoPlus(konto.Klient, konto.Bilans, konto.Limit);
        }

        public static explicit operator Konto(KontoLimit konto)
        {
            return new Konto(konto.Klient, konto.Bilans);
        }

        private Konto konto { get; set; }
        private decimal limit { get; set; }
        public decimal Bilans { get => konto.Bilans; }
        public string Klient { get => konto.Klient; }
        public bool Zablokowane { get => konto.Zablokowane; }
        public decimal Limit
        {
            get => limit;
            set => limit = value > 0 ? value : 0;
        }

        public KontoLimit(string name, decimal bilansNaStart = 0, decimal limit = 100)
        {
            konto = new Konto(name, bilansNaStart);
            Limit = limit;
        }

        public void Wyplata(decimal kwota)
        {
            if (Zablokowane) throw new ArgumentException("Konto jest zablokowane.");
            if (kwota <= 0) throw new ArgumentException("Wypłata musi być większa od zera.");
            if ((Bilans + Limit) < kwota) throw new ArgumentException("Brak wystarczających środków na koncie.");

            konto.ZmodyfikujBilans(-kwota);

            if (Bilans < 0) konto.BlokujKonto();
        }

        public void Wplata(decimal kwota)
        {
            if (kwota <= 0) throw new ArgumentException("Wpłata musi być większa od zera.");

            konto.ZmodyfikujBilans(kwota);

            if (Bilans >= 0) konto.OdblokujKonto();
        }

        public void BlokujKonto() => konto.BlokujKonto();
        public void OdblokujKonto() => konto.OdblokujKonto();

        public override string ToString()
        {
            string status = Zablokowane ? "Zablokowane" : "Odblokowane";
            return $"Nazwa: {Klient}\n Bilans: {Bilans} ({Limit})\n Status: {status}";
        }
    }
}
