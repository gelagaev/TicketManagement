import { createAction, props } from '@ngrx/store';
import {
  IRegisterRequest,
  ISignInRequest,
  RegisterResponse,
  SignInResponse
} from "../../services/auth-service-proxies";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";

export const signIn = createAction('[Auth] SignIn', props<ISignInRequest>());
export const signInResult = createAction('[Auth] SignInSuccess', props<SignInResponse>());
export const register = createAction('[Auth] Register', props<IRegisterRequest>());
export const registerResult = createAction('[Auth] RegisterSuccess', props<RegisterResponse>());
export const registerFailure = createAction('[Auth] RegisterFailure', props<BackendError>());
export const logout = createAction('[Auth] Logout');
