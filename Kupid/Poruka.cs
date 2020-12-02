using System;

namespace Kupid
{
    public class Poruka
    {
        #region Atributi

        Korisnik posiljalac, primalac;
        string sadrzaj;

        #endregion

        #region Properties

        public Korisnik Posiljalac
        {
            get => posiljalac;
        }
        public Korisnik Primalac
        {
            get => primalac;
        }
        public string Sadrzaj
        {
            get => sadrzaj;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Contains("pogrdna riječ"))
                    throw new InvalidOperationException("Neispravan sadržaj poruke!");

                sadrzaj = value;
            }
        }

        #endregion

        #region Konstruktor

        public Poruka(Korisnik sender, Korisnik receiver, string content)
        {
            if (sender == null || receiver == null)
                throw new ArgumentNullException("Nedefinisan pošiljalac/primalac!");

            posiljalac = sender;
            primalac = receiver;
            Sadrzaj = content;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u kojoj se izračunava kompatibilnost korisnika.
        /// Ako su lokacije, broj godina, minimalni i maksimalni željeni broj godina korisnika isti, kompatibilnost je 100%.
        /// Kompatibilnost je 100% bez obzira na parametre ukoliko se u sadržaju poruke nalazi string "volim te".
        /// Ako se tri parametra podudaraju, kompatibilnost je 75%, ako se podudaraju dva onda je 50%,
        /// ako se podudara jedan, onda je 25% a u suprotnom je 0%.
        /// </summary>
        /// <returns></returns>
        /// Medina Kapo
        public double IzračunajKompatibilnostKorisnika()
        {
            double kompatiblinost = 0;
            int brojKompatibilnosti = 0;
            if (posiljalac.ZeljeniMaxGodina == primalac.ZeljeniMaxGodina) brojKompatibilnosti++;
            if (posiljalac.Lokacija == primalac.Lokacija) brojKompatibilnosti++;
            if (posiljalac.ZeljeniMinGodina == primalac.ZeljeniMinGodina) brojKompatibilnosti++;
            if (posiljalac.Godine == primalac.Godine) brojKompatibilnosti++;
            else if (sadrzaj.Contains("volim te")) { kompatiblinost = 100; brojKompatibilnosti = 4; }

            if (brojKompatibilnosti == 4) kompatiblinost = 100;
            else if (brojKompatibilnosti == 3) kompatiblinost = 75;
            else if (brojKompatibilnosti == 2) kompatiblinost = 50;
            else if (brojKompatibilnosti == 1) kompatiblinost = 25;
            else kompatiblinost = 0;

            return kompatiblinost;
        }

        #endregion
    }
}
