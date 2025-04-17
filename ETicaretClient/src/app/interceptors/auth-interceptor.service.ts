import { HttpClient } from '@angular/common/http';
import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { Observable } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService {

  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  private getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('token');
    }
    return null;
  }

  public isTokenExpired(): boolean {
    const token = this.getToken();
    if (!token) {
      return true;
    }

    try {
      const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
      return (Math.floor((new Date).getTime() / 1000)) >= expiry;
    } catch (error) {
      console.error('Token parsing error:', error);
      return true;
    }
  }

  private getTokenRoles(): string[] {
    const token = this.getToken();
    if (!token) {
      return [];
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const roles = payload.role || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || [];
      return Array.isArray(roles) ? roles : [roles];
    } catch (error) {
      console.error('Role parsing error:', error);
      return [];
    }
  }

  identityCheck() {
    if (isPlatformBrowser(this.platformId)) {
      _isAuthenticated = this.getToken() != null && !this.isTokenExpired();
      _isTokenRoles = this.getTokenRoles();
      console.log(`Token Roles: ${_isTokenRoles.join(',')}`);
    } else {
      // Sunucu tarafında çalışırken
      _isAuthenticated = false;
      _isTokenRoles = [];
    }
  }

  get isAuthenticated() {
    if (isPlatformBrowser(this.platformId)) {
      return _isAuthenticated;
    }
    return false;
  }

  // Rol hiyerarşisi kontrol metodu
  hasRoleOrHigher(minimumRole: string): boolean {
    if (!isPlatformBrowser(this.platformId) || !_isAuthenticated) return false;

    // Rol hiyerarşisi tanımlama (düşükten yükseğe)
    const roleHierarchy = ['Customer', 'EmployeeRead', 'Employee', 'SupplierManager', 'Admin'];

    const minimumRoleIndex = roleHierarchy.indexOf(minimumRole);

    // Eğer minimum rol hiyerarşide bulunamadıysa false döner
    if (minimumRoleIndex === -1) {
      return false;
    }

    // Kullanıcının rollerinden herhangi biri minimum rolden yüksekse veya eşitse true döner
    return _isTokenRoles.some(role => {
      const userRoleIndex = roleHierarchy.indexOf(role);
      return userRoleIndex !== -1 && userRoleIndex >= minimumRoleIndex;
    });
  }
}

export let _isAuthenticated: boolean = false;
export let _isTokenRoles: string[] = [];
