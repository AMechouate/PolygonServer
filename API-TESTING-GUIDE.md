# PolygonServer API Testing Guide

## 🎯 Übersicht

Diese Anleitung zeigt dir, wie du alle API-Endpunkte testen kannst.

**API Base URL:** `https://polygonserver.onrender.com`

**Swagger UI:** `https://polygonserver.onrender.com/swagger/index.html`

---

## 📋 Verfügbare Endpunkte

### 1. Artikel (Articles)
- **POST** `/Artikel` - Neuen Artikel erstellen

### 2. Projekt (Projects)
- **POST** `/Projekt` - Neues Projekt erstellen

### 3. Mitarbeiter (Employees)
- **POST** `/Mitarbeiter` - Neuen Mitarbeiter erstellen

### 4. Kreditor (Creditors)
- **POST** `/Kreditor` - Neuen Kreditor erstellen

### 5. Lagerort (Storage Locations)
- **POST** `/Lagerort` - Neuen Lagerort erstellen

### 6. Artikeleinheit (Article Units)
- **POST** `/Artikeleinheit` - Neue Artikeleinheit erstellen

### 7. Artikelreferenz (Article References)
- **POST** `/Artikelreferenz` - Neue Artikelreferenz erstellen

### 8. Artikelvariante (Article Variants)
- **POST** `/Artikelvariante` - Neue Artikelvariante erstellen

### 9. Zustaendigkeitseinheitencod (Responsibility Unit Codes)
- **POST** `/Zustaendigkeitseinheitencod` - Neuen Code erstellen

### 10. ZuordnungVerkaeuferZustaendigkeitseinheit (Seller-Unit Assignment)
- **POST** `/ZuordnungVerkaeuferZustaendigkeitseinheit` - Neue Zuordnung erstellen

---

## 🚀 Test-Methoden

### Methode 1: Swagger UI (Einfachste Methode) ⭐

1. Öffne: `https://polygonserver.onrender.com/swagger/index.html`
2. Wähle einen Endpunkt (z.B. `POST /Artikel`)
3. Klicke auf "Try it out"
4. Fülle die JSON-Daten ein
5. Klicke auf "Execute"
6. Sieh dir die Antwort an

**Vorteile:**
- ✅ Keine Installation nötig
- ✅ Interaktive UI
- ✅ Automatische Validierung
- ✅ Siehst alle verfügbaren Endpunkte

---

### Methode 2: curl (Terminal/Command Line)

#### Artikel erstellen
```bash
curl -X POST "https://polygonserver.onrender.com/Artikel" \
  -H "Content-Type: application/json" \
  -d '{
    "articleNo": "19500086",
    "description": "Sopro Design Fuge Flex, 1-10mm, DF 10, 5kg",
    "baseUnit": "EIMER",
    "type": 0,
    "creditorNo": "712183",
    "creditorArticleNo": "1338666 - 1481770",
    "blocked": false,
    "blockReason": "",
    "purchaseUnitCode": "EIMER",
    "artCatCode": "VERBRAUCHSMATERIAL",
    "creditorDescr": "Sopro Design Fuge Flex 1-10mm DF 10 5kg",
    "creditorStdVariant": ""
  }'
```

#### Projekt erstellen
```bash
curl -X POST "https://polygonserver.onrender.com/Projekt" \
  -H "Content-Type: application/json" \
  -d '{
    "projectNo": "AB-MA-25002959",
    "projectDesc": "GDV, Wasserschaden Erlenbach, Stolbinger",
    "deliveryToName": "Berta Kramer",
    "deliveryAddress": "Am Stadtpark 8",
    "deliveryToCity": "Erlenbach a. Main",
    "deliveryToPostalCode": "63906",
    "damageAcceptaceNo": "1440271",
    "respUnitCode": "AB"
  }'
```

#### Mitarbeiter erstellen
```bash
curl -X POST "https://polygonserver.onrender.com/Mitarbeiter" \
  -H "Content-Type: application/json" \
  -d '{
    "employeeNo": "1324",
    "firstName": "Michael",
    "lastName": "Muster",
    "sellerBuyerCode": "MMUSTER",
    "userId": "VATROMMUSTER",
    "respUnit": ""
  }'
```

#### Kreditor erstellen
```bash
curl -X POST "https://polygonserver.onrender.com/Kreditor" \
  -H "Content-Type: application/json" \
  -d '{
    "creditorNo": "712183",
    "name": "WaWa AG Baustoffe",
    "name2": "",
    "address": "Grundstr. 2",
    "address2": "",
    "city": "Wattenscheid",
    "postalCode": "63868"
  }'
```

#### Lagerort erstellen
```bash
curl -X POST "https://polygonserver.onrender.com/Lagerort" \
  -H "Content-Type: application/json" \
  -d '{
    "storageLocCode": "LAGER AB L",
    "name": "POLYGON Deutschland GmbH",
    "name2": "",
    "address": "Duckstraße 2",
    "address2": "Aschaffenburg",
    "city": "Kleinostheim",
    "postalCode": "63801",
    "respUnitCode": "AB",
    "userId": "VATROMMUSTER"
  }'
```

---

### Methode 3: JavaScript/TypeScript (fetch API)

```javascript
const API_BASE_URL = 'https://polygonserver.onrender.com';

// Artikel erstellen
async function createArtikel() {
  const response = await fetch(`${API_BASE_URL}/Artikel`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      articleNo: '19500086',
      description: 'Sopro Design Fuge Flex, 1-10mm, DF 10, 5kg',
      baseUnit: 'EIMER',
      type: 0,
      creditorNo: '712183',
      creditorArticleNo: '1338666 - 1481770',
      blocked: false,
      blockReason: '',
      purchaseUnitCode: 'EIMER',
      artCatCode: 'VERBRAUCHSMATERIAL',
      creditorDescr: 'Sopro Design Fuge Flex 1-10mm DF 10 5kg',
      creditorStdVariant: ''
    })
  });

  const data = await response.json();
  console.log('Erfolg:', data);
}

// Projekt erstellen
async function createProjekt() {
  const response = await fetch(`${API_BASE_URL}/Projekt`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      projectNo: 'AB-MA-25002959',
      projectDesc: 'GDV, Wasserschaden Erlenbach, Stolbinger',
      deliveryToName: 'Berta Kramer',
      deliveryAddress: 'Am Stadtpark 8',
      deliveryToCity: 'Erlenbach a. Main',
      deliveryToPostalCode: '63906',
      damageAcceptaceNo: '1440271',
      respUnitCode: 'AB'
    })
  });

  const data = await response.json();
  console.log('Erfolg:', data);
}

// Testen
createArtikel();
createProjekt();
```

---

### Methode 4: Postman/Insomnia

1. **Neue Request erstellen**
   - Method: `POST`
   - URL: `https://polygonserver.onrender.com/Artikel`

2. **Headers setzen**
   - Key: `Content-Type`
   - Value: `application/json`

3. **Body (JSON)**
   ```json
   {
     "articleNo": "19500086",
     "description": "Sopro Design Fuge Flex, 1-10mm, DF 10, 5kg",
     "baseUnit": "EIMER",
     "type": 0,
     "creditorNo": "712183",
     "creditorArticleNo": "1338666 - 1481770",
     "blocked": false,
     "blockReason": "",
     "purchaseUnitCode": "EIMER",
     "artCatCode": "VERBRAUCHSMATERIAL",
     "creditorDescr": "Sopro Design Fuge Flex 1-10mm DF 10 5kg",
     "creditorStdVariant": ""
   }
   ```

4. **Send klicken**

---

## ✅ Erfolgreiche Antwort

```json
{
  "status": "success",
  "message": "Daten erfolgreich gespeichert."
}
```

**HTTP Status:** `200 OK`

---

## ❌ Fehlerantworten

### Duplikat-Fehler (400 Bad Request)
```json
{
  "status": "error",
  "message": "Artikel mit dieser Nummer existiert bereits.",
  "details": {
    "field": "articleNo",
    "issue": "Nummer bereits vorhanden"
  }
}
```

### Validierungsfehler (400 Bad Request)
```json
{
  "status": "error",
  "message": "Validierungsfehler",
  "details": {
    "articleNo": ["Das Feld articleNo ist erforderlich."]
  }
}
```

### Serverfehler (500 Internal Server Error)
```json
{
  "status": "error",
  "message": "Interner Serverfehler",
  "details": {
    "error": "Fehlermeldung"
  }
}
```

---

## 🧪 Test-Szenarien

### Szenario 1: Erfolgreiche Erstellung
1. ✅ Artikel mit gültigen Daten erstellen
2. ✅ Erwarte: `200 OK` mit `"status": "success"`

### Szenario 2: Duplikat-Prüfung
1. ✅ Gleichen Artikel zweimal erstellen
2. ✅ Erwarte: `400 Bad Request` mit Duplikat-Fehler

### Szenario 3: Validierung
1. ✅ Artikel ohne `articleNo` erstellen
2. ✅ Erwarte: `400 Bad Request` mit Validierungsfehler

### Szenario 4: Alle Endpunkte testen
1. ✅ Artikel erstellen
2. ✅ Projekt erstellen
3. ✅ Mitarbeiter erstellen
4. ✅ Kreditor erstellen
5. ✅ Lagerort erstellen
6. ✅ Weitere Endpunkte testen

---

## 📝 Test-Checkliste

- [ ] Swagger UI öffnen und Endpunkte ansehen
- [ ] Artikel erstellen (erfolgreich)
- [ ] Artikel mit gleicher Nummer erstellen (Duplikat-Fehler)
- [ ] Artikel ohne `articleNo` erstellen (Validierungsfehler)
- [ ] Projekt erstellen
- [ ] Mitarbeiter erstellen
- [ ] Kreditor erstellen
- [ ] Lagerort erstellen
- [ ] Alle anderen Endpunkte testen
- [ ] Verschiedene Test-Methoden ausprobieren (curl, JavaScript, Postman)

---

## 🔍 Debugging-Tipps

### Problem: CORS-Fehler
- ✅ API unterstützt CORS für alle Origins
- ✅ Prüfe ob `Content-Type: application/json` gesetzt ist

### Problem: 404 Not Found
- ✅ Prüfe die URL (Groß-/Kleinschreibung beachten!)
- ✅ Controller-Routen: `/Artikel`, `/Projekt`, etc.

### Problem: 500 Internal Server Error
- ✅ Prüfe die Render Logs
- ✅ Prüfe ob die Datenbank verbunden ist
- ✅ Prüfe ob alle Pflichtfelder gesetzt sind

### Problem: Timeout
- ✅ Render Free Tier kann beim ersten Request langsam sein (Cold Start)
- ✅ Warte 30-60 Sekunden und versuche es erneut

---

## 🎯 Nächste Schritte

Nach erfolgreichem Testing:

1. ✅ **Frontend integrieren**: React Native App mit API verbinden
2. ✅ **GET-Endpunkte hinzufügen**: Daten abrufen
3. ✅ **PUT/DELETE-Endpunkte hinzufügen**: Daten aktualisieren/löschen
4. ✅ **Authentication hinzufügen**: JWT Token-basierte Authentifizierung
5. ✅ **Pagination hinzufügen**: Für große Datenmengen

---

## 📚 Weitere Ressourcen

- **Swagger UI**: `https://polygonserver.onrender.com/swagger/index.html`
- **API Base URL**: `https://polygonserver.onrender.com`
- **GitHub Repository**: `https://github.com/AMechouate/PolygonServer`

---

**Viel Erfolg beim Testen! 🚀**

