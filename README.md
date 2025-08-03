# TODO List Application

A full-stack TODO list application built with Angular frontend and .NET Web API backend.

## Features

- ✅ View all TODO items
- ✅ Add new TODO items
- ✅ Delete TODO items
- ✅ Toggle TODO completion status
- ✅ Modern, responsive UI
- ✅ Real-time updates
- ✅ Error handling and loading states
- ✅ Comprehensive unit tests

## Technology Stack

### Frontend
- **Angular 19** - Latest version with standalone components
- **TypeScript** - Type-safe development
- **CSS3** - Modern styling with responsive design
- **Karma/Jasmine** - Unit testing framework

### Backend
- **.NET 8 Web API** - Latest version
- **C#** - Server-side logic
- **In-memory data storage** - No database setup required
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework

## Project Structure

```
todoList/
├── frontend/
│   └── todo-app/          # Angular application
│       ├── src/
│       │   ├── app/
│       │   │   ├── components/
│       │   │   │   └── todo/     # TODO component
│       │   │   │       ├── todo.component.ts
│       │   │   │       ├── todo.component.html
│       │   │   │       ├── todo.component.css
│       │   │   │       └── todo.component.spec.ts  # Component tests
│       │   │   └── services/
│       │   │       ├── todo.service.ts  # API service
│       │   │       └── todo.service.spec.ts  # Service tests
│       │   └── ...
│       └── ...
└── backend/
    └── TodoApi/           # .NET Web API
        ├── Controllers/
        │   └── TodoController.cs
        ├── Models/
        │   └── TodoItem.cs
        ├── Services/
        │   └── TodoService.cs
        ├── Tests/
        │   ├── TodoServiceTests.cs  # Service tests
        │   └── TodoControllerTests.cs  # Controller tests
        └── ...
```

## Prerequisites

- **Node.js** (v20.19+ or v22.12+)
- **.NET 8 SDK**
- **npm** (comes with Node.js)

## Setup Instructions

### 1. Clone and Navigate
```bash
cd todoList
```

### 2. Backend Setup
```bash
cd backend/TodoApi
dotnet restore
dotnet run --urls "http://localhost:5000"
```

The backend will start on `http://localhost:5000`

### 3. Frontend Setup
```bash
# In a new terminal
cd frontend/todo-app
npm install
npm start
```

The frontend will start on `http://localhost:4200`

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todo` | Get all TODO items |
| GET | `/api/todo/{id}` | Get specific TODO item |
| POST | `/api/todo` | Create new TODO item |
| PUT | `/api/todo/{id}` | Update TODO item |
| DELETE | `/api/todo/{id}` | Delete TODO item |
| PATCH | `/api/todo/{id}/toggle` | Toggle completion status |

## Usage

1. **View Todos**: All TODO items are displayed in a clean list
2. **Add Todo**: Type in the input field and press Enter or click "Add"
3. **Complete Todo**: Click the circle button next to any todo to mark it as complete
4. **Delete Todo**: Click the trash icon to remove a todo item

## Testing

### Backend Testing (xUnit)

The backend includes comprehensive unit tests for both the service and controller layers:

#### Running Backend Tests
```bash
cd backend/TodoApi
dotnet test
```

#### Test Coverage
- **TodoServiceTests**: 12 tests covering all CRUD operations
  - GetAll functionality
  - GetById with valid and invalid IDs
  - Create with validation
  - Update with valid and invalid IDs
  - Delete with valid and invalid IDs
  - ID increment logic
  - Data ordering

- **TodoControllerTests**: 11 tests covering HTTP responses
  - All HTTP methods (GET, POST, PUT, DELETE, PATCH)
  - Success and error scenarios
  - Model validation
  - HTTP status codes
  - Response types

### Frontend Testing (Karma/Jasmine)

The frontend includes comprehensive unit tests for components and services:

#### Running Frontend Tests
```bash
cd frontend/todo-app
npm test
```

#### Test Coverage
- **TodoService Tests**: 8 tests covering HTTP operations
  - All API methods (getTodos, getTodo, createTodo, updateTodo, deleteTodo, toggleTodo)
  - HTTP request validation
  - Error handling scenarios
  - Response type validation

- **TodoComponent Tests**: 17 tests covering component functionality
  - Component initialization
  - Data loading with success and error scenarios
  - Add todo functionality with validation
  - Toggle todo completion
  - Delete todo functionality
  - Keyboard event handling
  - Component state management
  - Loading states
  - Error handling

- **AppComponent Tests**: 4 tests covering app structure
  - Component creation
  - Title validation
  - Child component rendering
  - CSS class validation

#### Test Statistics
- **Backend**: 23 tests (all passing)
- **Frontend**: 29 tests (comprehensive coverage)
- **Total**: 52 unit tests

## Development

### Backend Development
- The backend uses in-memory storage, so data will reset when the server restarts
- Sample data is automatically loaded when the service starts
- CORS is configured to allow requests from `http://localhost:4200`
- All business logic is thoroughly tested with unit tests

### Frontend Development
- Built with Angular 19 standalone components
- Uses Angular's HttpClient for API communication
- Responsive design that works on mobile and desktop
- Error handling and loading states included
- All components and services are unit tested

## Troubleshooting

### Common Issues

1. **Port conflicts**: If ports 5000 or 4200 are in use, you can change them:
   - Backend: `dotnet run --urls "http://localhost:5001"`
   - Frontend: Update the API URL in `todo.service.ts`

2. **CORS errors**: Ensure the backend is running and CORS is properly configured

3. **Node.js version**: Make sure you have the correct Node.js version installed

4. **Test failures**: 
   - Backend: Ensure all dependencies are restored with `dotnet restore`
   - Frontend: Clear node_modules and reinstall with `npm install`

## Architecture

### Backend Architecture
- **Controllers**: Handle HTTP requests and responses
- **Services**: Business logic and data management
- **Models**: Data structures and validation
- **Dependency Injection**: Services are injected where needed
- **Testing**: xUnit with Moq for mocking

### Frontend Architecture
- **Components**: Reusable UI components
- **Services**: API communication and business logic
- **Standalone Components**: Modern Angular approach
- **TypeScript**: Type safety throughout the application
- **Testing**: Karma/Jasmine with Angular testing utilities

## Best Practices Implemented

- **Separation of Concerns**: Clear separation between frontend and backend
- **Type Safety**: TypeScript and C# provide compile-time type checking
- **Error Handling**: Comprehensive error handling on both frontend and backend
- **Responsive Design**: Mobile-first approach with CSS Grid and Flexbox
- **Modern APIs**: RESTful API design with proper HTTP status codes
- **Clean Code**: Well-structured, readable code with meaningful names
- **Testing**: Comprehensive unit test coverage for all critical functionality
- **Test-Driven Development**: Tests written before or alongside implementation
- **Mocking**: Proper use of mocks for isolated unit testing
- **Async Testing**: Proper handling of asynchronous operations in tests 