# Test Connection String für Railway

## Deine Datenbank-Daten:
- **Hostname:** `dpg-d4itn1fgi27c739sge80-a`
- **Port:** `5432`
- **Database:** `polygondb_v80x`
- **Username:** `polygondb_v80x_user`
- **Password:** `rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk`

## Korrekter Npgsql Connection String:

```
Host=dpg-d4itn1fgi27c739sge80-a;Port=5432;Database=polygondb_v80x;Username=polygondb_v80x_user;Password=rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk;SSL Mode=Require;Trust Server Certificate=true
```

## In Railway als Environment Variable setzen:

1. Railway Dashboard → **Web Service**
2. **Variables** Tab
3. **New Variable:**
   - **Name:** `DATABASE_URL`
   - **Value:** `postgresql://polygondb_v80x_user:rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk@dpg-d4itn1fgi27c739sge80-a:5432/polygondb_v80x`
4. **Add**

**ODER direkt als Npgsql Format:**

- **Name:** `DATABASE_URL`
- **Value:** `Host=dpg-d4itn1fgi27c739sge80-a;Port=5432;Database=polygondb_v80x;Username=polygondb_v80x_user;Password=rvWG1n0v0sd4ElAh3uWGBYWCE773DSVk;SSL Mode=Require;Trust Server Certificate=true`

Der Code konvertiert automatisch das `postgresql://` Format, aber falls das nicht funktioniert, kannst du direkt das Npgsql-Format verwenden.

