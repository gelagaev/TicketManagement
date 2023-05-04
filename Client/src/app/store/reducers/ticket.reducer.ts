import { Action, createReducer, on } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import * as TicketActions from '../actions/ticket.actions';
import { TicketRecord } from "../../services/web-api-service-proxies";

export interface State extends EntityState<TicketRecord> {
  selectedTicketId: string | null;
}

export const adapter: EntityAdapter<TicketRecord> = createEntityAdapter<TicketRecord>();

export const initialState: State = adapter.getInitialState({
  selectedTicketId: null,
});

export const ticketReducer = createReducer(
  initialState,
  on(TicketActions.loadTicketSuccess, (state, { tickets }) => {
    return adapter.setAll(tickets, state);
  }),
);


export const getSelectedTicketId = (state: State) => state.selectedTicketId;

// get the selectors
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
