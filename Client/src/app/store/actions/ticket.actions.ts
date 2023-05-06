import { createAction, props } from "@ngrx/store";
import { ICreateTicketRequest, IUpdateTicketRequest, TicketRecord } from "../../services/web-api-service-proxies";
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
