import { Injectable } from '@angular/core';
import { LocalStorageService } from "./local-storage.service";
import { NavigationService } from "./navigation.service";

@Injectable({
  providedIn: "root"
})
export class PermissionsService {
  constructor(private localStorageService: LocalStorageService, private navigationService: NavigationService) {
  }

  async canActivateAuth(): Promise<boolean> {
    const hasToken = this.localStorageService.hasAuthToken();
    if (!hasToken) {
      await this.navigationService.navigateToSignIn();
    }
    return hasToken;
  }
}
