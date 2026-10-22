using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teilnehmerverwaltung.Services;
using Teilnehmerverwaltung.Models;


namespace Teilnehmerverwaltung.ViewModels;

internal class MainWindowViewModel
{
    public ObservableCollection<Teilnehmer> Teilnehmers { get; set; }

    private Teilnehmer _selectedTeilnehmer;

    public Teilnehmer SelectedTelnehmer { get; set; }

    public MainWindowViewModel()
    {
        var service = new TeilnehmerVerwaltung();
        Teilnehmers = new ObservableCollection<Teilnehmer>(service.AlleTeilnehmer());

        //'SelectedBuch = Buecher[1];


        //FuegeHinzuCommand = new RelayCommand(FuegeHinzu);
        //EntferneCommand = new RelayCommand(Entferne, KannEntfernen);'
    }

}
