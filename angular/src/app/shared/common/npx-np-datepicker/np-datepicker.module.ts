import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {ToNpPipe} from './to-np.pipe';
import {CommonModule} from '@angular/common';
import {CalendarModule} from 'primeng/calendar';
import {NpxNpDatePickerComponent} from './np-datepicker.component';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';


@NgModule({
    declarations: [
        ToNpPipe,
        NpxNpDatePickerComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        CalendarModule,
        BsDatepickerModule.forRoot()
    ],
    exports: [
        NpxNpDatePickerComponent
    ]
})
export class NepaliDatepickerModule {
}
