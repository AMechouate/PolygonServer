# PolygonServer Start-Anleitung

## Voraussetzungen

✅ **Erledigt:**
- .NET 8.0 SDK installiert
- MariaDB 11.2.3 installiert und läuft
- Datenbank `PolygonDb` erstellt
- Projekt auf MariaDB umgestellt

## Projekt starten

### 1. In das Projektverzeichnis wechseln
```bash
cd /Users/adammechouate/Documents/PolygonServer/PolygonApi
```

### 2. Projekt starten
```bash
dotnet run
```

Oder mit spezifischem Profil:
```bash
# HTTP (Port 5104)
dotnet run --launch-profile http

# HTTPS (Port 7149)
dotnet run --launch-profile https
```

### 3. API-Zugriff

Nach dem Start ist die API verfügbar unter:

- **HTTP:** http://localhost:5104
- **HTTPS:** https://localhost:7149
- **Swagger UI:** http://localhost:5104/swagger oder https://localhost:7149/swagger

## API-Endpunkte

Alle Endpunkte akzeptieren POST-Requests mit JSON-Body:

- `POST /lagerort` - Erstellt einen neuen Lagerort
- `POST /kreditor` - Erstellt einen neuen Kreditor
- `POST /artikel` - Erstellt einen neuen Artikel
- `POST /mitarbeiter` - Erstellt einen neuen Mitarbeiter
- `POST /artikelvariante` - Erstellt eine neue Artikelvariante
- `POST /artikeleinheit` - Erstellt eine neue Artikeleinheit
- `POST /zustaendigkeitseinheitencod` - Erstellt einen neuen Zuständigkeitseinheitencode
- `POST /artikelreferenz` - Erstellt eine neue Artikelreferenz
- `POST /projekt` - Erstellt ein neues Projekt
- `POST /zuordnung-verkaeufer-zustaendigkeitseinheit` - Erstellt eine neue Zuordnung

## Datenbank

- **Datenbankname:** `PolygonDb`
- **Benutzer:** `adammechouate`
- **Passwort:** `naima`
- **Connection:** Unix Socket (`/tmp/mysql.sock`)

Die Datenbank wird automatisch erstellt, wenn die Anwendung startet (`EnsureCreated()`).

## Troubleshooting

### Port bereits belegt
```bash
# Anderen Port verwenden
dotnet run --urls "http://localhost:5105"
```

### Datenbankverbindungsfehler
```bash
# Prüfen ob MariaDB läuft
brew services list | grep mariadb

# Datenbank prüfen
mysql -u adammechouate -pnaima -e "SHOW DATABASES;"
```

### Build-Fehler
```bash
# Pakete neu herstellen
dotnet restore

# Projekt neu bauen
dotnet clean
dotnet build
```

## Entwicklung

- **Framework:** .NET 8.0
- **ORM:** Entity Framework Core 8.0
- **Datenbank:** MariaDB 11.2.3
- **Provider:** Pomelo.EntityFrameworkCore.MySql 8.0.0
- **API-Dokumentation:** Swagger/OpenAPI

