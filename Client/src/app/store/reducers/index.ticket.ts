import { createFeatureSelector, createSelector } from "@ngrx/store";
import * as fromTicket from "./ticket.reducer";
import { selectUserInfo } from "./index.common";

export const ticketFeatureName = 'ticket';
export const selectTicketState = createFeatureSelector<fromTicket.State>(ticketFeatureName);

export const selectTicketIds = createSelector(
  selectTicketState,
  fromTicket.selectTicketIds
);
export const selectTicketEntities = createSelector(
  selectTicketState,
  fromTicket.selectTicketEntities
);
export const selectAllTickets = createSelector(
  selectTicketState,
  fromTicket.selectAllTickets
);
export const selectTicketTotal = createSelector(
  selectTicketState,
  fromTicket.selectTicketTotal
);

export const isCurrentUserTicketAuthor = (ticketId: string) => createSelector(
  selectUserInfo,
  selectTicketEntities,
  (userInfo, tickets) => tickets[ticketId]?.authorId === userInfo.id);

export const selectCurrentUserId = createSelector(
  selectUserInfo,
  userInfo => userInfo.id);

export const isCurrentUserAdmin = createSelector(
  selectUserInfo,
  (userInfo) => userInfo.isAdministrator);

export const selectEditingTicketIds = createSelector(
  selectTicketState,
  fromTicket.getEditingTicketIds
);

export const isEditingTicket = (checkId: string) => createSelector(
  selectEditingTicketIds,
  selectEditingTicketIds => selectEditingTicketIds.some(id => id === checkId)
);
