import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from "@angular/material/card";
import { MatButtonModule } from "@angular/material/button";
import { select, Store } from "@ngrx/store";
import { AuthActions } from "../../store/actions";
import { SignInComponent } from "../sign-in/sign-in.component";
import { CreateTicketComponent } from "../create-ticket/create-ticket.component";
import { MatExpansionModule } from "@angular/material/expansion";
import { selectUserInfo } from "../../store/reducers/index.common";
import { TicketListPageComponent } from "../ticket-list-page/ticket-list-page.component";

@Component({
  selector: 'tm-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, SignInComponent, CreateTicketComponent, MatExpansionModule, TicketListPageComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DashboardComponent {
  public userInfo$ = this.store.pipe(select(selectUserInfo));

  constructor(private store: Store) {
  }

  onLogout(): void {
    this.store.dispatch(AuthActions.logout());
  }
}
