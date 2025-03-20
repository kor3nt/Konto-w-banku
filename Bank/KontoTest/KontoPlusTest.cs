using Bank;

namespace KontoTest
{
    [TestClass]
    public class KontoPlusTest
    {
        [TestMethod]
        public void PlusKonto_NazwaKlienta()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);

            Assert.AreEqual(klient, konto.Klient, "Nazwa się nie zgadza!");
        }

        [TestMethod]
        public void PlusKonto_BilansKlienta()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);

            Assert.AreEqual(bilans, konto.Bilans, "Bilans się nie zgadza!");
        }

        [TestMethod]
        public void PlusKonto_Działa()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);

            Assert.IsTrue(konto.Zablokowane == false, "Konto zablokowane!");
        }

        [TestMethod]
        public void PlusStworzKonto()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);
            Assert.IsTrue(konto != null, "Wystąpił błąd");
        }

        [TestMethod]
        public void PlusKontoNazwaNull()
        {
            string klient = "";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoPlus konto = new KontoPlus(klient, bilans, limit);
                Assert.IsTrue(konto.Klient == null, "Utworzono klienta bez nazwy!");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Nazwa konto nie może być pusta!"), "Wystapil blad!");
            }
        }

        [TestMethod]
        public void PlusKontoMinusowyBilans()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);
            Assert.IsTrue(konto.Bilans >= 0, "Utworzono konto z bilansem na minusie!");
        }

        [TestMethod]
        public void PlusKontoZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoPlus konto = new KontoPlus(klient, bilans, limit);
                konto.BlokujKonto();
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "Błąd sprawdź wypłatę!");
            }
        }

        [TestMethod]
        public void PlusKontoOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);
            konto.BlokujKonto();
            konto.OdblokujKonto();

            Assert.IsTrue(konto.Zablokowane == false, "Błąd, konto zostało zablokowane!");
        }

        [TestMethod]
        public void PlusKontoWplataOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);
            decimal kwota = 1000;

            konto.Wplata(kwota);
            Assert.IsTrue(konto.Bilans == (bilans + kwota), "Bilans konta sie nie zgadza!");
        }

        [TestMethod]
        public void PlusKontoWplataZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoPlus konto = new KontoPlus(klient, bilans, limit);
                konto.BlokujKonto();

                decimal kwota = 1000;
                konto.Wplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "Błąd sprawdź wpłatę!");
            }
        }

        [TestMethod]
        public void PlusKontoWyplataOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);
            decimal kwota = 1000;

            konto.Wyplata(kwota);
            Assert.IsTrue(konto.Bilans == (bilans - kwota), "Bilans konta sie nie zgadza!");
        }

        [TestMethod]
        public void PlusKontoWyplataZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoPlus konto = new KontoPlus(klient, bilans, limit);
                konto.BlokujKonto();

                decimal kwota = 1000;
                konto.Wyplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "Błąd sprawdź wypłatę!");
            }
        }

        [TestMethod]
        public void PlusKontoWplataOdblokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoPlus konto = new KontoPlus(klient, bilans, limit);

                decimal kwota = -1000;
                konto.Wplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Wpłata musi być większa od zera."), "Błąd sprawdź wpłatę!");
            }
        }

        [TestMethod]
        public void PlusKontoWplataZablokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;
            decimal limit = 1000;

            try
            {
                KontoPlus konto = new KontoPlus(klient, bilans, limit);
                konto.BlokujKonto();

                decimal kwota = 1000;
                konto.Wplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "Błąd sprawdź wpłatę!");
            }
        }

        [TestMethod]
        public void PlusKontoWyplataOdblokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;
            decimal limit = 1000;

            try
            {
                KontoPlus konto = new KontoPlus(klient, bilans, limit);

                decimal kwota = 1000;
                konto.Wyplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Brak wystarczających środków na koncie."), "Błąd sprawdź wypłate!");
            }
        }

        [TestMethod]
        public void PlusKontoWyplataZablokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;
            decimal limit = 1000;

            try
            {
                KontoPlus konto = new KontoPlus(klient, bilans, limit);
                konto.BlokujKonto();

                decimal kwota = 1000;
                konto.Wyplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "Błąd sprawdź wypłate!");
            }
        }

        [TestMethod]
        public void PlusZmianaNaNormal()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoPlus konto = new KontoPlus(klient, bilans, limit);

            var konto2 = konto.ZmianaKonta();

            Assert.IsFalse(konto2 is KontoPlus, "Zmiana niepoprawna!");
        }
    }
}
