import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Blog } from '../blog/blog';
import { BlogService } from '../../blog-service';

@Component({
  selector: 'app-home',
  imports: [MatCardModule, MatButtonModule],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  blogService = inject(BlogService);
  featuredBlogs!: Blog[];

  ngOnInit() {
    this.blogService.getFeaturedBlogs().subscribe(
      (data) => {
        this.featuredBlogs = data;
        console.log(this.featuredBlogs);
      },
      (error) => {
        console.error('Error fetching featured blogs:', error);
      },
    );
  }
}
