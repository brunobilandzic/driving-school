import { NgModule } from "@angular/core";
import { ToastrModule } from "ngx-toastr";
import {ModalModule} from "ngx-bootstrap/modal";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
    imports: [
        ToastrModule.forRoot(
            {positionClass: 'toast-bottom-right'}
        ),
        ModalModule.forRoot(),
        BsDropdownModule.forRoot(),
        BsDatepickerModule.forRoot(),
        TimepickerModule.forRoot(),
        PaginationModule.forRoot()
    ],
    exports: [
        ToastrModule,
        ModalModule,
        BsDropdownModule,
        BsDatepickerModule,
        TimepickerModule,
        PaginationModule
    ]
})

export default class SharedModule {};