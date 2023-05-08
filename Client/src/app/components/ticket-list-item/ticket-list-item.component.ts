import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  IAssignTicketRequest,
  ICloseTicketRequest,
  ICreateCommentRequest,
  IUpdateTicketRequest,
  TicketRecord,
  UserRecord
} from "../../services/web-api-service-proxies";
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
import { AddTicketCommentComponent } from "../add-ticket-comment/add-ticket-comment.component";
import { UntilDestroy } from "@ngneat/until-destroy";
import { MatOptionModule } from "@angular/material/core";
import { MatSelectModule } from "@angular/material/select";
import { TicketCommentListPageComponent } from "../ticket-comment-list-page/ticket-comment-list-page.component";

@UntilDestroy()
@Component({
  selector: 'tm-ticket-list-item',
  standalone: true,
  imports: [CommonModule, MatCardModule, ReactiveFormsModule, MatListModule, MatIconModule, MatButtonModule, ControlErrorsPipe, MatFormFieldModule, MatInputModule, MatExpansionModule, TicketCommentListComponent, AddTicketCommentComponent, MatOptionModule, MatSelectModule, TicketCommentListPageComponent],
  templateUrl: './ticket-list-item.component.html',
  styleUrls: ['./ticket-list-item.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class TicketListItemComponent implements OnInit {
  @Input({required: true})
  ticket!: TicketRecord;

  @Input({required: true})
  users: UserRecord[] = [];

  @Input({required: true})
  isAuthor = false;

  @Input({required: true})
  isAdmin = false;

  @Input({required: true})
  isEdit = false;

  @Output()
  delete = new EventEmitter<string>();

  @Output()
  save = new EventEmitter<IUpdateTicketRequest>();

  @Output()
  createComment = new EventEmitter<ICreateCommentRequest>();

  @Output()
  assignTicket = new EventEmitter<IAssignTicketRequest>();

  @Output()
  closeTicket = new EventEmitter<ICloseTicketRequest>();

  @Output()
  editTicket = new EventEmitter<boolean>();

  ngOnInit(): void {
    if (this.ticket.assignId) {
      this.selectedManagerId = this.ticket.assignId;
    }
  }

  get ticketId(): string {
    return this.ticket.id
  }

  form = new FormGroup({
    subject: new FormControl<string>('', [Validators.required, Validators.maxLength(100)]),
    description: new FormControl<string>('', [Validators.required, Validators.maxLength(1000)]),
  })

  isShowComments = false;

  selectedManagerId = "-1";

  trackByFn(index: number, {id}: UserRecord): string {
    return id;
  }

  onEdit(): void {
    this.form.controls.subject.setValue(this.ticket.subject ?? "");
    this.form.controls.description.setValue(this.ticket.description ?? "");
    this.editTicket.emit(true);
  }

  onDelete(): void {
    this.delete.emit(this.ticketId);
  }

  onSave(): void {
    this.save.emit({
      subject: this.form.controls.subject.value!,
      description: this.form.controls.description.value!,
      id: this.ticketId,
    });
  }

  onCancel(): void {
    this.editTicket.emit(false);
  }

  onCreateComment(commentText: string) {
    this.createComment.emit({
      ticketId: this.ticketId,
      commentText: commentText
    });
  }

  showSaveAssignButton(): boolean {
    if (this.selectedManagerId === '-1') {
      return !!this.ticket.assignId
    } else {
      return this.selectedManagerId !== this.ticket.assignId;
    }
  }

  onAssignManager(): void {
    this.assignTicket.emit({
      managerId: this.selectedManagerId === '-1' ? undefined : this.selectedManagerId,
      ticketId: this.ticketId
    });
  }

  onClose(): void {
    if (window.confirm("Close ticket?")) {
      this.closeTicket.emit({ticketId: this.ticketId})
    }
  }
}
