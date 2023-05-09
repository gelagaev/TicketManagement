import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketCommentListComponent } from "../ticket-comment-list/ticket-comment-list.component";
import { Observable, of } from "rxjs";
import { CommentRecord, IUpdateTicketCommentRequest } from "../../services/web-api-service-proxies";
import { select, Store } from "@ngrx/store";
import { selectEditingCommentIds, selectTicketComments } from "../../store/reducers/index.comment";
import { selectCurrentUserId } from "../../store/reducers/index.ticket";
import { CommentActions } from "../../store/actions";
import { Edit } from "../../models/edit.model";

@Component({
  selector: 'tm-ticket-comment-list-page',
  standalone: true,
  imports: [CommonModule, TicketCommentListComponent],
  templateUrl: './ticket-comment-list-page.component.html',
  styleUrls: ['./ticket-comment-list-page.component.less']
})
export class TicketCommentListPageComponent implements OnInit {
  @Input({required: true})
  ticketId!: string;

  @Input({required: true})
  isTicketDone = false;

  comments$: Observable<CommentRecord[]> = of([]);

  userId$ = this.store.pipe(select(selectCurrentUserId));
  editingCommentsIds$ = this.store.pipe(select(selectEditingCommentIds))


  constructor(private store: Store<CommentRecord>) {
  }

  ngOnInit(): void {
    this.comments$ = this.store.pipe(select(selectTicketComments(this.ticketId)));
    this.store.dispatch(CommentActions.loadTicketComment({ticketId: this.ticketId}));
  }

  onEdit(edit: Edit) {
    this.store.dispatch(CommentActions.editTicketComment(edit))
  }

  onDelete(commentId: string) {
    this.store.dispatch(CommentActions.deleteTicketComment({commentId}))
  }

  onSave(request: IUpdateTicketCommentRequest) {
    this.store.dispatch(CommentActions.updateTicketComment(request))
  }
}
