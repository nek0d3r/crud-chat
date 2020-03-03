import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SphereDialogComponent } from './sphere-dialog.component';

describe('SphereDialogComponent', () => {
  let component: SphereDialogComponent;
  let fixture: ComponentFixture<SphereDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SphereDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SphereDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
