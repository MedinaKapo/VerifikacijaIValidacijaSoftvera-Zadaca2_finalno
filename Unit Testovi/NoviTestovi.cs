using Kupid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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

        static IEnumerable<object[]> Podaci
        {
            get
            {
                return new[]
              { new object[] { "user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false},
                new object[] { "user2", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true},
                new object[] { "user3", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false }
                };
            }
        }
        static IEnumerable<object[]> PodaciKorisnikValidni
        {
            get
            {
                return new[]
              { new object[] { "user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false},
                new object[] { "user2", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true},
                new object[] { "user3", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false }
                };
            }
        }

        static IEnumerable<object[]> PodaciKorisnikNeValidni
        {
            get
            {
                return new[]
              { new object[] { "korisnikkorisnikkorisnik", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false},
                new object[] { null,"user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true}
                };
            }
        }


        static IEnumerable<object[]> PodaciKorisnikNeValidni2
        {
            get
            {
                return new[]
                {new object[] { "user3", null, Lokacija.Sarajevo, Lokacija.Tuzla, 20, false },
                new object[] { "user4", "etf", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false },
                new object[] { "user5", "user5*", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false }
};
            }
        }

        static IEnumerable<object[]> PodaciKorisnikNeValidni3
        {
            get
            {
                return new[]
                {new object[] { "user5", "user5*+", Lokacija.Sarajevo, Lokacija.Tuzla, 14, false },
                new object[] { "user6", "user6*+", Lokacija.Sarajevo, Lokacija.Tuzla, 10, false }
};
            }
        }

        static IEnumerable<object[]> PodaciKorisnikNeValidni4
        {
            get
            {
                return new[]
                { new object[] { "user6", "user6*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false, 0,35}
};
            }
        }


        static IEnumerable<object[]> PodaciKorisnikNeValidni5
        {
            get
            {
                return new[]
                { new object[] { "user6", "user6*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false,25,0 },
                  new object[] { "user6", "user6*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false,9,0 }
};
            }
        }

        static IEnumerable<object[]> ChatPodaciNeValidni
        {
            get
            {
                return new[]
                { new object[] { DateTime.Today.AddDays(1) },
                  new object[] { DateTime.Today.AddDays(5) }
};
            }
        }

        static IEnumerable<object[]> ChatPodaciValidni
        {
            get
            {
                return new[]
                { new object[] { DateTime.Today.AddDays(-1) },
                  new object[] { DateTime.Today.AddDays(-5) }
};
            }
        }


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
            Korisnik k6 = new Korisnik("user2", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 20, true, 15, 27);

            Korisnik k7 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 30, false, 26, 35);
            Korisnik k8 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Bihać, 30, false, 25, 27);

            Korisnik k9 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 30, false, 26, 35);
            Korisnik k10 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Bihać, 30, false, 26, 37);

            Poruka p1 = new Poruka(k1, k2, "Grrr");
            Poruka p2 = new Poruka(k3, k4, "volim te");
            Poruka p3 = new Poruka(k5, k6, "hahahah");
            Poruka p4 = new Poruka(k7, k8, "hohohoh");
            Poruka p5 = new Poruka(k9, k10, "kikiki");

            //Testiramo korisnike sa razlicitim godinama
            double broj1 = p1.IzračunajKompatibilnostKorisnika();

            //Testiramo text "volim te"
            double broj2 = p2.IzračunajKompatibilnostKorisnika();

            //Velika razlika u godinama
            double broj3 = p3.IzračunajKompatibilnostKorisnika();

            double broj4 = p4.IzračunajKompatibilnostKorisnika();

            double broj5 = p5.IzračunajKompatibilnostKorisnika();

            Assert.AreEqual(broj1, 25);
            Assert.AreEqual(broj2, 100);
            Assert.AreEqual(broj3, 0);
            Assert.AreEqual(broj4, 50);
            Assert.AreEqual(broj5, 75);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void IzracunajKompatibilnostKorisnika1()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 30, false, 7, 20);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 20, true, 20, 7);

            Poruka p = new Poruka(k1, k2, "hahha");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IzlistavanjaSvihPorukaSaSadrzajem1()
        {
            string sadrzaj = "sadrzaj";
            Komunikator k = new Komunikator();
            k.IzlistavanjeSvihPorukaSaSadržajem(sadrzaj);
        }

        [TestMethod]
        public void IzlistavanjeSvihPorukaSaSadrzajem2()
        {
            Komunikator k = new Komunikator();
            string sadrzaj1 = "matematika mi je bila draga";
            Korisnik korisnik1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false);
            Korisnik korisnik2 = new Korisnik("user2", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true);
            Chat chat1 = new Chat(korisnik1, korisnik2);
            chat1.DodajNovuPoruku(korisnik1, korisnik2, sadrzaj1);
            List<Korisnik> prvaLista = new List<Korisnik>();
            prvaLista.Add(korisnik1);
            prvaLista.Add(korisnik2);
            string sadrzaj2 = "matematika mi nije bila draga";
            Korisnik korisnik3 = new Korisnik("user3", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 25, false);
            Korisnik korisnik4 = new Korisnik("user4", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true);
            Chat chat2 = new Chat(korisnik3, korisnik4);
            chat2.DodajNovuPoruku(korisnik3, korisnik4, sadrzaj2);
            List<Korisnik> drugaLista = new List<Korisnik>();
            drugaLista.Add(korisnik3);
            drugaLista.Add(korisnik4);
            string sadrzaj3 = "fizika mi nije bila draga";
            Korisnik korisnik5 = new Korisnik("user5", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 25, false);
            Korisnik korisnik6 = new Korisnik("user6", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true);
            Chat chat3 = new Chat(korisnik5, korisnik6);
            chat2.DodajNovuPoruku(korisnik5, korisnik6, sadrzaj3);
            List<Korisnik> trecaLista = new List<Korisnik>();
            trecaLista.Add(korisnik5);
            trecaLista.Add(korisnik6);
            string sadrzaj = "matematika";
            k.RadSaKorisnikom(korisnik1, 0);
            k.RadSaKorisnikom(korisnik2, 0);
            k.RadSaKorisnikom(korisnik3, 0);
            k.RadSaKorisnikom(korisnik4, 0);
            k.RadSaKorisnikom(korisnik5, 0);
            k.RadSaKorisnikom(korisnik6, 0);
            k.Razgovori.Add(chat1);
            k.Razgovori.Add(chat2);
            k.Razgovori.Add(chat3);

            List<Poruka> rezultatFunckije = k.IzlistavanjeSvihPorukaSaSadržajem(sadrzaj);
            Assert.AreEqual(rezultatFunckije.Count, 2);

        }

        [TestMethod]
        public void IzlistavanjeSvihPorukaSaSadrzajemGrupni()
        {
            Komunikator k = new Komunikator();
            string sadrzaj1 = "matematika mi je bila draga";
            Korisnik korisnik1 = new Korisnik("admin", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false);
            Korisnik korisnik2 = new Korisnik("user2", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true);
            Chat chat1 = new Chat(korisnik1, korisnik2);
            chat1.DodajNovuPoruku(korisnik1, korisnik2, sadrzaj1);
            List<Korisnik> prvaLista = new List<Korisnik>();
            prvaLista.Add(korisnik1);
            prvaLista.Add(korisnik2);
            string sadrzaj2 = "matematika mi nije bila draga";
            Korisnik korisnik3 = new Korisnik("user3", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 25, false);
            Korisnik korisnik4 = new Korisnik("user4", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true);
            Chat chat2 = new Chat(korisnik3, korisnik4);
            chat2.DodajNovuPoruku(korisnik3, korisnik4, sadrzaj2);
            List<Korisnik> drugaLista = new List<Korisnik>();
            drugaLista.Add(korisnik3);
            drugaLista.Add(korisnik4);
            drugaLista.Add(korisnik1);
            GrupniChat gChat1 = new GrupniChat(drugaLista);
            gChat1.PosaljiPorukuViseKorisnika(korisnik1, drugaLista, sadrzaj2);
            string sadrzaj3 = "matematika mi nije bila draga";
            Korisnik korisnik5 = new Korisnik("user5", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 25, false);
            Korisnik korisnik6 = new Korisnik("user6", "user2*+", Lokacija.Tuzla, Lokacija.Bihać, 25, true);
            Chat chat3 = new Chat(korisnik5, korisnik6);
            chat3.DodajNovuPoruku(korisnik5, korisnik6, sadrzaj3);
            List<Korisnik> trecaLista = new List<Korisnik>();
            trecaLista.Add(korisnik5);
            trecaLista.Add(korisnik6);
            string sadrzaj = "matematika";
            k.RadSaKorisnikom(korisnik1, 0);
            k.RadSaKorisnikom(korisnik2, 0);
            k.RadSaKorisnikom(korisnik3, 0);
            k.RadSaKorisnikom(korisnik4, 0);
            k.RadSaKorisnikom(korisnik5, 0);
            k.RadSaKorisnikom(korisnik6, 0);
            k.Razgovori.Add(chat1);
            k.Razgovori.Add(gChat1);
            k.Razgovori.Add(chat3);

            List<Poruka> rezultatFunckije = k.IzlistavanjeSvihPorukaSaSadržajem(sadrzaj);
            Assert.AreEqual(rezultatFunckije.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IzlistavanjeSvihPorukaSaSadrzajem3()
        {
            Komunikator k = new Komunikator();
            k.IzlistavanjeSvihPorukaSaSadržajem("matematika");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [DynamicData("Podaci")]
        public void sadrzajPorukeIzuzetak(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            string sadrzaj = "";
            Poruka poruka1 = new Poruka(korisnik1, korisnik2, sadrzaj);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [DynamicData("Podaci")]
        public void sadrzajPorukeIzuzetak2(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            string sadrzaj = "pogrdna riječ";
            Poruka poruka1 = new Poruka(korisnik1, korisnik2, sadrzaj);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DynamicData("Podaci")]
        public void KonstruktorIzuzetak(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = null;
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            string sadrzaj = "sadrzaj";
            Poruka poruka1 = new Poruka(korisnik1, korisnik2, sadrzaj);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [DynamicData("Podaci")]
        public void DaLiJeSpajanjeUspjesnoIzuzetak(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            IRecenzija r = new Recenzija();
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik3 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            List<Korisnik> listaKorisnika = new List<Korisnik>();
            listaKorisnika.Add(korisnik1);
            listaKorisnika.Add(korisnik2);
            listaKorisnika.Add(korisnik3);

            Chat noviChat = new GrupniChat(listaKorisnika);
            Komunikator k = new Komunikator();
            k.DaLiJeSpajanjeUspjesno(noviChat, r);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SpajanjeKorisnikaIzuzetakMoj()
        {
            Komunikator k = new Komunikator();
            k.SpajanjeKorisnika();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [DynamicData("Podaci")]
        public void RadSaKorisnikomIzuzetak1(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
        Komunikator k=new Komunikator();
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            k.RadSaKorisnikom(korisnik1,0);
            k.RadSaKorisnikom(korisnik2,0);

                }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RadSaKorisnikomIzuzetak2()
        {
            Komunikator k = new Komunikator();
            Korisnik korisnik1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Tuzla, 20, false);
            k.RadSaKorisnikom(korisnik1, 1);

        }

        [TestMethod]
        public void DodavanjeRazgovoraIzuzetak()
        {
            List<Korisnik> lista = new List<Korisnik>();
            Komunikator k = new Komunikator();
            var ex = Assert.ThrowsException<ArgumentException>(() => k.DodavanjeRazgovora(lista, true));
            Assert.AreEqual(ex.Message, ("Nemoguće dodati razgovor!"));
        }

        [TestMethod]
        public void GrupniChatNull()
        {
            List<Korisnik> lista = new List<Korisnik>();
            GrupniChat grupniChat = new GrupniChat(lista);
            Assert.IsNotNull(grupniChat.Korisnici);
        }


        [TestMethod]
        [DynamicData("Podaci")]
        public void DodavanjeRazgovoraIzuzetak2(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            List<Korisnik> lista = new List<Korisnik>();
            Komunikator k = new Komunikator();
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            lista.Add(korisnik1);
            var ex = Assert.ThrowsException<ArgumentException>(() => k.DodavanjeRazgovora(lista, true));
            Assert.AreEqual(ex.Message, ("Nemoguće dodati razgovor!"));
        }

        [TestMethod]
        [DynamicData("Podaci")]
        public void DodavanjeRazgovoraIzuzetak3(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            List<Korisnik> lista = new List<Korisnik>();
            Komunikator k = new Komunikator();
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik3 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            lista.Add(korisnik1);
            lista.Add(korisnik2);
            lista.Add(korisnik3);
            var ex = Assert.ThrowsException<ArgumentException>(() => k.DodavanjeRazgovora(lista, false));
            Assert.AreEqual(ex.Message, ("Nemoguće dodati razgovor!"));
        }

        [TestMethod]
        [DynamicData("Podaci")]
        public void DodavanjeRazgovora4(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik3= new Korisnik(name, pass, location, desiredLoc, age, divorced);
            List<Korisnik> lista = new List<Korisnik>();
            lista.Add(korisnik1);
            lista.Add(korisnik2);
            lista.Add(korisnik3);
            GrupniChat noviChat = new GrupniChat(lista);
            Komunikator k = new Komunikator();
            k.DodavanjeRazgovora(lista, true);
            Assert.ReferenceEquals(k.Razgovori, noviChat);
        }

        [TestMethod]
        [DynamicData("Podaci")]
        public void DodavanjeRazgovora5(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            List<Korisnik> lista = new List<Korisnik>();
            lista.Add(korisnik1);
            lista.Add(korisnik2);
            GrupniChat noviChat = new GrupniChat(lista);
            Komunikator k = new Komunikator();
            k.DodavanjeRazgovora(lista, true);
            Assert.IsFalse(Assert.ReferenceEquals(k.Razgovori[0], noviChat));
        }

        [TestMethod]
        [DynamicData("Podaci")]
        public void DodavanjeRazgovora6(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            List<Korisnik> lista = new List<Korisnik>();
            lista.Add(korisnik1);
            lista.Add(korisnik2);
            Chat chat1 = new Chat(korisnik1, korisnik2);
            Komunikator k = new Komunikator();
            k.DodavanjeRazgovora(lista, false);

            Assert.IsFalse(Assert.ReferenceEquals(k.Razgovori[0], chat1));
        }

        [TestMethod]
        [DynamicData("Podaci")]
        public void RadSaKorisnikom2(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Komunikator k = new Komunikator();
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            k.RadSaKorisnikom(korisnik1, 0);
            k.RadSaKorisnikom(korisnik2, 1);
            Assert.AreEqual(k.Korisnici.Count, 1);
        }

        [TestMethod]
        [DynamicData("Podaci")]
        public void RadSaKorisnikom3(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0) { 
            Komunikator k = new Komunikator();
            Korisnik korisnik1 = new Korisnik(name,pass,location,desiredLoc,age,divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            k.RadSaKorisnikom(korisnik1, 0);
            List<Korisnik> lista = new List<Korisnik>();
            lista.Add(korisnik1);
            lista.Add(korisnik2);
            k.DodavanjeRazgovora(lista, false);
            k.RadSaKorisnikom(korisnik2, 1);
            Assert.AreEqual(k.Razgovori.Count, 0);
        }
        [TestMethod]
        [DynamicData("Podaci")]
        public void PosaljiPorukuViseKorisnikaIzuzetak(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik sender = null;
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Korisnik korisnik3 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            List<Korisnik> lista = new List<Korisnik>();
            lista.Add(korisnik1);
            lista.Add(korisnik2);
            lista.Add(korisnik3);
            string sadrzaj = "sadrzaj";
            GrupniChat grupniChat = new GrupniChat(lista);
            var ex = Assert.ThrowsException<FormatException>(() => grupniChat.PosaljiPorukuViseKorisnika(sender, lista, sadrzaj));
            Assert.AreEqual(ex.Message, ("Neispravni parametri!"));
        }

        [TestMethod]
        [DynamicData("PodaciKorisnikValidni")]
        public void KorisnikPodaciValidni(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Assert.IsNotNull(korisnik1);
            if (korisnik1.Razvod == true)
                Assert.AreEqual(korisnik1.Razvod, true);
        }

        [TestMethod]
        [DynamicData("PodaciKorisnikValidni")]
        public void KorisnikPodaciValidni2(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced);
            Assert.IsNotNull(korisnik1.Password);
        }

        [TestMethod]
        [DynamicData("PodaciKorisnikValidni")]
        public void ChatPodaciValidni4(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced, minDesiredAge, maxDesiredAge);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced, minDesiredAge, maxDesiredAge);
            Chat noviChat = new Chat(korisnik1, korisnik2);
            Assert.IsNotNull(noviChat);

        }

        [TestMethod]
        [DynamicData("PodaciKorisnikValidni")]
        public void GrupniChat(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik(name, pass, location, desiredLoc, age, divorced, minDesiredAge, maxDesiredAge);
            Korisnik korisnik2 = new Korisnik(name, pass, location, desiredLoc, age, divorced, minDesiredAge, maxDesiredAge);
            List<Korisnik> lista = new List<Korisnik>();
            lista.Add(korisnik1);
            lista.Add(korisnik2);
            GrupniChat grupniChat = new GrupniChat(lista);
            Assert.AreEqual(lista, grupniChat.Korisnici);
            grupniChat.PocetakChata = DateTime.Today.AddDays(-1);
            Assert.AreEqual(grupniChat.PocetakChata, DateTime.Today.AddDays(-1));
            Assert.IsNotNull(grupniChat.Poruke);
            List<Korisnik> praznaLista = null;
            GrupniChat grupniChat1 = new GrupniChat(praznaLista);
            Assert.IsNotNull(grupniChat1.Korisnici);
        }
        /*komentar*/


        


        [TestMethod]
        [DynamicData("PodaciKorisnikNeValidni")]
        public void KorisnikPodaciNeValidni(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik();

            var ex = Assert.ThrowsException<FormatException>(() => korisnik1.Ime = name);
            StringAssert.Equals(ex.Message, ("Neispravno ime!"));

        }

        [TestMethod]
        [DynamicData("PodaciKorisnikNeValidni2")]
        public void KorisnikPodaciNeValidni2(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik();

            var ex = Assert.ThrowsException<FormatException>(() => korisnik1.Password = pass);
            StringAssert.Equals(ex.Message, ("Neispravan password!"));

        }

        [TestMethod]
        [DynamicData("PodaciKorisnikNeValidni3")]
        public void KorisnikPodaciNeValidni3(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik();

            var ex = Assert.ThrowsException<FormatException>(() => korisnik1.Godine = age);
            StringAssert.Equals(ex.Message, ("Neispravne godine!"));

        }

        [TestMethod]
        [DynamicData("PodaciKorisnikNeValidni4")]
        public void KorisnikPodaciNeValidni4(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik();

            var ex = Assert.ThrowsException<FormatException>(() => korisnik1.ZeljeniMaxGodina = maxDesiredAge);
            StringAssert.Equals(ex.Message, ("Neispravni željeni maksimum godina!"));

        }

        [TestMethod]
        [DynamicData("PodaciKorisnikNeValidni5")]
        public void KorisnikPodaciNeValidni5(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Korisnik korisnik1 = new Korisnik();

            var ex = Assert.ThrowsException<FormatException>(() => korisnik1.ZeljeniMinGodina = minDesiredAge);
            StringAssert.Equals(ex.Message, ("Neispravni željeni minimum godina!"));

        }

        [TestMethod]
        [DynamicData("ChatPodaciNeValidni")]
        public void ChatPodaciNeValidni1(DateTime vrijeme)
        {
            Chat noviChat = new Chat();
            var ex = Assert.ThrowsException<InvalidOperationException>(() => noviChat.PocetakChata = vrijeme);
            StringAssert.Equals(ex.Message, ("Datum početka ne može biti u budućnosti!"));

        }

        [TestMethod]
        [DynamicData("ChatPodaciNeValidni")]
        public void ChatPodaciNeValidni2(DateTime vrijeme)
        {
            Chat noviChat = new Chat();
            var ex = Assert.ThrowsException<InvalidOperationException>(() => noviChat.NajnovijaPoruka = vrijeme);
            StringAssert.Equals(ex.Message, ("Datum najnovije poruke ne može biti u budućnosti"));

        }

        [TestMethod]
        [DynamicData("ChatPodaciValidni")]
        public void ChatPodaciValidni3(DateTime vrijeme)
        {
            Chat noviChat = new Chat();
            Assert.IsTrue(noviChat.NajnovijaPoruka < DateTime.Now);

        }





        #endregion
    }
}
