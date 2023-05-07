import { ActionReducerMap, } from '@ngrx/store';
import * as fromTicket from './ticket.reducer';
import * as fromComment from './comment.reducer';
import * as fromUser from './user.reducer';

export interface State {
  tickets: fromTicket.State;
  comments: fromComment.State;
  users: fromUser.State;
}

export const reducers: ActionReducerMap<State> = {
  tickets: fromTicket.ticketReducer,
  comments: fromComment.commentReducer,
  users: fromUser.userReducer,
};
