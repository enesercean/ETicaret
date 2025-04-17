import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$: Observable<boolean> = this.loadingSubject.asObservable();

  constructor() { }

  /**
   * Shows a spinner for the specified duration
   * @param duration Duration in milliseconds to show the spinner for (default: 500ms)
   */
  showSpinner(duration: number = 500): void {
    this.loadingSubject.next(true);

    if (duration > 0) {
      setTimeout(() => {
        this.hideSpinner();
      }, duration);
    }
  }

  /**
   * Hides the spinner immediately
   */
  hideSpinner(): void {
    this.loadingSubject.next(false);
  }
}
