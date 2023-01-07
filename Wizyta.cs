using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//sprawdzic czy lekarz moze przyjac ludzi - lambda w diagnoza
//interfejsy
namespace Przychodnia
{
    public class Wizyta
    {
        DateTime dataDo;
        DateTime dataOd;
        Lekarz lekarz;
        Pacjent pacjent;
        string pokoj;

        public DateTime DataDo { get => dataDo; set => dataDo = value; }
        public DateTime DataOd { get => dataOd; set => dataOd = value; }
        public Lekarz Lekarz { get => lekarz; set => lekarz = value; }
        public Pacjent Pacjent { get => pacjent; set => pacjent = value; }
        public string Pokoj { get => pokoj; set => pokoj = value; }

        public Wizyta()
        {
            Lekarz = new Lekarz();
            Pacjent = new Pacjent();
            DataOd = new DateTime();
            DataDo = new DateTime();
            Pokoj = string.Empty;
        }
        public Wizyta(string dataod, Lekarz lekarz, Pacjent pacjent, string pokoj) : this()
        {
            if (DateTime.TryParseExact(dataod,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                DataOd = res;
            }
            Lekarz = lekarz;
            Pacjent = pacjent;
            DataDo = new DateTime(dataOd.Year, dataOd.Month, dataOd.Day, dataOd.Hour, dataOd.Minute + 45, 0);
            Pokoj = pokoj;
        }   

        public Wizyta(string datado, string dataod, Lekarz lekarz, Pacjent pacjent, string pokoj) : this(dataod, lekarz, pacjent, pokoj)
        {
            if (DateTime.TryParseExact(datado,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                DataDo = res;
            }
        }

        public bool SprawdzCzyWolny(string t, TimeSpan t1, TimeSpan t2)
        {
            DateTime dzien = new DateTime();
            if (DateTime.TryParseExact(t,
                new string[] { "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy" ,"yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd"
                }, null, System.Globalization.DateTimeStyles.None,
                out DateTime res))
            {
                dzien = res;
            }

            DayOfWeek dzienTygodnia = dzien.DayOfWeek;

            if (Lekarz.GodzinyPracy.ContainsKey(dzien.DayOfWeek))
            {
                TimeSpan godzinaRozpoczecia = Lekarz.GodzinyPracy[dzienTygodnia].Item1;
                TimeSpan godzinaZakonczenia = Lekarz.GodzinyPracy[dzienTygodnia].Item2;
                if (t1 >= godzinaRozpoczecia && t1 < godzinaZakonczenia && t2 > t1 && t2 <= godzinaZakonczenia)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"Pacjent: {pacjent.Imie} {pacjent.Nazwisko} ({pacjent.Pesel})\nLekarz: {lekarz.Imie} {lekarz.Nazwisko} ({lekarz.Pesel})" +
                $"\nData: {DataOd:dd-MM-yyyy HH:mm}-{DataDo:HH:mm} Pokój: {Pokoj} ";
        }
    }
}
