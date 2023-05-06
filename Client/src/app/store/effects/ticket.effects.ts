import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, exhaustMap, map, of } from "rxjs";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { Injectable } from "@angular/core";
import { CreateTicketRequest, ServiceProxy, UpdateTicketRequest } from "../../services/web-api-service-proxies";
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

  create$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(TicketActions.createTicket),
        exhaustMap(request => {
          return this.serviceProxy.ticket_Create(new CreateTicketRequest(request))
            .pipe(
              map(response => TicketActions.createTicketSuccess(response.ticket)),
              catchError((error: BackendError) => of(TicketActions.createTicketFailure(error)))
            );
        })
      );
    }
  );

  update$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(TicketActions.updateTicket),
        exhaustMap(request => {
          return this.serviceProxy.tickets_Update(new UpdateTicketRequest(request))
            .pipe(
              map(response => TicketActions.updateTicketSuccess({
                update: {
                  id: response.ticket.id,
                  changes: {...response.ticket}
                }
              })),
              catchError((error: BackendError) => of(TicketActions.updateTicketFailure(error)))
            );
        })
      );
    }
  );

  delete$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(TicketActions.deleteTicket),
        exhaustMap(request => {
          return this.serviceProxy.tickets_Delete(request.ticketId)
            .pipe(
              map(response => TicketActions.deleteTicketSuccess({ticketId: request.ticketId})),
              catchError((error: BackendError) => of(TicketActions.deleteTicketFailure(error)))
            );
        })
      );
    }
  );
}
