import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransporteServiceService {

  private meuObjeto = new BehaviorSubject<any>(null);

  constructor() { }

  setObjeto(obj: any) {
    this.meuObjeto.next(obj);
  }

  getObjeto(): Observable<any> {
    return this.meuObjeto.asObservable();
  }
}
