import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentRecord } from "../../services/web-api-service-proxies";
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
export class TicketCommentListComponent {
  @Input({required: true})
  ticketId!: string;

  @Input({required: true})
  isTicketDone = false;

  @Input({required: true})
  comments!: CommentRecord[];

  @Input({required: true})
  userId!: string;

  @Input({required: true})
  editingCommentIds!: string[];

  isAuthor(comment: CommentRecord): boolean {
    return comment.authorId === this.userId;
  }

  constructor() {
  }

  public trackByFn(index: number, {id}: CommentRecord): string {
    return id;
  }

  public isCommentEditing(commentId: string): boolean {
    return this.editingCommentIds.some(id => id === commentId);
  }
}
