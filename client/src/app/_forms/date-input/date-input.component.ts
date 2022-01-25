import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})
export class DateInputComponent implements ControlValueAccessor {
  @Input() label: string;
  @Input() maxDate: Date;
  bsConfig: Partial<BsDatepickerConfig>; //Partial e folosit pentru a putea utiliza numai cateva configurari din BsDatepickerConfig si nu sa le initializam pe toate

  constructor(@Self() public ngControl: NgControl) {  //@Self() este folosit deoarece vrems a se faca un dependency injection local nu sa ia ceva valoare din alta parte(dependency tree)
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass: 'theme-red',
      dateInputFormat: 'DD MMMM YYYY',
      
    }
  }    
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

}
