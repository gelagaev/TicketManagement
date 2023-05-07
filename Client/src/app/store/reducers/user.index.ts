import { createFeatureSelector, createSelector } from "@ngrx/store";
import * as fromUser from "./user.reducer";

export const userFeatureName = 'user';
export const selectCommentState = createFeatureSelector<fromUser.State>(userFeatureName);

export const selectUserIds = createSelector(
  selectCommentState,
  fromUser.selectUserIds
);
export const selectUserEntities = createSelector(
  selectCommentState,
  fromUser.selectUserEntities
);
export const selectAllUsers = createSelector(
  selectCommentState,
  fromUser.selectAllUsers
);
export const selectUserTotal = createSelector(
  selectCommentState,
  fromUser.selectUsersTotal
);

export const selectUserFullName = (userId: string) => createSelector(
  selectCommentState,
  (users) => {
    debugger;
    return users.entities[userId]?.fullName;
  }
);
