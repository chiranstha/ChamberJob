/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SectorWiseJobComponent } from './sectorWiseJob.component';

describe('SectorWiseJobComponent', () => {
  let component: SectorWiseJobComponent;
  let fixture: ComponentFixture<SectorWiseJobComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SectorWiseJobComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SectorWiseJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
