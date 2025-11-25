# Railway - Manuelle DATABASE_URL Konfiguration

## Problem
Railway zeigt den Connection String, aber setzt ihn nicht automatisch als Environment Variable.

## Lösung: Manuell DATABASE_URL setzen

### Schritt 1: Connection String kopieren
Von der PostgreSQL-Datenbank:
```
postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a/polygondb_v80x
```

### Schritt 2: Port hinzufügen (falls fehlt)
Railway PostgreSQL verwendet Port **5432**. Falls der Port fehlt, füge ihn hinzu:

**Original:**
```
postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a/polygondb_v80x
```

**Mit Port:**
```
postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a:5432/polygondb_v80x
```

### Schritt 3: Environment Variable im Web Service setzen

1. Railway Dashboard → **Web Service** (nicht die Datenbank!)
2. Klicke auf **"Variables"** Tab
3. Klicke auf **"New Variable"** oder **"Raw Editor"**
4. Füge hinzu:
   - **Name:** `DATABASE_URL`
   - **Value:** `postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a:5432/polygondb_v80x`
5. Klicke auf **"Add"** oder **"Save"**

### Schritt 4: Service neu starten

1. Railway Dashboard → **Web Service**
2. Klicke auf **"..."** (drei Punkte oben rechts)
3. Klicke auf **"Restart"**

### Schritt 5: Prüfe Logs

Nach dem Restart sollten die Logs zeigen:
```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (XXms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Artikel" (...)
```

## Alternative: Connection String Format

Falls der Port nicht funktioniert, versuche dieses Format:

```
postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a.frankfurt-postgres.render.com:5432/polygondb_v80x
```

Oder ohne Port (Railway fügt ihn manchmal automatisch hinzu):
```
postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a/polygondb_v80x
```

## Prüfen ob DATABASE_URL gesetzt ist

1. Railway Dashboard → **Web Service** → **"Variables"**
2. Suche nach `DATABASE_URL`
3. Sollte vorhanden sein mit deinem Connection String

## Troubleshooting

### "Connection refused"
- Prüfe ob der Port `:5432` hinzugefügt wurde
- Prüfe ob die Datenbank läuft (Railway Dashboard → PostgreSQL → Status)

### "Database does not exist"
- Der Connection String ist korrekt, aber die Datenbank wurde noch nicht erstellt
- Das ist OK - die App erstellt sie automatisch beim ersten Start

### "Authentication failed"
- Prüfe ob Username und Password korrekt sind
- Kopiere den Connection String nochmal von Railway

