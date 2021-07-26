import { NgModule } from "@angular/core";
import { ToastrModule } from "ngx-toastr";
import {ModalModule} from "ngx-bootstrap/modal";

@NgModule({
    imports: [
        ToastrModule.forRoot(
            {positionClass: 'toast-bottom-right'}
        ),
        ModalModule.forRoot()
    ],
    exports: [
        ToastrModule,
        ModalModule,
    ]
})

export default class SharedModule {};