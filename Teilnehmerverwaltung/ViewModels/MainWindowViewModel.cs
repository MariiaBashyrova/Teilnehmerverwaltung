using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Teilnehmerverwaltung.Helpers;
using Teilnehmerverwaltung.Models;
using Teilnehmerverwaltung.Services;


namespace Teilnehmerverwaltung.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Teilnehmer> Teilnehmers { get; set; }

    private Teilnehmer _selectedTeilnehmer;

    private TeilnehmerVerwaltung TeilnehmerVerwaltung;
    public Teilnehmer SelectedTeilnehmer { get =>_selectedTeilnehmer; 
        set
        {
            if (SetProperty(ref _selectedTeilnehmer, value))
            {
                FormularFuellen();
                LoeschenCommand?.AusfuehrbarkeitAktualisieren();
                //SpeichernCommand?.AusfuehrbarkeitAktualisieren();
                OnPropertyChanged(nameof(FormularAktiv));


            }
            ;
        }
    }
    private bool _hatAenderungen;
    public bool HatAenderungen
    {
        get => _hatAenderungen;
        private set { if (SetProperty(ref _hatAenderungen, value)) SpeichernCommand?.AusfuehrbarkeitAktualisieren() ; }
    }

    private bool _formularWirdGeladen;
    public string Suche {  get; set; }

    private bool _istNeuerTeilnehmer;

    public bool IstNeuerTeilnehmer
    {
        get => _istNeuerTeilnehmer;
        set
        {
            if (SetProperty(ref _istNeuerTeilnehmer, value))
            {
                OnPropertyChanged(nameof(FormularAktiv));
            }
        }
    }

    public bool FormularAktiv => SelectedTeilnehmer != null || IstNeuerTeilnehmer;

    private string _vorname = string.Empty;
    public string Vorname
    {
        get => _vorname;
        set
        {
            if (SetProperty(ref _vorname, value) && !_formularWirdGeladen) HatAenderungen = true;
      
        }
    }

    private string _email = string.Empty;
    public string EMail
    {
        get => _email;
        set
        {
            if (SetProperty(ref _email, value) && !_formularWirdGeladen) HatAenderungen = true;

        }
    }

    private string _nachname = string.Empty;
    public string Nachname
    {
        get => _nachname;
        set
        {
            if (SetProperty(ref _nachname, value) && !_formularWirdGeladen) HatAenderungen = true;

        }
    }
    private DateTime? _geburtsdatum;
    public DateTime? Geburtsdatum
    {
        get => _geburtsdatum;
        set
        {
            if (SetProperty(ref _geburtsdatum, value) && !_formularWirdGeladen) HatAenderungen = true;

        }
    }

    public ObservableCollection<Umschulung> Umschulungen { get; }
    public ObservableCollection<Kostentraeger> Kostentraegers { get; }

    private Umschulung? _umschulung;
    public Umschulung? Umschulung
    {
        get => _umschulung;
        set { if (SetProperty(ref _umschulung, value) && !_formularWirdGeladen) HatAenderungen = true; }
    }

    private Kostentraeger? _kostentraeger;
    public Kostentraeger? Kostentraeger
    {
        get => _kostentraeger;
        set { if (SetProperty(ref _kostentraeger, value) && !_formularWirdGeladen) HatAenderungen = true; }
    }
    public Array TeilnehmerStatusWerte => Enum.GetValues(typeof(TeilnehmerStatus));
    private TeilnehmerStatus? _status;
    public TeilnehmerStatus? Status 
    {
        get => _status;
        set { if (SetProperty(ref _status, value) && !_formularWirdGeladen) HatAenderungen = true; }

    }
    public MainWindowViewModel()
    {
        TeilnehmerVerwaltung = new TeilnehmerVerwaltung();
        Teilnehmers = new ObservableCollection<Teilnehmer>(TeilnehmerVerwaltung.AlleTeilnehmer());
        Umschulungen = new ObservableCollection<Umschulung>(TeilnehmerVerwaltung.AlleUmschulungen());
        Kostentraegers = new ObservableCollection<Kostentraeger>(TeilnehmerVerwaltung.AlleKostentraeger());
        //'SelectedBuch = Buecher[1];


        NeuCommand = new RelayCommand(FuegeHinzu);
        SpeichernCommand = new RelayCommand(Speichern, KannSpeichern);
        LoeschenCommand = new RelayCommand(Loeschen, KannLoeschen); 
    }

    private void FormularFuellen()
    {
        _formularWirdGeladen = true;
        HatAenderungen = false;
        if (SelectedTeilnehmer == null)
        {
            Vorname = string.Empty;
            Nachname = string.Empty;
            EMail = string.Empty;
            Geburtsdatum = null;
            Umschulung = null;
            Kostentraeger = null;
            Status = null;
            _formularWirdGeladen = false;
            return;
        }

        Vorname = SelectedTeilnehmer.Vorname;
        Nachname = SelectedTeilnehmer.Nachname;
        EMail = SelectedTeilnehmer.EMail;

        Geburtsdatum    = SelectedTeilnehmer.Geburtsdatum?.ToDateTime(TimeOnly.MinValue);
        Umschulung      = SelectedTeilnehmer.Umschulung;
        Kostentraeger   = SelectedTeilnehmer.Kostentraeger;
        Status          = SelectedTeilnehmer.Status;
        _formularWirdGeladen = false;
    }

    public RelayCommand NeuCommand { get; }
    public RelayCommand SpeichernCommand { get; }
    public RelayCommand LoeschenCommand { get; }

    private void Loeschen()
    {
        var result = MessageBox.Show(
        "Möchten Sie diesen Teilnehmer wirklich löschen?",
        "Teilnehmer löschen",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        
        int index = Teilnehmers.IndexOf(SelectedTeilnehmer);
        
        
        TeilnehmerVerwaltung.Entfernen(SelectedTeilnehmer);
        Teilnehmers.Remove(SelectedTeilnehmer);

        if (Teilnehmers.Count > 0)
        {
            if (index >= Teilnehmers.Count)
                index = Teilnehmers.Count - 1;

            SelectedTeilnehmer = Teilnehmers[index];
        }
        else SelectedTeilnehmer = null;
        
    }

    private bool KannLoeschen()
    {
        return SelectedTeilnehmer != null;
    }

    private void FuegeHinzu()
    {
        SelectedTeilnehmer = null;
        IstNeuerTeilnehmer = true;
        FormularFuellen();
    }

    private void Speichern()
    {
        var result = MessageBox.Show(
       "Möchten Sie diesen Teilnehmer wirklich speichern?",
       "Teilnehmer löschen",
       MessageBoxButton.YesNo,
       MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;
        

        // 1. Daten prüfen
        if (!HatAenderungen) 
            return;

        DateOnly? geburtsdatum = Geburtsdatum.HasValue ? DateOnly.FromDateTime(Geburtsdatum.Value) : null;

        if (!TeilnehmerVerwaltung.Pruefen(
                Vorname,
                Nachname,
                EMail,
                geburtsdatum, Umschulung, Kostentraeger, SelectedTeilnehmer))
        {
            MessageBox.Show(
                "Bitte überprüfen Sie die Eingaben. " +
                "Möglicherweise existiert dieser Teilnehmer bereits.");

            return;
        }

        // 2. Daten übernehmen
        int tnummer = (SelectedTeilnehmer == null)? TeilnehmerVerwaltung.GetTeilnehmerNummer() : SelectedTeilnehmer.TeilnehmerNr;
        
            DateOnly ? geburst = Geburtsdatum.HasValue ? DateOnly.FromDateTime(Geburtsdatum.Value) : null;
        Teilnehmer neuerTeilnehmer = new Teilnehmer(tnummer, Vorname.Trim(), Nachname.Trim(),
            geburst, EMail.Trim(), Status, Umschulung, Kostentraeger);
        
        int index = (SelectedTeilnehmer==null) ? -1: Teilnehmers.IndexOf(SelectedTeilnehmer);

        try 
            {
            if (index < 0)
            {
                TeilnehmerVerwaltung.Hinzufuegen(neuerTeilnehmer);
                Teilnehmers.Add(neuerTeilnehmer);
            }

            else
            {
                TeilnehmerVerwaltung.Aktualisieren(neuerTeilnehmer);
                Teilnehmers[index] = neuerTeilnehmer;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Der Teilnehmer konnte nicht gespeichert werden."+ex);

            return;
        }
        
        SelectedTeilnehmer = neuerTeilnehmer;

        // 3. Änderungen zurücksetzen
        HatAenderungen = false;
        SpeichernCommand?.AusfuehrbarkeitAktualisieren();

    }

    private bool KannSpeichern()
    {
        return HatAenderungen;
    }

}
