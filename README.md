# TODO List Application

A modern, full-stack TODO list application built with **Angular 19** frontend and **.NET 8 Web API** backend. Features a clean, responsive UI with comprehensive unit testing coverage.

## 🚀 Features

- ✅ **View all TODO items** with creation timestamps
- ✅ **Add new TODO items** with real-time updates
- ✅ **Delete TODO items** with confirmation
- ✅ **Toggle completion status** with visual feedback
- ✅ **Modern, responsive UI** that works on all devices
- ✅ **Real-time updates** without page refresh
- ✅ **Error handling** with user-friendly messages
- ✅ **Loading states** for better UX
- ✅ **Comprehensive unit tests** (52 total tests)
- ✅ **Type-safe development** with TypeScript and C#

## 🛠 Technology Stack

### Frontend
- **Angular 19** - Latest version with standalone components
- **TypeScript** - Type-safe development
- **CSS3** - Modern styling with responsive design
- **Karma/Jasmine** - Unit testing framework
- **HttpClient** - API communication

### Backend
- **.NET 8 Web API** - Latest version
- **C#** - Server-side logic
- **In-memory data storage** - No database setup required
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework
- **CORS** - Cross-origin resource sharing

## 📁 Project Structure

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
│       │   │   ├── services/
│       │   │   │   ├── todo.service.ts  # API service
│       │   │   │   └── todo.service.spec.ts  # Service tests
│       │   │   ├── app.component.ts
│       │   │   ├── app.component.html
│       │   │   ├── app.component.css
│       │   │   └── app.component.spec.ts
│       │   └── ...
│       └── ...
├── backend/
│   └── TodoApi/           # .NET Web API
│       ├── Controllers/
│       │   └── TodoController.cs
│       ├── Models/
│       │   └── TodoItem.cs
│       ├── Services/
│       │   └── TodoService.cs
│       ├── Tests/
│       │   ├── TodoServiceTests.cs  # Service tests
│       │   └── TodoControllerTests.cs  # Controller tests
│       └── Program.cs
├── test-add.sh            # Add functionality test script
├── test-delete.sh         # Delete functionality test script
└── README.md
```

## ⚙️ Prerequisites

- **Node.js** (v20.19+ or v22.12+)
- **.NET 8 SDK**
- **npm** (comes with Node.js)

## 🚀 Quick Start

### 1. Clone and Navigate
```bash
cd todoList
```

### 2. Backend Setup
```bash
cd backend/TodoApi
dotnet restore
dotnet run --urls "http://localhost:5206"
```

The backend will start on `http://localhost:5206`

### 3. Frontend Setup
```bash
# In a new terminal
cd frontend/todo-app
npm install
npm start
```

The frontend will start on `http://localhost:4200`

### 4. Access the Application
Open your browser and navigate to `http://localhost:4200`

## 📡 API Endpoints

| Method | Endpoint | Description | Status Code |
|--------|----------|-------------|-------------|
| GET | `/api/todo` | Get all TODO items | 200 OK |
| GET | `/api/todo/{id}` | Get specific TODO item | 200 OK / 404 Not Found |
| POST | `/api/todo` | Create new TODO item | 201 Created |
| PUT | `/api/todo/{id}` | Update TODO item | 200 OK / 404 Not Found |
| DELETE | `/api/todo/{id}` | Delete TODO item | 204 No Content / 404 Not Found |
| PATCH | `/api/todo/{id}/toggle` | Toggle completion status | 200 OK / 404 Not Found |

### API Data Model
```json
{
  "id": 1,
  "title": "frontend Angular",
  "isCompleted": false,
  "createdAt": "2025-08-03T09:10:11.155945Z"
}
```

## 🎯 Usage

1. **View Todos**: All TODO items are displayed in a clean, organized list
2. **Add Todo**: Type in the input field and press Enter or click "Add"
3. **Complete Todo**: Click the circle button next to any todo to mark it as complete
4. **Delete Todo**: Click the trash icon to remove a todo item
5. **Real-time Updates**: All changes are immediately reflected in the UI

## 🧪 Testing

### Backend Testing (xUnit)

The backend includes comprehensive unit tests for both the service and controller layers:

#### Running Backend Tests
```bash
cd backend/TodoApi
dotnet test
```

#### Test Coverage
- **TodoServiceTests**: 12 tests covering all CRUD operations
  - GetAll functionality with ordering
  - GetById with valid and invalid IDs
  - Create with validation and ID increment
  - Update with valid and invalid IDs
  - Delete with valid and invalid IDs
  - Data persistence (Singleton pattern)

- **TodoControllerTests**: 11 tests covering HTTP responses
  - All HTTP methods (GET, POST, PUT, DELETE, PATCH)
  - Success and error scenarios
  - Model validation
  - HTTP status codes
  - Response types and content

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

- **TodoComponent Tests**: 18 tests covering component functionality
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
- **Frontend**: 30 tests (all passing)
- **Total**: 53 unit tests with 100% pass rate

### End-to-End Testing

The project includes shell scripts for testing critical functionality:

#### Test Add Functionality
```bash
./test-add.sh
```

#### Test Delete Functionality
```bash
./test-delete.sh
```

## 🔧 Development

### Backend Development
- **In-memory storage**: Data persists during server runtime (Singleton pattern)
- **Sample data**: Automatically loaded when the service starts
- **CORS configuration**: Allows requests from `http://localhost:4200` and `http://localhost:4201`
- **Comprehensive testing**: All business logic is thoroughly tested
- **RESTful API**: Follows REST principles with proper HTTP status codes

### Frontend Development
- **Angular 19**: Latest version with standalone components
- **HttpClient**: For API communication with proper error handling
- **Responsive design**: Works seamlessly on mobile and desktop
- **TypeScript**: Full type safety throughout the application
- **Modern UI**: Clean, intuitive interface with loading states

## 🐛 Troubleshooting

### Common Issues

1. **Port conflicts**: If ports 5206 or 4200 are in use:
   - Backend: `dotnet run --urls "http://localhost:5207"`
   - Frontend: `npm start -- --port 4201`

2. **CORS errors**: Ensure the backend is running and CORS is properly configured

3. **Node.js version**: Make sure you have the correct Node.js version installed

4. **Test failures**: 
   - Backend: Ensure all dependencies are restored with `dotnet restore`
   - Frontend: Clear node_modules and reinstall with `npm install`

5. **Data persistence**: The backend uses in-memory storage, so data resets when the server restarts

### Debugging Tips

- Check browser console for frontend errors
- Check terminal output for backend errors
- Use the test scripts to verify API functionality
- Ensure both frontend and backend are running simultaneously

## 🏗 Architecture

### Backend Architecture
- **Controllers**: Handle HTTP requests and responses with proper status codes
- **Services**: Business logic and data management with dependency injection
- **Models**: Data structures with validation attributes
- **Dependency Injection**: Services registered as Singleton for data persistence
- **Testing**: xUnit with Moq for comprehensive mocking

### Frontend Architecture
- **Components**: Reusable UI components with standalone approach
- **Services**: API communication with proper error handling
- **TypeScript**: Type safety throughout the application
- **Testing**: Karma/Jasmine with Angular testing utilities
- **Modern Angular**: Uses latest Angular features and best practices

## ✅ Best Practices Implemented

- **Separation of Concerns**: Clear separation between frontend and backend
- **Type Safety**: TypeScript and C# provide compile-time type checking
- **Error Handling**: Comprehensive error handling on both frontend and backend
- **Responsive Design**: Mobile-first approach with modern CSS
- **RESTful API**: Proper HTTP methods and status codes
- **Clean Code**: Well-structured, readable code with meaningful names
- **Testing**: Comprehensive unit test coverage for all critical functionality
- **Test-Driven Development**: Tests written alongside implementation
- **Mocking**: Proper use of mocks for isolated unit testing
- **Async Testing**: Proper handling of asynchronous operations in tests
- **Singleton Pattern**: Ensures data persistence in in-memory storage
- **CORS Configuration**: Proper cross-origin resource sharing setup

## 📊 Performance

- **Fast Loading**: Optimized Angular build with tree shaking
- **Efficient API**: Minimal HTTP requests with proper caching
- **Responsive UI**: Smooth interactions with loading states
- **Memory Efficient**: In-memory storage with proper cleanup

## 🔒 Security

- **Input Validation**: Server-side validation for all inputs
- **CORS Protection**: Properly configured cross-origin requests
- **Error Handling**: Secure error messages without exposing internals
- **Type Safety**: Compile-time type checking prevents runtime errors

## 📈 Future Enhancements

- Database integration (SQL Server, PostgreSQL)
- User authentication and authorization
- Real-time updates with SignalR
- Offline support with service workers
- Advanced filtering and search
- Todo categories and tags
- Export/import functionality

---

**Built with ❤️ using Angular 19 and .NET 8** 