using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teilnehmerverwaltung.Models;

namespace Teilnehmerverwaltung.Repositories;

public class InMemoryTeilnehmerRepository : ITeilnehmerRepository
{
    private List<Teilnehmer> _teilnehmer = new();
    private List<Umschulung> _umschulungen = new();
    private List<Kostentraeger> _kostentraeger = new();
    public InMemoryTeilnehmerRepository()
    {
        TestdatenAnlegen();
    }
    public bool Speichern(Teilnehmer t)
    {
        _teilnehmer.Add(t); 
        return true;
       
    }
    public bool Aktualisieren(Teilnehmer t)
    {
        int index = _teilnehmer.FindIndex(x => x.TeilnehmerNr == t.TeilnehmerNr);
        if (index == -1) return false;

        _teilnehmer[index] = t;
        return true;
    }
    public bool Loeschen(Teilnehmer t)
    {
        _teilnehmer.Remove(t);
        return true;
    }
    public int GetTeilnehmerNummer() =>
    _teilnehmer.Count > 0 ? _teilnehmer.Max(t => t.TeilnehmerNr) + 1 : 1001;
    public List<Teilnehmer> AlleLaden() => _teilnehmer;
    public List<Umschulung> AlleUmschulungen() => _umschulungen;
    public List<Kostentraeger> AlleKostentraeger() => _kostentraeger;

    public void TestdatenAnlegen()
    {
        _teilnehmer.Clear();
        _umschulungen.Clear();
        _kostentraeger.Clear();
        var fiae25a = new Umschulung(
    "FIAE-25A",
    "Fachinformatiker/in Anwendungsentwicklung",
    new DateOnly(2025, 9, 1),
    new DateOnly(2027, 8, 31),
    "2.14");

        var fisi25a = new Umschulung(
            "FISI-25A",
            "Fachinformatiker/in Systemintegration",
            new DateOnly(2025, 9, 1),
            new DateOnly(2027, 8, 31),
            "1.08");

        var fiae26b = new Umschulung(
            "FIAE-26B",
            "Fachinformatiker/in Anwendungsentwicklung",
            new DateOnly(2026, 3, 2),
            new DateOnly(2028, 3, 1),
            "2.15");

        _umschulungen.AddRange(new[] { fiae25a, fisi25a, fiae26b });

        var afaStuttgart = new Kostentraeger(
            1,
            "Agentur für Arbeit Stuttgart",
            "0711 555-1200",
            "Frau Yilmaz");

        var jobcenterStuttgart = new Kostentraeger(
            2,
            "Jobcenter Stuttgart",
            "0711 555-4000",
            "Herr Brandt");

        var drvBw = new Kostentraeger(
            3,
            "Deutsche Rentenversicherung BW",
            "0711 555-8480",
            "Frau Kessler");

        var bfdBundeswehr = new Kostentraeger(
            4,
            "Berufsförderungsdienst der Bundeswehr",
            "0711 555-3300",
            "Herr Albrecht");
        _kostentraeger.AddRange(new[] { afaStuttgart, jobcenterStuttgart, drvBw, bfdBundeswehr });
        var teilnehmer = new List<Teilnehmer>
{
    new Teilnehmer(
        1001, "Ada", "Lovelace",
        new DateOnly(1991, 3, 14),
        "ada.lovelace@example.de",
        TeilnehmerStatus.Aktiv, fiae25a, afaStuttgart),

    new Teilnehmer(
        1002, "Robert C.", "Martin",
        new DateOnly(1987, 11, 2),
        "robert.martin@example.de",
        TeilnehmerStatus.Aktiv, fiae25a, jobcenterStuttgart),

    new Teilnehmer(
        1003, "Linus", "Torvalds",
        new DateOnly(1995, 6, 27),
        "l.torvalds@example.de",
        TeilnehmerStatus.Praktikum, fisi25a, afaStuttgart),

    new Teilnehmer(
        1004, "Grace", "Hopper",
        new DateOnly(1984, 1, 9),
        "g.hopper@example.de",
        TeilnehmerStatus.Aktiv, fisi25a, drvBw),

    new Teilnehmer(
        1005, "Franz", "Beckenbauer",
        new DateOnly(1990, 8, 21),
        "franz.beckenbauer@example.de",
        TeilnehmerStatus.Abgebrochen, fiae25a, afaStuttgart),

    new Teilnehmer(
        1006, "Margaret", "Hamilton",
        new DateOnly(1998, 4, 30),
        "margaret.hamilton@example.de",
        TeilnehmerStatus.Aktiv, fiae26b, jobcenterStuttgart),

    new Teilnehmer(
        1007, "Tim", "Berners-Lee",
        new DateOnly(1993, 12, 12),
        "tim.berners-lee@example.de",
        TeilnehmerStatus.Aktiv, fisi25a, jobcenterStuttgart),

    new Teilnehmer(
        1008, "Anders", "Hejlsberg",
        new DateOnly(1986, 5, 5),
        "a.hejlsberg@example.de",
        TeilnehmerStatus.Aktiv, fiae26b, bfdBundeswehr),

    new Teilnehmer(
        1009, "Radia", "Perlman",
        new DateOnly(1996, 7, 18),
        "radia.perlman@example.de",
        TeilnehmerStatus.Praktikum, fisi25a, afaStuttgart),

    new Teilnehmer(
        1010, "Barbara", "Liskov",
        new DateOnly(1989, 2, 23),
        "barbara.liskov@example.de",
        TeilnehmerStatus.Aktiv, fiae26b, drvBw)
};
        _teilnehmer = teilnehmer;
    }
}
