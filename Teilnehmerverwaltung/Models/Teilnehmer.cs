using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teilnehmerverwaltung.Models;

public class Teilnehmer
{
    
    public int TeilnehmerNr { get; init; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public DateOnly? Geburtsdatum { get; set; }
    public string EMail { get; set; }
    public TeilnehmerStatus? Status { get; set; }
    public string VollName { get; init; } 
    public Umschulung Umschulung { get; set; }
    public Kostentraeger Kostentraeger { get; set; }

    public Teilnehmer(int teilnehmerNr, string vorname, string nachname, DateOnly geburtsdatum, string eMail, 
        TeilnehmerStatus status, Umschulung umschulung, Kostentraeger kostentraeger)
    {
        TeilnehmerNr = teilnehmerNr;
        Vorname = vorname;
        Nachname = nachname;
        Geburtsdatum = geburtsdatum;
        EMail = eMail;
        Status = status;
        Umschulung = umschulung;
        Kostentraeger = kostentraeger;
        VollName = string.Concat(vorname, " ", nachname);
    }
}
