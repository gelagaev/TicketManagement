import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from "@angular/core";
import { LocalStorageService } from "../services/local-storage.service";

export const authTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const token = inject(LocalStorageService).getAuthToken();
  if (!token) return next(req);

  req = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });

  return next(req);
};
