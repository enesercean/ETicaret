import { Injectable } from '@angular/core';
declare var alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  message(message: string, options: Partial<AlertifyOptions>){
    alertify.set('notifier','delay',options.delay);
    alertify.set('notifier','position',options.position);
    alertify[options.messageType](message);
  }
}

export class AlertifyOptions{
  messageType: MessageType = MessageType.Message;
  position: Position = Position.BottomCenter;
  delay: number = 3;
}


export enum MessageType{
  Error = "error",
  Message = "message",
  Notify = "notify",
  Success = "success",
  Warning = "warning"
}

export enum Position{
  TopCenter = "top-center",
  TopRight = "top-right",
  TopLeft = "top-left",
  BottomCenter = "bottom-center",
  BottomRight = "bottom-right",
  BottomLeft = "bottom-left"
}
