import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { select, Store } from "@ngrx/store";
import { TicketActions } from "../../store/actions";
import { TicketRecord } from "../../services/web-api-service-proxies";
import { TicketListItemComponent } from "../ticket-list-item/ticket-list-item.component";
import { MatListModule } from "@angular/material/list";
import { MatCardModule } from "@angular/material/card";
import { isCurrentUserTicketAuthor, selectAllTickets } from "../../store/reducers/index.ticket";
import { Observable } from "rxjs";

@Component({
  selector: 'tm-ticket-list',
  standalone: true,
  imports: [CommonModule, TicketListItemComponent, MatListModule, MatCardModule],
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TicketListComponent {
  tickets$ = this.store.pipe(select(selectAllTickets));

  public isAuthor$(ticketId: string): Observable<boolean> {
    return this.store.pipe(select(isCurrentUserTicketAuthor(ticketId)));
  }

  constructor(private store: Store<TicketRecord>) {
    this.store.dispatch(TicketActions.loadTickets());
  }

  public trackByFn(index: number, {id}: TicketRecord): string {
    return id;
  }
}
