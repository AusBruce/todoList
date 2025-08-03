import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodoService, TodoItem } from '../../services/todo.service';

@Component({
  selector: 'app-todo',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {
  todos: TodoItem[] = [];
  newTodoTitle: string = '';
  loading: boolean = false;
  error: string = '';

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.loading = true;
    this.error = '';
    
    this.todoService.getTodos().subscribe({
      next: (todos) => {
        this.todos = todos;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load todos. Please try again.';
        this.loading = false;
        console.error('Error loading todos:', error);
      }
    });
  }

  addTodo(): void {
    if (!this.newTodoTitle.trim()) {
      return;
    }

    const newTodo = {
      title: this.newTodoTitle.trim(),
      isCompleted: false
    };

    this.todoService.createTodo(newTodo).subscribe({
      next: (createdTodo) => {
        this.todos.unshift(createdTodo);
        this.newTodoTitle = '';
        this.error = '';
      },
      error: (error) => {
        this.error = 'Failed to add todo. Please try again.';
        console.error('Error adding todo:', error);
      }
    });
  }

  toggleTodo(todo: TodoItem): void {
    this.todoService.toggleTodo(todo.id).subscribe({
      next: (updatedTodo) => {
        const index = this.todos.findIndex(t => t.id === todo.id);
        if (index !== -1) {
          this.todos[index] = updatedTodo;
        }
        this.error = '';
      },
      error: (error) => {
        this.error = 'Failed to update todo. Please try again.';
        console.error('Error updating todo:', error);
      }
    });
  }

  deleteTodo(todo: TodoItem): void {
    this.todoService.deleteTodo(todo.id).subscribe({
      next: () => {
        this.todos = this.todos.filter(t => t.id !== todo.id);
        this.error = '';
      },
      error: (error) => {
        this.error = 'Failed to delete todo. Please try again.';
        console.error('Error deleting todo:', error);
      }
    });
  }

  onKeyPress(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      this.addTodo();
    }
  }
} 