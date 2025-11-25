# Railway Setup - PolygonServer mit PostgreSQL

## Übersicht

Railway bietet im Free Plan nur **PostgreSQL**, nicht MySQL. Das Projekt wurde angepasst, um **sowohl PostgreSQL als auch MySQL** zu unterstützen.

## Schritt-für-Schritt Anleitung

### Schritt 1: Railway Account erstellen
1. Gehe zu https://railway.app
2. Klicke auf "Start a New Project"
3. Melde dich mit **GitHub** an
4. Autorisiere Railway, auf deine Repositories zuzugreifen

### Schritt 2: Projekt von GitHub deployen
1. Im Railway Dashboard: Klicke auf **"New Project"**
2. Wähle **"Deploy from GitHub repo"**
3. Wähle das Repository: **`AMechouate/PolygonServer`**
4. Railway erkennt automatisch das .NET Projekt

### Schritt 3: PostgreSQL Datenbank hinzufügen
1. Im Railway Dashboard: Klicke auf **"New"** → **"Database"** → **"Add PostgreSQL"**
2. Railway erstellt automatisch eine PostgreSQL-Datenbank
3. **WICHTIG:** Die Datenbank wird automatisch mit dem Web Service verbunden

### Schritt 4: Environment Variables prüfen
Railway setzt automatisch diese Environment Variables:
- `DATABASE_URL` - PostgreSQL Connection String
- `POSTGRES_URL` - Alternative PostgreSQL URL
- `PORT` - Port für den Web Service

**Du musst nichts manuell setzen!** Railway macht das automatisch.

### Schritt 5: Service konfigurieren (optional)
Im Railway Dashboard → Service Settings:

- **Root Directory:** `PolygonApi` (falls nicht automatisch erkannt)
- **Build Command:** (automatisch)
- **Start Command:** (automatisch)

### Schritt 6: Deploy
1. Railway deployt automatisch bei jedem Push zu `main`
2. Oder klicke auf **"Deploy"** → **"Deploy Now"**
3. Warte 2-3 Minuten für den Build

### Schritt 7: Domain/URL kopieren
1. Nach dem Deploy: Klicke auf **"Settings"** → **"Generate Domain"**
2. Kopiere die URL (z.B. `polygon-api-production.up.railway.app`)

### Schritt 8: Cloudflare DNS konfigurieren
1. Gehe zu https://dash.cloudflare.com
2. Wähle deine Domain
3. Gehe zu **"DNS"** → **"Records"**
4. Klicke auf **"Add record"**
5. Konfiguration:
   - **Type:** `CNAME`
   - **Name:** `polygon-api` (oder `api`)
   - **Target:** `<railway-url>.up.railway.app` (von Schritt 7)
   - **Proxy status:** ✅ **Proxied** (orange Wolke aktiviert)
   - **TTL:** Auto
6. Klicke auf **"Save"**

### Schritt 9: Testen
Warte 1-2 Minuten für DNS-Propagation, dann:

1. **Swagger UI:** `https://polygon-api.yourdomain.com/swagger`
2. **API Health:** `https://polygon-api.yourdomain.com/swagger/v1/swagger.json`

## ✅ Fertig!

Deine API ist jetzt:
- ✅ Über HTTPS erreichbar (automatisch von Cloudflare)
- ✅ Mit Swagger UI unter `/swagger`
- ✅ Mit PostgreSQL-Datenbank verbunden (automatisch von Railway)
- ✅ Automatisch deployed bei jedem Git Push

## Automatische Datenbank-Erstellung

Die Datenbank wird automatisch erstellt durch `EnsureCreated()` in `Program.cs` beim ersten Start.

## PostgreSQL vs MySQL

Das Projekt unterstützt jetzt **beide Datenbanken**:
- **PostgreSQL:** Wird automatisch verwendet, wenn `DATABASE_URL` oder `POSTGRES_URL` gesetzt ist (Railway)
- **MySQL/MariaDB:** Wird verwendet für lokale Entwicklung

## Wichtige URLs

- **Railway Dashboard:** https://railway.app/dashboard
- **API URL:** `https://polygon-api.yourdomain.com`
- **Swagger:** `https://polygon-api.yourdomain.com/swagger`

## Kosten

- **Railway Free Tier:** 
  - $5 kostenloses Guthaben/Monat
  - Genug für einen Web Service + PostgreSQL
  - Nach Free Tier: Pay-as-you-go

- **Cloudflare:** 
  - DNS: Kostenlos
  - HTTPS: Kostenlos
  - CDN: Kostenlos

## Troubleshooting

### Build schlägt fehl
- Prüfe ob `Root Directory: PolygonApi` gesetzt ist
- Prüfe die Logs im Railway Dashboard → **"Deployments"** → **"View Logs"**

### Datenbank-Verbindungsfehler
- Railway setzt automatisch `DATABASE_URL` - prüfe in **"Variables"** Tab
- Stelle sicher, dass die PostgreSQL-Datenbank läuft
- Prüfe die Logs für Fehlermeldungen

### Swagger nicht erreichbar
- Warte 2-3 Minuten nach dem Deploy
- Prüfe ob die URL korrekt ist: `/swagger`
- Prüfe die Railway Logs

### Port-Fehler
- Railway setzt automatisch die `PORT` Variable
- Die App verwendet automatisch `$PORT` von Railway

## Nächste Schritte

1. ✅ Code ist bereits auf GitHub
2. ✅ Projekt unterstützt PostgreSQL
3. → Folge den Schritten oben für Railway Setup
4. → Konfiguriere Cloudflare DNS
5. → Teste die API!

