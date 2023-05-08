import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  IAssignTicketRequest,
  ICloseTicketRequest,
  ICreateCommentRequest,
  IUpdateTicketRequest,
  TicketRecord,
  UserRecord
} from "../../services/web-api-service-proxies";
import { TicketListItemComponent } from "../ticket-list-item/ticket-list-item.component";
import { MatListModule } from "@angular/material/list";
import { MatCardModule } from "@angular/material/card";

@Component({
  selector: 'tm-ticket-list',
  standalone: true,
  imports: [CommonModule, TicketListItemComponent, MatListModule, MatCardModule],
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TicketListComponent {
  @Input({required: true})
  tickets: TicketRecord[] = [];

  @Input({required: true})
  userId!: string;

  @Input({required: true})
  isAdmin = false;

  @Input({required: true})
  isEdit = false;

  @Input({required: true})
  users: UserRecord[] = [];

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

  isAuthor(ticket: TicketRecord): boolean {
    return ticket.authorId === this.userId;
  };

  public trackByFn(index: number, {id}: TicketRecord): string {
    return id;
  }

  onDelete(ticketId: string) {
    this.delete.emit(ticketId);
  }

  onSave(request: IUpdateTicketRequest) {
    this.save.emit(request);
  }

  onCreateComment(request: ICreateCommentRequest) {
    this.createComment.emit(request);
  }

  onAssignTicket(request: IAssignTicketRequest) {
    this.assignTicket.emit(request);
  }

  onCloseTicket(request: ICloseTicketRequest) {
    this.closeTicket.emit(request);
  }

  onEditTicket(isEdit: boolean) {
    this.editTicket.emit(isEdit);
  }
}
