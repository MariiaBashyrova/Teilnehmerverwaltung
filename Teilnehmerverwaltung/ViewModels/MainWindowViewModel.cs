using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Teilnehmerverwaltung.Helpers;
using Teilnehmerverwaltung.Models;
using Teilnehmerverwaltung.Services;


namespace Teilnehmerverwaltung.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Teilnehmer> Teilnehmers { get; set; }
    private TeilnehmerVerwaltung TeilnehmerVerwaltung;
    private Teilnehmer _selectedTeilnehmer;

    public Teilnehmer SelectedTeilnehmer { get =>_selectedTeilnehmer; 
        set
        {
            if (SetProperty(ref _selectedTeilnehmer, value))
            {
                FormularFuellen();
                LoeschenCommand?.AusfuehrbarkeitAktualisieren();
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

    public ICollectionView TeilnehmerView { get; }
    #region Konstruktor
    public MainWindowViewModel()
    {
        TeilnehmerVerwaltung = new TeilnehmerVerwaltung();
        Teilnehmers = new ObservableCollection<Teilnehmer>(TeilnehmerVerwaltung.AlleTeilnehmer());
        Umschulungen = new ObservableCollection<Umschulung>(TeilnehmerVerwaltung.AlleUmschulungen());
        Kostentraegers = new ObservableCollection<Kostentraeger>(TeilnehmerVerwaltung.AlleKostentraeger());
        

        NeuCommand = new RelayCommand(FuegeHinzu);
        SpeichernCommand = new RelayCommand(Speichern, KannSpeichern);
        LoeschenCommand = new RelayCommand(Loeschen, KannLoeschen);
        TeilnehmerView = CollectionViewSource.GetDefaultView(Teilnehmers);
        TeilnehmerView.Filter = FilterTeilnehmer;
    }
    #endregion
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
    #region Commands
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
        TeilnehmerVerwaltung.Entfernen(SelectedTeilnehmer);
        Teilnehmers.Remove(SelectedTeilnehmer);
        SelectedTeilnehmer = null;
        
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
    #endregion

    #region Filter
    private string _suche;
    public string Suche
    {
        get => _suche;
        set
        {
            if (SetProperty(ref _suche, value)) TeilnehmerView.Refresh();
        }
    }

    private Umschulung? _ausgewaehlteUmschulung;

    public Umschulung? AusgewaehlteUmschulung
    {
        get => _ausgewaehlteUmschulung;
        set
        {
            if (SetProperty(ref _ausgewaehlteUmschulung, value))
                TeilnehmerView.Refresh();
        }
    }

    private TeilnehmerStatus? _ausgewaehlterStatus;

    public TeilnehmerStatus? AusgewaehlterStatus
    {
        get => _ausgewaehlterStatus;
        set
        {
            if (SetProperty(ref _ausgewaehlterStatus, value))
                TeilnehmerView.Refresh();
        }
    }
    private bool FilterTeilnehmer(object obj)
    {
        if (obj is not Teilnehmer teilnehmer)
            return false;

        string suchtext = Suche?.Trim() ?? "";

        bool namePasst =
            string.IsNullOrEmpty(suchtext) ||
            (teilnehmer.VollName?.Contains(
                suchtext,
                StringComparison.OrdinalIgnoreCase) ?? false);

        bool umschulungPasst =
            AusgewaehlteUmschulung == null ||
            teilnehmer.Umschulung == AusgewaehlteUmschulung;

        bool statusPasst =
            AusgewaehlterStatus == null ||
            teilnehmer.Status == AusgewaehlterStatus;

        return namePasst && umschulungPasst && statusPasst;
    }
    #endregion
}
