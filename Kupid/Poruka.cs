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
        public double IzračunajKompatibilnostKorisnika()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
