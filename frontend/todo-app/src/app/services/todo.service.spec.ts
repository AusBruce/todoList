import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TodoService, TodoItem } from './todo.service';

describe('TodoService', () => {
  let service: TodoService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TodoService]
    });
    service = TestBed.inject(TodoService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getTodos', () => {
    it('should return an Observable<TodoItem[]>', () => {
      const mockTodos: TodoItem[] = [
        { id: 1, title: 'Test Todo 1', isCompleted: false, createdAt: '2023-01-01T00:00:00Z' },
        { id: 2, title: 'Test Todo 2', isCompleted: true, createdAt: '2023-01-02T00:00:00Z' }
      ];

      service.getTodos().subscribe(todos => {
        expect(todos).toEqual(mockTodos);
      });

      const req = httpMock.expectOne('http://localhost:5000/api/todo');
      expect(req.request.method).toBe('GET');
      req.flush(mockTodos);
    });
  });

  describe('getTodo', () => {
    it('should return an Observable<TodoItem> for valid id', () => {
      const mockTodo: TodoItem = {
        id: 1,
        title: 'Test Todo',
        isCompleted: false,
        createdAt: '2023-01-01T00:00:00Z'
      };

      service.getTodo(1).subscribe(todo => {
        expect(todo).toEqual(mockTodo);
      });

      const req = httpMock.expectOne('http://localhost:5000/api/todo/1');
      expect(req.request.method).toBe('GET');
      req.flush(mockTodo);
    });
  });

  describe('createTodo', () => {
    it('should return an Observable<TodoItem> for valid todo', () => {
      const newTodo = { title: 'New Todo', isCompleted: false };
      const createdTodo: TodoItem = {
        id: 3,
        title: 'New Todo',
        isCompleted: false,
        createdAt: '2023-01-03T00:00:00Z'
      };

      service.createTodo(newTodo).subscribe(todo => {
        expect(todo).toEqual(createdTodo);
      });

      const req = httpMock.expectOne('http://localhost:5000/api/todo');
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(newTodo);
      req.flush(createdTodo);
    });
  });

  describe('updateTodo', () => {
    it('should return an Observable<TodoItem> for valid update', () => {
      const updateData = { title: 'Updated Todo', isCompleted: true };
      const updatedTodo: TodoItem = {
        id: 1,
        title: 'Updated Todo',
        isCompleted: true,
        createdAt: '2023-01-01T00:00:00Z'
      };

      service.updateTodo(1, updateData).subscribe(todo => {
        expect(todo).toEqual(updatedTodo);
      });

      const req = httpMock.expectOne('http://localhost:5000/api/todo/1');
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(updateData);
      req.flush(updatedTodo);
    });
  });

  describe('deleteTodo', () => {
    it('should return an Observable<void> for valid delete', () => {
      service.deleteTodo(1).subscribe(response => {
        expect(response).toBeNull();
      });

      const req = httpMock.expectOne('http://localhost:5000/api/todo/1');
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });
  });

  describe('toggleTodo', () => {
    it('should return an Observable<TodoItem> for valid toggle', () => {
      const toggledTodo: TodoItem = {
        id: 1,
        title: 'Test Todo',
        isCompleted: true,
        createdAt: '2023-01-01T00:00:00Z'
      };

      service.toggleTodo(1).subscribe(todo => {
        expect(todo).toEqual(toggledTodo);
      });

      const req = httpMock.expectOne('http://localhost:5000/api/todo/1/toggle');
      expect(req.request.method).toBe('PATCH');
      req.flush(toggledTodo);
    });
  });

  describe('error handling', () => {
    it('should handle HTTP errors for getTodos', () => {
      service.getTodos().subscribe({
        next: () => fail('should have failed'),
        error: (error) => {
          expect(error.status).toBe(500);
        }
      });

      const req = httpMock.expectOne('http://localhost:5000/api/todo');
      req.error(new ErrorEvent('Network error'), { status: 500 });
    });

    it('should handle HTTP errors for createTodo', () => {
      const newTodo = { title: 'New Todo', isCompleted: false };

      service.createTodo(newTodo).subscribe({
        next: () => fail('should have failed'),
        error: (error) => {
          expect(error.status).toBe(400);
        }
      });

      const req = httpMock.expectOne('http://localhost:5000/api/todo');
      req.error(new ErrorEvent('Bad Request'), { status: 400 });
    });
  });
}); 