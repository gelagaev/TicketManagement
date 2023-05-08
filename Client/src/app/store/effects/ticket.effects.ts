import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, concatMap, exhaustMap, map, of, withLatestFrom } from "rxjs";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { Injectable } from "@angular/core";
import {
  AssignTicketRequest,
  CloseTicketRequest,
  CreateTicketRequest,
  UpdateTicketRequest,
  WebApiServiceProxy
} from "../../services/web-api-service-proxies";
import { TicketActions } from "../actions";
import { select, Store } from "@ngrx/store";
import { selectUserFullName } from "../reducers/user.index";

@Injectable()
export class TicketEffects {
  constructor(private actions$: Actions, private webApiServiceProxy: WebApiServiceProxy, private store: Store) {
  }

  load$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(TicketActions.loadTickets),
        exhaustMap(() => {
          return this.webApiServiceProxy.ticket_List()
            .pipe(
              map(response => TicketActions.loadTicketSuccess({tickets: response.tickets ?? []})),
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
          return this.webApiServiceProxy.ticket_Create(new CreateTicketRequest(request))
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
          return this.webApiServiceProxy.tickets_Update(new UpdateTicketRequest(request))
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
          return this.webApiServiceProxy.tickets_Delete(request.ticketId)
            .pipe(
              map(() => TicketActions.deleteTicketSuccess({ticketId: request.ticketId})),
              catchError((error: BackendError) => of(TicketActions.deleteTicketFailure(error)))
            );
        })
      );
    }
  );

  assign$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(TicketActions.assignTicket),
        concatMap(action => of(action).pipe(withLatestFrom(this.store.pipe(select(selectUserFullName(action.managerId ?? "")))))),
        exhaustMap(([request, assignToFullName]) =>
          this.webApiServiceProxy.tickets_Assign(request.ticketId, new AssignTicketRequest(request))
            .pipe(
              map(() =>
                TicketActions.assignTicketSuccess({
                  id: request.ticketId,
                  changes: {
                    assignId: request.managerId,
                    assignToFullName: assignToFullName
                  }
                })),
              catchError((error: BackendError) => of(TicketActions.assignTicketFailure(error)))
            ))
      );
    }
  );

  close$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(TicketActions.closeTicket),
        exhaustMap(request => {
          return this.webApiServiceProxy.tickets_Close(request.ticketId, new CloseTicketRequest(request))
            .pipe(
              map(() => TicketActions.closeTicketSuccess({
                update: {
                  id: request.ticketId,
                  changes: {isDone: true}
                }
              })),
              catchError((error: BackendError) => of(TicketActions.closeTicketFailure(error)))
            );
        })
      );
    }
  );
}
