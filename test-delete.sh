#!/bin/bash

echo "🧪 Testing Delete Functionality"
echo "================================"

# Test 1: Get all todos
echo "1. Getting all todos..."
curl -s http://localhost:5206/api/todo | jq .

# Test 2: Delete a specific todo
echo -e "\n2. Deleting todo with ID 2..."
curl -X DELETE http://localhost:5206/api/todo/2 -v

# Test 3: Verify the todo was deleted
echo -e "\n3. Verifying todo was deleted..."
curl -s http://localhost:5206/api/todo | jq .

# Test 4: Try to delete non-existent todo
echo -e "\n4. Testing delete with invalid ID..."
curl -X DELETE http://localhost:5206/api/todo/999 -v

# Test 5: Create a new todo and then delete it
echo -e "\n5. Creating a new todo..."
NEW_TODO=$(curl -s -X POST http://localhost:5206/api/todo \
  -H "Content-Type: application/json" \
  -d '{"title": "Test Todo for Delete", "isCompleted": false}' | jq -r '.id')

echo "Created todo with ID: $NEW_TODO"

echo -e "\n6. Deleting the newly created todo..."
curl -X DELETE http://localhost:5206/api/todo/$NEW_TODO -v

echo -e "\n7. Final verification..."
curl -s http://localhost:5206/api/todo | jq .

echo -e "\n✅ Delete functionality test completed!" 