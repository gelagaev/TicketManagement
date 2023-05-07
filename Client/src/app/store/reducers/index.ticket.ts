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
export const selectCurrentTicketId = createSelector(
  selectTicketState,
  fromTicket.getSelectedTicketId
);

export const selectCurrentTicket = createSelector(
  selectTicketEntities,
  selectCurrentTicketId,
  (ticketEntities, ticketId) => ticketId && ticketEntities[ticketId]
);

export const isCurrentUserTicketAuthor = (ticketId: string) =>  createSelector(
  selectUserInfo,
  selectTicketEntities,
  (userInfo, tickets) => tickets[ticketId]?.authorId === userInfo.id);

export const isCurrentUserAdmin =  createSelector(
  selectUserInfo,
  (userInfo) => userInfo.isAdministrator);
