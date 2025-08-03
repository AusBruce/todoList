#!/bin/bash

echo "🧪 Testing Add Functionality"
echo "============================"

# Test 1: Get current todos
echo "1. Getting current todos..."
curl -s http://localhost:5206/api/todo | jq .

# Test 2: Add a new todo via API
echo -e "\n2. Adding new todo via API..."
NEW_TODO_RESPONSE=$(curl -s -X POST http://localhost:5206/api/todo \
  -H "Content-Type: application/json" \
  -d '{"title": "Test Todo from Script", "isCompleted": false}')

echo "Response: $NEW_TODO_RESPONSE"

# Test 3: Verify the todo was added
echo -e "\n3. Verifying todo was added..."
curl -s http://localhost:5206/api/todo | jq .

# Test 4: Test CORS by making a request from frontend perspective
echo -e "\n4. Testing CORS headers..."
curl -H "Origin: http://localhost:4201" \
     -H "Access-Control-Request-Method: POST" \
     -H "Access-Control-Request-Headers: Content-Type" \
     -X OPTIONS http://localhost:5206/api/todo -v

echo -e "\n✅ Add functionality test completed!" 