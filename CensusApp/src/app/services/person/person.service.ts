import { HttpClient,HttpParams, HttpHeaders,HttpInterceptor, HttpHandler, HttpRequest,HttpErrorResponse  } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Person } from 'src/app/components/person/person';
import { ActivatedRoute } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { LocalStorageService } from '../local-storage.service';
import { FamilyTree } from 'src/app/components/person/familytree';

const httpOptions ={
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  url = 'https://localhost:7266/v1/person';
 
  constructor(private http: HttpClient, private route: ActivatedRoute,private localStorageService: LocalStorageService) { }

setAuth() : Object{
  const httpOptionsAuth ={
    headers: new HttpHeaders({
      'Content-Type' : 'application/json',
      'Authorization' : 'Bearer '+this.localStorageService.get("token")
    })
  }
  return httpOptionsAuth;
}


  GetAll(): Observable<Person[]>{
    return this.http.get<Person[]>(`${this.url}/GetAll`, this.setAuth());
  }

  Create(person: Person) : Observable<any>{
    const apiUrl = `${this.url}/Create`;
    return this.http.post<any>(apiUrl, person, this.setAuth()).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error, "Error to create user!"))
    );
  }

  Authenticated() : Observable<any>{
    const apiUrl = `${this.url}/authenticated`;
    return this.http.get<any>(apiUrl, this.setAuth()).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error, "Error to Authenticate!"))
    ); 
  }


  GetById(personId: string): Observable<Person>{
    const apiUrl = `${this.url}/GetById?id=${personId}`;
    return this.http.get<Person>(apiUrl, this.setAuth());
  }

  GetFamilyTree(personId: string, level: number): Observable<FamilyTree>{
    return this.http.get<FamilyTree>(`${this.url}/GetFamilyTree?id=${personId}&level=${level}`, this.setAuth());
  }

  Update(person: Person) : Observable<any>{
    const apiUrl = `${this.url}/Update`;
    return this.http.put<Person>(apiUrl, person, this.setAuth())
  }

  GetByFilter(person: Person, pageNumber: Number, pageSize: Number) : Observable<any>{
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<Person[]>(`${this.url}/GetByFilter?pageNumber=${pageNumber}&pageSize=${pageSize}`, person, this.setAuth());
  }

  GetPercentualByRegion(person: Person) : Observable<any>{
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<Person[]>(`${this.url}/GetPercentualByRegion`, person, this.setAuth());
  }

  Delete(personId: string) : Observable<any>{
    const apiUrl = `${this.url}/Delete?id=${personId}`;
    return this.http.delete<string>(apiUrl, this.setAuth())
  }

  private handleError(error: HttpErrorResponse, customErrorMessage: string) {
    if (error.error instanceof ErrorEvent) {
      console.error('Ocorreu um erro:', error.error.message);
    } else {
      console.error(
        `Código do erro ${error.status}, ` +
        `erro: ${JSON.stringify(error.error)}`);
    }
    return throwError(customErrorMessage);
  }

  

}
