# GitHub Setup für PolygonServer

## Repository wurde vorbereitet ✅

- Git-Repository initialisiert
- .gitignore erstellt
- Initial Commit erstellt (40 Dateien, 1901 Zeilen)

## Nächste Schritte

### 1. GitHub-Repository erstellen

1. Gehe zu https://github.com/new
2. Repository-Name: `PolygonServer` (oder einen anderen Namen wählen)
3. **WICHTIG:** Repository **NICHT** mit README, .gitignore oder License initialisieren (da wir bereits Dateien haben)
4. Klicke auf "Create repository"

### 2. Repository mit GitHub verbinden

Nachdem das Repository erstellt wurde, führe diese Befehle aus:

```bash
cd /Users/adammechouate/Documents/PolygonServer

# Remote hinzufügen (ersetze USERNAME mit deinem GitHub-Username)
git remote add origin https://github.com/USERNAME/PolygonServer.git

# Branch umbenennen (falls nötig)
git branch -M main

# Code auf GitHub pushen
git push -u origin main
```

### Alternative: Mit SSH

Wenn du SSH-Keys konfiguriert hast:

```bash
git remote add origin git@github.com:USERNAME/PolygonServer.git
git branch -M main
git push -u origin main
```

## Schnellstart (Kopieren & Einfügen)

```bash
cd /Users/adammechouate/Documents/PolygonServer

# Ersetze USERNAME mit deinem GitHub-Username
git remote add origin https://github.com/USERNAME/PolygonServer.git
git branch -M main
git push -u origin main
```

## Was wurde committed?

- ✅ Alle Source-Dateien (Controllers, Models, DTOs)
- ✅ Program.cs (mit MariaDB-Konfiguration)
- ✅ appsettings.json (ohne Passwörter)
- ✅ README.md
- ✅ START-ANLEITUNG.md
- ✅ .gitignore (schützt sensible Dateien)

## Was wurde NICHT committed?

- ❌ bin/ und obj/ Ordner (Build-Artefakte)
- ❌ appsettings.Development.json (kann sensible Daten enthalten)
- ❌ .vs/ und .vscode/ (IDE-Einstellungen)
- ❌ *.db Dateien (Datenbanken)

## Nach dem ersten Push

Nach dem ersten Push kannst du weitere Änderungen mit:

```bash
git add .
git commit -m "Beschreibung der Änderungen"
git push
```

pushen.

