import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentRecord, IUpdateTicketCommentRequest } from "../../services/web-api-service-proxies";
import { ControlErrorsPipe } from "../../pipes/control-errors.pipe";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { Edit } from "../../models/edit.model";

@Component({
  selector: 'tm-ticket-comment-list-item',
  standalone: true,
  imports: [CommonModule, ControlErrorsPipe, MatCardModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatButtonModule, MatIconModule],
  templateUrl: './ticket-comment-list-item.component.html',
  styleUrls: ['./ticket-comment-list-item.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TicketCommentListItemComponent {
  @Input({required: true})
  comment!: CommentRecord;

  @Input({required: true})
  isEditing = false;

  @Input({required: true})
  isAuthor = false;

  @Input({required: true})
  isTicketDone = false;

  @Output()
  delete = new EventEmitter<string>();

  @Output()
  save = new EventEmitter<IUpdateTicketCommentRequest>();

  @Output()
  edit = new EventEmitter<Edit>();

  form = new FormGroup({
    commentText: new FormControl<string>('', [Validators.required, Validators.maxLength(1000)]),
  })

  private get commentId(): string {
    return this.comment.id;
  }

  onEdit(): void {
    this.edit.emit({
      id: this.commentId,
      isEdit: true,
    });

    this.form.controls.commentText.setValue(this.comment.commentText!);
  }

  onDelete(): void {
    if (window.confirm("Delete comment?")) {
      this.delete.emit(this.commentId);
    }
  }

  onSave(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.save.emit({
      id: this.comment.id,
      commentText: this.form.controls.commentText.value!,
    });
  }

  onCancel(): void {
    this.edit.emit({
      id: this.commentId,
      isEdit: false,
    });
  }
}
