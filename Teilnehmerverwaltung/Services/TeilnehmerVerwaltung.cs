using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teilnehmerverwaltung.Models;
using Teilnehmerverwaltung.Repositories;

namespace Teilnehmerverwaltung.Services;

public class TeilnehmerVerwaltung
{
    private ITeilnehmerRepository _repository;

    //private List<Teilnehmer> _teilnehmer = new();

    public TeilnehmerVerwaltung()
    {
        _repository = new InMemoryTeilnehmerRepository();
      //  _teilnehmer = _repository.AlleLaden();
    }
    public void Hinzufuegen(Teilnehmer t)
    {      
       
        if (!_repository.Speichern(t))      
             return;
       

    }

    public void Entfernen(Teilnehmer t)
    {
        if (_repository.Loeschen(t))
           return;
        
    }

    public IReadOnlyList<Teilnehmer> AlleTeilnehmer() => _repository.AlleLaden();
        
}
