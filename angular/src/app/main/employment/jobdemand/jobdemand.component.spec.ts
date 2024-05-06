/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { JobdemandComponent } from './jobdemand.component';

describe('JobdemandComponent', () => {
  let component: JobdemandComponent;
  let fixture: ComponentFixture<JobdemandComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JobdemandComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JobdemandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
