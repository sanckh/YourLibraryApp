import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorComponentComponent } from './author-component.component';

describe('AuthorComponentComponent', () => {
  let component: AuthorComponentComponent;
  let fixture: ComponentFixture<AuthorComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthorComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthorComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
