using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teilnehmerverwaltung.Models;

public class Kostentraeger
{
    public int KostentraegerNr { get; set; }
    public string Bezeichnung { get; set; }
    public string TelefonNummer { get; set; }
    public string Ansprechpartner { get; set; }

}
