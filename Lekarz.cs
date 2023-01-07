using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia
{
    public enum EnumSpecjalizacja { Dzieciecy, Kardiolog, Okulista, Pedolog, Onkolog }
    public class Lekarz : Osoba
    {
        bool zajety;
        EnumSpecjalizacja specjalizacja;
        Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> godzinyPracy;

        public Lekarz() 
        {
            Zajety = false;
            Specjalizacja= new EnumSpecjalizacja();
            GodzinyPracy = new();
        }

        public Lekarz(string imie, string nazwisko, string dataUrodzenia, string pesel, EnumPlec plec,
            EnumSpecjalizacja specjalizacja, Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> godzinyPracy) : base(imie, nazwisko, dataUrodzenia, pesel, plec)
        {
            Zajety = false;
            this.Specjalizacja = specjalizacja;
            this.GodzinyPracy = godzinyPracy;
        }

        public bool Zajety { get => zajety; set => zajety = value; }
        public EnumSpecjalizacja Specjalizacja { get => specjalizacja; set => specjalizacja = value; }
        public Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> GodzinyPracy { get => godzinyPracy; set => godzinyPracy = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<DayOfWeek, Tuple<TimeSpan, TimeSpan>> dzien in GodzinyPracy)
            {
                sb.AppendLine($"{dzien.Key.ToString()}: {dzien.Value.Item1.ToString()[0..5]}-{dzien.Value.Item2.ToString()[0..5]}");
            }
            return $"{Imie} {Nazwisko}, Specjalizacja: {Specjalizacja}\n{sb}";
        }
    }
}
