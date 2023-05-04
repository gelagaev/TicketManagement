import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { NavigationActions } from "../actions";
import { tap } from "rxjs";
import { NavigationService } from "../../services/navigation.service";

@Injectable()
export class NavigationEffects {
  constructor(private actions$: Actions,
              private navigationService: NavigationService) {
  }

  navigationToRegister$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(NavigationActions.navigateToRegister),
      tap(async () => {
        return await this.navigationService.navigateToRegister();
      })
    );
  }, {dispatch: false});

  navigationToSignIn$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(NavigationActions.navigateToSignIn),
      tap(async () => {
        return await this.navigationService.navigateToSignIn();
      })
    );
  }, {dispatch: false});
}
