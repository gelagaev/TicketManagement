import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { select, Store } from "@ngrx/store";
import { CommentRecord } from "../../services/web-api-service-proxies";
import { isEditingComment, selectAllComments } from "../../store/reducers/index.comment";
import { CommentActions } from "../../store/actions";
import { TicketListItemComponent } from "../ticket-list-item/ticket-list-item.component";
import { TicketCommentListItemComponent } from "../ticket-comment-list-item/ticket-comment-list-item.component";

@Component({
  selector: 'tm-ticket-comment-list',
  standalone: true,
  imports: [CommonModule, TicketListItemComponent, TicketCommentListItemComponent],
  templateUrl: './ticket-comment-list.component.html',
  styleUrls: ['./ticket-comment-list.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TicketCommentListComponent implements OnInit {
  @Input()
  ticketId: string = "";

  public comments$ = this.store.pipe(select(selectAllComments));

  constructor(private store: Store<CommentRecord>) {
  }

  ngOnInit(): void {
    this.store.dispatch(CommentActions.loadTicketComment({ticketId: this.ticketId}));
  }

  public trackByFn(index: number, {id}: CommentRecord): string {
    return id;
  }

  public isCommentEditing(id: string) {
    return this.store.pipe(select(isEditingComment(id)))
  }
}
