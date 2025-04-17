import { Component, OnInit, OnDestroy, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { BaseComponent } from '../../../base/base.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  standalone: false
})
export class HomeComponent implements OnInit, OnDestroy {
  currentSlide: number = 0;
  totalSlides: number = 3; // Adjust this based on the total number of slides
  private slideInterval: any;
  private isBrowser: boolean;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  ngOnInit(): void {
    // Only start automatic slide transitions in browser environment
    if (this.isBrowser) {
      this.startAutoSlideTransition();
    }
  }

  ngOnDestroy(): void {
    // Clear the interval when component is destroyed to prevent memory leaks
    if (this.isBrowser) {
      this.stopAutoSlideTransition();
    }
  }

  goToSlide(slideIndex: number): void {
    this.currentSlide = slideIndex;
  }

  nextSlide(): void {
    this.currentSlide = (this.currentSlide + 1) % this.totalSlides;
  }

  prevSlide(): void {
    this.currentSlide = (this.currentSlide - 1 + this.totalSlides) % this.totalSlides;
  }

  startAutoSlideTransition(): void {
    // Set interval to change slides every 5 seconds (5000 milliseconds)
    this.slideInterval = setInterval(() => {
      this.nextSlide();
    }, 5000);
  }

  stopAutoSlideTransition(): void {
    if (this.slideInterval) {
      clearInterval(this.slideInterval);
      this.slideInterval = null;
    }
  }
}

