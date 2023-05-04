import { Routes } from '@angular/router';
import { SignInComponent } from "./components/sign-in/sign-in.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { RegisterComponent } from "./components/register/register.component";
import { ticketFeatureName } from "./store/reducers";
import { provideState } from "@ngrx/store";
import { authGuard } from "./guards/auth.guard";
import { ticketReducer } from "./store/reducers/ticket.reducer";

export const DASHBOARD = "dashboard";
export const SIGN_IN = "sign-in";
export const REGISTER = "register";

export const routes: Routes = [
  {path: SIGN_IN, component: SignInComponent},
  {path: REGISTER, component: RegisterComponent},
  {
    path: DASHBOARD, component: DashboardComponent, canActivate: [authGuard], providers:
      [
        provideState(ticketFeatureName, ticketReducer)
      ]
  }
];

export class AppRoutingModule {
}
