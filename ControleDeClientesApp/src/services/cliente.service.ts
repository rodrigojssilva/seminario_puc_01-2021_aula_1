import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from 'src/models/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  }

  constructor(private http: HttpClient) { 
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/Clientes/';
  }

  getClientes(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(this.myAppUrl + this.myApiUrl) // https://localhost:44308/api/Clientes
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getCliente(clienteId: number): Observable<Cliente> {
      return this.http.get<Cliente>(this.myAppUrl + this.myApiUrl + clienteId) // https://localhost:44308/api/Clientes/1
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  saveCliente(cliente: Cliente): Observable<Cliente> {
      return this.http.post<Cliente>(this.myAppUrl + this.myApiUrl, JSON.stringify(cliente), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  updateCliente(clienteId: number, cliente: Cliente): Observable<Cliente> {
      return this.http.put<Cliente>(this.myAppUrl + this.myApiUrl + clienteId, JSON.stringify(cliente), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  deleteCliente(clienteId: number): Observable<Cliente> {
      return this.http.delete<Cliente>(this.myAppUrl + this.myApiUrl + clienteId)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  errorHandler(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
