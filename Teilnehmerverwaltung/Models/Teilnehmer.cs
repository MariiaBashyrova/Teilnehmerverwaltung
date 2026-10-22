using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teilnehmerverwaltung.Models;

public class Teilnehmer
{
    public string TeilnehmerNr { get; set; }

    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public DateOnly Geburtsdatum { get; set; }
    public string EMail { get; set; }
    public TeilnehmerStatus Status { get; set; }

    protected string VollName { get; set; } 

    public Umschulung Umschulung { get; set; }
    public Kostentraeger Kostentraeger { get; set; }
}
