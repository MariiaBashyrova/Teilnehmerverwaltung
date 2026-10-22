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

    public bool Loeschen(Teilnehmer t); 
}
