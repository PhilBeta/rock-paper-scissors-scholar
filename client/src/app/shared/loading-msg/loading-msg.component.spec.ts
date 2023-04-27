import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingMsgComponent } from './loading-msg.component';

describe('LoadingMsgComponent', () => {
  let component: LoadingMsgComponent;
  let fixture: ComponentFixture<LoadingMsgComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoadingMsgComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoadingMsgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
