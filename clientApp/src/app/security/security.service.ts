import { Injectable } from '@angular/core';
import { AppUserAuth } from './app-user-auth';
import { AppUser } from './app-user';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const API_URL = 'https://localhost:5001/api/security';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  securityObject: AppUserAuth = new AppUserAuth();
  constructor(private http: HttpClient) { }

  login(entity: AppUser): Observable<AppUserAuth> {
    this.reset();
    return this.http.post<AppUserAuth>(API_URL + '/login',
      entity, httpOptions).pipe(
        tap(resp => {
          Object.assign(this.securityObject, resp);
          localStorage.setItem('JwtBearer', this.securityObject.bearerToken);
        })
      );
  }

  logout(): void {
    this.reset();
  }

  reset(): void {
    this.securityObject.userName = '';
    this.securityObject.bearerToken = '';
    this.securityObject.isAuthenticated = false;
    localStorage.removeItem('jwttoken');
  }
}
