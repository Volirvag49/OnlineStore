import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

export class HttpErrorInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(
                retry(1),
                catchError((error: HttpErrorResponse) => {
                    let errorMessage = '';

                    console.log('Error:|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||')
                    console.log(error);
                    console.log('|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||')

                    if (error.error instanceof ErrorEvent) {
                        // client-side error
                        errorMessage = `Error: ${error.error.message}`;
                    } else {
                        // server-side error

                        let message = null;

                        if (error.error.key) {
                            message = error.error.key;
                        }
                        else if (typeof(error.error) == "string") {
                            message = error.error;
                        }
                        else {
                            message = error.statusText;
                        }

                        errorMessage = `Error Code: ${error.status} Message: ${message}`;
                    }
                    return throwError(errorMessage);
                })
            )
    }
}
