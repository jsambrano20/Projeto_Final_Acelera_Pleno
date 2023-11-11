import { TestBed } from '@angular/core/testing';

import { TransporteServiceService } from './transporte-service.service';

describe('TransporteServiceService', () => {
  let service: TransporteServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TransporteServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
