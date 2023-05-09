import { createReducer, on } from '@ngrx/store';
import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import * as TicketActions from '../actions/ticket.actions';
import { TicketRecord } from "../../services/web-api-service-proxies";

export interface State extends EntityState<TicketRecord> {
  editingIds: string [];
}

export const adapter: EntityAdapter<TicketRecord> = createEntityAdapter<TicketRecord>();

export const initialState: State = adapter.getInitialState({
  editingIds: [],
});

export const ticketReducer = createReducer(
  initialState,
  on(TicketActions.loadTicketSuccess, (state, {tickets}) => {
    return adapter.setAll(tickets, state);
  }),
  on(TicketActions.createTicketSuccess, (state, ticket) => {
    return adapter.addOne(ticket, state);
  }),
  on(TicketActions.deleteTicketSuccess, (state, payload) => {
    return adapter.removeOne(payload.ticketId, state);
  }),
  on(TicketActions.updateTicketSuccess, (state, {update}) => {
    return adapter.updateOne(update, state);
  }),
  on(TicketActions.assignTicketSuccess, (state, update) => {
    return adapter.updateOne(update, state);
  }),
  on(TicketActions.closeTicketSuccess, (state, {update}) => {
    return adapter.updateOne(update, state);
  }),
  on(TicketActions.updateTicketSuccess, (state, response) => ({
    ...state,
    editingIds: [...state.editingIds.filter(id => id !== response.update.id)]
  })),
  on(TicketActions.editTicket, (state, edit) => ({
    ...state,
    editingIds:
      edit.isEdit ?
        [...state.editingIds, edit.id] :
        [...state.editingIds.filter(id => id !== edit.id)]
  })),
);

export const getEditingTicketIds = (state: State) => state.editingIds;

const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

export const selectTicketIds = selectIds;

export const selectTicketEntities = selectEntities;

export const selectAllTickets = selectAll;

export const selectTicketTotal = selectTotal;
