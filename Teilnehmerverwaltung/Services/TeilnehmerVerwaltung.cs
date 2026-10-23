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

    public void Aktualisieren(Teilnehmer t)
    {
        if (_repository.Aktualisieren(t))
            return;

    }
    public IReadOnlyList<Teilnehmer> AlleTeilnehmer() => _repository.AlleLaden();
    public IReadOnlyList<Umschulung> AlleUmschulungen() => _repository.AlleUmschulungen();
    public IReadOnlyList<Kostentraeger> AlleKostentraeger() => _repository.AlleKostentraeger();
    public bool Pruefen(string vorname, string nachname, string email, DateOnly? geburtsdatum,
        Umschulung? umschulung, Kostentraeger? kostentraeger, Teilnehmer? aktuellerTeilnehmer)
    {
        if (string.IsNullOrWhiteSpace(vorname) || string.IsNullOrWhiteSpace(nachname) ||
            string.IsNullOrWhiteSpace(email) || !email.Contains('@') || umschulung == null || kostentraeger == null)
            return false;

        return !_repository.AlleLaden().Any(t => t != aktuellerTeilnehmer &&
            t.Vorname.Equals(vorname.Trim(), StringComparison.OrdinalIgnoreCase) &&
            t.Nachname.Equals(nachname.Trim(), StringComparison.OrdinalIgnoreCase) &&
            t.Geburtsdatum == geburtsdatum);
    }

    public int GetTeilnehmerNummer()
    {
        return _repository.GetTeilnehmerNummer();
    }

}
