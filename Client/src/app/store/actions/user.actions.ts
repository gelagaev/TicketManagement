import { createAction, props } from '@ngrx/store';
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { UserRecord } from "../../services/web-api-service-proxies";

export const getUsers = createAction('[Users] Get');
export const getUsersSuccess = createAction('[Users] Get Success', props<{users: UserRecord[] }>());
export const getUsersFailure = createAction('[Users] Me Failure', props<BackendError>());
