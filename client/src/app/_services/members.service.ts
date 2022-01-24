import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

// const httpOptions = {
//   headers : new HttpHeaders({
//   Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user')!)?.token 
// })}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) { }

  getMembers() {
    if(this.members.length > 0) return of(this.members);    //of() returneaza membrii din serviuciu ca un Observable daca exista in members[]
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members => {
        this.members = members;
        return members;   //Aici nu am mai pus of() pentru ca map() ca default returneaza valori ca Observable
      })
    )
  }

  getMember(username: string) {
    const member = this.members.find(x => x.username === username);
    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(    //Recheck: pipe este folosit cand vrem sa facem ceva cu datele prelucrate din httpRequest
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );    
  }
}
