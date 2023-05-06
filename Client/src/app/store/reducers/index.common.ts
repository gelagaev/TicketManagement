import { createFeatureSelector, createSelector } from "@ngrx/store";
import * as fromCommon from "./common.reducer";
import { commonFeatureName } from "./common.reducer";

export const selectCommonState = createFeatureSelector<fromCommon.State>(commonFeatureName);

export const selectEditingCommentIds = createSelector(
  selectCommonState,
  fromCommon.getShowLoadingIndicator
);

export const showLoadingIndicator = createSelector(
  selectEditingCommentIds,
  showLoadingIndicator => showLoadingIndicator);
