import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { User } from '../../../entities/user';
import { CreateUser } from '../../../contracts/users/CreateUser';
import { Observable, firstValueFrom } from 'rxjs';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { TokenResponse } from '../../../contracts/token/tokenResponse';
import { SocialUser } from '@abacritt/angularx-social-login';
import { ListUser } from '../../../contracts/users/listUser';
import { UserProfile } from '../../../contracts/users/userProfile';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClientService: HttpClientService, public toastr: CustomToastrService) { }

  async create(user: User) {
    const observable: Observable<CreateUser | User> = this.httpClientService.post<CreateUser | User>({
      controller: 'users'
    }, user);

    return await firstValueFrom(observable) as CreateUser;
  }

  async refreshTokenLogin(refreshTokens: string, callBack?: () => void) {
    const observable: Observable<any | TokenResponse> = this.httpClientService.post({
      controller: 'auth',
      action: 'refreshtokenlogin'
    }, { refreshToken: refreshTokens });

    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;

    if (tokenResponse) {
      localStorage.setItem('token', tokenResponse.token.accessToken);
      localStorage.setItem('refreshToken', tokenResponse.token.refreshToken);
    }
  }

  async login(userNameOrEmail: string, password: string, callback?: () => void): Promise<any> {
    const observable: Observable<any | TokenResponse> = this.httpClientService.post<any | TokenResponse>({
      controller: 'auth',
      action: 'login'
    }, { userNameOrEmail, password })
    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
    if (tokenResponse) {
      this.toastr.message('Login successful', 'Login', {
        messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
      });
      localStorage.setItem('token', tokenResponse.token.accessToken);
      localStorage.setItem('refreshToken', tokenResponse.token.refreshToken);
    }
    callback();
  }

  async googleLogin(user: SocialUser, callBack?: () => void): Promise<any> {
    const observable: Observable<SocialUser | TokenResponse> =
      this.httpClientService.post<SocialUser | TokenResponse>({
        action: 'google-login',
        controller: 'auth'
      }, user);

    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
    if (tokenResponse) {
      localStorage.setItem('token', tokenResponse.token.accessToken);
      if (tokenResponse) {
        this.toastr.message('Google Login successful', 'Login', {
          messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
        });
        localStorage.setItem('token', tokenResponse.token.accessToken);
        localStorage.setItem('refreshToken', tokenResponse.token.refreshToken);
      }
      callBack();
    }
  }

  async getUserByUserName(username: string): Promise<ListUser> {
    const observable: Observable<ListUser> = this.httpClientService.get<ListUser>({
      controller: 'users',
      action: 'GetUserByUserName/' + username,
    });
    return await firstValueFrom(observable) as ListUser;
  }

  logOut(): Promise<any> {
    const observable: Observable<any> = this.httpClientService.post<any>({
      controller: 'auth',
      action: 'google-logout'
    }, {});

    return firstValueFrom(observable);
  }

  getProfile(): Promise<UserProfile> {
    const observable: Observable<UserProfile> = this.httpClientService.get({
      controller: 'users',
      action: 'getProfile'
    });
    console.log('observable', observable);
    return firstValueFrom(observable);
  }

  updateProfile(phone: string, name: string): Promise<any> {
    const observable: Observable<any> = this.httpClientService.post({
      controller: 'users',
      action: 'updateProfile'
    }, { phoneNumber: phone, name: name });
    return firstValueFrom(observable);
  }

  getAllUsers(): Promise<ListUser[]> {
    const observable: Observable<ListUser[]> = this.httpClientService.get({
      controller: 'users',
      action: 'all'
    });
    return firstValueFrom(observable);
  }
}
