import { createReducer, on } from '@ngrx/store';
import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { CommentRecord } from "../../services/web-api-service-proxies";
import { CommentActions } from "../actions";

export interface State extends EntityState<CommentRecord> {
  editingIds: string [];
}

export const adapter: EntityAdapter<CommentRecord> = createEntityAdapter<CommentRecord>();

export const initialState: State = adapter.getInitialState({
  editingIds: [],
});

export const commentReducer = createReducer(
  initialState,
  on(CommentActions.loadTicketCommentsSuccess, (state, {comments}) =>
    adapter.setMany(comments, state)),
  on(CommentActions.deleteTicketCommentSuccess, (state, {commentId}) =>
    adapter.removeOne(commentId, state)),
  on(CommentActions.createTicketCommentSuccess, (state, ticket) =>
    adapter.addOne(ticket, state)),
  on(CommentActions.updateTicketCommentSuccess, (state, { update }) =>
    adapter.updateOne(update, state)),
  on(CommentActions.editEditTicketComment, (state, {commentId}) => ({
    ...state,
    editingIds: [...state.editingIds, commentId]
  })),
  on(CommentActions.updateTicketCommentSuccess, (state, response) => ({
      ...state,
      editingIds: [...state.editingIds.filter(id => id !== response.update.id)]
    })),
);

export const getEditingCommentIds = (state: State) => state.editingIds;

const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

export const selectCommentIds = selectIds;

export const selectCommentEntities = selectEntities;

export const selectAllComments = selectAll;

export const selectCommentTotal = selectTotal;
