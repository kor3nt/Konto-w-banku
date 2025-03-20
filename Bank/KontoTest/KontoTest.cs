using Bank;

namespace KontoTest
{
    [TestClass]
    public class KontoTest
    {
        [TestMethod]
        public void Konto_NazwaKlienta()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            Konto konto = new Konto(klient, bilans);

            Assert.AreEqual(klient, konto.Klient, "Nazwa siê nie zgadza!");
        }

        [TestMethod]
        public void Konto_BilansKlienta()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            Konto konto = new Konto(klient, bilans);

            Assert.AreEqual(bilans, konto.Bilans, "Bilans siê nie zgadza!");
        }

        [TestMethod]
        public void Konto_Dzia³a()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            Konto konto = new Konto(klient, bilans);

            Assert.IsTrue(konto.Zablokowane == false, "Konto zablokowane!");
        }

        [TestMethod]
        public void StworzKonto()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            Konto konto = new Konto(klient, bilans);
            Assert.IsTrue(konto != null, "Wyst¹pi³ b³¹d");
        }

        [TestMethod]
        public void KontoNazwaNull()
        {
            string klient = "";
            decimal bilans = 10000;

            try
            {
                Konto konto = new Konto(klient, bilans);
                Assert.IsTrue(konto.Klient == null, "Utworzono klienta bez nazwy!");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Nazwa konto nie mo¿e byæ pusta!"), "Wystapil blad!");
            }
        }

        [TestMethod]
        public void KontoMinusowyBilans()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;

            Konto konto = new Konto(klient, bilans);
            Assert.IsTrue(konto.Bilans >= 0, "Utworzono konto z bilansem na minusie!");
        }

        [TestMethod]
        public void KontoZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            try
            {
                Konto konto = new Konto(klient, bilans);
                konto.BlokujKonto();
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "B³¹d sprawdŸ wyp³atê!");
            }
        }

        [TestMethod]
        public void KontoOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            Konto konto = new Konto(klient, bilans);
            konto.BlokujKonto();
            konto.OdblokujKonto();

            Assert.IsTrue(konto.Zablokowane == false, "B³¹d, konto zosta³o zablokowane!");
        }

        [TestMethod]
        public void KontoWplataOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            Konto konto = new(klient, bilans);
            decimal kwota = 1000;

            konto.Wplata(kwota);
            Assert.IsTrue(konto.Bilans == (bilans + kwota), "Bilans konta sie nie zgadza!");
        }

        [TestMethod]
        public void KontoWplataZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            try
            {
                Konto konto = new(klient, bilans);
                konto.BlokujKonto();

                decimal kwota = 1000;
                konto.Wplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "B³¹d sprawdŸ wp³atê!");
            }
        }

        [TestMethod]
        public void KontoWyplataOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            Konto konto = new(klient, bilans);
            decimal kwota = 1000;

            konto.Wyplata(kwota);
            Assert.IsTrue(konto.Bilans == (bilans - kwota), "Bilans konta sie nie zgadza!");
        }

        [TestMethod]
        public void KontoWyplataZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            try
            {
                Konto konto = new(klient, bilans);
                konto.BlokujKonto();

                decimal kwota = 1000;
                konto.Wyplata(kwota);
            }
            catch(ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "B³¹d sprawdŸ wyp³atê!");
            }
        }

        [TestMethod]
        public void KontoWplataOdblokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            try
            {
                Konto konto = new(klient, bilans);

                decimal kwota = -1000;
                konto.Wplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Wp³ata musi byæ wiêksza od zera."), "B³¹d sprawdŸ wp³atê!");
            }
        }

        [TestMethod]
        public void KontoWplataZablokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;

            try
            {
                Konto konto = new(klient, bilans);
                konto.BlokujKonto();

                decimal kwota = 1000;
                konto.Wplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "B³¹d sprawdŸ wp³atê!");
            }
        }

        [TestMethod]
        public void KontoWyplataOdblokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;

            try
            {
                Konto konto = new(klient, bilans);

                decimal kwota = 1000;
                konto.Wyplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Brak wystarczaj¹cych œrodków na koncie."), "B³¹d sprawdŸ wyp³ate!");
            }
        }

        [TestMethod]
        public void KontoWyplataZablokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;

            try
            {
                Konto konto = new(klient, bilans);
                konto.BlokujKonto();

                decimal kwota = 1000;
                konto.Wyplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "B³¹d sprawdŸ wyp³ate!");
            }
        }

        [TestMethod]
        public void KontoZmianaNaPlus()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            Konto konto = new(klient, bilans);
            konto = konto.ZmianaNaPlus();

            Assert.IsTrue(konto is KontoPlus, "Zmiana niepoprawna!");
        }
    }
}