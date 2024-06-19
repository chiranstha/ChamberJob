/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AddressWiseJobDemandComponent } from './addressWiseJobDemand.component';

describe('AddressWiseJobDemandComponent', () => {
  let component: AddressWiseJobDemandComponent;
  let fixture: ComponentFixture<AddressWiseJobDemandComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddressWiseJobDemandComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressWiseJobDemandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
