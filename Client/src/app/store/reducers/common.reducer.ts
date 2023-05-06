import { CommonActions } from "../actions";
import { createReducer, createSelector, on } from "@ngrx/store";

export const commonFeatureName = 'common';

export interface State {
  showLoadingIndicator: boolean;
}

export const initialState: State = {
  showLoadingIndicator: false
};

export const commonReducer = createReducer(
  initialState,
  on(CommonActions.showLoadingIndicator, state => ({...state, showLoadingIndicator: true})),
  on(CommonActions.hideLoadingIndicator, state => ({...state, showLoadingIndicator: false})),
);

export const selectFeature = (state: State) => state;

export const getShowLoadingIndicator = createSelector(
  selectFeature,
  (state: State) => state.showLoadingIndicator
);
