import { ActionReducerMap, } from '@ngrx/store';
import * as fromTicket from './ticket.reducer';
import * as fromComment from './comment.reducer';

export interface State {
  tickets: fromTicket.State;
  comments: fromComment.State;
}

export const reducers: ActionReducerMap<State> = {
  tickets: fromTicket.ticketReducer,
  comments: fromComment.commentReducer,
};
