import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Blog } from './Models/blog';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  http = inject(HttpClient);

  getFeaturedBlogs() {
    return this.http.get<Blog[]>('https://localhost:7147/api/Blog/featured');
  }

    getBlogs() {
    return this.http.get<Blog[]>('https://localhost:7147/api/Blog');
  }
}
