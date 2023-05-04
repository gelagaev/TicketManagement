import { CanActivateFn } from '@angular/router';
import { inject } from "@angular/core";
import { PermissionsService } from "../services/permissions.service";

export const authGuard: CanActivateFn = () => inject(PermissionsService).canActivateAuth();
