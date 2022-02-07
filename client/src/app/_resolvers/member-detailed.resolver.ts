import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';

@Injectable({
  providedIn: 'root'
})
export class MemberDetailedResolver implements Resolve<Member> {

  constructor(private memberService: MembersService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Member> {
    return this.memberService.getMember(route.paramMap.get('username'));  //Nu trebuie sa folosim subscribe() deoarece router-ul o sa faca asta pentru noi
                                                                          //Asta e buna de folosit in orice deoarece putem sa luam orice date inainte de a se incarca html-ul
  }
}
