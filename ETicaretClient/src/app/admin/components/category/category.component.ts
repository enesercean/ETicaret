import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'primeng/basecomponent';
import { SpinnerService } from '../../../services/common/spinner/spinner.service';

@Component({
  selector: 'app-category',
  standalone: false,
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss'
})
export class CategoryComponent extends BaseComponent implements OnInit {

  constructor(private spinnerService: SpinnerService) {
      super();
  }

  override ngOnInit(): void {
    this.spinnerService.showSpinner();
  }
}
