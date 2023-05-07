import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, debounceTime, exhaustMap, filter, map, of, tap } from "rxjs";
import { AuthActions } from "../actions";
import { Injectable } from "@angular/core";
import { LocalStorageService } from "../../services/local-storage.service";
import { SnackBarService } from "../../services/snack-bar.service";
import { NavigationService } from "../../services/navigation.service";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { AuthServiceProxy, RegisterRequest, SignInRequest } from "../../services/auth-service-proxies";
import { WebApiServiceProxy } from "../../services/web-api-service-proxies";


@Injectable()
export class AuthEffects {
  constructor(private actions$: Actions,
              private authServiceProxy: AuthServiceProxy,
              private localStorageService: LocalStorageService,
              private snackBarService: SnackBarService,
              private navigationService: NavigationService,
              private webApiServiceProxy: WebApiServiceProxy) {
  }

  signIn$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(AuthActions.signIn),
        exhaustMap((payload) => {
          return this.authServiceProxy.auth_SignIn(new SignInRequest(payload))
            .pipe(
              map(response => AuthActions.signInSuccess(response)),
              catchError((error: BackendError) => of(AuthActions.signInFailure(error)))
            );
        })
      );
    }
  );

  signInResultSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.signInSuccess),
      filter(({success, token}) => !!success && !!token),
      tap(async ({token}) => {
        this.localStorageService.setAuthToken(token!);
        await this.navigationService.navigateToDashboard();
      })
    );
  }, {dispatch: false});


  signInResultFailure$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.signInSuccess),
      filter(({success}) => !success),
      tap(async ({token}) =>
        this.snackBarService.showSignInFailure())
    );
  }, {dispatch: false});

  me = createEffect(() => {
      return this.actions$.pipe(
        ofType(AuthActions.me, AuthActions.signInSuccess),
        exhaustMap(() => {
          return this.webApiServiceProxy.user_Me()
            .pipe(
              map(response => AuthActions.meSuccess(response)),
              catchError((error: BackendError) => of(AuthActions.meFailure(error)))
            );
        })
      );
    }
  );

  register$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(AuthActions.register),
        exhaustMap((payload) => {
          return this.authServiceProxy.auth_Register(new RegisterRequest(payload))
            .pipe(
              map(response => AuthActions.registerResult(response)),
              catchError((error: BackendError) => of(AuthActions.registerFailure(error)))
            );
        })
      );
    }
  );

  registerResultSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.registerResult),
      filter(({success, errors}) => !!success && !errors),
      tap(async ({success}) => this.snackBarService.showRegisterSuccess()),
      debounceTime(2000),
      tap(async () => await this.navigationService.navigateToSignIn())
    );
  }, {dispatch: false});

  registerResultFailure$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.registerFailure),
      tap(async (error: BackendError) =>
        this.snackBarService.showRegisterFailure(error)),
    );
  }, {dispatch: false});

  logout$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.logout),
      tap(async () => {
        this.localStorageService.removeAuthToken();
        await this.navigationService.navigateToSignIn();
      })
    );
  }, {dispatch: false});
}
