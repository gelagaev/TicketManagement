import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketListComponent } from "../ticket-list/ticket-list.component";

@Component({
  selector: 'tm-dashboard',
  standalone: true,
  imports: [CommonModule, TicketListComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DashboardComponent {
  constructor() {
  }
}
