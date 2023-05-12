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
import {
  isCurrentUserAdmin,
  selectAllTickets,
  selectCurrentUserId,
  selectEditingTicketIds
} from "../../store/reducers/index.ticket";
import { Observable } from "rxjs";
import { selectAllUsers } from "../../store/reducers/user.index";
import { UntilDestroy } from "@ngneat/until-destroy";
import { Edit } from "../../models/edit.model";

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
  editingTicketIds$ = this.store.pipe(select(selectEditingTicketIds));

  constructor(private store: Store<TicketRecord>) {
    this.store.dispatch(TicketActions.loadTickets());
    this.store.dispatch(UserActions.getUsers());
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

  onTicket(request: IAssignTicketRequest) {
    this.store.dispatch(TicketActions.assignTicket(request));
  }

  onClose(request: ICloseTicketRequest) {
    this.store.dispatch(TicketActions.closeTicket(request));
  }

  onEdit(edit: Edit) {
    const action = edit.isEdit ?
      TicketActions.startEditTicket({ticketId: edit.id}) :
      TicketActions.endEditTicket({ticketId: edit.id});
    this.store.dispatch(action);
  }
}
