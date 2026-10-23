using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teilnehmerverwaltung.Helpers;
using Teilnehmerverwaltung.Models;
using Teilnehmerverwaltung.Services;


namespace Teilnehmerverwaltung.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Teilnehmer> Teilnehmers { get; set; }

    private Teilnehmer _selectedTeilnehmer;

    public Teilnehmer SelectedTeilnehmer { get =>_selectedTeilnehmer; 
        set
        {
            if (SetProperty(ref _selectedTeilnehmer, value))
            {
                //EntferneCommand?.AusfuehrbarkeitAktualisieren();
                FormularFuellen();

            }
            ;
        }
    }

    public string Suche {  get; set; }
    
    private string _vorname = string.Empty;
    public string Vorname
    {
        get => _vorname;
        set => SetProperty(ref _vorname, value);
    }

    private string _email = string.Empty;
    public string Nachname
    {
        get => _nachname;
        set => SetProperty(ref _nachname, value);
    }

    private string _nachname = string.Empty;
    public string EMail
    {
        get => _nachname;
        set => SetProperty(ref _nachname, value);
    }
    private DateTime? _geburtsdatum;
    public DateTime? Geburtsdatum
    {
        get => _geburtsdatum;
        set => SetProperty(ref _geburtsdatum, value);
    }


    public MainWindowViewModel()
    {
        var service = new TeilnehmerVerwaltung();
        Teilnehmers = new ObservableCollection<Teilnehmer>(service.AlleTeilnehmer());

        //'SelectedBuch = Buecher[1];


        //FuegeHinzuCommand = new RelayCommand(FuegeHinzu);
        //EntferneCommand = new RelayCommand(Entferne, KannEntfernen);'
    }

    private void FormularFuellen()
    {
        if (SelectedTeilnehmer == null)
        {
            Vorname = string.Empty;
            Nachname = string.Empty;
            EMail = string.Empty;
            Geburtsdatum = null;
            return;
        }

        Vorname = SelectedTeilnehmer.Vorname;
        Nachname = SelectedTeilnehmer.Nachname;
        EMail = SelectedTeilnehmer.EMail;

        Geburtsdatum = SelectedTeilnehmer.Geburtsdatum
            .ToDateTime(TimeOnly.MinValue);
    }
}
