import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectUserCountryComponent } from './select-user-country.component';

describe('SelectUserCountryComponent', () => {
  let component: SelectUserCountryComponent;
  let fixture: ComponentFixture<SelectUserCountryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectUserCountryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SelectUserCountryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
