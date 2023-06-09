import { enableProdMode, importProvidersFrom, isDevMode } from '@angular/core';
import { AppComponent } from './app/app.component';
import { AppRoutingModule, routes } from './app/app-routing.module';
import { bootstrapApplication, BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from "@angular/router";
import { provideStore } from "@ngrx/store";
import { provideEffects } from "@ngrx/effects";
import { AuthEffects } from "./app/store/effects/auth.effects";
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { environment } from "./environments/environment";
import { MAT_SNACK_BAR_DEFAULT_OPTIONS, MatSnackBarModule } from "@angular/material/snack-bar";
import { NavigationEffects } from "./app/store/effects/navigation.effects";
import { httpRequestFailureInterceptor } from "./app/interceptors/http-request-failure.interceptor";
import { authTokenInterceptor } from "./app/interceptors/auth-token.interceptor";
import { unauthorizedRequestInterceptor } from "./app/interceptors/unauthorized-request.interceptor";
import { TicketEffects } from "./app/store/effects/ticket.effects";
import { AUTH_BASE_URL } from "./app/services/auth-service-proxies";
import { WEB_API_BASE_URL } from "./app/services/web-api-service-proxies";
import { provideStoreDevtools } from "@ngrx/store-devtools";
import { ticketReducer } from "./app/store/reducers/ticket.reducer";
import { ticketFeatureName } from "./app/store/reducers/index.ticket";
import { commentFeatureName } from "./app/store/reducers/index.comment";
import { commentReducer } from "./app/store/reducers/comment.reducer";
import { CommentEffects } from "./app/store/effects/comment.effects";
import { MatInputModule } from "@angular/material/input";
import { CustomMaterialFormsMatcher } from "./app/helpers/CustomMaterialFormsMatcher";
import { ErrorStateMatcher } from "@angular/material/core";
import { commonFeatureName, commonReducer } from "./app/store/reducers/common.reducer";
import { globalLoadingIndicatorInterceptor } from "./app/interceptors/global-loading-indicator.interceptor";
import { userFeatureName } from "./app/store/reducers/user.index";
import { userReducer } from "./app/store/reducers/user.reducer";
import { UserEffects } from "./app/store/effects/user.effects";

if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
  providers: [
    {provide: AUTH_BASE_URL, useValue: environment.AUTH_BASE_URL},
    {provide: WEB_API_BASE_URL, useValue: environment.WEB_API_BASE_URL},
    {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: {duration: 3000}},
    {provide: ErrorStateMatcher, useClass: CustomMaterialFormsMatcher},
    importProvidersFrom(BrowserModule, AppRoutingModule, BrowserAnimationsModule, BrowserAnimationsModule, MatSnackBarModule, MatInputModule),
    provideRouter(routes),
    provideStore({[ticketFeatureName]: ticketReducer}),
    provideStore({[commentFeatureName]: commentReducer}),
    provideStore({[userFeatureName]: userReducer}),
    provideStore({[commonFeatureName]: commonReducer}),

    provideEffects([AuthEffects, NavigationEffects, TicketEffects, CommentEffects, UserEffects]),
    provideStoreDevtools({
      maxAge: 25, // Retains last 25 states
      logOnly: !isDevMode(), // Restrict extension to log-only mode
      autoPause: true, // Pauses recording actions and state changes when the extension window is not open
      trace: false, //  If set to true, will include stack trace for every dispatched action, so you can see it in trace tab jumping directly to that part of code
      traceLimit: 75, // maximum stack trace frames to be stored (in case trace option was provided as true)
    }),
    provideAnimations(),
    provideHttpClient(
      withInterceptors([
        httpRequestFailureInterceptor,
        authTokenInterceptor,
        unauthorizedRequestInterceptor,
        globalLoadingIndicatorInterceptor,
      ])
    )
  ]
}).catch(err => console.error(err));

