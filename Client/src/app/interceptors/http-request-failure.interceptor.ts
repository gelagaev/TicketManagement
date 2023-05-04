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
    catchError(error => {
        if (error instanceof HttpErrorResponse) {
          if (error.status === 422) {
            return backendErrorParser.parseErrorResponse(error)
              .pipe(switchMap(backendError => throwError(() => backendError)));
          }
        }
        return throwError(() => ({code: "UNKNOWN_ERROR", detail: null} as BackendError));
      }
    )
  );
};
