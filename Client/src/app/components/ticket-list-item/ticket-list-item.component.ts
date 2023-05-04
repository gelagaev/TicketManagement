import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketRecord } from "../../services/web-api-service-proxies";
import { MatCardModule } from "@angular/material/card";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatListModule } from "@angular/material/list";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { ControlErrorsPipe } from "../../pipes/control-errors.pipe";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatExpansionModule } from "@angular/material/expansion";
import { TicketCommentListComponent } from "../ticket-comment-list/ticket-comment-list.component";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { TicketActions } from "../../store/actions";
import { tap } from "rxjs";

@Component({
  selector: 'tm-ticket-list-item',
  standalone: true,
  imports: [CommonModule, MatCardModule, ReactiveFormsModule, MatListModule, MatIconModule, MatButtonModule, ControlErrorsPipe, MatFormFieldModule, MatInputModule, MatExpansionModule, TicketCommentListComponent],
  templateUrl: './ticket-list-item.component.html',
  styleUrls: ['./ticket-list-item.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class TicketListItemComponent {
  constructor(private actions$: Actions,) {
  }

  @Input()
  ticket: TicketRecord = new TicketRecord();

  public isEdit = false;

  form = new FormGroup({
    subject: new FormControl<string>('', [Validators.required, Validators.maxLength(100)]),
    description: new FormControl<string>('', [Validators.required, Validators.maxLength(1000)]),
  })
  public isShowComments = false;

  onEdit(): void {
    this.form.controls.subject.setValue(this.ticket.subject ?? "");
    this.form.controls.description.setValue(this.ticket.description ?? "");
    this.isEdit = true;
  }

  ticketUpdateSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(TicketActions.updateSuccess),
      tap(() => {
        this.isEdit = false;
      })
    );
  }, {dispatch: false});

  onDelete(): void {
  }

  onSave(): void {
    this.isEdit = false;
  }

  onCancel(): void {
    this.isEdit = false;
  }
}
