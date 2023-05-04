import { createAction, props } from "@ngrx/store";
import { TicketRecord } from "../../services/web-api-service-proxies";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";

export const loadTickets = createAction('[Tickets] Load');
export const loadTicketSuccess = createAction('[Tickets] Load success', props<{tickets: TicketRecord[]}>());
export const loadTicketFailure = createAction('[Tickets] Load failure', props<BackendError>());
