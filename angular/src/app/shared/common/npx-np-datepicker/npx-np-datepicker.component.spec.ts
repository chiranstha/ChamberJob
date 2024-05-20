import {ComponentFixture, TestBed} from '@angular/core/testing';

import {NpxNpDatePickerComponent} from './np-datepicker.component';

describe('NpxNpDatepickerComponent', () => {
    let component: NpxNpDatePickerComponent;
    let fixture: ComponentFixture<NpxNpDatePickerComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [NpxNpDatePickerComponent]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(NpxNpDatePickerComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
