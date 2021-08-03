import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-time-input',
  templateUrl: './time-input.component.html',
  styleUrls: ['./time-input.component.css']
})
export class TimeInputComponent implements ControlValueAccessor {
  @Input() label: string;
  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;

  }
  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {
    
    
  }
  registerOnTouched(fn: any): void {
     
  }

}
