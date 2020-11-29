﻿using Kupid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Testovi
{
    /*kreirana je klasa Recenzija1.
     * Ova klasa treba da sadrzi implementaciju metode DatjUtisak(), koja treba da vraca vrijednost "Pozitivan", ukoliko
     *zelimo da nam prodje definisani test.*/

    public class Recenzija1 : IRecenzija
    {


        string IRecenzija.DajUtisak()
        {
            return "Pozitivan";
        }
    }
    [TestClass]
    public class NoviTestovi
    {
        #region Zamjenski Objekti

        [TestMethod]
        public void TestZamjenskiObjekti()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Chat chat = new Chat(k1, k2);
            chat.DodajNovuPoruku(k1, k2, "volim te");
            IRecenzija r = new Recenzija1();

            Komunikator k = new Komunikator();
            bool uspješnost = k.DaLiJeSpajanjeUspjesno(chat, r);

            Assert.IsTrue(uspješnost);
        }

        #endregion

        #region TDD

        [TestMethod]
        public void SpajanjeKorisnikaPoLokaciji()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 25, false);

            Komunikator k = new Komunikator();
            k.RadSaKorisnikom(k1, 0);
            k.RadSaKorisnikom(k2, 0);

            k.SpajanjeKorisnika();

            Assert.AreEqual(k.Razgovori.Count, 1);
        }

        [TestMethod]
        public void SpajanjeKorisnikaPoGodinama()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Trebinje, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Bihać, 20, false);

            Komunikator k = new Komunikator();
            k.RadSaKorisnikom(k1, 0);
            k.RadSaKorisnikom(k2, 0);

            k.SpajanjeKorisnika();

            Assert.AreEqual(k.Razgovori.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SpajanjeKorisnikaIzuzetak()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Trebinje, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Bihać, 25, false);

            Komunikator k = new Komunikator();
            k.RadSaKorisnikom(k1, 0);
            k.RadSaKorisnikom(k2, 0);

            k.SpajanjeKorisnika();
        }

        [TestMethod]
        public void PromjenaParametara()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false, 10);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Bihać, 20, false, 15);
            
            k1.PromjenaParametara(true);

            Assert.AreEqual(k1.Lokacija, k1.ZeljenaLokacija);
            Assert.AreEqual(k1.ZeljeniMinGodina, k1.Godine - 2);

            k2.PromjenaParametara(false);

            Assert.AreNotEqual(k2.Lokacija, k2.ZeljenaLokacija);
            Assert.AreEqual(k2.ZeljeniMaxGodina, k2.Godine + 10);
        }

        [TestMethod]
        public void IzracunajKompatibilnostKorisnika()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 30, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Bihać, 20, false);

            Korisnik k3 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 30, false);
            Korisnik k4 = new Korisnik("user2", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 20, false);

            Korisnik k5 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 30, false, 26, 35);
            Korisnik k6 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Bihać, 20, false, 15, 27);

            Poruka p1 = new Poruka(k1, k2, "Grrr");
            Poruka p2 = new Poruka(k3, k4, "volim te");
            Poruka p3 = new Poruka(k5, k6, "hahahah");

            //Testiramo korisnike sa razlicitim godinama
            double broj1 = p1.IzračunajKompatibilnostKorisnika();

            //Testiramo text "volim te"
            double broj2 = p2.IzračunajKompatibilnostKorisnika();

            //Velika razlika u godinama
            double broj3 = p3.IzračunajKompatibilnostKorisnika();

            Assert.AreEqual(broj1, 25);
            Assert.AreEqual(broj2, 100);
            Assert.AreEqual(broj3, 0);
        }



        #endregion
    }
}
