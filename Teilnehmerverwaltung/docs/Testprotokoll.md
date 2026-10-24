| ID | Anwendungsfall | Vorbedingung / Testschritte | Erwartetes Ergebnis | Ist-Ergebnis | bestanden? |
|---|---|---|---|---|---|
| T01 | Teilnehmerliste anzeigen | Programm starten | Liste zeigt 10 Teilnehmende, „Löschen" ist deaktiviert | Liste zeigt 10 Teilnehmende, „Löschen" ist deaktiviert | Ja |
| T02 | Teilnehmerdetails anzeigen | Teilnehmer 1003 auswählen | Felder zeigen Linus Torvalds, FISI-25A, Status Praktikum | Felder zeigen Linus Torvalds, FISI-25A, Status Praktikum | Ja |
| T03 | Teilnehmer anlegen | „Neu", Kent Beck, FIAE-26B, Jobcenter Stuttgart eingeben, „Speichern" | erscheint mit TN-Nr 1011 in der Liste | Teilnehmer Kent Beck erscheint mit TN-Nr 1011 in der Liste | Ja |
| T04 | Teilnehmer anlegen | „Neu", Nachname leer lassen, „Speichern" | Fehlermeldung, Liste unverändert | Fehlermeldung wird angezeigt, Liste bleibt unverändert | Ja |
| T05 | Teilnehmer bearbeiten | 1007 auswählen, Status auf „Abgebrochen" setzen, „Speichern" | Liste zeigt den neuen Status sofort an | Liste zeigt den Status „Abgebrochen" sofort an | Ja |
| T06 | Teilnehmer löschen | 1005 auswählen, „Löschen", Abfrage mit „Nein" beantworten | 1005 bleibt in der Liste | Teilnehmer 1005 bleibt in der Liste | Ja |
| T07 | Teilnehmer löschen | 1005 auswählen, „Löschen", Abfrage mit „Ja" beantworten | 1005 ist entfernt, „Löschen" wieder deaktiviert | Teilnehmer 1005 ist entfernt, „Löschen" ist wieder deaktiviert | Ja |
