import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError, retry, catchError } from 'rxjs';
import { Mesa } from 'src/app/models/mesa';

@Injectable({
  providedIn: 'root',
})
export class ClienteService {
  //Injetando Cliente para requisição e serviço de Token para as requisições.
  constructor(private httpClient: HttpClient) {}

  //Url da API
  url = 'https://localhost:7198/api/Mesa/';

  getClienteByCPF(cpf: string, token: string): Observable<Mesa[]> {
    return this.httpClient
      .get<Mesa[]>(`${this.url}FiltrarPorCpf/${cpf}`, {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + token,
          'Content-Type': 'application/json',
        }),
      })
      .pipe(retry(2), catchError(this.handleError));
  }
  //Manuseio de erros
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage =
        `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
