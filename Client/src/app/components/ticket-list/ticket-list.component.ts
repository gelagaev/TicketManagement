import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { select, Store } from "@ngrx/store";
import { TicketActions } from "../../store/actions";
import { TicketRecord } from "../../services/web-api-service-proxies";
import { selectAllTickets } from "../../store/reducers";
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
  public tickets$ = this.store.pipe(select(selectAllTickets));

  constructor(private store: Store<TicketRecord>) {
    this.store.dispatch(TicketActions.loadTickets());
  }
}
