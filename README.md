<h1>ConnectMyPrinter.NET</h1>

![Hauptanwendung](ConnectMyPrinterImages/drex_hauptfenster_1_screen.png)

**Wenn Netzwerkdrucker im Unternehmensnetzwerk eingesetzt werden, werden diese in der Regel �ber einen zentralen Printserver mit dem Client verbunden. 
Leider kommt es in vielen Umgebungen vor, dass es nicht nur einen Printserver gibt. Das f�hrt oft zu Verwirrungen bei den Endanwendern.
Wenn hierzu dann die Netzwerkerkennung unter Windows �ber Gruppenrichtlinien deaktiviert sind und der Benutzer nat�rlich keine Administrator-Rechte besitzt, 
ist dieser relativ aufgeschmissen. Somit kann der Drucker auch mit der Eingabe der IP-Adresse nicht wie gewohnt unter "Ger�te und Drucker" installiert werden.**

In diesem Fall m�ssen die Benutzer den Drucker mit Eingabe des UNC-Pfades unter "Start" verbinden oder auf dem Printserver direkt nach dem Drucker suchen. Doch was ist, wenn die Endanwender den Namen des Printservers nicht wissen?

In der Regel w�rde hier ein Anruf bei der IT, bzw. des Service Desks erfolgen. Mit diesem kleinen Programm k�nnen allerdings einige Stolperfallen verhindert werden:

Es sind beliebig viele Windows-Printserver definierbar, welche nach freigegebenen Druckern durchsucht werden und dem Anwender bei der Suche nach einem Freigabenamen direkt vorgeschlagen werden.
Zus�tzlich ist es m�glich, bei jedem Printserver Priorit�ten festzulegen. Ist z.B. ein Drucker auf 2 Printservern angelegt, wird der h�her eingestufte Printserver verwendet. 
Optional kann die Anwendung auch nach dem gew�nschten Printserver fragen.
Der Anwender wird nicht mehr mit Printservernamen konfrontiert, sondern muss nur noch den Freigabenamen wissen.
�ber die Anwendung werden optional auch alle lokalen Drucker sowie Netzwerkdrucker angezeigt, welche mit ein paar Klicks auch gel�scht bzw. einfacher ge�ndert werden k�nnen.
Der Standarddrucker ist mit 2 Klicks angepasst und dem Anwender direkt auf den ersten Blick �bersichtlich dargestellt.
Optional kann ein Drucker auch in einem Profil gespeichert und sp�ter wieder verbunden werden.
F�r Support-Admins werden wichtige Informationen (Treiberversion, Treiberpfad) direkt beim Aufklappen eines Eintrags angezeigt:

![Druckerdetails](ConnectMyPrinterImages/drex_druckerdetails_screen.png)

Ein Drucker kann mit wenigen Klicks neu installiert (der Drucker, Treiber sowie das Treiberpaket wird hierbei entfernt) oder die Standardeinstellungen des Drucker zur�ckgesetzt werden.
Dokumente, welche in der Druckerwarteschlange h�ngen, k�nnen einfach gel�scht werden.
Wenn Abh�ngigkeiten zwischen den Druckern bestehen, da diese den gleichen Treiber verwenden, gibt es die M�glichkeit beide (bzw. alle abh�ngigen Drucker) zu l�schen und erneut zu installieren.
Vollst�ndige Unterst�tzung der UAC: Wird ein administrativer Eingriff notwendig, wird f�r diesen explizit ein Prozess generiert.
Optionale �nderung der Berechtigung f�r den Windows-Spoolerdienst: Mit einer kleinen Anwendung werden die Berechtigungen so gesetzt, dass auch ein Benutzer mit eingeschr�nkten Berechtigungen die Druckerwarteschlange selbst neu starten kann.
Optionaler Minimal-Mode: Es k�nnen nur Drucker verbunden werden. Optional kann festgelegt werden, ob der Endanwender die Anwendung auch im normalen Modus erweitern darf.

Einfaches Customizing: Es kann ein eigenes Unternehmenslogo festgelegt, sowie einige Texte in der Anwendung angepasst werden.
Einige Anzeigetexte k�nnen f�r die Anpassung an Firmenanforderungen ge�ndert oder Limitierungen f�r Anwender eingestellt werden.

Trayanwendung, welche alle installierten Drucker anzeigt und der aktuelle Standarddrucker ge�ndert werden kann:
Bei jedem Drucker kann auch in die Standardeinstellungen gesprungen, sowie der Drucker gel�scht bzw. getrennt werden:

![Trayanwendung (Untermen�)](ConnectMyPrinterImages/drex_untermenu_eines_druckers_screen.png)

Profileditor f�r die Erstellung von "Druckerprofilen":
Mit einem kleinen Tool k�nnen Druckerprofile erstellt werden, welche automatisch z.B. 2 Drucker verbinden, den Standarddrucker �ndern sowie einen Drucker l�schen. Hierzu wird eine Profildatei mit der Dateierweiterung *.prpr erstellt. Die Profildatei ist in einer XML-Struktur aufgebaut, kann also jederzeit auch ohne den Profileditor bearbeitet werden.
Es ist m�glich, Druckerprofile �ber eine UNC-Freigabe auf einen anderen Client zu "pushen", die aktuell verbundenen Drucker abzurufen oder eine Konfiguration von einem Client auf einen anderen Client zu klonen.
Falls ein Drucker bzw. Treiber sich nicht mit "normalen" Mitteln aus der Druckerwarteschlange entfernen l�sst, stehen dem Administrator weitere Funktionen zur Verf�gung.

![Druckertreiber entfernen](ConnectMyPrinterImages/drex_hauptfenster_3_screen.png)

Diese Anwendung entfernt die Drucker sowie Treiber Low-Level direkt in der Windows Registry.

Falls die Anwendung auf einem englischsprachigen Windows (en-US) ausgef�hrt wird, ist die Oberfl�che lokalisiert.

<h2>Verteilung</h2>

Um die Anwendung unbeaufsichtigt zu installieren, kann der folgende Aufruf verwendet werden:

```ConnectMyPrinter.NET.msi /q /norestart APPSETTINGSFILE=AppSettings.xml LOGOFILE=logo.gif```

F�r eine unbeaufsichtigte Deinstallation folgender Aufruf:

```msiexec /x {D9F305BE-DA52-4B9B-BCD9-9CF4C34BBE07} /q /norestart```