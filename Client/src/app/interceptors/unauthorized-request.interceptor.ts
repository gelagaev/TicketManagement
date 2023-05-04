import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from "rxjs";
import { AuthActions } from "../store/actions";
import { inject } from "@angular/core";
import { Store } from "@ngrx/store";

export const unauthorizedRequestInterceptor: HttpInterceptorFn = (req, next) => {
  const store = inject(Store);
  return next(req).pipe(
    catchError(error => {
      if (error instanceof HttpErrorResponse) {
        const response = error;
        if (response.status === 401 || response.status === 403) {
          store.dispatch(AuthActions.logout());
        }
      }
      return throwError(error);
    }),
  );
  ;
};
