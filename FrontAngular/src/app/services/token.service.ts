import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { TokenModel } from '../models/token_model';
import { Observable, catchError, retry, throwError } from 'rxjs';
import { Tokenretorno } from '../models/tokenretorno';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor(private httpClient: HttpClient) {

  }
  url = 'https://localhost:7198/api/Token/autenticarAngular';
  token = {
    clienteId: 'abacaxi123',
    clienteSecret: 'segredodoabacaxi',
  } as TokenModel;
  //Headers
  httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) }

  //Chamada para obter token
    getToken(): Observable<Tokenretorno> {
    return this.httpClient.post<Tokenretorno>(this.url, JSON.stringify(this.token), this.httpOptions).pipe(
      retry(2), catchError(this.handleError)
    )
  }


  handleError(error: HttpErrorResponse) {
    let errorMessage = ''; if (error.error instanceof ErrorEvent) { // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else { // Erro ocorreu no lado do servidor
      errorMessage = `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    } console.log(errorMessage); return throwError(errorMessage);
  };

}
