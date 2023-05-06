import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, switchMap, throwError } from "rxjs";
import { inject } from "@angular/core";
import { BackendErrorParser } from "../helpers/backend-error-parser";

export interface BackendError {
  code: string | null;
  detail: string | null;
}

export const httpRequestFailureInterceptor: HttpInterceptorFn = (req, next) => {
  const backendErrorParser = inject(BackendErrorParser);

  return next(req).pipe(
    catchError(response => {
        if (response instanceof HttpErrorResponse) {
          if (response.status === 422) {
            return backendErrorParser.parseErrorResponse(response)
              .pipe(switchMap(backendError => throwError(() => backendError)));
          }
          if (response.status === 0 && !navigator.onLine) {
            return throwError(() => ({code: "OFFLINE_ERROR"} as BackendError));
          }
        }
        return throwError(() => ({code: "UNKNOWN_ERROR"} as BackendError));
      }
    )
  );
};
