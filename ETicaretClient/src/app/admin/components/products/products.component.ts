import { Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from '../../../base/base.component';
import { HttpClientService } from '../../../services/common/http-client.service';
import { ListComponent } from './list/list.component';
import { Create_Product } from '../../../contracts/Create_Product';


@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrl: './products.component.scss',
    standalone: false
})
export class ProductsComponent implements OnInit {
  constructor(private httpClientService: HttpClientService) { }


  ngOnInit(): void {
  }


  @ViewChild(ListComponent) listComponents: ListComponent;

  createdProduct(create_product: Create_Product){
    this.listComponents.getProducts();
  }
  addProductImages(id:string) {

  }
}
