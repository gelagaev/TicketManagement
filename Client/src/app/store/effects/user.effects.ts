import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, exhaustMap, filter, map, mergeMap, of } from "rxjs";
import { UserActions } from "../actions";
import { Injectable } from "@angular/core";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { WebApiServiceProxy } from "../../services/web-api-service-proxies";
import { select, Store } from "@ngrx/store";
import { selectUserInfo } from "../reducers/index.common";


@Injectable()
export class UserEffects {
  constructor(private actions$: Actions,
              private webApiServiceProxy: WebApiServiceProxy,
              private store: Store) {
  }

  signIn$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(UserActions.getUsers),
        mergeMap(() => this.store.pipe(select(selectUserInfo))),
        filter((userInfo) => userInfo.isAdministrator),
        exhaustMap(() =>
          this.webApiServiceProxy.user_ManagerList()
            .pipe(
              map(response => UserActions.getUsersSuccess({users: response.users ?? []})),
              catchError((error: BackendError) => of(UserActions.getUsersFailure(error)))
            ))
      );
    }
  );
}
