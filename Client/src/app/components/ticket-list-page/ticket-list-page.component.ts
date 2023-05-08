import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketListComponent } from "../ticket-list/ticket-list.component";
import { CommentActions, TicketActions, UserActions } from "../../store/actions";
import { select, Store } from "@ngrx/store";
import {
  IAssignTicketRequest,
  ICloseTicketRequest,
  ICreateCommentRequest,
  IUpdateTicketRequest,
  TicketRecord,
  UserRecord
} from "../../services/web-api-service-proxies";
import { isCurrentUserAdmin, selectAllTickets, selectCurrentUserId } from "../../store/reducers/index.ticket";
import { Observable, tap } from "rxjs";
import { selectAllUsers } from "../../store/reducers/user.index";
import { Actions, ofType } from "@ngrx/effects";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";

@UntilDestroy()
@Component({
  selector: 'tm-ticket-list-page',
  standalone: true,
  imports: [CommonModule, TicketListComponent],
  templateUrl: './ticket-list-page.component.html',
  styleUrls: ['./ticket-list-page.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TicketListPageComponent {
  tickets$ = this.store.pipe(select(selectAllTickets));
  isEdit = false;

  constructor(private store: Store<TicketRecord>, private actions$: Actions) {
    this.store.dispatch(TicketActions.loadTickets());
    this.store.dispatch(UserActions.getUsers());
    this.actions$.pipe(
      untilDestroyed(this),
      ofType(TicketActions.updateTicketSuccess),
      tap(() => {
        this.isEdit = false;
      })).subscribe();
  }

  get userId$(): Observable<string> {
    return this.store.pipe(select(selectCurrentUserId));
  }

  get users$(): Observable<UserRecord[]> {
    return this.store.pipe(select(selectAllUsers));
  }

  get isAdmin$(): Observable<boolean> {
    return this.store.pipe(select(isCurrentUserAdmin));
  }

  onDelete(ticketId: string) {
    if (window.confirm("Delete ticket?")) {
      this.store.dispatch(TicketActions.deleteTicket({ticketId}));
    }
  }

  onSave(request: IUpdateTicketRequest) {
    this.store.dispatch(TicketActions.updateTicket(request));
  }

  onCreateComment(request: ICreateCommentRequest) {
    this.store.dispatch(
      CommentActions.createTicketComment(request)
    );
  }

  onAssignTicket(request: IAssignTicketRequest) {
    this.store.dispatch(TicketActions.assignTicket(request));
  }

  onCloseTicket(request: ICloseTicketRequest) {
    this.store.dispatch(TicketActions.closeTicket(request));
  }

  onEditTicket(isEdit: boolean) {
    this.isEdit = isEdit;
  }
}
