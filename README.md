# Teilnehmerverwaltung

## Projektbeschreibung

WPF-Anwendung zur Verwaltung von Teilnehmenden einer Umschulung. Die Anwendung ermöglicht das Anzeigen, Anlegen, Bearbeiten und Löschen von Teilnehmendendatensätzen.

## Pflichtumfang

Die Pflichtaufgaben 1 bis 4 wurden umgesetzt.

## Umgesetzte optionale Erweiterungen

- **a) Filter:** Die Teilnehmerliste kann über eine Suchfunktion nach Namen gefiltert werden. Zusätzlich kann nach Umschulung und Status gefiltert werden. Die Namenssuche berücksichtigt Groß- und Kleinschreibung nicht.
- **b) DataBinding:** Die Eingabefelder sind über WPF-Bindings mit den Daten verbunden. Die Eingaben werden nicht mehr manuell im Code-Behind befüllt und ausgelesen.
- **c) MVVM:** Die Anwendung verwendet das MVVM-Muster mit einem ViewModel, Commands und einer `ObservableCollection<Teilnehmer>`. Die Löschfunktion kann nur ausgeführt werden, wenn ein Teilnehmender ausgewählt ist. Das Code-Behind setzt den `DataContext`.
- **d) Dublettenprüfung:** Vor dem Speichern wird nach einem Datensatz mit gleichem Vor- und Nachnamen sowie gleichem Geburtsdatum gesucht. Bei einem Treffer wird der vorhandene Datensatz angezeigt und gefragt, ob trotzdem gespeichert werden soll. Bei Ablehnung wird der vorhandene Datensatz in der Liste ausgewählt.

## Technologien

- C#
- WPF
- .NET
- MVVM
