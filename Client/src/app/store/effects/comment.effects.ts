import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, exhaustMap, map, of } from "rxjs";
import { BackendError } from "../../interceptors/http-request-failure.interceptor";
import { Injectable } from "@angular/core";
import {
  CreateCommentRequest,
  UpdateTicketCommentRequest,
  WebApiServiceProxy
} from "../../services/web-api-service-proxies";
import { CommentActions } from "../actions";

@Injectable()
export class CommentEffects {
  constructor(private actions$: Actions, private webApiServiceProxy: WebApiServiceProxy,) {
  }

  createComment$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(CommentActions.createTicketComment),
      exhaustMap((request) => {
        return this.webApiServiceProxy.ticket_CreateComment(request.ticketId, new CreateCommentRequest(request))
          .pipe(
            map(response => CommentActions.createTicketCommentSuccess(response.comment)),
            catchError((error: BackendError) => of(CommentActions.createTicketCommentFailure(error)))
          )
      })
    );
  })

  load$ = createEffect(() =>
    this.actions$.pipe(
      ofType(CommentActions.loadTicketComment),
      exhaustMap(({ticketId}) => {
        return this.webApiServiceProxy.ticket_GetComments(ticketId)
          .pipe(
            map(response => CommentActions.loadTicketCommentsSuccess({comments: response.comments!})),
            catchError((error: BackendError) => of(CommentActions.loadTicketCommentsFailure(error)))
          );
      })
    )
  );

  delete$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(CommentActions.deleteTicketComment),
        exhaustMap(({commentId}) => {
          return this.webApiServiceProxy.tickets_DeleteComment(commentId)
            .pipe(
              map(() => CommentActions.deleteTicketCommentSuccess({commentId})),
              catchError((error: BackendError) => of(CommentActions.deleteTicketCommentFailure(error)))
            );
        })
      );
    }
  );

  update$ = createEffect(() =>
    this.actions$.pipe(
      ofType(CommentActions.updateTicketComment),
      exhaustMap((request) => {
        return this.webApiServiceProxy.tickets_CommentUpdate(new UpdateTicketCommentRequest(request))
          .pipe(
            map(response => CommentActions.updateTicketCommentSuccess({
              update: {
                id: response.comment.id,
                changes: {commentText: response.comment.commentText}
              }
            })),
            catchError((error: BackendError) => of(CommentActions.updateTicketCommentFailure(error)))
          );
      })
    )
  );
}
