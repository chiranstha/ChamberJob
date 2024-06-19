/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TopJobDemandComponent } from './topJobDemand.component';

describe('TopJobDemandComponent', () => {
  let component: TopJobDemandComponent;
  let fixture: ComponentFixture<TopJobDemandComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TopJobDemandComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopJobDemandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
