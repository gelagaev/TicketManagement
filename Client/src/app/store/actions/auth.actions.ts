import { createAction, props } from '@ngrx/store';
import {
  IRegisterRequest,
  ISignInRequest,
  RegisterResponse,
  SignInResponse
} from "../../services/auth-service-proxies";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { MeResponse } from "../../services/web-api-service-proxies";

export const me = createAction('[Auth] Me');
export const meSuccess = createAction('[Auth] Me Success', props<MeResponse>());
export const meFailure = createAction('[Auth] Me Success', props<BackendError>());

export const signIn = createAction('[Auth] SignIn', props<ISignInRequest>());
export const signInSuccess = createAction('[Auth] SignIn Success', props<SignInResponse>());
export const signInFailure = createAction('[Auth] SignIn Success', props<BackendError>());

export const register = createAction('[Auth] Register', props<IRegisterRequest>());
export const registerResult = createAction('[Auth] RegisterSuccess', props<RegisterResponse>());
export const registerFailure = createAction('[Auth] RegisterFailure', props<BackendError>());

export const logout = createAction('[Auth] Logout');
