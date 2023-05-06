import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatGridListModule } from "@angular/material/grid-list";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { select, Store } from "@ngrx/store";
import { showLoadingIndicator } from "../../store/reducers/index.common";

@Component({
  selector: 'tm-global-loading-indicator',
  standalone: true,
  imports: [CommonModule, MatGridListModule, MatProgressSpinnerModule],
  templateUrl: './global-loading-indicator.component.html',
  styleUrls: ['./global-loading-indicator.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GlobalLoadingIndicatorComponent {
  constructor(private store: Store) {
  }
  public isVisible$ = this.store.pipe(select(showLoadingIndicator));
}
