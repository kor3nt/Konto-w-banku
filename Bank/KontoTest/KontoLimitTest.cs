using Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontoTest
{
    [TestClass]
    public class KontoLimitTest
    {
        [TestMethod]
        public void LimitKonto_NazwaKlienta()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoLimit konto = new KontoLimit(klient, bilans, limit);

            Assert.AreEqual(klient, konto.Klient, "Nazwa się nie zgadza!");
        }

        [TestMethod]
        public void LimitKonto_BilansKlienta()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoLimit konto = new KontoLimit(klient, bilans, limit);

            Assert.AreEqual(bilans, konto.Bilans, "Bilans się nie zgadza!");
        }

        [TestMethod]
        public void LimitKonto_Działa()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoLimit konto = new KontoLimit(klient, bilans, limit);

            Assert.IsTrue(konto.Zablokowane == false, "Konto zablokowane!");
        }

        [TestMethod]
        public void LimitStworzKonto()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoLimit konto = new KontoLimit(klient, bilans, limit);
            Assert.IsTrue(konto != null, "Wystąpił błąd");
        }

        [TestMethod]
        public void LimitKontoNazwaNull()
        {
            string klient = "";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoLimit konto = new KontoLimit(klient, bilans, limit);
                Assert.IsTrue(konto.Klient == null, "Utworzono klienta bez nazwy!");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Nazwa konto nie może być pusta!"), "Wystapil blad!");
            }
        }

        [TestMethod]
        public void LimitKontoMinusowyBilans()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;
            decimal limit = 1000;

            KontoLimit konto = new KontoLimit(klient, bilans, limit);
            Assert.IsTrue(konto.Bilans >= 0, "Utworzono konto z bilansem na minusie!");
        }

        [TestMethod]
        public void LimitKontoZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoLimit konto = new KontoLimit(klient, bilans, limit);
                konto.BlokujKonto();
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Konto jest zablokowane."), "Błąd sprawdź wypłatę!");
            }
        }

        [TestMethod]
        public void LimitKontoOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoLimit konto = new KontoLimit(klient, bilans, limit);
            konto.BlokujKonto();
            konto.OdblokujKonto();

            Assert.IsTrue(konto.Zablokowane == false, "Błąd, konto zostało zablokowane!");
        }

        [TestMethod]
        public void LimitKontoWplataOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoLimit konto = new KontoLimit(klient, bilans, limit);
            decimal kwota = 1000;

            konto.Wplata(kwota);
            Assert.IsTrue(konto.Bilans == (bilans + kwota), "Bilans konta sie nie zgadza!");
        }

        [TestMethod]
        public void LimitKontoWplataZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoLimit konto = new KontoLimit(klient, bilans, limit);
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
        public void LimitKontoWyplataOdblokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            KontoLimit konto = new KontoLimit(klient, bilans, limit);
            decimal kwota = 1000;

            konto.Wyplata(kwota);
            Assert.IsTrue(konto.Bilans == (bilans - kwota), "Bilans konta sie nie zgadza!");
        }

        [TestMethod]
        public void LimitKontoWyplataZablokowane()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoLimit konto = new KontoLimit(klient, bilans, limit);
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
        public void LimitKontoWplataOdblokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;
            decimal limit = 1000;

            try
            {
                KontoLimit konto = new KontoLimit(klient, bilans, limit);

                decimal kwota = -1000;
                konto.Wplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Wpłata musi być większa od zera."), "Błąd sprawdź wpłatę!");
            }
        }

        [TestMethod]
        public void LimitKontoWplataZablokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;
            decimal limit = 1000;

            try
            {
                KontoLimit konto = new KontoLimit(klient, bilans, limit);
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
        public void LimitKontoWyplataOdblokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;
            decimal limit = 1000;

            try
            {
                KontoLimit konto = new KontoLimit(klient, bilans, limit);

                decimal kwota = 1000;
                konto.Wyplata(kwota);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains("Brak wystarczających środków na koncie."), "Błąd sprawdź wypłate!");
            }
        }

        [TestMethod]
        public void LimitKontoWyplataZablokowaneNegatywna()
        {
            string klient = "Klaudiusz";
            decimal bilans = -10000;
            decimal limit = 1000;

            try
            {
                KontoLimit konto = new KontoLimit(klient, bilans, limit);
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
        public void LimitZmianaNaNormal()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            KontoLimit konto = new KontoLimit(klient, bilans);

            Konto konto2 = (Konto)konto;

            Assert.IsTrue(konto2 is Konto, "Zmiana niepoprawna!");
        }

        [TestMethod]
        public void LimitZmianaNaPlus()
        {
            string klient = "Klaudiusz";
            decimal bilans = 10000;

            KontoLimit konto = new KontoLimit(klient, bilans);

            Konto konto2 = (KontoPlus)konto;

            Assert.IsTrue(konto2 is KontoPlus, "Zmiana niepoprawna!");
        }
    }
}
