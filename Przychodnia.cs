using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia
{
    public class Placówka
    {
        List<Lekarz> lekarze;
        List<Pacjent> pacjenci;
        Queue<Wizyta> wizyty;
        TimeSpan godzinaOtwarcia;
        TimeSpan godzinaZamkniecia;

        public Placówka() 
        {
            Lekarze = new();
            Pacjenci = new();
            Wizyty = new();
            godzinaOtwarcia= new();
            godzinaZamkniecia = new();
        }

        public Placówka(TimeSpan godzinaOtwarcia, TimeSpan godzinaZamkniecia) : this()
        {
            GodzinaOtwarcia = godzinaOtwarcia;
            GodzinaZamkniecia = godzinaZamkniecia;
        }
        public Placówka(List<Lekarz> lekarze, List<Pacjent> pacjenci, Queue<Wizyta> wizyty, TimeSpan godzinaOtwarcia, TimeSpan godzinaZamkniecia) : this(godzinaOtwarcia, godzinaZamkniecia)
        {
            Lekarze = lekarze;
            Pacjenci = pacjenci;
            Wizyty = wizyty;
        }

        public TimeSpan GodzinaOtwarcia { get => godzinaOtwarcia; set => godzinaOtwarcia = value; }
        public TimeSpan GodzinaZamkniecia { get => godzinaZamkniecia; set => godzinaZamkniecia = value; }
        public List<Lekarz> Lekarze { get => lekarze; set => lekarze = value; }
        public Queue<Wizyta> Wizyty { get => wizyty; set => wizyty = value; }
        public List<Pacjent> Pacjenci { get => pacjenci; set => pacjenci = value; }

        public void DodajWizyte(Wizyta wizyta)
        {
            if(wizyta == null) { return; }
            wizyty.Enqueue(wizyta);
            wizyty.ToList().Sort();
        }
        public void ZakonczWizyte(Diagnoza diagnoza)
        {
            Wizyta w1 = wizyty.Peek();
            w1.Pacjent.DodajDiagnoze(diagnoza);
            wizyty.Dequeue();
        }

        public void DodajPacjenta(Pacjent p1)
        {
            Pacjenci.Add(p1);
        }

        public void UsuńPacjenta(string pesel)
        {
            Pacjent p1 = Pacjenci.Find(p => p.Pesel == pesel);
            if (Pacjenci.Find(p => p.Pesel == pesel) == null) { return; }
            Pacjenci.Remove(p1);
        }
        public string HistoriaPacjenta(string pesel)
        {
            StringBuilder sb = new();
            Pacjent pacjent = Pacjenci.Find(p => p.Pesel == pesel);
            if(pacjent == null) { return "Brak pacjenta w bazie danych."; }
            pacjent.HistoriaWizyt.ForEach(w => sb.AppendLine(w.ToString()));
            if(sb.ToString() == null) { return "Brak historii"; }
            return sb.ToString();
        }
        public string LekarzWDanymDniu(string pesel, DateTime data)
        {
            StringBuilder sb = new StringBuilder();
            List<Wizyta>wizytyulekarza = wizyty.ToList().FindAll(w => w.Lekarz.Pesel == pesel && w.DataOd.Date == data);
            wizytyulekarza.ForEach(w => sb.AppendLine(w.ToString()));
            return sb.ToString();
        }
        public string WszystkieWizyty()
        {
            StringBuilder sb = new StringBuilder();
            wizyty.ToList().ForEach(w => sb.AppendLine(w.ToString()));
            return sb.ToString();
        }
    }
}
