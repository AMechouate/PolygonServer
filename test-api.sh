#!/bin/bash

# PolygonServer API Test Script
# Usage: ./test-api.sh

API_BASE="https://polygonserver.onrender.com"

echo "🚀 PolygonServer API Testing"
echo "=============================="
echo ""

# Colors
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Test function
test_endpoint() {
    local name=$1
    local endpoint=$2
    local json=$3
    
    echo -e "${YELLOW}Testing: $name${NC}"
    echo "Endpoint: POST $endpoint"
    
    response=$(curl -s -w "\n%{http_code}" -X POST "$API_BASE$endpoint" \
        -H "Content-Type: application/json" \
        -d "$json")
    
    http_code=$(echo "$response" | tail -n1)
    body=$(echo "$response" | sed '$d')
    
    if [ "$http_code" -eq 200 ]; then
        echo -e "${GREEN}✅ Success (HTTP $http_code)${NC}"
        echo "Response: $body"
    elif [ "$http_code" -eq 400 ]; then
        echo -e "${YELLOW}⚠️  Bad Request (HTTP $http_code) - Möglicherweise Duplikat${NC}"
        echo "Response: $body"
    else
        echo -e "${RED}❌ Error (HTTP $http_code)${NC}"
        echo "Response: $body"
    fi
    echo ""
}

# Test 1: Artikel erstellen
echo "📦 Test 1: Artikel erstellen"
test_endpoint "Artikel" "/Artikel" '{
  "articleNo": "TEST-'$(date +%s)'",
  "description": "Test Artikel",
  "baseUnit": "EIMER",
  "type": 0,
  "creditorNo": "712183",
  "blocked": false
}'

# Test 2: Projekt erstellen
echo "📋 Test 2: Projekt erstellen"
test_endpoint "Projekt" "/Projekt" '{
  "projectNo": "TEST-PROJ-'$(date +%s)'",
  "projectDesc": "Test Projekt",
  "deliveryToName": "Test Name",
  "deliveryAddress": "Test Adresse",
  "deliveryToCity": "Test Stadt",
  "deliveryToPostalCode": "12345",
  "respUnitCode": "AB"
}'

# Test 3: Mitarbeiter erstellen
echo "👤 Test 3: Mitarbeiter erstellen"
test_endpoint "Mitarbeiter" "/Mitarbeiter" '{
  "employeeNo": "TEST-EMP-'$(date +%s)'",
  "firstName": "Test",
  "lastName": "Mitarbeiter",
  "sellerBuyerCode": "TEST",
  "userId": "TESTUSER"
}'

# Test 4: Kreditor erstellen
echo "🏢 Test 4: Kreditor erstellen"
test_endpoint "Kreditor" "/Kreditor" '{
  "creditorNo": "TEST-CRED-'$(date +%s)'",
  "name": "Test Kreditor GmbH",
  "address": "Test Straße 1",
  "city": "Test Stadt",
  "postalCode": "12345"
}'

# Test 5: Lagerort erstellen
echo "📍 Test 5: Lagerort erstellen"
test_endpoint "Lagerort" "/Lagerort" '{
  "storageLocCode": "TEST-LOC-'$(date +%s)'",
  "name": "Test Lagerort",
  "address": "Test Adresse",
  "city": "Test Stadt",
  "postalCode": "12345",
  "respUnitCode": "AB",
  "userId": "TESTUSER"
}'

# Test 6: Duplikat-Test (sollte fehlschlagen)
echo "🔄 Test 6: Duplikat-Test (sollte fehlschlagen)"
test_endpoint "Artikel Duplikat" "/Artikel" '{
  "articleNo": "DUPLICATE-TEST",
  "description": "Duplikat Test"
}'

# Versuche nochmal (sollte 400 zurückgeben)
test_endpoint "Artikel Duplikat (2. Versuch)" "/Artikel" '{
  "articleNo": "DUPLICATE-TEST",
  "description": "Duplikat Test"
}'

echo "=============================="
echo "✅ Testing abgeschlossen!"
echo ""
echo "💡 Tipp: Öffne Swagger UI für interaktive Tests:"
echo "   $API_BASE/swagger/index.html"

