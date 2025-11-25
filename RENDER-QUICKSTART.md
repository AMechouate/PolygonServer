# Render Quickstart - PolygonServer Deployment

## Schnellstart (5 Minuten)

### Schritt 1: Render Account erstellen
1. Gehe zu https://render.com
2. Klicke auf "Get Started for Free"
3. Melde dich mit **GitHub** an
4. Autorisiere Render, auf deine Repositories zuzugreifen

### Schritt 2: Web Service erstellen
1. Im Render Dashboard: Klicke auf **"New +"** → **"Web Service"**
2. Klicke auf **"Connect account"** neben GitHub (falls noch nicht verbunden)
3. Wähle das Repository: **`AMechouate/PolygonServer`**
4. Klicke auf **"Connect"**

### Schritt 3: Service konfigurieren
- **Name:** `polygon-api` (oder wie du möchtest)
- **Region:** Wähle die nächstgelegene Region (z.B. Frankfurt)
- **Branch:** `main`
- **Root Directory:** `PolygonApi` (wichtig!)
- **Environment:** `.NET Core`
- **Build Command:** `dotnet publish -c Release -o ./publish`
- **Start Command:** `dotnet ./publish/PolygonApi.dll`

### Schritt 4: Datenbank hinzufügen
1. Klicke auf **"New +"** → **"PostgreSQL"** (oder MySQL falls verfügbar)
2. **Name:** `polygon-db`
3. **Database Name:** `PolygonDb`
4. Klicke auf **"Create Database"**
5. **WICHTIG:** Kopiere die **Internal Database URL**

### Schritt 5: Environment Variables setzen
Zurück im Web Service → **"Environment"** Tab:

1. Klicke auf **"Add Environment Variable"**
2. Füge hinzu:
   ```
   ASPNETCORE_ENVIRONMENT = Production
   ```
3. Füge hinzu:
   ```
   ConnectionStrings__DefaultConnection = <Internal Database URL von Schritt 4>
   ```
   **Wichtig:** Ersetze `<Internal Database URL>` mit der kopierten URL

4. Klicke auf **"Save Changes"**

### Schritt 6: Deploy
1. Klicke auf **"Manual Deploy"** → **"Deploy latest commit"**
2. Warte 3-5 Minuten für den Build
3. Wenn fertig, kopiere die **URL** (z.B. `polygon-api-xxxx.onrender.com`)

### Schritt 7: Cloudflare DNS konfigurieren
1. Gehe zu https://dash.cloudflare.com
2. Wähle deine Domain
3. Gehe zu **"DNS"** → **"Records"**
4. Klicke auf **"Add record"**
5. Konfiguration:
   - **Type:** `CNAME`
   - **Name:** `polygon-api` (oder `api`)
   - **Target:** `<render-url>.onrender.com` (von Schritt 6)
   - **Proxy status:** ✅ **Proxied** (orange Wolke aktiviert)
   - **TTL:** Auto
6. Klicke auf **"Save"**

### Schritt 8: Testen
Warte 1-2 Minuten für DNS-Propagation, dann:

1. **Swagger UI:** `https://polygon-api.yourdomain.com/swagger`
2. **API Health:** `https://polygon-api.yourdomain.com/swagger/v1/swagger.json`

## ✅ Fertig!

Deine API ist jetzt:
- ✅ Über HTTPS erreichbar (automatisch von Cloudflare)
- ✅ Mit Swagger UI unter `/swagger`
- ✅ Mit Datenbank verbunden
- ✅ Automatisch deployed bei jedem Git Push

## Wichtige URLs

- **Render Dashboard:** https://dashboard.render.com
- **API URL:** `https://polygon-api.yourdomain.com`
- **Swagger:** `https://polygon-api.yourdomain.com/swagger`

## Automatische Deployments

Render deployt automatisch bei jedem Push zu `main` Branch!

## Troubleshooting

### Build schlägt fehl
- Prüfe ob `Root Directory: PolygonApi` gesetzt ist
- Prüfe die Logs im Render Dashboard

### Datenbank-Verbindungsfehler
- Prüfe ob die Internal Database URL korrekt ist
- Stelle sicher, dass die Datenbank läuft
- Prüfe die Logs für Fehlermeldungen

### Swagger nicht erreichbar
- Warte 2-3 Minuten nach dem Deploy
- Prüfe ob die URL korrekt ist: `/swagger`
- Prüfe die Render Logs

## Kosten

- **Render Free Tier:** 
  - Web Service: 750 Stunden/Monat (kostenlos)
  - PostgreSQL: 90 Stunden/Monat (kostenlos)
  - Nach Free Tier: $7/Monat für Web Service

- **Cloudflare:** 
  - DNS: Kostenlos
  - HTTPS: Kostenlos
  - CDN: Kostenlos

