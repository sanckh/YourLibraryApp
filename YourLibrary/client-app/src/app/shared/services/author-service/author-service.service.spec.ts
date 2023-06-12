import { TestBed } from '@angular/core/testing';

import { AuthorServiceService } from './author-service.service';

describe('AuthorServiceService', () => {
  let service: AuthorServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthorServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
