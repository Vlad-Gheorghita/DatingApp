import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { finalize, map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeader } from './paginationHelper';

// const httpOptions = {
//   headers : new HttpHeaders({
//   Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user')!)?.token 
// })}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = []; //Putem salva aici date pentru ca serviciile sunt singleton
  memberCache = new Map();
  user: User;
  userParams: UserParams;


  constructor(private http: HttpClient, private accountService: AccountService) { //ATENTIE!: putem sa injectam servicii in alte servicii, DAR trebuie sa avem grija sa
    //nu facem o injectie circulara. Ex: injectam AccountService in MemberService si vice-versa
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
    })
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    this.userParams = new UserParams(this.user);
    return this.userParams;
  }

  getMembers(userParams: UserParams) {  //Se recomdanda ca atunci cand tre sa introduci mai multi parametrii sa se faca o clasa
    // if (this.members.length > 0) return of(this.members);    //of() returneaza membrii din serviuciu ca un Observable daca exista in members[]
    var response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = getPaginationHeader(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http)
      .pipe(map(response => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      }));

    // map(members => {
    //   this.members = members;
    //   return members;   //Aici nu am mai pus of() pentru ca map() ca default returneaza valori ca Observable
    // })
  }

  getMember(username: string) {
    // const member = this.members.find(x => x.username === username);
    // if (member !== undefined) return of(member);
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.username === username);

    if (member) {
      return of(member);
    }

    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }
  //Recheck: pipe este folosit cand vrem sa facem ceva cu datele prelucrate din httpRequest
  updateMember(member: Member) {
    //return this.http.put(this.baseUrl + 'users', member).pipe(finalize(() => (this.members[this.members.indexOf(member)]= member) //Chestia asta a facut o ovi
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      }));
  }

  upddd(member: any): Observable<any> {
    const index = this.members.indexOf(member);
    this.members[index] = member;

    return of(true);
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {}) //Punem un Obiect gol ca nu vrem sa trimitem niciun Boddy la HttpRequest
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }

  addLike(username: string) {
    return this.http.post(this.baseUrl + 'likes/' + username, {});
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number) { //Se poate crea o clasa pentru parametrii, dar pentru ca nu sunt foarte multe le adaugam direct in metoda
    let params = getPaginationHeader(pageNumber, pageSize);

    params = params.append('predicate', predicate);
    return getPaginatedResult<Partial<Member[]>>(this.baseUrl + 'likes', params, this.http);

    //return this.http.get<Partial<Member[]>>(this.baseUrl + 'likes?predicate=' + predicate);
  }

}
