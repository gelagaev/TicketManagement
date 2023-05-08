import { Routes } from '@angular/router';
import { provideState } from "@ngrx/store";
import { authGuard } from "./guards/auth.guard";
import { ticketReducer } from "./store/reducers/ticket.reducer";
import { ticketFeatureName } from "./store/reducers/index.ticket";
import { commentFeatureName } from "./store/reducers/index.comment";
import { commentReducer } from "./store/reducers/comment.reducer";
import { commonFeatureName, commonReducer } from "./store/reducers/common.reducer";
import { userReducer } from "./store/reducers/user.reducer";
import { userFeatureName } from "./store/reducers/user.index";

export const DASHBOARD = "dashboard";
export const SIGN_IN = "sign-in";
export const REGISTER = "register";

export const routes: Routes = [
  {
    path: "",
    loadComponent: () =>
      import('./components/dashboard/dashboard.component').then(mod => mod.DashboardComponent),
    providers: [
      provideState(ticketFeatureName, ticketReducer),
      provideState(commentFeatureName, commentReducer),
      provideState(commonFeatureName, commonReducer),
      provideState(userFeatureName, userReducer),
    ],
  },
  {
    path: SIGN_IN,
    loadComponent: () =>
      import('./components/sign-in/sign-in.component').then(mod => mod.SignInComponent)
  },
  {
    path: REGISTER,
    loadComponent: () =>
      import('./components/register/register.component').then(mod => mod.RegisterComponent)
  },
  {
    path: DASHBOARD,
    loadComponent: () =>
      import('./components/dashboard/dashboard.component').then(mod => mod.DashboardComponent),
    canActivate: [authGuard]
  }
];

export class AppRoutingModule {
}
