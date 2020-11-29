using System;
using System.Collections.Generic;
using System.Text;

namespace Kupid
{
    public class Chat
    {
        #region Atributi

        protected List<Korisnik> korisnici;
        protected List<Poruka> poruke;
        DateTime pocetakChata, najnovijaPoruka;

        #endregion

        #region Properties

        public List<Korisnik> Korisnici
        {
            get => korisnici;
        }

        public List<Poruka> Poruke
        {
            get => poruke;
        }

        public DateTime PocetakChata
        {
            get => pocetakChata;
            set
            {
                if (value > DateTime.Now)
                    throw new InvalidOperationException("Datum početka ne može biti u budućnosti!");

                pocetakChata = value;
            }
        }

        public DateTime NajnovijaPoruka
        {
            get => najnovijaPoruka;
            set
            {
                if (value > DateTime.Now)
                    throw new InvalidOperationException("Datum najnovije poruke ne može biti u budućnosti!");

                najnovijaPoruka = value;
            }
        }

        #endregion

        #region Konstruktor

        public Chat(Korisnik k1, Korisnik k2)
        {
            korisnici = new List<Korisnik>() { k1, k2 };
            poruke = new List<Poruka>();
            PocetakChata = DateTime.Now;
        }

        public Chat()
        {
            korisnici = new List<Korisnik>() { };
            poruke = new List<Poruka>();
            PocetakChata = DateTime.Now;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u kojoj se registruje nova poruka.
        /// Ukoliko je neki od parametara pogrešan, dolazi do pojave izuzetka prema postojećoj programskoj logici.
        /// Potrebno je dodati poruku u listu poruka i registrovati trenutak dolaska najnovije poruke.
        /// </summary>
        /// <param name="primalac"></param>
        /// <param name="posiljalac"></param>
        /// <param name="sadrzaj"></param>
        public void DodajNovuPoruku(Korisnik primalac, Korisnik posiljalac, string sadrzaj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
