import { Injectable } from '@angular/core';
import { MatSnackBar } from "@angular/material/snack-bar";
import { BackendError } from "../interceptors/http-request-failure.interceptor";

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {
  constructor(private _snackBar: MatSnackBar) {
  }

  showSignInFailure(): void {
    this._snackBar.open("Sign-in failed", "Dismiss");
  }

  showRegisterSuccess(): void {
    this._snackBar.open("Register success. You will be redirected to Sign-in.", "Dismiss");
  }

  showRegisterFailure(errors: BackendError | undefined): void {
    this._snackBar.open(`Register failure. ${errors?.detail}`, "Dismiss", {duration: 0});
  }
}
