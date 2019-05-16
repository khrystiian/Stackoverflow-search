import { Injectable } from '@angular/core';
import { HttpErrorHandler, HandleError } from './http-error-handler.service';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SearchModel } from '../models/SearchModel';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root'
})
/**
* ENABLE CORS FOR ELASTICSEARCH.
* MODIFY elasticsearch.yml
*/
export class StackService {
  url = "https://localhost:44344/";  // URL to web api
  private handleError: HandleError;

  constructor(private http: HttpClient, httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('StackService');
  }

  search(search: SearchModel): Observable<any> {
    return this.http.post<SearchModel>(this.url + "api/stack", search, httpOptions).pipe(catchError(this.handleError('search', search)));
  }

}
