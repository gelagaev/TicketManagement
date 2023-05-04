import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { DASHBOARD, REGISTER, SIGN_IN } from "../app-routing.module";

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  constructor(private router: Router) {
  }

  async navigateToDashboard(): Promise<void> {
    await this.router.navigate(['/', DASHBOARD])
  }

  async navigateToSignIn(): Promise<void> {
    await this.router.navigate(['/', SIGN_IN])
  }

  async navigateToRegister(): Promise<void> {
    await this.router.navigate(['/', REGISTER])
  }
}
