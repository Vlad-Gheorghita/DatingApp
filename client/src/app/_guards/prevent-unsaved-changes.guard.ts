import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(component: MemberEditComponent): boolean {
    if (component.editForm.dirty) {
      return confirm('Are you sure you want to continue? Any unsaved changes will be lost'); // Acesta este o metoda din JS, o sa apara ceva si ai optiunea de a selecta "Yes" sau "No"
    }
      return true; //Daca se selecteaza "Yes" se va returna true si v-a iesi din componenta, altfel ramani acolo
  }

}
