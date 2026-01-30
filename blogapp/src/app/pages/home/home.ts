import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { Blog } from '../../Models/blog';
import { BlogService } from '../../blog-service';
import { RouterLink } from "@angular/router";
import { BlogCard } from '../blog-card/blog-card';

@Component({
  selector: 'app-home',
  imports: [CommonModule, MatCardModule, MatButtonModule, RouterLink,BlogCard],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  blogService = inject(BlogService);
  featuredBlogs!: Blog[];

  ngOnInit() {
    this.blogService.getBlogs().subscribe(
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
