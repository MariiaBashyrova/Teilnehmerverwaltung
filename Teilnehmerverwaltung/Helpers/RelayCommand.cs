using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Teilnehmerverwaltung.Helpers;

public class RelayCommand : ICommand
{
    private readonly Action _ausfuehren;
    private readonly Func<bool>? _kannAusfuehren;

    public RelayCommand(Action ausfuehren, Func<bool>? kannAusfuehren = null)
    {
        _ausfuehren = ausfuehren;
        _kannAusfuehren = kannAusfuehren;
    }

    // WPF fragt hier nach, ob der Button aktiv sein soll.
    // Ohne Bedingung ist der Command immer ausführbar.
    public bool CanExecute(object? parameter)
    {
        if (_kannAusfuehren == null)
        {
            return true;
        }
        return _kannAusfuehren();
    }

    // Wird beim Klick (bzw. bei der KeyBinding) aufgerufen
    public void Execute(object? parameter)
    {
        _ausfuehren();
    }

    // Auch hier wieder das Observer-Muster: Der Button lauscht auf dieses Ereignis
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Fordert WPF auf, CanExecute erneut abzufragen (z. B. nach einer Auswahländerung).
    /// </summary>
    public void AusfuehrbarkeitAktualisieren()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
