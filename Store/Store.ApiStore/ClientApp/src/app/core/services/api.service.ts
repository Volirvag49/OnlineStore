import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable()
export class ApiService {
    constructor(
        private http: HttpClient,
    ) { }

    get(path: string, params: HttpParams = new HttpParams()): Observable<any> {
        return this.http.get(`${environment.api_url}${path}`, { params });
    }

    put(path: string, body: Object = {}): Observable<any> {
        return this.http.put(
            `${environment.api_url}${path}`,
            JSON.stringify(body));
    }

    post(path: string, body: Object): Observable<any> {
        return this.http.post(
            `${environment.api_url}${path}`,
            JSON.stringify(body));
    }

    delete(path): Observable<any> {
        return this.http.delete(
            `${environment.api_url}${path}`);
    }
}
