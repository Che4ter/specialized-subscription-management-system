# PAWI

Dieses Repository enthält den Source Code, welcher für das Projekt Modul PAWI entwickelt wurde. Ziel der Arbeit war es eine Webapplikation zu erstellen, welche Kunden und Patenschaften verwalten kann. Dafür wurde ein Flexibles Abonnementen System implementiert. Zusätzlich gibt es die Möglichkeit PDF Dokumente zu generieren. Für die Entwicklung wurde ASP.NET Core 2.0 verwendet.

# Einrichten der Entwicklungsumgebung
Entwickelt wurde das Projekt mit dem Visual Studio 2017. Durch den Einsatz von .NET Core wäre auch eine Entwicklung und Betrieb auf Linux Betriebssystemen denkbar. Für die Datenbank wird ein MS-SQL Server verwendet. DB Scripts befinden sich im Ordner /devdocs. Das Projekt verwendet für die Generierung der PDFs die [Rotativa Library](https://github.com/webgio/Rotativa). Da zurzeit noch eine Inkompatibilität mitmit .NET Core 2.0 bestand wurde folgender Fork eingesetzt: https://github.com/Stefanone91/Rotativa.NetCore

- Clonen des Repository
- Hinzufügen der Rotativa Abhängigkeit
- Erstellen einer Datenbank und importieren der DB Scripts

## E-Mail Versand
Damit das Passwort-Reset E-Mail versendet werden kann, muss im appsettings.json ein SMTP Server angegeben werden

# Urheberrecht
Die Applikation wurde unter Apache License 2.0 veröffentlicht. Ausgenommen sind die Logos und die Bilder innerhalb der PDF Dokumente. Diese gehören dem Industriepartner und dürfen nicht ohne Genehmigung weiterverwendet werden.
