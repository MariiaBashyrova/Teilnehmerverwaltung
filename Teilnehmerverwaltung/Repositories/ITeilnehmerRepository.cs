using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teilnehmerverwaltung.Models;

namespace Teilnehmerverwaltung.Repositories;

internal interface ITeilnehmerRepository
{
    public bool Speichern(Teilnehmer t);
    public List<Teilnehmer> AlleLaden();
    List<Umschulung> AlleUmschulungen();
    List<Kostentraeger> AlleKostentraeger();
    public bool Loeschen(Teilnehmer t);
    public bool Aktualisieren(Teilnehmer t);
    public int GetTeilnehmerNummer();
}
