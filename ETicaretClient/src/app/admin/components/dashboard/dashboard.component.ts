import { Component, OnInit, inject } from '@angular/core';
import { BaseComponent } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { SignalrService } from '../../../services/common/signalr.service';
import { HubUrls } from '../../../Constants/hubUrl';
import { ReceiveFunctions } from '../../../Constants/receiveFunctions';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../services/ui/custom-toastr.service';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.scss',
    standalone: false
})
export class DashboardComponent implements OnInit{
  constructor(private signalRService: SignalrService
  ) {  }
  private toastr = inject(CustomToastrService);
  ngOnInit(): void {

    this.signalRService.on(HubUrls.ProductHub, ReceiveFunctions.ProductAddedMessageReceiveFunction, message => {
      this.toastr.message(message, 'Product Added', {
        messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
      });
    });

    this.signalRService.addToGroup(HubUrls.OrderHub, ReceiveFunctions.OrderAddedMessageReceiveFunction);

    this.signalRService.on(HubUrls.OrderHub, ReceiveFunctions.OrderAddedMessageReceiveFunction, message => {
      this.toastr.message(message, 'New Order!', {
        messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
      });
    });
  }
}
