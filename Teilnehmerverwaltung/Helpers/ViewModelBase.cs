using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Teilnehmerverwaltung.Helpers;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? eigenschaftsName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(eigenschaftsName));
    }

    /// <summary>
    /// Setzt ein Feld und meldet die Änderung – aber nur, wenn sich der Wert unterscheidet.
    /// Rückgabe true = es wurde etwas geändert.
    /// </summary>
    protected bool SetProperty<T>(ref T feld, T wert, [CallerMemberName] string? eigenschaftsName = null)
    {
        if (EqualityComparer<T>.Default.Equals(feld, wert))
        {
            return false;
        }

        feld = wert;
        OnPropertyChanged(eigenschaftsName);
        return true;
    }
}
