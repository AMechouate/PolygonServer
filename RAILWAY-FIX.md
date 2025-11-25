# Railway PostgreSQL Verbindungsfehler - Lösung

## Problem
Die App versucht MySQL zu verwenden, obwohl Railway PostgreSQL bereitstellt.

## Lösung

### Schritt 1: Prüfe Environment Variables in Railway

1. Railway Dashboard → **Web Service** (nicht die Datenbank)
2. Klicke auf **"Variables"** Tab
3. **WICHTIG:** Prüfe ob `DATABASE_URL` vorhanden ist

### Schritt 2: Datenbank mit Web Service verbinden

Falls `DATABASE_URL` fehlt:

1. Railway Dashboard → **PostgreSQL Datenbank**
2. Klicke auf **"Connect"** Tab
3. Wähle dein **Web Service** aus der Liste
4. Railway setzt automatisch `DATABASE_URL`

**ODER:**

1. Railway Dashboard → **Web Service**
2. Klicke auf **"Variables"** Tab
3. Klicke auf **"New Variable"**
4. **Name:** `DATABASE_URL`
5. **Value:** Kopiere die **Internal Database URL** von der PostgreSQL-Datenbank
   - Gehe zu PostgreSQL → **"Connect"** Tab
   - Kopiere die **Internal Database URL** (beginnt mit `postgresql://`)

### Schritt 3: Service neu starten

1. Railway Dashboard → **Web Service**
2. Klicke auf **"..."** (drei Punkte oben rechts)
3. Klicke auf **"Restart"**

### Schritt 4: Prüfe Logs

Nach dem Restart sollten die Logs zeigen:
```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (XXms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Artikel" (...)
```

**Nicht mehr:**
```
Cannot find Unix Socket at /tmp/mysql.sock
```

## Alternative: Manuell DATABASE_URL setzen

Falls Railway die Variable nicht automatisch setzt:

1. Railway Dashboard → **PostgreSQL Datenbank**
2. Klicke auf **"Connect"** Tab
3. Kopiere die **Internal Database URL**
   - Format: `postgresql://postgres:password@host:port/database`
4. Railway Dashboard → **Web Service** → **"Variables"**
5. Klicke auf **"New Variable"**
6. **Name:** `DATABASE_URL`
7. **Value:** Füge die kopierte URL ein
8. Klicke auf **"Add"**
9. Service neu starten

## Prüfen ob es funktioniert

Nach dem Restart:
1. Prüfe Logs - sollte `CREATE TABLE` Befehle zeigen
2. Teste Swagger: `https://<deine-url>.up.railway.app/swagger`
3. Teste einen Endpunkt (z.B. `POST /Artikel`)

