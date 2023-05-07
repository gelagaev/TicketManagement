import { createReducer, on } from '@ngrx/store';
import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { UserRecord } from "../../services/web-api-service-proxies";
import { AuthActions, UserActions } from "../actions";

export interface State extends EntityState<UserRecord> {
}

export const adapter: EntityAdapter<UserRecord> = createEntityAdapter<UserRecord>();

export const initialState: State = adapter.getInitialState({});

export const userReducer = createReducer(
  initialState,
  on(UserActions.getUsersSuccess, (state, {users}) =>
    adapter.setMany(users, state)),
  on(AuthActions.logout, (state) =>
    adapter.removeAll(state)),
);

const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

export const selectUserIds = selectIds;

export const selectUserEntities = selectEntities;

export const selectAllUsers = selectAll;

export const selectUsersTotal = selectTotal;
