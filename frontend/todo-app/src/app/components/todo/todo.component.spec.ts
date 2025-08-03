import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of, throwError } from 'rxjs';
import { delay } from 'rxjs/operators';
import { TodoComponent } from './todo.component';
import { TodoService, TodoItem } from '../../services/todo.service';

describe('TodoComponent', () => {
  let component: TodoComponent;
  let fixture: ComponentFixture<TodoComponent>;
  let todoService: jasmine.SpyObj<TodoService>;

  const mockTodos: TodoItem[] = [
    { id: 1, title: 'Test Todo 1', isCompleted: false, createdAt: '2023-01-01T00:00:00Z' },
    { id: 2, title: 'Test Todo 2', isCompleted: true, createdAt: '2023-01-02T00:00:00Z' }
  ];

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('TodoService', [
      'getTodos', 'createTodo', 'toggleTodo', 'deleteTodo'
    ]);

    await TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientTestingModule],
      providers: [
        { provide: TodoService, useValue: spy }
      ]
    }).compileComponents();

    todoService = TestBed.inject(TodoService) as jasmine.SpyObj<TodoService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TodoComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('ngOnInit', () => {
    it('should load todos on init', fakeAsync(() => {
      todoService.getTodos.and.returnValue(of(mockTodos));

      component.ngOnInit();
      tick();

      expect(todoService.getTodos).toHaveBeenCalled();
      expect(component.todos).toEqual(mockTodos);
      expect(component.loading).toBeFalse();
      expect(component.error).toBe('');
    }));

    it('should handle error when loading todos fails', fakeAsync(() => {
      const errorMessage = 'Failed to load todos';
      todoService.getTodos.and.returnValue(throwError(() => new Error(errorMessage)));

      component.ngOnInit();
      tick();

      expect(component.error).toBe('Failed to load todos. Please try again.');
      expect(component.loading).toBeFalse();
    }));
  });

  describe('loadTodos', () => {
    it('should load todos successfully', fakeAsync(() => {
      todoService.getTodos.and.returnValue(of(mockTodos));

      component.loadTodos();
      tick();

      expect(component.todos).toEqual(mockTodos);
      expect(component.loading).toBeFalse();
      expect(component.error).toBe('');
    }));

    it('should handle error when loading todos', fakeAsync(() => {
      todoService.getTodos.and.returnValue(throwError(() => new Error('Network error')));

      component.loadTodos();
      tick();

      expect(component.error).toBe('Failed to load todos. Please try again.');
      expect(component.loading).toBeFalse();
    }));
  });

  describe('addTodo', () => {
    it('should add todo successfully', fakeAsync(() => {
      const newTodo = { title: 'New Todo', isCompleted: false };
      const createdTodo: TodoItem = {
        id: 3,
        title: 'New Todo',
        isCompleted: false,
        createdAt: '2023-01-03T00:00:00Z'
      };

      todoService.createTodo.and.returnValue(of(createdTodo));
      component.newTodoTitle = 'New Todo';

      component.addTodo();
      tick();

      expect(todoService.createTodo).toHaveBeenCalledWith(newTodo);
      expect(component.todos).toContain(createdTodo);
      expect(component.newTodoTitle).toBe('');
      expect(component.error).toBe('');
    }));

    it('should not add todo when title is empty', () => {
      component.newTodoTitle = '';

      component.addTodo();

      expect(todoService.createTodo).not.toHaveBeenCalled();
    });

    it('should not add todo when title is only whitespace', () => {
      component.newTodoTitle = '   ';

      component.addTodo();

      expect(todoService.createTodo).not.toHaveBeenCalled();
    });

    it('should handle error when adding todo', fakeAsync(() => {
      todoService.createTodo.and.returnValue(throwError(() => new Error('Network error')));
      component.newTodoTitle = 'New Todo';

      component.addTodo();
      tick();

      expect(component.error).toBe('Failed to add todo. Please try again.');
    }));
  });

  describe('toggleTodo', () => {
    it('should toggle todo successfully', fakeAsync(() => {
      const todo = mockTodos[0];
      const toggledTodo = { ...todo, isCompleted: !todo.isCompleted };

      todoService.toggleTodo.and.returnValue(of(toggledTodo));
      component.todos = [...mockTodos];

      component.toggleTodo(todo);
      tick();

      expect(todoService.toggleTodo).toHaveBeenCalledWith(todo.id);
      expect(component.todos[0]).toEqual(toggledTodo);
      expect(component.error).toBe('');
    }));

    it('should handle error when toggling todo', fakeAsync(() => {
      const todo = mockTodos[0];
      todoService.toggleTodo.and.returnValue(throwError(() => new Error('Network error')));
      component.todos = [...mockTodos];

      component.toggleTodo(todo);
      tick();

      expect(component.error).toBe('Failed to update todo. Please try again.');
    }));
  });

  describe('deleteTodo', () => {
    it('should delete todo successfully', fakeAsync(() => {
      const todo = mockTodos[0];
      todoService.deleteTodo.and.returnValue(of(void 0));
      component.todos = [...mockTodos];

      component.deleteTodo(todo);
      tick();

      expect(todoService.deleteTodo).toHaveBeenCalledWith(todo.id);
      expect(component.todos).not.toContain(todo);
      expect(component.error).toBe('');
    }));

    it('should handle error when deleting todo', fakeAsync(() => {
      const todo = mockTodos[0];
      todoService.deleteTodo.and.returnValue(throwError(() => new Error('Network error')));
      component.todos = [...mockTodos];

      component.deleteTodo(todo);
      tick();

      expect(component.error).toBe('Failed to delete todo. Please try again.');
    }));
  });

  describe('onKeyPress', () => {
    it('should call addTodo when Enter key is pressed', () => {
      spyOn(component, 'addTodo');
      const event = new KeyboardEvent('keyup', { key: 'Enter' });

      component.onKeyPress(event);

      expect(component.addTodo).toHaveBeenCalled();
    });

    it('should not call addTodo when other key is pressed', () => {
      spyOn(component, 'addTodo');
      const event = new KeyboardEvent('keyup', { key: 'Space' });

      component.onKeyPress(event);

      expect(component.addTodo).not.toHaveBeenCalled();
    });
  });

  describe('component state', () => {
    it('should have correct initial state', () => {
      expect(component.todos).toEqual([]);
      expect(component.newTodoTitle).toBe('');
      expect(component.loading).toBeFalse();
      expect(component.error).toBe('');
    });

    it('should set loading to true when loading todos', () => {
      // Use a delayed observable to test loading state
      const delayedTodos = of(mockTodos).pipe(delay(100));
      todoService.getTodos.and.returnValue(delayedTodos);

      component.loadTodos();
      
      // Check loading state immediately after calling loadTodos
      expect(component.loading).toBeTrue();
    });
  });
}); 