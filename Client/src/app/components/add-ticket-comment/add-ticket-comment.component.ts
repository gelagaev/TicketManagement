import { ChangeDetectionStrategy, Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlErrorsPipe } from "../../pipes/control-errors.pipe";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatInputModule } from "@angular/material/input";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";

@Component({
  selector: 'tm-add-ticket-comment',
  standalone: true,
  imports: [CommonModule, ControlErrorsPipe, MatButtonModule, MatCardModule, MatFormFieldModule, MatGridListModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './add-ticket-comment.component.html',
  styleUrls: ['./add-ticket-comment.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddTicketCommentComponent {
  @Output()
  createComment = new EventEmitter<string>();

  form = new FormGroup({
    commentText: new FormControl<string>('', [Validators.required, Validators.maxLength(1000)])
  })

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.createComment.emit(this.form.controls.commentText.value!);
    this.form.reset();
  }
}
