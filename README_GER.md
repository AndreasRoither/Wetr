<h1 align="center">
  <!--<a name="logo" href=""><img src="" alt="Logo" width="200"></a>-->
  <br>
  WeatherTracer – wetr [ˈwetər]
  <br>
  - Archiviert -

  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)
</h1>

# Vorwort

Dieses Projekt ist archiviert und dient nur als Zukunftsreferenz.

# Projekt Übersicht

Ein System zum Visualisieren von lokalen Wetterdaten mithilfe einer WPF Anwendung.

**Developers:**

* Englisch Daniel
* Roither Andreas

Die Dokumentation dieses Projekts befindet sich im `docs/` Verzeichnis.

# Showcase

## Cockpit

Login          |  Dashboard
:-------------------------:|:-------------------------:
<img src="docs/swk5-wetr-documentation/pictures/Cockpit/Cockpit_1.png" width="400"/>  |  <img src="docs/swk5-wetr-documentation/pictures/Cockpit/Cockpit_2.png" width="400"/>

Analysis          |  Stations
:-------------------------:|:-------------------------:
<img src="docs/swk5-wetr-documentation/pictures/Cockpit/Cockpit_3.png" width="400"/>  |  <img src="docs/swk5-wetr-documentation/pictures/Cockpit/Cockpit_4.png" width="400"/>

<p align="left">
  <img src="docs/swk5-wetr-documentation/pictures/Cockpit/Cockpit_5.png" width="400"/>
</p>

## Simulator

Station Selection          |  Preset Creation
:-------------------------:|:-------------------------:
<img src="docs/swk5-wetr-documentation/pictures/Simulator/Simulator_1_StationSelection.png" width="400"/>  |  <img src="docs/swk5-wetr-documentation/pictures/Simulator/Simulator_2_PresetCreation.png" width="400"/>

Preset Assignment          |  Simulation
:-------------------------:|:-------------------------:
<img src="docs/swk5-wetr-documentation/pictures/Simulator/Simulator_3_PresetAssignment.png" width="400"/>  |  <img src="docs/swk5-wetr-documentation/pictures/Simulator/Simulator_4_Simulation.png" width="400"/>

## Technologie

IDE:  
[Visual Studio](https://www.visualstudio.com/)  

Andere tools/libraries:  
[Python](https://www.python.org/)  
[Docker](https://www.docker.com/)  
[MahApps.Metro](https://github.com/MahApps/MahApps.Metro)  
[WPF](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/overview/)

### Angabe

Es existieren zahlreiche Web-Seiten und mobile Anwendungen, die uns Wettervorhersagen bis weit in die
Zukunft anbieten. Nicht immer sind diese Prognosen seriös, da sie auf relativ wenigen Wetterstationen
basieren (und eine langfristige Prognose mit großen Unsicherheiten verbunden ist). Aufgrund der geografischen Struktur Österreichs weist das Wetter in unserem Land teilweise große regionale Unterschiede auf.
Das ist ein weiterer Faktor, der eine Wetterprognose in Österreich sehr schwierig macht.

Die Basis für jede Wettervorhersage sind Daten von einem möglichst dichten Netz an Wetterstationen.
Nicht nur offizielle Stellen wie die Zentralanstalt für Meteorologie und Geodynamik betreiben Wetterstationen, auch eine zunehmende Anzahl von Privatpersonen verfügt über einfache Funkstationen, die über
einen Netzwerkanschluss Wetterdaten liefern können.

Im Rahmen dieser Projektarbeit soll eine Anwendung entwickelt werden, welche Daten von vielen Wetterstationen übernehmen kann, diese statistisch auswertet und die Ergebnisse grafisch ansprechend visualisiert. Diese Daten bilden zwar die Basis für eine Wetterprognose, allerdings ist diese Funktionalität nicht
Bestandteil dieser Projektarbeit.

## Funktionale Anforderungen

Im Rahmen dieser Projektarbeit ist das Softwaresystem zur Verwaltung der Stammdaten von Wetterstationen und der von diesen Stationen gesendeten Messdaten zu entwickeln. Die Stations- und Wetterdaten
sind in einer zentralen Datenbank zu speichern, Wetterdaten müssen statistisch ausgewertet und visualisiert werden. Das Softwaresystem besteht aus folgenden Komponenten:

* **Wetr.Server**  
ist die zentrale Komponente zur Datenverwaltung. Wetr.Server verfügt auch über eine flexible und effiziente Abfragekomponente, welche Möglichkeiten zur Datenfilterung und Datenkumulation bietet.
* **Wetr.WebService**  
exportiert die gesamte Funktionalität von Wetr.Server in Form eines Web-Dienstes, der über zwei Endpunkte verfügt. Über einen Endpunkt kann Wetr.Web mit dem Server kommunizieren. Über den zweiten Endpunkt können Wetterdaten in das System eingespeist werden.
* **Wetr.Cockpit**  
dient zur Darstellung der aktuellen Wetterlage und zur Visualisierung der längerfristigen Entwicklung von Wetterdaten. Mit entsprechenden Rechten können die Stammdaten der Wetterstationen gewartet werden (hinzufügen, aktualisieren, löschen).
* **Wetr.Web**  
Der Web-Client stellt ebenfalls die aktuelle Wetterlage dar, bietet Möglichkeiten zur Visualisierung von historischen Wetterdaten und hat eingeschränkte Funktionalität zur Administration von Wetterstationen.
* **Wetr.Simulator**  
Mit dieser Komponente können automatisiert Wetterdaten generiert und an Wetr.Server übergeben werden

### (a) Datenmodell

Im Folgenden werden die wesentlichen Attribute der wichtigsten Entitäten des Systems definiert. Aus der
Anforderungsspezifikation können sich zusätzliche Entitäten und weitere Eigenschaften der angeführten
Entitäten ergeben, die entsprechend zu ergänzen sind.

#### (a.1) Station

Für jede Wetterstation sind der Name, der Stationstyp (Fabrikat u. dgl.), die Adresse
sowie deren Geokoordinaten (Längen- und Breitengrad) zu speichern. Die Postleitzahl definiert
die Zuordnung der Station zu einer Gemeinde und damit auch die Zuordnung zu einem Bezirk
und einem Bundesland.

#### (a.2) Community/District/Province

In diesen Entitäten sind die Stammdaten der Gemeinden, Bezirke
und Bundesländer gespeichert. Auf diese Daten wird nur lesend zugegriffen. Die Administration
dieser Daten ist nicht Teil dieses Projekts.

#### (a.3) User

Diese Entität dient zur Verwaltung der Zugangsdaten jener BenutzerInnen, welche die
Stammdaten einer Station verändern dürfen.

#### (a.4) Measurement

Eine Messung enthält unterschiedliche Wetterdaten, wie Lufttemperatur, Luftdruck, Regenmenge, Luftfeuchtigkeit, Windgeschwindigkeit, Windrichtung. Jede Messung enthält einen Messwert und einen Zeitstempel. Messattributen ist eine Einheit (Grad Celsius, km/h
etc.) zuzuordnen. Es ist davon auszugehen, dass Wetterdaten unregelmäßig und mit unterschiedlicher Frequenz gemeldet werden.

### (b) Wetr.Server

Die Serverkomponente stellt die zentrale Funktionalität für die Clients zur Verfügung.

#### (b.1) Stammdatenverwaltung

Die Serverkomponente muss eine geeignete Schnittstelle zur Verwaltung der Wetterstationen (hinzufügen, löschen, ändern) anbieten.

#### (b.2) Verwaltung der Messdaten

Die Wetterdaten sind die wesentlichen Bewegungsdaten des Systems. Es ist eine Funktion zum Hinzufügen eines neuen Messdatensatzes vorzusehen. Messdaten sind immer mit einer Wetterstation verbunden.

#### (b.3) Abfragen

Die Abfragekomponente ist das Kernmodul des Servers. Das Abfragemodul soll Möglichkeiten zur Datenkumulation und zur Filterung von Wetterdaten bieten:

* Filtern nach Wetterstation bzw. Region: Eine Region ist durch einen Punkt und einen Radius definiert, kann aber auch eine Gemeinde, ein Bezirk oder ein Bundesland sein.
* Kumulation von Daten für bestimmte Zeitintervalle (Stunde, Tag, Woche, Monat): Datenkumulation kann Minimum-, Maximum-, Durchschnittsbildung aber auch Aufsummieren der Messwerte bedeuten. Hier einige Beispiele für Abfragen, welche diese Komponente unterstützen soll:
* Was war die Durchschnitts-/Minimal-/Maximal-Temperatur bei einer bestimmten Wetterstation/in einer Region gruppiert nach Tagen/Woche /Monaten?
* Was war die tage-/wochen-/monatsweise kumulierte Niederschlagsmenge bei einer bestimmten Wetterstation?
* Was war die durchschnittliche Niederschlagsmenge in einer bestimmten Region gruppiert nach Tagen/Wochen/Monaten?Seite 3

#### (b.4) Messdatenanalyse

Es soll eine einfache Auswertungskomponente geschaffen werden, mitder in regelmäßigen Abständen Warnungen über aktuell auftretende  Wetterphänomene über Twitter gegeben werden können. Darunter fallen z.B. Starkregen, Sturm oder gefrierender Regen. Die Wetterwarnung sollte, neben dem auftretenden Phänomen selbst, auch unbedingt die Region beinhalten.

### (c) Wetr.WebService

Die für Wetr.Web und Wetr.Simulator erforderliche Funktionalität ist in Form eines REST-basierten
Web-Service zu exportieren. Der Zugriff auf das Web-Service muss nicht abgesichert werden.

### (d) Wetr.Simulator

Der Wettersimulator hat die Aufgabe, die Messdaten, die im Realbetrieb von den Wetterstationen
geliefert werden, zufallszahlengesteuert zu generieren und über die dafür vorgesehene RESTSchnittstelle in das System einzuspeisen. Der Simulator kann Messwerte in einem vorgegebenen Zeitbereich, der auch in der Vergangenheit liegen kann, simulieren und besitzt eine Zeitrafferfunktion zur
beschleunigten Generierung von Daten. Sie können den Simulator dafür verwenden, die Robustheit
und Leistungsfähigkeit der Implementierung zu demonstrieren.

#### (d.1) Hinzufügen/Entfernen einer Wetterstation

Aus der Liste der verfügbaren Wetterstationen muss eine Station ausgewählt und zum Simulator hinzugefügt werden können. Da der Simulator mehrere Wetterstationen unterstützen muss, muss dieser Vorgang beliebig oft wiederholbar sein. Auch das Löschen einer Wetterstation ist vorzusehen.

#### (d.2) Simulationseinstellungen

Für einen Simulationsdurchgang muss die simulierte Wetterstation, das Simulationsdatum und die -uhrzeit (Beginn und Ende) sowie die Simulationsgeschwindigkeit (z. B. halbe Geschwindigkeit, reale Geschwindigkeit, 10-fache Geschwindigkeit) festgelegt werden. Verwenden Sie ansprechende Steuerelemente (Combobox, Datumsauswahl, Slider, etc.).

#### (d.3) Konfiguration der Messfolgen

Eine Messfolge ist eine Menge von zusammenhängenden Messwerten über einen gewissen Zeitraum (z. B. Verlauf der Temperatur bei einer bestimmten Wetterstation über einen Tag hinweg). Pro Wetterstation sind mehrere Messfolgen zu simulieren.
Für jede Messfolgen müssen folgende Parameter eingestellt werden können:

* Messwert, der simuliert werden soll (z. B. Temperatur, Luftdruck etc.)
* Frequenz, in der neue Daten generiert werden sollen (z. B alle 10 s)
* Wertebereich (von/bis) für die erzeugten Werte
* Strategie zur Verteilung
  * linearer Anstieg/Abstieg zwischen von und bis
  * zufälliger Wert zwischen von und bis
  * mindestens eine frei wählbare zusätzliche Strategie, die einen beliebigen
  
Messwert besonders gut simulieren kann (z. B. Temperaturverteilung über den Tag, ansteigende und abnehmende Regenmenge, etc.)
Generieren Sie möglichst realistische Messfolgen. Beachten Sie, dass ein Messwert nicht vollkommen unabhängig vom vorausgehende Wert ist. Die Temperatur wird beispielsweise in einem
kurzen Zeitraum nicht völlig zufällige Werte in einem bestimmten Zahlenbereich annehmen.

#### (d.4) Steuern der Simulation

Die Simulation kann gestartet und gestoppt werden. Dadurch wird mit der Generierung von Messwerten begonnen bzw. diese wieder angehalten. Die Benutzeroberfläche muss während der Simulation responsiv sein, um jederzeit eine Simulation stoppen zu können.

#### (d.5) Ändern der Simulationsgeschwindigkeit

Die Simulationsgeschwindigkeit kann während einer laufenden Simulation verändert werden und wirkt sich unmittelbar aus.

#### (d.6) Darstellung der Wetterstationen

Die aktuellen Messwerte der Wetterstationen sowie der Messdatenverlauf muss grafisch ansprechend aufbereitet werden. Als Orientierungshilfe können reale Wetterstationen dienen. Die simulierten Wetterstationen sollen optisch klar voneinander getrennt werden (z. B. Aufteilung in Reiter o. Ä.).

#### (d.7) Lastssimulation

Im Simulator kann die Generierung von Messwerten für eine größere Anzahl von Wetterstationen ausgelöst werden. Die resultierenden Messwertfolgen müssen nicht grafisch dargestellt werden. Welche Parameter Sie für diesen Anwendungsfall konfigurierbar machen, ist Ihnen überlassen.

### (e) Wetr.Cockpit

Wetr.Cockpit ist ein Client, der vor allem von Spezialisten (Meteorologen) genutzt wird. Diese Komponente hat zwei wesentliche Funktionalitäten: die Bearbeitung von Wetterstationen und die Analyse
von Wetterdaten.

#### (e.1) Verwaltung von Wetterstationen

Es können neue Wetterstationen erfasst, editiert und gelöscht werden (siehe (b.1)). Das Löschen einer Wetterstation soll nur möglich sein, wenn für diese Wetterstation noch keine Messdaten vorhanden sind.

#### (e.2) Wetteranalysen

So wie in (b.3) beschrieben, sollen auf Basis der erfassten Wetterdaten Abfragen durchgeführt werden können. Die BenutzerIn muss die dafür benötigten Abfrageparameter, wie Filterbedingungen, Parameter zur Gruppierung der Daten, in einer übersichtlichen Art und
Weise eingeben können. Orientieren Sie sich an der in (b.3) beschriebenen Funktionalität.
Das Ergebnis einer Abfrage muss in grafischer Form angezeigt werden. Die Verwendung einer
bestehenden Chart-Komponente wird empfohlen.
Einerteams können sich auf eine tabellarische Darstellung der Daten beschränken.
Es ist besonders darauf zu achten, dass während der Durchführung einer Abfrage – die längere
Zeit in Anspruch nehmen kann – die Benutzeroberfläche nicht blockiert ist.

### (f) Wetr.Web

Mithilfe dieses Klienten können sich BenutzerInnen einen Überblick über die aktuelle Wettersituation
verschaffen. Wichtig ist u. a., dass BenutzerInnen eine Präferenzliste führen können, in die sie Wetterstationen eintragen und so die Wetterentwicklung an mehreren Orten verfolgen können. Grundsätzlich ist bei der Benutzerschnittstelle auf eine einfache Benutzbarkeit und eine optisch ansprechende
Umsetzung zu achten. Als ein gutes Beispiel sei auf <https://www.wunderground.com/> hingewiesen.Seite 5

#### (f.1) Öffentliche Suche

BenutzerInnen sollen nach Wetterdaten für einen bestimmten Ort suchen können. Die Ergebnisse sollen sowohl grafisch (z.B. mit Hilfe von Charts), als auch tabellarisch angezeigt werden. Achten Sie auf eine möglichst übersichtliche Darstellung. Erlauben Sie den BenutzerInnen, Details (bspw. Stationstyp) zu Wetterstationen ein- und ausblenden zu können.

#### (f.2) Visualisierung

Zeigen Sie die Wetterdaten in unterschiedlichen Varianten an, beispielsweise stündliche Wetterdaten, Tageszusammenfassung mit niedrigstem und höchstem Wert und stündliche Temperaturkurve. Zeigen Sie auch Statistiken zu den einzelnen Messdaten an (grafisch und tabellarisch). Erlauben Sie dem Benutzer diese anzupassen (Zeitbereich, Ort, etc.).

#### (f.3) Login

Ein Benutzer soll die Möglichkeit haben, sich einzuloggen. Ein besonderer Bonus hier wäre die Verwendung von OAuth und OpenID.

#### (f.4) Benutzerdefinierte Ansicht / Verwalten von Präferenzen

Die BenutzerInnen sollen sich ihr eigenes „Wetterdashboard“ zusammenstellen können. Die Präferenzen (z.B. Temperatureinheit,
Wetterstationen) sollen über die Benutzersession hinaus im Browser (Local Storage) verwaltet/gespeichert werden. Die BenutzerInnen können die anzuzeigenden Wetterstationen auf Basis einer Suchfunktion selbst bestimmen. Geben Sie den BenutzerInnen auch die Möglichkeit, die
Art der Anzeige (z.B. stündliche Wetterdaten, Tageszusammenfassung, etc.) zu konfigurieren und
speichern Sie diese Einstellungen, sodass beim nächsten Login das benutzerdefinierte Dashboard
wieder angezeigt wird.

#### (f.5) Verwalten eigener Wetterstationen und Eingabe eigener Wetterdaten

Schaffen Sie eine Möglichkeit, dass registrierte BenutzerInnen eigene Wetterstationen verwalten und Wetterdaten eingeben können. Das soll allerdings nur authentifizierten Benutzern erlaubt sein. Wir wollen damit sicherstellen, dass nur hochwertige Daten in die Datenbank gelangen.
