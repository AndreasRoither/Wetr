# WeatherTracer – wetr
Ein System zum Visualisieren von lokalen Wetterdaten

**Entwickler:**
* Englisch Daniel (S1610307056)
* Roither Andreas (S1610307097)

## Docker und Datenbank

Zum starten der Datenbank wird Docker verausgesetzt. Zum Starten des Datenbankcontainers, muss die Datei `run.ps1` mit der Windows Powershell gestartet werden.
Danach kann die Datenbank unter `http://localhost:8080/` mit PhpMyAdmin verwaltet werden. Beim ersten Start müssen die `create` Skripte in PhpMyAdmin
importiert werden, welche sich im `sql/create` Ordner befinden. Danach können Beispieldaten vom Ordner `sql/insert` eingefügt werden.
Für eine detailiertere Anleitung siehe die [Dokumentation](docs/Latex/Documentation.pdf
