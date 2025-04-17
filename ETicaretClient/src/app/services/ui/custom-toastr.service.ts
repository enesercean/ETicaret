import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CustomToastrService {

  constructor(private toastr : ToastrService) {
    
  }

  message(message: string, title: string, toastrOptions: Partial<ToastrOptions>) {
    this.toastr[toastrOptions.messageType](message, title, { positionClass: toastrOptions.position, timeOut: 2000 });
  }

}

export class ToastrOptions{
  messageType:ToastrMessageType;
  position:ToastrPosition;
}

export enum ToastrMessageType{
  Success = "success",
  Info = "info",
  Error = "error",
  Warning = "warning"

}

export enum ToastrPosition{
  TopLeft = "toast-top-left",
  TopRight = "toast-top-right",
  BottomLeft = "toast-bottom-left",
  BottomRight = "toast-bottom-right",
  TopFullWith = "toast-top-full-width",
  TopCenter = "toast-top-center",
  BottomCenter = "toast-bottom-center",
  BottomFullWith = "toast-bottom-full-width"
}
