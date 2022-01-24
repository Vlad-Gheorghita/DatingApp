import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);  //E ca un Buffer, ii spunem ce tip de data sa stocheze in buffer si cate aceste date
  currentUser$ = this.currentUserSource.asObservable();    // "currentUser$" este o conventie unde $ reprezinta ca tipul de data este un Observable

  constructor(private http: HttpClient) { }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {  // puteam folosi si (user: User) dar ts e destul de smart sa isi dea seama ca vorbim de obiect User
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null!); //"!" Ii spune ts-ul ca cu toate ca ceva poate sa fie null, acesta o sa aiba incredere ca nu o sa fie
  }
}
