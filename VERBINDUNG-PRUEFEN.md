# Datenbank-Verbindung prüfen

## Methoden zur Überprüfung

### Methode 1: Railway Logs prüfen (Schnellste Methode)

1. Gehe zu https://railway.app/dashboard
2. Klicke auf dein **Web Service** (nicht die Datenbank)
3. Klicke auf **"Deployments"** Tab
4. Klicke auf das neueste Deployment
5. Klicke auf **"View Logs"**

**Was du sehen solltest:**

✅ **Erfolgreiche Verbindung:**
```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (XXms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE WHEN COUNT(*) = 0 THEN FALSE ELSE TRUE END
      FROM information_schema.tables
      WHERE table_type = 'BASE TABLE' AND table_schema = 'public'
```

✅ **Tabellen werden erstellt:**
```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (XXms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Artikel" (...)
```

❌ **Fehler bei Verbindung:**
```
fail: Microsoft.EntityFrameworkCore.Database.Connection[20004]
      An error occurred using the connection to database...
      Npgsql.NpgsqlException: Connection refused
```

---

### Methode 2: Swagger UI testen

1. Öffne die Swagger UI: `https://polygon-api.yourdomain.com/swagger`
   Oder Railway URL: `https://<deine-url>.up.railway.app/swagger`

2. Teste einen Endpunkt, z.B.:
   - `POST /Artikel` - Erstelle einen neuen Artikel
   - `POST /Kreditor` - Erstelle einen neuen Kreditor

3. **Erfolgreich:** Du erhältst `200 OK` mit einer Erfolgsmeldung
4. **Fehler:** Du erhältst `500 Internal Server Error` mit Datenbank-Fehlermeldung

**Beispiel Request:**
```json
POST /Artikel
{
  "articleNo": "TEST-001",
  "description": "Test Artikel",
  "type": 0
}
```

**Erfolgreiche Antwort:**
```json
{
  "status": "success",
  "message": "Daten erfolgreich gespeichert."
}
```

---

### Methode 3: API-Endpunkt direkt testen

**Mit curl:**
```bash
# Test: Artikel erstellen
curl -X POST https://polygon-api.yourdomain.com/Artikel \
  -H "Content-Type: application/json" \
  -d '{
    "articleNo": "TEST-001",
    "description": "Test Artikel",
    "type": 0
  }'
```

**Erfolgreich:** `{"status":"success","message":"Daten erfolgreich gespeichert."}`
**Fehler:** `{"status":"error","message":"..."}`

---

### Methode 4: Railway Environment Variables prüfen

1. Im Railway Dashboard: Klicke auf dein **Web Service**
2. Gehe zu **"Variables"** Tab
3. Prüfe ob diese Variablen vorhanden sind:

✅ **Muss vorhanden sein:**
- `DATABASE_URL` - PostgreSQL Connection String
- `POSTGRES_URL` - Alternative PostgreSQL URL (manchmal)
- `PORT` - Port für den Service

**Beispiel `DATABASE_URL`:**
```
postgresql://postgres:password@containers-us-west-xxx.railway.app:5432/railway
```

---

### Methode 5: Health Check Endpunkt (falls vorhanden)

Falls du einen Health Check hast:
```bash
curl https://polygon-api.yourdomain.com/health
```

Oder prüfe die Swagger JSON:
```bash
curl https://polygon-api.yourdomain.com/swagger/v1/swagger.json
```

Wenn das funktioniert, läuft der Service. Dann teste einen Datenbank-Endpunkt.

---

### Methode 6: Datenbank direkt prüfen (Railway)

1. Im Railway Dashboard: Klicke auf deine **PostgreSQL-Datenbank**
2. Klicke auf **"Query"** Tab
3. Führe eine SQL-Query aus:

```sql
-- Prüfe ob Tabellen existieren
SELECT table_name 
FROM information_schema.tables 
WHERE table_schema = 'public';
```

**Erfolgreich:** Du siehst Tabellen wie `Artikel`, `Kreditor`, `Projekt`, etc.
**Leer:** Keine Tabellen = Datenbank nicht verbunden oder Migration nicht ausgeführt

---

## Häufige Probleme und Lösungen

### Problem: "Connection refused" oder "Database does not exist"

**Lösung:**
1. Prüfe ob die Datenbank läuft (Railway Dashboard → Database → Status)
2. Prüfe ob `DATABASE_URL` im Web Service gesetzt ist
3. Stelle sicher, dass beide Services im gleichen Railway-Projekt sind

### Problem: "Table does not exist"

**Lösung:**
- Die Datenbank ist verbunden, aber Tabellen wurden nicht erstellt
- Prüfe die Logs - sollte `CREATE TABLE` Befehle zeigen
- Falls nicht: Service neu starten (Railway → Service → Restart)

### Problem: Swagger lädt, aber API-Endpunkte schlagen fehl

**Lösung:**
- Service läuft, aber Datenbank-Verbindung fehlt
- Prüfe Environment Variables
- Prüfe Logs für spezifische Fehlermeldungen

---

## Schnelltest-Checkliste

- [ ] Railway Logs zeigen keine Datenbank-Fehler
- [ ] `DATABASE_URL` ist in Environment Variables gesetzt
- [ ] Swagger UI ist erreichbar
- [ ] Ein POST-Request zu `/Artikel` gibt `200 OK`
- [ ] Datenbank Query zeigt Tabellen

Wenn alle Punkte ✅ sind → **Datenbank ist verbunden!**

---

## Debugging-Befehle

**In Railway Logs nach Fehlern suchen:**
- Suche nach: `error`, `fail`, `exception`
- Suche nach: `Npgsql` (PostgreSQL Driver)
- Suche nach: `Database.Connection`

**Service neu starten:**
1. Railway Dashboard → Service
2. Klicke auf **"..."** (drei Punkte)
3. Klicke auf **"Restart"**

