using System;
using System.Collections.Generic;
using System.Text;

namespace Kupid
{
    public class Komunikator
    {
        #region Atributi

        List<Korisnik> korisnici;
        List<Chat> razgovori;

        #endregion

        #region Properties

        public List<Korisnik> Korisnici
        {
            get => korisnici;
        }

        public List<Chat> Razgovori
        {
            get => razgovori;
        }

        #endregion

        #region Konstruktor

        public Komunikator()
        {
            korisnici = new List<Korisnik>();
            razgovori = new List<Chat>();
        }

        #endregion

        #region Metode

        public void RadSaKorisnikom(Korisnik k, int opcija)
        {
            if (opcija == 0)
            {
                Korisnik postojeci = korisnici.Find(korisnik => korisnik.Ime == k.Ime);
                if (postojeci != null)
                    throw new InvalidOperationException("Korisnik već postoji!");

                korisnici.Add(k);
            }
            else if (opcija == 1)
            {
                Korisnik postojeci = korisnici.Find(korisnik => korisnik.Ime == k.Ime);
                if (postojeci == null)
                    throw new InvalidOperationException("Korisnik ne postoji!");

                korisnici.Remove(k);

                List<Chat> razgovoriZaBrisanje = new List<Chat>();
                foreach (Chat c in razgovori)
                {
                    if (c.Korisnici.Find(korisnik => korisnik.Ime == k.Ime) != null)
                        razgovoriZaBrisanje.Add(c);
                }

                foreach (Chat brisanje in razgovoriZaBrisanje)
                    razgovori.Remove(brisanje);
            }
        }

        public void DodavanjeRazgovora(List<Korisnik> korisnici, bool grupniChat)
        {
            if (korisnici == null || korisnici.Count < 2 || (!grupniChat && korisnici.Count > 2))
                throw new ArgumentException("Nemoguće dodati razgovor!");

            if (grupniChat)
                razgovori.Add(new GrupniChat(korisnici));

            else
                razgovori.Add(new Chat(korisnici[0], korisnici[1]));
        }

        /// <summary>
        /// Metoda u kojoj se vrši pronalazak svih poruka koje u sebi sadrže traženi sadržaj.
        /// Ukoliko je sadržaj prazan ili ne postoji nijedan chat, baca se izuzetak.
        /// U razmatranje se uzimaju i grupni, i individualni chatovi, a ne smije se uzeti u razmatranje
        /// nijedan chat u kojem se nalazi korisnik sa imenom "admin".
        /// </summary>
        public List<Poruka> IzlistavanjeSvihPorukaSaSadržajem(string sadržaj)
        {
            if (razgovori.Count == 0) throw new ArgumentNullException("greska");
            List<Poruka> vracam = new List<Poruka>();
            for (int i = 0; i < razgovori.Count; i++)
            {
                for (int t = 0; t < razgovori[i].Korisnici.Count; t++)
                {
                    if (!razgovori[i].Korisnici[t].Equals("admin"))
                    {
                        List<Poruka> lokalne = razgovori[i].Poruke;
                        for (int j = 0; j < lokalne.Count; j++)
                        {
                            if (lokalne[j].Sadrzaj.Contains(sadržaj) && !lokalne[j].Posiljalac.Ime.Equals("admin") &&
                                !lokalne[j].Primalac.Ime.Equals("admin") && !vracam.Contains(lokalne[j]))
                            {
                                vracam.Add(lokalne[j]);
                            }
                        }

                    }
                }
            }
            if (vracam.Count == 0) throw new ArgumentNullException("greska");
            return vracam;
        }

        public bool DaLiJeSpajanjeUspjesno(Chat c, IRecenzija r)
        {
            if (c is GrupniChat)
                throw new InvalidOperationException("Grupni chatovi nisu podržani!");

            if (c.Poruke.Find(poruka => poruka.IzračunajKompatibilnostKorisnika() == 100) != null
                && r.DajUtisak() == "Pozitivan")
                return true;

            else
                return false;
        }

        public void SpajanjeKorisnika()
        {
            Korisnik k1 = new Korisnik();
            if (korisnici.Count != 0)
                k1 = korisnici[0];
            else throw new ArgumentException("greska");
            Korisnik k2 = new Korisnik();
            for (int i = 1; i < korisnici.Count; i++)
            {
                k2 = korisnici[i];
                if ((k1.Lokacija == k2.Lokacija && k1.ZeljenaLokacija == k2.ZeljenaLokacija && k1.Godine == k2.Godine) ||
                    (k1.Lokacija == k2.Lokacija && k1.ZeljenaLokacija == k2.ZeljenaLokacija) || (k1.Lokacija == k2.Lokacija &&
                    k1.Godine == k2.Godine))
                {
                    Chat novi = new Chat(k1, k2);
                    razgovori.Add(novi);
                }
                else
                {
                    throw new ArgumentException("greska");
                }

            }
        }

        #endregion
    }
}
