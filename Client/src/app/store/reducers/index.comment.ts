import { createFeatureSelector, createSelector } from "@ngrx/store";
import * as fromComment from "./comment.reducer";

export const commentFeatureName = 'comment';
export const selectCommentState = createFeatureSelector<fromComment.State>(commentFeatureName);

export const selectCommentIds = createSelector(
  selectCommentState,
  fromComment.selectCommentIds
);
export const selectCommentEntities = createSelector(
  selectCommentState,
  fromComment.selectCommentEntities
);
export const selectAllComments = createSelector(
  selectCommentState,
  fromComment.selectAllComments
);
export const selectCommentTotal = createSelector(
  selectCommentState,
  fromComment.selectCommentTotal
);

export const selectEditingCommentIds = createSelector(
  selectCommentState,
    fromComment.getEditingCommentIds
);

export const isEditingComment = (checkId: string) => createSelector(
  selectEditingCommentIds,
  selectEditingCommentIds => selectEditingCommentIds.some(id => id === checkId)
);
