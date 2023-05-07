import { AuthActions, CommonActions } from "../actions";
import { createReducer, createSelector, on } from "@ngrx/store";
import { IMeResponse } from "../../services/web-api-service-proxies";

export const commonFeatureName = 'common';

export interface State {
  showLoadingIndicator: boolean;
  userInfo: IMeResponse;
}

export const initialState: State = {
  showLoadingIndicator: false,
  userInfo: {
    id: "",
    fullName: undefined,
    isAdministrator: false,
    isClient: false,
    isManager: false
  }
};

export const commonReducer = createReducer(
  initialState,
  on(CommonActions.showLoadingIndicator, state => ({...state, showLoadingIndicator: true})),
  on(CommonActions.hideLoadingIndicator, state => ({...state, showLoadingIndicator: false})),
  on(AuthActions.meSuccess, (state, userInfo) => ({...state, userInfo: {...userInfo}})),
);

export const selectCommonFeature = (state: State) => state;

export const getShowLoadingIndicator = createSelector(
  selectCommonFeature,
  (state: State) => state.showLoadingIndicator
);

export const getUserInfo = createSelector(
  selectCommonFeature,
  (state: State) => state.userInfo
);
