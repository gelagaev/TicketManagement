import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'tm-ticket-comment-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ticket-comment-list.component.html',
  styleUrls: ['./ticket-comment-list.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TicketCommentListComponent {
  constructor() {
    console.log("TicketCommentListComponent");
  }
}
