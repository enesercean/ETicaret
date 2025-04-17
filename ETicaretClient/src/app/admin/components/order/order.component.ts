import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent } from '../../../base/base.component';
import { HttpClientService } from '../../../services/common/http-client.service';

@Component({
    selector: 'app-order',
    templateUrl: './order.component.html',
    styleUrl: './order.component.scss',
    standalone: false
})
export class OrderComponent implements OnInit {
  constructor(private httpClientService: HttpClientService){

  }

  ngOnInit(): void {
    this.httpClientService.get({controller:"products"}).subscribe(data => console.log(data));
  }
}
