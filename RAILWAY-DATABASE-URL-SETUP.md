# Railway DATABASE_URL Setup - Schritt für Schritt

## Problem
Die App verwendet noch MySQL, obwohl PostgreSQL vorhanden ist. Das bedeutet: `DATABASE_URL` ist nicht gesetzt.

## Lösung: DATABASE_URL manuell setzen

### Schritt 1: Connection String kopieren

Von der PostgreSQL-Datenbank in Railway:
```
postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a/polygondb_v80x
```

### Schritt 2: Im Web Service setzen

**WICHTIG:** Setze die Variable im **Web Service**, nicht in der Datenbank!

1. Railway Dashboard → **Web Service** (klicke auf den Web Service, nicht die Datenbank!)
2. Klicke auf **"Variables"** Tab (oben im Service)
3. Prüfe ob `DATABASE_URL` bereits vorhanden ist:
   - ✅ Wenn vorhanden: Klicke darauf und bearbeite den Wert
   - ❌ Wenn nicht vorhanden: Klicke auf **"New Variable"**

4. Setze:
   - **Name:** `DATABASE_URL` (genau so, Großbuchstaben!)
   - **Value:** `postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a:5432/polygondb_v80x`
   - **WICHTIG:** Port `:5432` hinzufügen!

5. Klicke auf **"Add"** oder **"Save"**

### Schritt 3: Service neu starten

1. Railway Dashboard → **Web Service**
2. Klicke auf **"..."** (drei Punkte oben rechts)
3. Klicke auf **"Restart"**

### Schritt 4: Logs prüfen

Nach dem Restart sollten die Logs zeigen:

✅ **Erfolgreich:**
```
[DB] DATABASE_URL env var: SET
[DB] Database type detected: PostgreSQL
[DB] PostgreSQL detected. Host=dpg-d4itn1fgi27c739sge80-a, Port=5432, Database=polygondb_v80x, Username=polygondb_v80x_user
[DB] PostgreSQL connection string validated. Host=..., Database=..., Username=...
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (XXms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Artikel" (...)
```

❌ **Nicht gesetzt:**
```
[DB] DATABASE_URL env var: NOT SET
[DB] Database type detected: MySQL/MariaDB
```

## Alternative: Direktes Npgsql-Format

Falls das URL-Format nicht funktioniert, verwende direkt das Npgsql-Format:

1. Railway Dashboard → **Web Service** → **Variables**
2. Setze:
   - **Name:** `DATABASE_URL`
   - **Value:** 
   ```
   Host=dpg-d4itn1fgi27c739sge80-a;Port=5432;Database=polygondb_v80x;Username=polygondb_v80x_user;Password=rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk;SSL Mode=Require;Trust Server Certificate=true
   ```
3. **Add** → **Restart**

## Prüfen ob Variable gesetzt ist

1. Railway Dashboard → **Web Service** → **Variables**
2. Suche nach `DATABASE_URL`
3. Sollte vorhanden sein und den Connection String enthalten

## Häufige Fehler

### Variable in der falschen Stelle gesetzt
- ❌ In der **Datenbank** gesetzt → Funktioniert nicht!
- ✅ Im **Web Service** gesetzt → Funktioniert!

### Falscher Variablenname
- ❌ `database_url` (kleingeschrieben)
- ❌ `DATABASE_CONNECTION`
- ✅ `DATABASE_URL` (genau so!)

### Port fehlt
- ❌ `postgresql://...@host/db` (ohne Port)
- ✅ `postgresql://...@host:5432/db` (mit Port)

## Nach erfolgreichem Setup

Die API sollte jetzt:
- ✅ Mit PostgreSQL verbunden sein
- ✅ Tabellen automatisch erstellen
- ✅ Swagger unter `/swagger` verfügbar sein
- ✅ Endpunkte funktionieren

