import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from "@angular/core";
import { Store } from "@ngrx/store";
import { CommonActions } from "../store/actions";
import { finalize } from "rxjs";

export const globalLoadingIndicatorInterceptor: HttpInterceptorFn = (req, next) => {
  const store = inject(Store);
  store.dispatch(CommonActions.showLoadingIndicator());
  return next(req).pipe(
    finalize(() => store.dispatch(CommonActions.hideLoadingIndicator())),
  );
};
