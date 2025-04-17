import { Inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor(@Inject('baseSignalRUrl') private baseSignalRUrl) { }

  private hubConnection: HubConnection;
  private connectionId: string;
  get connection(): HubConnection {
    return this.hubConnection;
  }

  async start(hubUrl: string): Promise<void> {
    hubUrl = this.baseSignalRUrl + hubUrl;

    const builder: HubConnectionBuilder = new HubConnectionBuilder();

    this.hubConnection = builder.withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    try {
      await this.hubConnection.start();
      console.log("Connected...");
      this.connectionId = this.hubConnection.connectionId;
    } catch (error) {
      console.error("Connection error: ", error);
      setTimeout(() => this.start(hubUrl), 2000);
    }
  }

  async invoke(hubUrl: string, methodName: string, message: any, successCallBack?: () => void, errorCallBack?: () => void) {
    if (!this.hubConnection || this.hubConnection.state !== HubConnectionState.Connected) {
      await this.start(hubUrl);
    }
    return this.hubConnection.invoke(methodName, message)
      .then(successCallBack)
      .catch(errorCallBack);
  }

  async on(hubUrl: string, methodName: string, newMethod: (...args: any[]) => void) {
    if (!this.hubConnection || this.hubConnection.state !== HubConnectionState.Connected) {
      await this.start(hubUrl);
    }
    this.hubConnection.on(methodName, newMethod);
  }

  stop() {
    this.hubConnection.stop()
      .then(() => console.log('SignalR connection stopped'))
      .catch(err => console.log('Error while stopping SignalR connection: ' + err));
  }

  getConnectionId(): string {
    return this.connectionId;
  }

  async addToGroup(hubUrl: string, groupName: string) {
    await this.invoke(hubUrl, 'AddToGroup', groupName);
  }

  async removeFromGroup(hubUrl: string, groupName: string) {
    await this.invoke(hubUrl, 'RemoveFromGroup', groupName);
  }

  off(hubUrl: string, methodName: string) {
    if (this.hubConnection && this.hubConnection.state === HubConnectionState.Connected) {
      this.hubConnection.off(methodName);
    }
  }
}
