import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, retry, throwError } from 'rxjs';
import { Pedidos } from 'src/app/models/pedidos';
import { RealizarPedido } from 'src/app/models/realizar-pedido';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

  constructor(private httpClient: HttpClient) { }

  url = 'https://localhost:7198/api/Pedido/';

  getPedidosByMesaCPF( cpf:string,idMesa:string, token:string):Observable<Pedidos[]>{
    return this.httpClient.get<Pedidos[]>(`${this.url}FiltrarPorMesaCliente/${cpf}/${idMesa}`, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token,
        'Content-Type': 'application/json',
      }),
    })
    .pipe(retry(2), catchError(this.handleError));
  }

  postPedido(pedido: RealizarPedido, token: string): Observable<Pedidos> {
    return this.httpClient.post<Pedidos>(`${this.url}Incluir`, pedido, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token,
        'Content-Type': 'application/json',
      }),
    }).pipe(retry(2), catchError(this.handleError));
  }

  cancelarPedido(id:string, token: string): Observable<Pedidos> {
    return this.httpClient.put<Pedidos>(`${this.url}Cancelado/${id}`,null, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token,
        'Content-Type': 'application/json',
      }),
    }).pipe(retry(2), catchError(this.handleError));
  }

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
