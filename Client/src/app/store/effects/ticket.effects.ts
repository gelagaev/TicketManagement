import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, exhaustMap, map, of } from "rxjs";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { Injectable } from "@angular/core";
import { ServiceProxy } from "../../services/web-api-service-proxies";
import { TicketActions } from "../actions";

@Injectable()
export class TicketEffects {
  constructor(private actions$: Actions, private serviceProxy: ServiceProxy,) {
  }

  load$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(TicketActions.loadTickets),
        exhaustMap(() => {
          return this.serviceProxy.ticket_List(undefined) //todo fix
            .pipe(
              map(response => TicketActions.loadTicketSuccess({tickets: response.tickets!})), //todo
              catchError((error: BackendError) => of(TicketActions.loadTicketFailure(error)))
            );
        })
      );
    }
  );

  loadSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TicketActions.loadTicketSuccess),
      map(response => TicketActions.loadTicketSuccess({tickets: response.tickets}))
    ), {dispatch: false});
}
