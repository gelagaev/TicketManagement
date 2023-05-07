import { AfterViewInit, ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentRecord, TicketRecord, UserRecord } from "../../services/web-api-service-proxies";
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
import { Actions, ofType } from "@ngrx/effects";
import { CommentActions, TicketActions } from "../../store/actions";
import { tap } from "rxjs";
import { AddTicketCommentComponent } from "../add-ticket-comment/add-ticket-comment.component";
import { select, Store } from "@ngrx/store";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";
import { selectAllUsers } from "../../store/reducers/user.index";
import { MatOptionModule } from "@angular/material/core";
import { MatSelectModule } from "@angular/material/select";

@UntilDestroy()
@Component({
  selector: 'tm-ticket-list-item',
  standalone: true,
  imports: [CommonModule, MatCardModule, ReactiveFormsModule, MatListModule, MatIconModule, MatButtonModule, ControlErrorsPipe, MatFormFieldModule, MatInputModule, MatExpansionModule, TicketCommentListComponent, AddTicketCommentComponent, MatOptionModule, MatSelectModule],
  templateUrl: './ticket-list-item.component.html',
  styleUrls: ['./ticket-list-item.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class TicketListItemComponent implements AfterViewInit {
  constructor(private actions$: Actions, private store: Store<CommentRecord>) {
    this.actions$.pipe(
      untilDestroyed(this),
      ofType(TicketActions.updateTicketSuccess),
      tap(() => {
        this.isEdit = false;
      })
    ).subscribe();
  }

  @Input()
  ticket: TicketRecord = new TicketRecord();

  @Input()
  isAuthor: boolean | null = null;
  @Input()
  isAdmin: boolean | null = null;

  isEdit = false;

  users$ = this.store.pipe(select(selectAllUsers));

  form = new FormGroup({
    subject: new FormControl<string>('', [Validators.required, Validators.maxLength(100)]),
    description: new FormControl<string>('', [Validators.required, Validators.maxLength(1000)]),
  })
  public isShowComments = false;
  selectedManagerId: string = "-1";

  public trackByFn(index: number, {id}: UserRecord): string {
    return id;
  }

  onEdit(): void {
    this.form.controls.subject.setValue(this.ticket.subject ?? "");
    this.form.controls.description.setValue(this.ticket.description ?? "");
    this.isEdit = true;
  }

  onDelete(): void {
    if (window.confirm("Delete ticket?")) {
      this.store.dispatch(
        TicketActions.deleteTicket({ticketId: this.ticket.id})
      );
    }
  }

  onSave(): void {
    this.store.dispatch(
      TicketActions.updateTicket({
        subject: this.form.controls.subject.value!,
        description: this.form.controls.description.value!,
        id: this.ticket.id,
      })
    );
  }

  onCancel(): void {
    this.isEdit = false;
  }

  onCreateComment(commentText: string) {
    this.store.dispatch(
      CommentActions.createTicketComment({commentText, ticketId: this.ticket.id})
    );
  }

  showSaveAssignButton(): boolean {
    if (this.selectedManagerId === '-1') {
      return !!this.ticket.assignId
    } else {
      return this.selectedManagerId !== this.ticket.assignId;
    }
  }

  onAssignManager(): void {
    this.store.dispatch(TicketActions.assignTicket({
      managerId: this.selectedManagerId === '-1' ? undefined : this.selectedManagerId,
      ticketId: this.ticket.id
    }));
  }

  ngAfterViewInit(): void {
    if (this.ticket.assignId) {
      this.selectedManagerId = this.ticket.assignId;
    }
  }

  protected readonly onclose = onclose;

  onClose(): void {
    if (window.confirm("Close ticket?")) {
      this.store.dispatch(TicketActions.closeTicket({ticketId: this.ticket.id}));

    }
  }
}
