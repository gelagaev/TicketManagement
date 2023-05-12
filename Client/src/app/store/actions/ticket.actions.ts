import { createAction, props } from "@ngrx/store";
import {
  IAssignTicketRequest,
  ICloseTicketRequest,
  ICreateTicketRequest,
  IUpdateTicketRequest,
  TicketRecord
} from "../../services/web-api-service-proxies";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { Update } from "@ngrx/entity";

export const loadTickets = createAction('[Tickets] Load');
export const loadTicketSuccess = createAction('[Tickets] Load success', props<{ tickets: TicketRecord[] }>());
export const loadTicketFailure = createAction('[Tickets] Load failure', props<BackendError>());

export const createTicket = createAction('[Tickets] Create', props<ICreateTicketRequest>());
export const createTicketSuccess = createAction('[Tickets] Create Success', props<TicketRecord>());
export const createTicketFailure = createAction('[Tickets] Create Failure', props<BackendError>());

export const updateTicket = createAction('[Tickets] Update', props<IUpdateTicketRequest>());
export const updateTicketSuccess = createAction('[Tickets] Update Success', props<{ update: Update<TicketRecord> }>());
export const updateTicketFailure = createAction('[Tickets] Update Failure', props<BackendError>());

export const deleteTicket = createAction('[Tickets] Delete', props<{ ticketId: string }>());
export const deleteTicketSuccess = createAction('[Tickets] Delete Success', props<{ ticketId: string }>());
export const deleteTicketFailure = createAction('[Tickets] Delete Failure', props<BackendError>());

export const assignTicket = createAction('[Tickets] Assign', props<IAssignTicketRequest>());
export const assignTicketSuccess = createAction('[Tickets] Assign Success', props<Update<TicketRecord>>());
export const assignTicketFailure = createAction('[Tickets] Assign Failure', props<BackendError>());

export const closeTicket = createAction('[Tickets] Close', props<ICloseTicketRequest>());
export const closeTicketSuccess = createAction('[Tickets] Close Success', props<{ update: Update<TicketRecord> }>());
export const closeTicketFailure = createAction('[Tickets] Close Failure', props<BackendError>());

export const startEditTicket = createAction('[Ticket] Start Edit Ticket Ticket', props<{ ticketId: string }>());
export const endEditTicket = createAction('[Ticket] End Edit Ticket Ticket', props<{ ticketId: string }>());
