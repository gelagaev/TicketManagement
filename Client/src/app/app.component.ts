import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgSwitch, NgSwitchCase, NgSwitchDefault } from '@angular/common';
import {
  GlobalLoadingIndicatorComponent
} from "./components/global-loading-indicator/global-loading-indicator.component";
import { LocalStorageService } from "./services/local-storage.service";
import { Store } from "@ngrx/store";
import { AuthActions } from "./store/actions";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
  standalone: true,
  imports: [NgSwitch, NgSwitchDefault, NgSwitchCase, RouterOutlet, GlobalLoadingIndicatorComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
  title = 'ticket-management';

  constructor(private store: Store, private localStorageService: LocalStorageService) {
    if (localStorageService.hasAuthToken()) {
      this.store.dispatch(AuthActions.me());
    }
  }
}
