import { createFeatureSelector, createSelector } from "@ngrx/store";
import * as fromComment from "./comment.reducer";
import { selectUserInfo } from "./index.common";

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

export const selectTicketComments = (ticketId: string) => createSelector(
  selectAllComments,
  comments => comments.filter(comment => comment.ticketId === ticketId)
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

export const isCurrentUserCommentAuthor = (commentId: string) => createSelector(
  selectUserInfo,
  selectCommentEntities,
  (userInfo, comments) => comments[commentId]?.authorId === userInfo.id);
