import { createAction, props } from "@ngrx/store";
import {
  CommentRecord,
  ICreateCommentRequest,
  IUpdateTicketCommentRequest,
} from "../../services/web-api-service-proxies";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { Update } from "@ngrx/entity";

export let addTicketComment = undefined;


export const loadTicketComment = createAction('[Comments] Load Ticket Comments', props<{ ticketId: string }>());
export const loadTicketCommentsSuccess = createAction(
  '[Comments] Load Ticket Comments success',
  props<{ comments: CommentRecord[] }>()
);
export const loadTicketCommentsFailure = createAction(
  '[Comment] Load Ticket Comments failure',
  props<BackendError>());


export const createTicketComment = createAction('[Comments] Create Ticket Comment', props<ICreateCommentRequest>());
export const createTicketCommentSuccess = createAction('[Comments] Create Ticket Comment Success', props<CommentRecord>());
export const createTicketCommentFailure = createAction('[Comment] Create Ticket Comment Failure', props<BackendError>());

export const editEditTicketComment = createAction('[Comments] Start Edit Ticket Comment', props<{ commentId: string }>());

export const deleteTicketComment = createAction('[Comments] Delete Ticket Comment', props<{ commentId: string }>());
export const deleteTicketCommentSuccess = createAction('[Comments] Delete Ticket Comment Success', props<{commentId: string}>());
export const deleteTicketCommentFailure = createAction('[Comment] Create Ticket Comment Failure', props<BackendError>());


export const updateTicketComment = createAction('[Comments] Update Ticket Comment', props<IUpdateTicketCommentRequest>());
export const updateTicketCommentSuccess = createAction('[Comments] Update Ticket Comment Success', props<{update: Update<CommentRecord>}>());
export const updateTicketCommentFailure = createAction('[Comment] Update Ticket Comment Failure', props<BackendError>());
