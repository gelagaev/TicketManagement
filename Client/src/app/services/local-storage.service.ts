import { Injectable } from '@angular/core';

@Injectable({
  providedIn: "root"
})
export class LocalStorageService {
  private ACCESS_TOKEN_KEY_NAME = "access-token";

  constructor() {
  }

  setAuthToken(token: string): void {
    localStorage.setItem(this.ACCESS_TOKEN_KEY_NAME, token);
  }

  hasAuthToken(): boolean {
    return !!this.getAuthToken();
  }

  getAuthToken(): string | null {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY_NAME)
  }
}
