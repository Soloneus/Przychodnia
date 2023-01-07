using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Przychodnia
{
    public enum EnumPlec { K, M }
    [DataContract]
    public abstract class Osoba : IComparable<Osoba>
    {     
        string imie;
        string nazwisko;
        DateTime dataUrodzenia;
        string pesel;
        private EnumPlec plec;
        
        [DataMember]
        public string Imie { get => imie; set => imie = value; }
        [DataMember]
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        [DataMember]
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }
        [DataMember]
        public EnumPlec Plec { get => plec; set => plec = value; }
        [DataMember]
        public string Pesel
        {
            get => pesel;
            set
            {
                if (!WeryfikujPesel(value))
                {
                    throw new ArgumentException("Zły pesel.");
                }
                pesel = value;
            }
        }
        
        public Osoba()
        {
            Imie = string.Empty;
            Nazwisko = string.Empty;
            DataUrodzenia = DateTime.Now;
            Pesel = new string('0', 11);
        }
        public Osoba(string imie, string nazwisko, EnumPlec plec) : this()
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Plec = plec;
        }
        public Osoba(string imie, string nazwisko, string dataUrodzenia,
            string pesel, EnumPlec plec) : this(imie, nazwisko, plec)
        {
            if (DateTime.TryParseExact(dataUrodzenia,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            { 
                DataUrodzenia = res; 
            }
            Pesel = pesel;
        }
        
        public int CompareTo(Osoba? other)
        {
            if (other is null) { return 1; }
            int cmpnazw = Nazwisko.CompareTo(other.Nazwisko);
            if (cmpnazw != 0) { return cmpnazw; }
            return Imie.CompareTo(other.Imie);
        }
        bool WeryfikujPesel(string pesel)
        {
            return Regex.IsMatch(pesel, @"\d{11}");
        }
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ({Plec}), {DataUrodzenia:dd-MM-yyyy} ({Pesel})";
        }
    }
}
