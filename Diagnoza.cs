using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia
{
    public class Diagnoza
    {
        Wizyta wizyta;
        string choroba;

        public Wizyta Wizyta { get => wizyta; set => wizyta = value; }
        public string Choroba { get => choroba; set => choroba = value; }
        public Diagnoza(Wizyta wizyta, string choroba)
        {
            Wizyta = wizyta;
            Choroba = choroba;
        }

        public override string ToString()
        {
            return $"{Wizyta}\nDiagnoza: {Choroba}";
        }
    }
}
