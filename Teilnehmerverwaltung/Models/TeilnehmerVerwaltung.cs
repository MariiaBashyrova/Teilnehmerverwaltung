using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teilnehmerverwaltung.Models;

public class TeilnehmerVerwaltung
{
    private List<Teilnehmer> _teilnehmer = new();

    public void Hinzufuegen(Teilnehmer t)
    {
        _teilnehmer.Add(t);
    }

    public void Entfernen(Teilnehmer t)
    {
        _teilnehmer.Remove(t);
    }

    public IReadOnlyList<Teilnehmer> AlleTeilnehmer() => _teilnehmer;
        
}
