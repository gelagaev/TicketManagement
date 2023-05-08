import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from "@angular/material/card";
import { MatButtonModule } from "@angular/material/button";
import { select, Store } from "@ngrx/store";
import { AuthActions, TicketActions } from "../../store/actions";
import { SignInComponent } from "../sign-in/sign-in.component";
import { CreateTicketComponent } from "../create-ticket/create-ticket.component";
import { MatExpansionModule } from "@angular/material/expansion";
import { selectUserInfo } from "../../store/reducers/index.common";
import { TicketListPageComponent } from "../ticket-list-page/ticket-list-page.component";
import { ICreateTicketRequest } from "../../services/web-api-service-proxies";
import { Actions, ofType } from "@ngrx/effects";

@Component({
  selector: 'tm-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, SignInComponent, CreateTicketComponent, MatExpansionModule, TicketListPageComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DashboardComponent {
  userInfo$ = this.store.pipe(select(selectUserInfo));

  createTicketSuccess$ = this.actions$.pipe(
    ofType(TicketActions.createTicketSuccess),
  );

  constructor(private store: Store, private actions$: Actions) {
  }

  onLogout(): void {
    this.store.dispatch(AuthActions.logout());
  }

  onSubmit(request: ICreateTicketRequest) {
    this.store.dispatch(TicketActions.createTicket(request));
  }
}
