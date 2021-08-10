import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";
import { ToastrModule } from "ngx-toastr";
import {ModalModule} from "ngx-bootstrap/modal";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgxSpinnerModule } from "ngx-spinner";
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CollapseModule } from 'ngx-bootstrap/collapse';
@NgModule({
    imports: [
        ToastrModule.forRoot(
            {positionClass: 'toast-bottom-right'}
        ),
        ModalModule.forRoot(),
        BsDropdownModule.forRoot(),
        BsDatepickerModule.forRoot(),
        TimepickerModule.forRoot(),
        PaginationModule.forRoot(),
        NgxSpinnerModule,
        BrowserAnimationsModule,
        TabsModule.forRoot(),
        CollapseModule.forRoot()
    ],
    exports: [
        ToastrModule,
        ModalModule,
        BsDropdownModule,
        BsDatepickerModule,
        TimepickerModule,
        PaginationModule,
        NgxSpinnerModule,
        TabsModule,
        BrowserAnimationsModule,
        CollapseModule
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export default class SharedModule {};