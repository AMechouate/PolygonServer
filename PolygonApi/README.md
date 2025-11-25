# Polygon API

ASP.NET Core Web API für die Polygon-Server-Anwendung.

## Voraussetzungen

- .NET 8.0 SDK
- SQL Server (lokal oder Remote)

## Konfiguration

### Datenbankverbindung

Die Datenbankverbindung wird in `appsettings.json` konfiguriert:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PolygonDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"
  }
}
```

**Wichtig:** Passen Sie die Verbindungszeichenfolge an Ihre SQL Server-Instanz an.

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

## Antwort-Status-Codes

- `200 OK` - Daten erfolgreich gespeichert
- `400 Bad Request` - Validierungsfehler oder ungültige Eingaben
- `401 Unauthorized` - Unauthorisiert (Token fehlt oder ist ungültig)
- `500 Internal Server Error` - Interner Serverfehler

## Antwortformate

### Erfolgreiche Antwort (200)
```json
{
  "status": "success",
  "message": "Daten erfolgreich gespeichert."
}
```

### Fehlerantwort (400/500)
```json
{
  "status": "error",
  "message": "Fehler bei der Verarbeitung.",
  "details": {
    "field": "articleNo",
    "issue": "Nummer bereits vorhanden"
  }
}
```

## Ausführen der Anwendung

1. Stellen Sie sicher, dass SQL Server läuft und die Verbindungszeichenfolge korrekt ist.
2. Führen Sie die Anwendung aus:
   ```bash
   dotnet run
   ```
3. Die API ist unter `https://localhost:5001` oder `http://localhost:5000` erreichbar.
4. Swagger UI ist unter `https://localhost:5001/swagger` verfügbar.

## Datenbank-Migration

Die Datenbank wird automatisch erstellt, wenn die Anwendung startet (`EnsureCreated()`).

Für Produktionsumgebungen sollten Sie Migrations verwenden:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Entwicklung

- **Framework:** .NET 8.0
- **ORM:** Entity Framework Core 8.0
- **Datenbank:** SQL Server
- **API-Dokumentation:** Swagger/OpenAPI

