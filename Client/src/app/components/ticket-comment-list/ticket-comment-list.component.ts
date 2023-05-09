import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentRecord, IUpdateTicketCommentRequest } from "../../services/web-api-service-proxies";
import { TicketListItemComponent } from "../ticket-list-item/ticket-list-item.component";
import { TicketCommentListItemComponent } from "../ticket-comment-list-item/ticket-comment-list-item.component";
import { Edit } from "../../models/edit.model";

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

  @Output()
  delete = new EventEmitter<string>();

  @Output()
  save = new EventEmitter<IUpdateTicketCommentRequest>();

  @Output()
  edit = new EventEmitter<Edit>();

  isAuthor(comment: CommentRecord): boolean {
    return comment.authorId === this.userId;
  }

  public trackByFn(index: number, {id}: CommentRecord): string {
    return id;
  }

  public isCommentEditing(commentId: string): boolean {
    return this.editingCommentIds.some(id => id === commentId);
  }

  onSave(request: IUpdateTicketCommentRequest) {
    this.save.emit(request);
  }

  onDelete(id: string) {
    this.delete.emit(id);
  }

  onEdit(edit: Edit) {
    this.edit.emit(edit)
  }
}
