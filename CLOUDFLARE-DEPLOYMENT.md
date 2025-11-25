# Cloudflare Deployment für PolygonServer

## Übersicht

Da Cloudflare Pages/Workers .NET nicht direkt unterstützen, verwenden wir eine Kombination aus:
- **Railway** oder **Render** für das Backend (unterstützt .NET und MariaDB)
- **Cloudflare** für DNS und HTTPS (über Tunnel oder DNS)

## Option 1: Railway + Cloudflare Tunnel (Empfohlen)

### Schritt 1: Railway Setup

1. Gehe zu https://railway.app
2. Melde dich mit GitHub an
3. Klicke auf "New Project" → "Deploy from GitHub repo"
4. Wähle das Repository: `AMechouate/PolygonServer`
5. Railway erkennt automatisch das .NET Projekt

### Schritt 2: Datenbank auf Railway hinzufügen

1. Im Railway Dashboard: Klicke auf "New" → "Database" → "Add MySQL"
2. Railway erstellt automatisch eine MySQL/MariaDB Datenbank
3. Kopiere die Connection String aus den Environment Variables

### Schritt 3: Environment Variables in Railway setzen

Im Railway Dashboard → Variables:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:$PORT
ConnectionStrings__DefaultConnection=<Railway MySQL Connection String>
```

**Wichtig:** Ersetze `<Railway MySQL Connection String>` mit dem Connection String von Railway.

### Schritt 4: Railway Deployment konfigurieren

Erstelle eine `railway.json` Datei:

```json
{
  "$schema": "https://railway.app/railway.schema.json",
  "build": {
    "builder": "NIXPACKS"
  },
  "deploy": {
    "startCommand": "dotnet PolygonApi.dll",
    "restartPolicyType": "ON_FAILURE",
    "restartPolicyMaxRetries": 10
  }
}
```

### Schritt 5: Cloudflare Tunnel einrichten

1. Installiere `cloudflared`:
   ```bash
   # macOS
   brew install cloudflare/cloudflare/cloudflared
   ```

2. Authentifiziere dich:
   ```bash
   cloudflared tunnel login
   ```

3. Erstelle einen Tunnel:
   ```bash
   cloudflared tunnel create polygon-server
   ```

4. Erstelle eine Config-Datei `~/.cloudflared/config.yml`:
   ```yaml
   tunnel: <TUNNEL-ID>
   credentials-file: /Users/adammechouate/.cloudflared/<TUNNEL-ID>.json
   
   ingress:
     - hostname: polygon-api.yourdomain.com
       service: https://<railway-url>.railway.app
     - service: http_status:404
   ```

5. Route den Tunnel zu deiner Domain:
   ```bash
   cloudflared tunnel route dns polygon-server polygon-api.yourdomain.com
   ```

6. Starte den Tunnel:
   ```bash
   cloudflared tunnel run polygon-server
   ```

---

## Option 2: Render + Cloudflare DNS (Einfacher)

### Schritt 1: Render Setup

1. Gehe zu https://render.com
2. Melde dich mit GitHub an
3. Klicke auf "New" → "Web Service"
4. Verbinde das Repository: `AMechouate/PolygonServer`
5. Konfiguration:
   - **Environment:** `.NET Core`
   - **Build Command:** `dotnet publish -c Release -o ./publish`
   - **Start Command:** `dotnet ./publish/PolygonApi.dll`
   - **Port:** `5104` (oder automatisch)

### Schritt 2: Datenbank auf Render

1. Im Render Dashboard: Klicke auf "New" → "PostgreSQL" (oder MySQL wenn verfügbar)
2. Erstelle die Datenbank
3. Kopiere die Connection String

### Schritt 3: Environment Variables

In Render → Environment:

```
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=<Render Database Connection String>
```

### Schritt 4: Cloudflare DNS konfigurieren

1. Gehe zu https://dash.cloudflare.com
2. Wähle deine Domain
3. Gehe zu "DNS" → "Records"
4. Füge einen A-Record hinzu:
   - **Type:** CNAME
   - **Name:** `polygon-api` (oder `api`)
   - **Target:** `<render-url>.onrender.com`
   - **Proxy status:** Proxied (orange Wolke) ✅
   - **TTL:** Auto

5. HTTPS wird automatisch von Cloudflare bereitgestellt!

---

## Option 3: Fly.io (Direkt mit .NET Support)

### Schritt 1: Fly.io Setup

1. Installiere `flyctl`:
   ```bash
   curl -L https://fly.io/install.sh | sh
   ```

2. Login:
   ```bash
   fly auth login
   ```

3. Initialisiere das Projekt:
   ```bash
   cd /Users/adammechouate/Documents/PolygonServer/PolygonApi
   fly launch
   ```

4. Erstelle eine `fly.toml`:
   ```toml
   app = "polygon-server"
   primary_region = "fra"
   
   [build]
   
   [http_service]
     internal_port = 5104
     force_https = true
     auto_stop_machines = true
     auto_start_machines = true
     min_machines_running = 0
     processes = ["app"]
   
   [[services]]
     protocol = "tcp"
     internal_port = 5104
   
     [[services.ports]]
       port = 80
       handlers = ["http"]
       force_https = true
   
     [[services.ports]]
       port = 443
       handlers = ["tls", "http"]
   ```

5. Erstelle eine Datenbank:
   ```bash
   fly postgres create --name polygon-db
   fly postgres attach polygon-db
   ```

6. Deploy:
   ```bash
   fly deploy
   ```

### Schritt 2: Cloudflare DNS

1. Hole die Fly.io URL: `polygon-server.fly.dev`
2. In Cloudflare DNS:
   - **Type:** CNAME
   - **Name:** `polygon-api`
   - **Target:** `polygon-server.fly.dev`
   - **Proxy:** Proxied ✅

---

## Datenbank-Migration

Für alle Optionen: Die Datenbank wird automatisch erstellt durch `EnsureCreated()` in `Program.cs`.

Für Production sollte man Migrations verwenden:

```bash
# In Railway/Render/Fly.io Shell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Swagger öffentlich zugänglich machen

Swagger ist bereits konfiguriert und sollte unter:
- `https://polygon-api.yourdomain.com/swagger` erreichbar sein

Falls nicht, stelle sicher, dass in `Program.cs`:
```csharp
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Polygon API V1");
    c.RoutePrefix = "swagger";
});
```

---

## Sicherheit

### Environment Variables schützen

Stelle sicher, dass sensible Daten in Environment Variables sind:
- ✅ Database Connection Strings
- ✅ API Keys
- ✅ Secrets

### CORS konfigurieren

In `Program.cs` ist bereits CORS konfiguriert:
```csharp
app.UseCors("AllowAll");
```

Für Production sollte man spezifische Origins erlauben:
```csharp
options.AddPolicy("AllowSpecificOrigins", policy =>
{
    policy.WithOrigins("https://yourdomain.com")
          .AllowAnyMethod()
          .AllowAnyHeader();
});
```

---

## Empfohlene Lösung

**Für schnelles Setup:** Render + Cloudflare DNS (Option 2)
- ✅ Einfachste Konfiguration
- ✅ Automatisches HTTPS über Cloudflare
- ✅ GitHub Integration
- ✅ Kostenloser Plan verfügbar

**Für mehr Kontrolle:** Railway + Cloudflare Tunnel (Option 1)
- ✅ Mehr Flexibilität
- ✅ Bessere Performance
- ✅ Detaillierte Logs

---

## Nächste Schritte

1. Wähle eine Option (empfohlen: Render)
2. Folge den Schritten für die gewählte Option
3. Teste die API unter `https://polygon-api.yourdomain.com/swagger`
4. Teste die Endpunkte

## Troubleshooting

### API nicht erreichbar
- Prüfe ob der Service läuft (Railway/Render/Fly.io Dashboard)
- Prüfe die Logs im Dashboard
- Prüfe ob die Ports korrekt konfiguriert sind

### Datenbank-Verbindungsfehler
- Prüfe den Connection String
- Stelle sicher, dass die Datenbank läuft
- Prüfe ob Firewall-Regeln korrekt sind

### Swagger nicht erreichbar
- Prüfe ob Swagger in Production aktiviert ist
- Prüfe die Route: `/swagger`
- Prüfe die Logs für Fehler

