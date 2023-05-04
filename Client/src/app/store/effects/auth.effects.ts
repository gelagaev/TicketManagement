import { Actions, createEffect, ofType } from '@ngrx/effects';
import { RegisterRequest, ServiceProxy, SignInRequest } from "../../services/auth-service-proxies";
import { catchError, debounceTime, EMPTY, exhaustMap, filter, map, of, tap } from "rxjs";
import { AuthActions } from "../actions";
import { Injectable } from "@angular/core";
import { LocalStorageService } from "../../services/local-storage.service";
import { SnackBarService } from "../../services/snack-bar.service";
import { NavigationService } from "../../services/navigation.service";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";

@Injectable()
export class AuthEffects {
  constructor(private actions$: Actions,
              private serviceProxy: ServiceProxy,
              private localStorageService: LocalStorageService,
              private snackBarService: SnackBarService,
              private navigationService: NavigationService) {
  }

  signIn$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(AuthActions.signIn),
        exhaustMap((payload) => {
          return this.serviceProxy.auth_SignIn(new SignInRequest(payload))
            .pipe(
              map(response => AuthActions.signInResult(response)),
              catchError(() => EMPTY)
            );
        })
      );
    }
  );

  register$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(AuthActions.register),
        exhaustMap((payload) => {
          return this.serviceProxy.auth_Register(new RegisterRequest(payload))
            .pipe(
              map(response => AuthActions.registerResult(response)),
              catchError((error: BackendError) => of(AuthActions.registerFailure(error)))
            );
        })
      );
    }
  );

  signInResultSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.signInResult),
      filter(({success, token}) => !!success && !!token),
      tap(async ({token}) => {
        this.localStorageService.setAuthToken(token!);
        await this.navigationService.navigateToDashboard();
      })
    );
  }, {dispatch: false});

  signInResultFailure$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.signInResult),
      filter(({success}) => !success),
      tap(async ({token}) =>
        this.snackBarService.showSignInFailure())
    );
  }, {dispatch: false});

  registerResultSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AuthActions.registerResult),
      filter(({success, errors}) => !!success && !errors),
      tap(async ({success}) =>
        this.snackBarService.showRegisterSuccess()),
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
}
