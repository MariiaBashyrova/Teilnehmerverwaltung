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
    public string Pruefen(
    string vorname,
    string nachname,
    string email,
    Umschulung? umschulung,
    Kostentraeger? kostentraeger)
    {
        StringBuilder fehler = new();

        if (string.IsNullOrWhiteSpace(vorname))
            fehler.AppendLine("Vorname darf nicht leer sein.");

        if (string.IsNullOrWhiteSpace(nachname))
            fehler.AppendLine("Nachname darf nicht leer sein.");

        if (!string.IsNullOrWhiteSpace(email) && !email.Contains('@'))
            fehler.AppendLine("Die E-Mail-Adresse ist ungültig.");

        if (umschulung == null)
            fehler.AppendLine("Bitte eine Umschulung auswählen.");

        if (kostentraeger == null)
            fehler.AppendLine("Bitte einen Kostenträger auswählen.");

        return fehler.ToString();
    }

    public int GetTeilnehmerNummer()
    {
        return _repository.GetTeilnehmerNummer();
    }

    public Teilnehmer? DoppeltenTeilnehmerFinden(
    string vorname,
    string nachname,
    DateOnly? geburtsdatum,
    Teilnehmer? aktuellerTeilnehmer = null)
    {
        return _repository.AlleLaden().FirstOrDefault(t =>
            t != aktuellerTeilnehmer &&
            t.Vorname.Equals(vorname.Trim(), StringComparison.OrdinalIgnoreCase) &&
            t.Nachname.Equals(nachname.Trim(), StringComparison.OrdinalIgnoreCase) &&
            t.Geburtsdatum == geburtsdatum);
    }
}
