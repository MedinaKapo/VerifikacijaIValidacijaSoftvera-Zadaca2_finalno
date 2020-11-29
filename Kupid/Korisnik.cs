using System;
using System.Collections.Generic;
using System.Text;

namespace Kupid
{
    public enum Lokacija
    {
        Sarajevo,
        Zenica,
        Mostar,
        Tuzla,
        Bihać,
        Trebinje,
        Banja_Luka
    }

    public class Korisnik
    {
        #region Atributi

        string ime, password;
        Lokacija lokacija, zeljenaLokacija;
        int godine, zeljeniMinGodina, zeljeniMaxGodina;
        bool razvod;

        #endregion

        #region Properties

        public string Ime
        {
            get => ime;
            set
            {
                if (String.IsNullOrEmpty(value) || value.Length > 20)
                    throw new FormatException("Neispravno ime!");

                ime = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (String.IsNullOrEmpty(value) || value.Length < 5
                    || !value.Contains("*") || !value.Contains("+") || value.Contains("etf"))
                    throw new FormatException("Neispravan password!");

                password = value;
            }
        }

        public Lokacija Lokacija
        {
            get => lokacija;
            set => lokacija = value;
        }

        public Lokacija ZeljenaLokacija
        {
            get => zeljenaLokacija;
            set => zeljenaLokacija = value;
        }

        public int Godine
        {
            get => godine;
            set
            {
                if (value < 18)
                    throw new FormatException("Neispravne godine!");

                godine = value;
            }
        }

        public int ZeljeniMinGodina
        {
            get => zeljeniMinGodina;
            set
            {
                if (value < godine - 10 || value > godine + 5)
                    throw new FormatException("Neispravni željeni minimum godina!");

                zeljeniMinGodina = value;
            }
        }

        public int ZeljeniMaxGodina
        {
            get => zeljeniMaxGodina;
            set
            {
                if (value < godine - 5 || value > godine + 10)
                    throw new FormatException("Neispravni željeni maksimum godina!");

                zeljeniMaxGodina = value;
            }
        }

        public bool Razvod
        {
            get => razvod;
            set => razvod = value;
        }

        #endregion

        #region Konstruktor

        public Korisnik(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Ime = name;
            Password = pass;
            Lokacija = location;
            ZeljenaLokacija = desiredLoc;
            Godine = age;

            if (minDesiredAge < 18)
                ZeljeniMinGodina = age;
            else
                ZeljeniMinGodina = minDesiredAge;

            if (maxDesiredAge < 18)
                ZeljeniMaxGodina = age;
            else
                ZeljeniMaxGodina = maxDesiredAge;

            Razvod = divorced;
        }

        public Korisnik()
        {

        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u kojoj se željene vrijednosti postavljaju prema želji korisnika.
        /// Ukoliko korisnik želi slične vrijednosti, željena lokacija će se postaviti na njegovu lokaciju,
        /// minimalne željene godine na njegove godine - 2, a maksimalne željene godine na njegove godine + 2.
        /// U suprotnom, željena lokacija će se postaviti na bilo koju lokaciju koja je različita od njegove,
        /// minimalne željene godine na godine - 10, a maksimalne na godine + 10.
        /// </summary>
        /// <param name="slično"></param>
        public void PromjenaParametara(bool slično)
        {
            if (slično)
            {
                zeljenaLokacija = lokacija;
                zeljeniMinGodina = godine - 2;
                ZeljeniMaxGodina = godine + 2;
            }
            else
            {
                while (true)
                {

                    Random random = new Random();
                    Type type = typeof(Lokacija);

                    Array values = type.GetEnumValues();
                    //Array values = Enum.GetValues(type);

                    int index = random.Next(values.Length);
                    Lokacija value = (Lokacija)values.GetValue(index);

                    if (value != lokacija)
                    {
                        zeljenaLokacija = value;
                        break;
                    }
                }
                zeljeniMinGodina = godine - 10;
                ZeljeniMaxGodina = godine + 10;
            }
        }

        #endregion
    }
}
