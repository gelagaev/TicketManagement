import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentRecord } from "../../services/web-api-service-proxies";
import { ControlErrorsPipe } from "../../pipes/control-errors.pipe";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { Store } from "@ngrx/store";
import { CommentActions } from "../../store/actions";
import { MatIconModule } from "@angular/material/icon";

@Component({
  selector: 'tm-ticket-comment-list-item',
  standalone: true,
  imports: [CommonModule, ControlErrorsPipe, MatCardModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatButtonModule, MatIconModule],
  templateUrl: './ticket-comment-list-item.component.html',
  styleUrls: ['./ticket-comment-list-item.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TicketCommentListItemComponent {
  @Input()
  comment: CommentRecord = new CommentRecord();

  @Input()
  isEditing = false;

  @Input()
  isAuthor: boolean | null = false;

  @Input()
  isTicketDone = false;

  form = new FormGroup({
    commentText: new FormControl<string>('', [Validators.required, Validators.maxLength(1000)]),
  })

  private get commentId(): string  { return this.comment.id; }

  constructor(private store: Store<CommentRecord>) {
  }

  onEdit(): void {
    this.store.dispatch(CommentActions.startEditTicketComment({commentId: this.comment.id}));
    this.form.controls.commentText.setValue(this.comment.commentText!);
  }

  onDelete(): void {
    if (window.confirm("Delete comment?")) {
      this.store.dispatch(CommentActions.deleteTicketComment({commentId: this.commentId}));
    }
  }

  onSave(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.store.dispatch(CommentActions.updateTicketComment({
      id: this.comment.id,
      commentText: this.form.controls.commentText.value!,
    }));
  }

  onCancel(): void {
    this.store.dispatch(CommentActions.endEditTicketComment({commentId: this.commentId}))
  }
}
