import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { SpinnerService } from '../services/common/spinner/spinner.service';

@Injectable({
  providedIn: 'root'
})

// Şimdi BaseComponent'ı SpinnerService'i kullanacak şekilde düzenleyin
export class BaseComponent {
  constructor(protected spinnerService: SpinnerService) { }

  /**
   * Shows a spinner for the specified duration
   * @param duration Duration in milliseconds to show the spinner for (default: 500ms)
   */
  showSpinner(duration: number = 500): void {
    this.spinnerService.showSpinner(duration);
  }

  /**
   * Hides the spinner immediately
   */
  hideSpinner(): void {
    this.spinnerService.hideSpinner();
  }

  /**
   * Shows the spinner and executes the provided function
   * @param fn Function to execute while showing spinner
   * @returns Promise with the result of the function
   */
  async withSpinner<T>(fn: () => Promise<T>): Promise<T> {
    try {
      this.showSpinner();
      return await fn();
    } finally {
      this.hideSpinner();
    }
  }

  /**
   * Scrolls the window to the top of the page smoothly
   * @param behavior Scrolling behavior, defaults to 'smooth'
   */
  scrollToTop(behavior: ScrollBehavior = 'smooth'): void {
    window.scrollTo({
      top: 0,
      left: 0,
      behavior: behavior
    });
  }

  /**
   * Scrolls to a specific element on the page
   * @param elementId The ID of the element to scroll to
   * @param behavior Scrolling behavior, defaults to 'smooth' 
   * @param offset Optional offset from the top in pixels
   */
  scrollToElement(elementId: string, behavior: ScrollBehavior = 'smooth', offset: number = 0): void {
    const element = document.getElementById(elementId);
    if (element) {
      const y = element.getBoundingClientRect().top + window.pageYOffset + offset;
      window.scrollTo({
        top: y,
        left: 0,
        behavior: behavior
      });
    }
  }
}
